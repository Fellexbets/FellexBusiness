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
                return null;
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


 
    }
}







