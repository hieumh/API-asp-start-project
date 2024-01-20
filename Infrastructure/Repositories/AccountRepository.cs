using API_asp_start_project.Domain.Helpers;
using API_asp_start_project.Domain.Interfaces;
using API_asp_start_project.Domain.Models;
using API_asp_start_project.Domain.Pagings;

namespace API_asp_start_project.Infrastructure.Repositories
{
    public class AccountRepository: RepositoryBase<Account>, IAccountRepository
    {
        private ISortHelper<Account> _sortHelper;
        private IDataShaper<Account> _shaper;
        public AccountRepository(RepositoryContext context, ISortHelper<Account> sortHelper, IDataShaper<Account> shaper) : base(context)
        {
            _sortHelper = sortHelper;
            _shaper = shaper;
        }

        public PagedList<Account> GetAccountsByOwner(Guid id, AccountParameters accountParams)
        {
            return PagedList<Account>.ToPagedList(FindByCondition(account => account.OwnerId.Equals(id)), accountParams.PageNumber, accountParams.PageSize );
        }

        public IEnumerable<Account> GetAccountsByOwner(Guid id)
        {
            return FindByCondition(account => account.OwnerId.Equals(id));
        }
    }
}
