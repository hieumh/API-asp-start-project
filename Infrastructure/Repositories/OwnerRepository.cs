using API_asp_start_project.Domain.Interfaces;
using API_asp_start_project.Domain.Models;
using API_asp_start_project.Domain.Pagings;
using Microsoft.EntityFrameworkCore;

namespace API_asp_start_project.Infrastructure.Repositories
{
    public class OwnerRepository: RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext): base(repositoryContext) { }

        public IEnumerable<Owner> GetAllOwners() 
        {
            return FindAll().OrderBy(owner => owner.Name).ToList(); 
        }

        public PagedList<Owner> GetOwners(OwnerParameters owners)
        {
            return PagedList<Owner>.ToPagedList(FindAll().OrderBy(owner => owner.Name),owners.PageNumber, owners.PageSize);
        }

        public Owner? GetOwnerById(Guid id)
        {
            Owner? owner = FindByCondition(owner => owner.Id == id).FirstOrDefault();
            return owner;
        }

        public Owner? GetOwnerWithDetails(Guid id)
        {
            return FindByCondition(owner => owner.Id.Equals(id)).Include(account => account.Accounts).FirstOrDefault();
        }

        public void CreateOwner(Owner owner)
        {
            Create(owner);
        }

        public void UpdateOwner(Owner owner)
        {
            Update(owner);
        }

        public void DeleteOwner(Guid id)
        {
            var ownerEntity = FindByCondition(owner => owner.Id.Equals(id)).FirstOrDefault();
            if (ownerEntity != null)
            { 
                Delete(ownerEntity);
            }

        }
    }
}
