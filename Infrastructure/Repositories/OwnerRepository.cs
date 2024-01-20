using API_asp_start_project.Domain.Helpers;
using API_asp_start_project.Domain.Interfaces;
using API_asp_start_project.Domain.Models;
using API_asp_start_project.Domain.Pagings;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace API_asp_start_project.Infrastructure.Repositories
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        private ISortHelper<Owner> _sortHelper;
        private IDataShaper<Owner> _shaper;
        public OwnerRepository(RepositoryContext repositoryContext, ISortHelper<Owner> sortHelper, IDataShaper<Owner> shaper) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
            _shaper = shaper;
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return FindAll().OrderBy(owner => owner.Name).ToList();
        }

        public PagedList<ExpandoObject>? GetOwners(OwnerParameters owners)
        {
            var ownersParameters = FindByCondition(o => o.DateOfBirth.Year >= owners.MinYearOfBirth && o.DateOfBirth.Year <= owners.MaxYearOfBirth);

            if (ownersParameters == null)
            {
                return null;
            }

            SearchByName(ref ownersParameters, owners.Name);
            _sortHelper.ApplySort(ownersParameters, owners.OrderBy);
            var shapedData = _shaper.ShapeData(ownersParameters, owners.Fields);

            return PagedList<ExpandoObject>.ToPagedList(shapedData, owners.PageNumber, owners.PageSize);
        }

        private void SearchByName(ref IQueryable<Owner> owners, string ownerName)
        {
            if (!owners.Any() || string.IsNullOrWhiteSpace(ownerName))
            {
                return;
            }

            owners = owners.Where(o => o.Name.ToLower().Contains(ownerName.Trim().ToLower()));
        }

        public Owner? GetOwnerById(Guid id)
        {
            Owner? owner = FindByCondition(owner => owner.Id == id).FirstOrDefault();
            return owner;
        }

        public ExpandoObject GetOwnerById(Guid ownerId, string fields)
        {
            var owner = FindByCondition(owner => owner.Id.Equals(ownerId)).DefaultIfEmpty(new Owner()).FirstOrDefault();

            return _shaper.ShapeData(owner, fields);
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
