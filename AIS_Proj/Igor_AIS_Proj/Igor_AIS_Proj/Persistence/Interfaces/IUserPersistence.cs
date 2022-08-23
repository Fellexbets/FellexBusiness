
namespace Igor_AIS_Proj.Persistence.Interfaces
{
    public interface IUserPersistence 
    {
        List<User> GetAll();

        Task<bool> Update(User user);

        Task<bool> Delete(User user);

        Task<User> Create(User user);
        Task<User> GetById(int id);

        Task<bool> Delete(int id);


        Task<LoginUserResponse> Authenticate(LoginUserRequest model);

        Task<User> GetByEmail(string Email);




    }
}
