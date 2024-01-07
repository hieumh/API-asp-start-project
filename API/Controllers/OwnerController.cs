using API_asp_start_project.Domain.Dtos;
using API_asp_start_project.Domain.Interfaces;
using API_asp_start_project.Infrastructure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API_asp_start_project.API.Controllers
{
    [Route("owner")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllOwners() {
            try
            {
                _logger.LogInfo($"Returned all from database.");
                
                var owners = _repository.Owner.GetAllOwners();
                var mappedOwners = _mapper.Map<IEnumerable<OwnerDto>>(owners);
                return Ok(mappedOwners);
            } 
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOwnserById(Guid id) {
            try
            {
                _logger.LogInfo("Returned Owner by Id.");

                var owner = _repository.Owner.GetOwnerById(id);

                if (owner is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in database.");
                    return NotFound();
                }

                return Ok(_mapper.Map<OwnerDto>(owner));
            } 
            catch (Exception ex) {
                _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}/account")]
        public IActionResult GetOwnerWithDetails(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerWithDetails(id);

                if(owner is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in database.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned owner with details for id: {id}");
                return Ok(_mapper.Map<OwnerDto>(owner));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerWithDetails action {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
