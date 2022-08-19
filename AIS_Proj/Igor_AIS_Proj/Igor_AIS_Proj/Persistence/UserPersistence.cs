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
using Igor_AIS_Proj.Models.Responses;

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

        public async Task<CreateUserResponse> Register(UserRegistrationRequest model)
        {

            (model.UserPassword, model.PasswordSalt) = Auxiliary.PasswordHasher.ReturnHashedPasswordAndSalt(model.UserPassword);

            try
            {
                await _context.AddAsync(model);
                await _context.SaveChangesAsync();
                return new CreateUserResponse
                {
                    PasswordSalt = model.PasswordSalt
                };
            }
            catch
            {
                return null;
            }
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


 










