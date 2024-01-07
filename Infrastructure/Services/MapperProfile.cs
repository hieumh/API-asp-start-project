using API_asp_start_project.Domain.Dtos;
using API_asp_start_project.Domain.Models;
using AutoMapper;

namespace API_asp_start_project.Infrastructure.Services
{
    public class MapperProfile: Profile
    {
        public MapperProfile() {
            CreateMap<Owner, OwnerDto>(); 
            CreateMap<Account, AccountDto>();
        }
    }
}
