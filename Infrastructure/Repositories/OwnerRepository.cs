using API_asp_start_project.Domain.Interfaces;
using API_asp_start_project.Domain.Models;

namespace API_asp_start_project.Infrastructure.Repositories
{
    public class OwnerRepository: RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext): base(repositoryContext) { }
    }
}
