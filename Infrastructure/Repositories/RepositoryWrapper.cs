using API_asp_start_project.Domain.Helpers;
using API_asp_start_project.Domain.Interfaces;
using API_asp_start_project.Domain.Models;

namespace API_asp_start_project.Infrastructure.Repositories
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private RepositoryContext _repositoryContext;
        private IOwnerRepository _ownerRepository;
        private IAccountRepository _accountRepository;
        private ISortHelper<Owner> _ownerSortHelper;
        private ISortHelper<Account> _accountSortHelper;
        private IDataShaper<Owner> _ownerDataShaper;
        private IDataShaper<Account> _accountDataShaper;
        public RepositoryWrapper(RepositoryContext repositoryContext, ISortHelper<Owner> ownerSortHelper, ISortHelper<Account> accountSortHelper, IDataShaper<Owner> ownerDataShaper, IDataShaper<Account> accountDataShaper)
        {
            _repositoryContext = repositoryContext;
            _ownerSortHelper = ownerSortHelper;
            _accountSortHelper = accountSortHelper;
            _ownerDataShaper = ownerDataShaper;
            _accountDataShaper = accountDataShaper;
        }

        public IOwnerRepository Owner
        {
            get
            {
                if (_ownerRepository == null)
                {
                    _ownerRepository = new OwnerRepository(_repositoryContext, _ownerSortHelper, _ownerDataShaper);

                }

                return _ownerRepository;
            }
        }

        public IAccountRepository Account
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_repositoryContext, _accountSortHelper, _accountDataShaper);
                }

                return _accountRepository;
            }
        }

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}
