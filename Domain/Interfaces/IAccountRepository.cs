using API_asp_start_project.Domain.Models;
using API_asp_start_project.Domain.Pagings;

namespace API_asp_start_project.Domain.Interfaces
{
    public interface IAccountRepository: IRepositoryBase<Account>
    {
        public PagedList<Account> GetAccountsByOwner(Guid onwerId, AccountParameters accountParams);
    }
}
