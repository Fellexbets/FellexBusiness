
namespace Igor_AIS_Proj.Persistence.Interfaces
{
    public interface IAccountPersistence
    {
        List<Account> GetAll();

        Task<bool> Update(Account account);

        Task<bool> Delete(Account account);

        Task<Account> Create(Account account);

        Account GetById(int id);

        Task<bool> Delete(int id);


    }
}
