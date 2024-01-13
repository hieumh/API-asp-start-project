using API_asp_start_project.Domain.Interfaces;
using API_asp_start_project.Domain.Pagings;
using API_asp_start_project.Infrastructure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API_asp_start_project.API.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public AccountController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAccountsForOwner(Guid ownerId, [FromQuery] AccountParameters parameters) 
        {
            var accounts = _repository.Account.GetAccountsByOwner(ownerId, parameters);

            var metadata = new
            {
                accounts,
                accounts.PageSize,
                accounts.CurrentPage,
                accounts.TotalPages,
                accounts.HasNext,
                accounts.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned {accounts.TotalCount} owners from database.");

            return Ok(accounts);
        }
    }
}
