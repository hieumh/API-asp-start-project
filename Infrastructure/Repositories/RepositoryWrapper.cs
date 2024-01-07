using API_asp_start_project.Domain.Interfaces;

namespace API_asp_start_project.Infrastructure.Repositories
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private RepositoryContext _repositoryContext;
        private IOwnerRepository _ownerRepository;
        private IAccountRepository _accountRepository;
        public RepositoryWrapper(RepositoryContext repositoryContext) {
            _repositoryContext = repositoryContext; 
        }

        public IOwnerRepository Owner
        {
            get
            {
                if (_ownerRepository == null)
                {
                    _ownerRepository = new OwnerRepository(_repositoryContext);

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
                    _accountRepository = new AccountRepository(_repositoryContext);
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
