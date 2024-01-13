using API_asp_start_project.Domain.Interfaces;
using API_asp_start_project.Domain.Models;
using API_asp_start_project.Domain.Pagings;

namespace API_asp_start_project.Infrastructure.Repositories
{
    public class AccountRepository: RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext context) : base(context) { }
        public PagedList<Account> GetAccountsByOwner(Guid id, AccountParameters accountParams)
        {
            return PagedList<Account>.ToPagedList(FindByCondition(account => account.OwnerId.Equals(id)), accountParams.PageNumber, accountParams.PageSize );
        }
    }
}
