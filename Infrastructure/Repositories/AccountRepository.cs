using API_asp_start_project.Domain.Interfaces;
using API_asp_start_project.Domain.Models;

namespace API_asp_start_project.Infrastructure.Repositories
{
    public class AccountRepository: RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext context) : base(context) { }
    }
}
