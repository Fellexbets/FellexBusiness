using QuizzalT_API.Models;
using QuizzalT_API.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzalT_API.Business
{
    public class UserBusiness : BaseBusiness<UserPersistence, User>
    {
        public UserBusiness() => database = new UserPersistence();

        public async Task<User> GetById(int id) => await database.GetById(id);
        public async Task<bool> Delete(int id) => await database.Delete(id);


        public async Task<User> Authenticate(UserCredentials model) => await database.Authenticate(model);
        public async Task<List<User>> GetAllDetailsUser(int id) => await database.GetAllDetailsUser(id);

        public async Task<List<RelatedUser>> GetRelatedUsers(List<int> userIds) => await database.GetRelatedUsers(userIds);

        public async Task<User> UpdateUser(User user)
        {
            string photoString = user.PhotoString;
            int id = user.UserId;
            return await database.UpdateUser(photoString, id);
        }
    }
}
