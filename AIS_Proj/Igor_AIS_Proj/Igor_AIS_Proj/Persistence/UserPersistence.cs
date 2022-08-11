using Igor_AIS_Proj.Auxiliary;
using Igor_AIS_Proj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Igor_AIS_Proj.Persistence
{
    public class UserPersistence : BasePersistence<User>
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json")
                   .Build();
        public UserPersistence() => _contextEntity = _context.Users;

        public async Task<User> GetById(int id) => await _contextEntity.FindAsync(id);
        public async Task<bool> Delete(int id) => await Delete(_contextEntity.Find(id));

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

        public User Register(User user)
        {
            try
            {
              
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration["Secret"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(type: JwtRegisteredClaimNames.Sub, value: user.Email),
                    new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                    new Claim(type: JwtRegisteredClaimNames.Email, value: user.Email)
                }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.UserToken = tokenHandler.WriteToken(token);
                _context.Add(user);
                _context.SaveChanges();
                return user;

            }
            catch 
            {
                return null;
            }            
            

        }


    }
}
