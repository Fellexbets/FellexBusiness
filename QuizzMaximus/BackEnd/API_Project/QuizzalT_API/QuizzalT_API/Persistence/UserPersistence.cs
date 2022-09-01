using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizzalT_API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuizzalT_API.Persistence
{
    public class UserPersistence : BasePersistence<User>
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

        private readonly SecretKey _appSettings;
        public UserPersistence(IOptions<SecretKey> appSettings)
        {
            _contextEntity = _context.Users;
            _appSettings = appSettings.Value;
        }
        public UserPersistence() => _contextEntity = _context.Users;

        public async Task<User> GetById(int id) => await _contextEntity.FindAsync(id);
        public async Task<bool> Exists(int id) => await _contextEntity.AnyAsync(e => e.ReturnId().Contains(id));
        public async Task<bool> Delete(int id) => await Delete(_contextEntity.Find(id));

        [AllowAnonymous]
        public override async Task<User> Create(User user)
        {
            (user.Password, user.PasswordSalt) = Auxiliary.PasswordHasher.ReturnHashedPasswordAndSalt(user.Password);

            if (user.PhotoString != null)
            {
                user.Photo = Convert.FromBase64String(user.PhotoString);
            }

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

            if (user == null) { return null; }
         
            bool confirmedPassword = Auxiliary.PasswordHasher.CompareHashedPasswords(model.Password, user.Password, user.PasswordSalt);
            if (confirmedPassword)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration["Secret"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("Store", user.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                
                try
                {
                    _contextEntity.Update(user);
                    //await _context.SaveChangesAsync();
                    return user;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<List<User>> GetAllDetailsUser(int id) => await _contextEntity.AsNoTracking().Where(e => e.UserId == id).ToListAsync();

        /// <summary>
        /// Receives a list of user ids and returns RelatedUsers with username and userIds.
        /// </summary>
        /// <param name="userIds">UserIds to receive RelatedUsers from.</param>
        /// <returns>A list of RelatedUsers filtered by the userIds param</returns>
        public async Task<List<RelatedUser>> GetRelatedUsers(List<int> userIds)
        {
            List<RelatedUser> relatedUsers = new List<RelatedUser>();
            foreach (int userId in userIds)
            {
                User user = await _contextEntity.FindAsync(userId);
                if (user != null)
                {
                    relatedUsers.Add(new RelatedUser(user));
                }
            }
            return relatedUsers;
        }

        public async Task<User> UpdateUser(string photoString, int id)
        {
            try
            {
                User userToFind = await _contextEntity.FirstOrDefaultAsync(u => u.UserId == id);

                if (photoString != String.Empty)
                {
                    userToFind.Photo = Convert.FromBase64String(photoString);
                }
                else
                {
                    userToFind.Photo = null;
                }

                _context.Update(userToFind);
                await _context.SaveChangesAsync();
                return userToFind;
            }
            catch
            {
                return null;
            }
        }
    }
}
