
namespace BlazorOpenBank2.Data.Models
{
    public class AccountMovims
    {
        public AccountMovims()
        {
        }

        public AccountMovims(CreateAccountResponse account, ICollection<Movim> movims)
        {
            Account = account;
            Movims = movims;
        }

        public CreateAccountResponse? Account { get; set; } = new CreateAccountResponse();

        public virtual ICollection<Movim>? Movims { get; set; } = new List<Movim>();


    }
}
