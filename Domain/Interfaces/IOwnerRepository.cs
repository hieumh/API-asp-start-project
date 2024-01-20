using API_asp_start_project.Domain.Models;
using API_asp_start_project.Domain.Pagings;
using System.Dynamic;

namespace API_asp_start_project.Domain.Interfaces
{
    public interface IOwnerRepository: IRepositoryBase<Owner>
    {
        IEnumerable<Owner> GetAllOwners();
        PagedList<ExpandoObject> GetOwners(OwnerParameters owners);
        ExpandoObject GetOwnerById(Guid id, string fields);
        Owner? GetOwnerById(Guid id);
        Owner? GetOwnerWithDetails(Guid id);
        void CreateOwner(Owner owner);
        void UpdateOwner(Owner owner);
        void DeleteOwner(Guid id);
    }
}
