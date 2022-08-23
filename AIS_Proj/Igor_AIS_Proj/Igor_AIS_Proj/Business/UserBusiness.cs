using Igor_AIS_Proj.Auxiliary;
using Igor_AIS_Proj.Business.Interfaces;
using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Models.Responses;
using Igor_AIS_Proj.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Igor_AIS_Proj.Business
{
    public class UserBusiness : IUserBusiness
    {
        public UserBusiness(IUserPersistence userPersistence) => _userPersistence = userPersistence;

        private readonly IUserPersistence _userPersistence;

        IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json")
                   .Build();
        public async Task<User> GetById(int id) => await _userPersistence.GetById(id);
        public async Task<bool> Delete(int id) => await _userPersistence.Delete(id);

        public List<User> GetAll() => _userPersistence.GetAll();

        public Task<bool> Update(User user) => _userPersistence.Update(user);

        public async Task<User> Create(User user)

        {
            var passwordHasher = new PasswordHasher<User>();
            string passwordHash = passwordHasher.HashPassword(user, user.UserPassword);
            return await _userPersistence.Create(user);

        }


        public async Task<LoginUserResponse> Authenticate(LoginUserRequest model) => await _userPersistence.Authenticate(model);

        public async Task<User> GetByEmail(string Email) => await _userPersistence.GetByEmail(Email);



    }
}
