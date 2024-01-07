using API_asp_start_project.Domain.Interfaces;
using API_asp_start_project.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_asp_start_project.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public WeatherForecastController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<string> Get()
        {
            IQueryable<Domain.Models.Account> domesticAccounts = _repository.Account.FindByCondition(x => x.AccountType.Equals("Domestic"));
            var owners = _repository.Owner.FindAll();

                       return new string[] { "value1", "value2" };
        }
    }
}
