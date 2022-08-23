
namespace Igor_AIS_Proj.Business.Interfaces
{
    public interface IUserBusiness
    {
        Task<User> GetById(int id);

        Task<bool> Delete(int id);

        Task<LoginUserResponse> Authenticate(LoginUserRequest model);

        List<User> GetAll();

        Task<bool> Update(User user);
        Task<User> Create(User user);
        Task<User> GetByEmail(string Email);

    }
}
