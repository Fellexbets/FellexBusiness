﻿
namespace Igor_AIS_Proj.Persistence
{
    public class UserPersistence : BasePersistence<User>, IUserPersistence
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json")
                   .Build();

        public UserPersistence()
        {
            _contextEntity = _context.Users;
        }

        public async Task<User> GetById(int id) => await _contextEntity.FindAsync(id);
        public async Task<bool> Delete(int id) => await Delete(_contextEntity.Find(id));
        public async override Task<User> Create(User user)
        {
            if (await _contextEntity.AnyAsync(x => x.Email == user.Email))
                throw new ArgumentException("This email is already registered.");
            if (await _contextEntity.AnyAsync(x => x.Email == user.Username))
                throw new ArgumentException("This username is already registered.");
            (user.UserPassword, user.PasswordSalt) = PasswordHasher.ReturnHashedPasswordAndSalt(user.UserPassword);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> GetByEmail (string Email)
        {
            return await _contextEntity.FirstOrDefaultAsync(x => x.Email == Email);
        }
        public async Task<LoginUserResponse> Authenticate(LoginUserRequest model)
        {
            var user = _contextEntity.SingleOrDefault(x => x.Email == model.Email);

            if (model == null) { return null; }
            try
            {
                bool confirmedPassword = PasswordHasher.CompareHashedPasswords(model.UserPassword, user.UserPassword, user.PasswordSalt);
                if (confirmedPassword)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(configuration["Secret"]);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                         }),
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    user.UserToken = tokenHandler.WriteToken(token);
                    _contextEntity.Update(user);
                    return new LoginUserResponse
                    {
                        UserToken = user.UserToken
                    };
                }
            }
            catch
            {
                return null;
            }
            return null;

        }
    }
}


 










