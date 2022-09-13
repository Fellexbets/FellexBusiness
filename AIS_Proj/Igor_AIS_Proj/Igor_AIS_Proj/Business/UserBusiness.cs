﻿using Igor_AIS_Proj.Auxiliary;
using Igor_AIS_Proj.Business.Interfaces;
using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Models.Responses;
using Igor_AIS_Proj.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Igor_AIS_Proj.Business
{
    public class UserBusiness : IUserBusiness
    {
        public UserBusiness(IUserPersistence userPersistence, ISessionPersistence sessionPersistence, IJwtServices jwtServices)
        {
            _userPersistence = userPersistence;
            _sessionPersistence = sessionPersistence;
            _jwtServices = jwtServices;
        }


        private readonly IUserPersistence _userPersistence;
        private readonly ISessionPersistence _sessionPersistence;
        private readonly IJwtServices _jwtServices;
        

        IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json")
                   .Build();
        public async Task<User> GetById(int id) => await _userPersistence.GetById(id);
        public async Task<bool> Delete(int id) => await _userPersistence.Delete(id);

        public List<User> GetAll() => _userPersistence.GetAll();

        public Task<bool> Update(User user) => _userPersistence.Update(user);

        public async Task<CreateUserResponse> Create(CreateUserRequest request)

        {
            return await _userPersistence.Create(request);
        }

        
        public async Task<(bool, string?, User?, Session?)> Authenticate(LoginUserRequest model)
        {
            User user = GetByEmail(model.Email);
            if (user is not null)
            {
                bool confirmedPassword = PasswordHasher.CompareHashedPasswords(model.UserPassword, user.UserPassword, user.PasswordSalt);
                if (confirmedPassword)
                {
                    Session session = _jwtServices.GenerateToken(user);
                    session.Active = true;
                    session.UserId = user.UserId;
                    var result = await _sessionPersistence.Create(session);
                    return (true, null, user, session);
                }
                return (false, "Authentication failed", null, null);
            }
            return (false, "This user does not exist in our database", null, null);
            //return await _userPersistence.Authenticate(model);
        }

        public async Task<(bool, string?, Session)> Logout(Session session)
        {
            var sessionObj = await _sessionPersistence.GetById(session.SessionId);
            if (sessionObj is not null)
            {
                if (sessionObj.Active)
                {
                    sessionObj.Active = false;
                    var result = await _sessionPersistence.Update(sessionObj);
                    return (true, null, sessionObj);
                }
                return (false, "Session is already inactive", null);
            }
            return (false, "Session not found", null);
        }

        public User GetByEmail(string email) => _userPersistence.GetByEmail(email);




    }
}
