using API_asp_start_project.Domain.Models;
using API_asp_start_project.Domain.Pagings;

namespace API_asp_start_project.Domain.Interfaces
{
    public interface IOwnerRepository: IRepositoryBase<Owner>
    {
        IEnumerable<Owner> GetAllOwners();
        PagedList<Owner> GetOwners(OwnerParameters owners);
        Owner? GetOwnerById(Guid id);
        Owner? GetOwnerWithDetails(Guid id);
        void CreateOwner(Owner owner);
        void UpdateOwner(Owner owner);
        void DeleteOwner(Guid id);
    }
}
