using Igor_AIS_Proj.Auxiliary;
using Igor_AIS_Proj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using QuizzalT_API.Models;

namespace Igor_AIS_Proj.Persistence
{
    public class UserPersistence : BasePersistence<User>
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

        public override async Task<User> Create(User user)
        {
            (user.Userpassword, user.Passwordsalt) = Auxiliary.PasswordHasher.ReturnHashedPasswordAndSalt(user.Userpassword);

            try
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch
            {
                return user;
            }
        }

        [AllowAnonymous]
        public async Task<User> Authenticate(UserCredentials model)
        {
            var user = _contextEntity.SingleOrDefault(x => x.Email == model.Email);

            if (model == null) { return null; }

            bool confirmedPassword = PasswordHasher.CompareHashedPasswords(model.UserPassword, user.Userpassword, user.Passwordsalt);
            if (confirmedPassword)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration["Secret"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Userid.ToString())
                     }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Usertoken = tokenHandler.WriteToken(token);
                try
                {
                    _contextEntity.Update(user);
                    return user;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }


        public int? ValidateJwt(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Secret"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var IdToken = int.Parse(jwtToken.Claims.First(x => x.Type == "nameid").Value);

                return IdToken;
            }
            catch
            {
                return null;
            }
        }
    }
}







            //public User Register(User user)
            //{
            //    try
            //    {

//        var tokenHandler = new JwtSecurityTokenHandler();
//        var key = Encoding.ASCII.GetBytes(configuration["Secret"]);
//        var tokenDescriptor = new SecurityTokenDescriptor
//        {
//            Subject = new ClaimsIdentity(new[]
//            {
//            new Claim(type: JwtRegisteredClaimNames.Sub, value: user.Email),
//            new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
//            new Claim(type: JwtRegisteredClaimNames.Email, value: user.Email)
//        }),
//            Expires = DateTime.UtcNow.AddMinutes(5),
//            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//        };
//        var token = tokenHandler.CreateToken(tokenDescriptor);
//        user.UserToken = tokenHandler.WriteToken(token);
//        _context.Add(user);
//        _context.SaveChanges();
//        return user;

//    }
//    catch
//    {
//        return null;
//    }


//    public async Task<AuthenticationResult> RegisterAsync(string email, string password)
//{
//    var existingUser = await userManager.FindByEmailAsync(email);
//    if (existingUser != null)
//        return new AuthenticationResult
//        {
//            Errors = new[] { "User with this email address already exists." }
//        };
//    var newUser = new IdentityUser
//    {
//        Email = email
//    };
//    var createdUser = await userManager.CreateAsync(newUser, password);
//    if (!createdUser.Succeeded)
//        return new AuthenticationResult
//        {
//            Errors = createdUser.Errors.Select(x => x.Description)
//        };
//    var tokenHandler = new JwtSecurityTokenHandler();
//    var key = Encoding.ASCII.GetBytes(configuration["Secret"]);
//    var tokenDescriptor = new SecurityTokenDescriptor
//    {
//        Subject = new ClaimsIdentity(new[]
//        {
//            new Claim(type: JwtRegisteredClaimNames.Sub, value: newUser.Email),
//            new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
//            new Claim(type: JwtRegisteredClaimNames.Email, value: newUser.Email),
//            new Claim(type: "id", value: newUser.Id)
//        }),
//        Expires = DateTime.UtcNow.AddMinutes(5),
//        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//    };
//    var token = tokenHandler.CreateToken(tokenDescriptor);
//    return new AuthenticationResult
//    {
//        Success = true,
//        Token = tokenHandler.WriteToken(token)
//    };

//}










//public async Task<List<User>> Get(string username, string userPassword) 
//    => await _contextEntity.AsNoTracking().Where(x => x.Username == username && x.Userpassword == userPassword).FirstOrDefault().ToListAsync(); 


//public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
//{
//    dynamic user = Get(model.Username, model.Userpassword);

//    if (user == null)
//        return HttpStatusCode.NotFound;

//    var token = TokenHelper.GenerateToken(user);

//    return new
//    {
//        user = user,
//        token = token
//    };
//}




//}

//    public User Login(User user)
//    {
//        return
//    }

//    public async Task<User> Authenticate(User user)
//    {
//        var userHasValidPassword = await userManager.CheckPasswordAsync( user.Userpassword);
//        var tokenHandler = new JwtSecurityTokenHandler();
//        var key = Encoding.ASCII.GetBytes(configuration["Secret"]);
//        var tokenDescriptor = new SecurityTokenDescriptor
//        {
//            Subject = new ClaimsIdentity(new[]
//            {
//                new Claim(type: JwtRegisteredClaimNames.Sub, value: user.Email),
//                new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
//                new Claim(type: JwtRegisteredClaimNames.Email, value: user.Email)
//            }),
//            Expires = DateTime.UtcNow.AddMinutes(5),
//            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//        };
//        var token = tokenHandler.CreateToken(tokenDescriptor);
//        user.UserToken = tokenHandler.WriteToken(token);
//        _context.SaveChanges();
//        return user;
//    }

//}
//        }
//    }
//}

