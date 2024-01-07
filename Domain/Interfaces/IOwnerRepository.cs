using API_asp_start_project.Domain.Models;

namespace API_asp_start_project.Domain.Interfaces
{
    public interface IOwnerRepository: IRepositoryBase<Owner>
    {
        IEnumerable<Owner> GetAllOwners();
        Owner? GetOwnerById(Guid id);
        Owner? GetOwnerWithDetails(Guid id);
    }
}
