using Igor_AIS_Proj.Auxiliary;
using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Models.Responses;
using Igor_AIS_Proj.Persistence;
using QuizzalT_API.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Igor_AIS_Proj.Business
{
    public class UserBusiness : BaseBusiness<UserPersistence, User>
    {
        public UserBusiness() => database = new UserPersistence();
        public async Task<User> GetById(int id) => await database.GetById(id);
        public async Task<bool> Delete(int id) => await database.Delete(id);

        public async Task<CreateUserResponse> Register(UserRegistrationRequest model) => await database.Register(model);

        public async Task<LoginUserResponse> Authenticate(LoginUserRequest model) => await database.Authenticate(model);


        //public User Register(User user) => database.Register(user);



        //public string SubjectId(this ClaimsPrincipal user)
        //{
        //    return user?.Claims?.FirstOrDefault(c => c.Type.Equals("sub", StringComparison.OrdinalIgnoreCase))?.Value;
        //}


    }
}
