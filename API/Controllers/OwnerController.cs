using API_asp_start_project.Domain.Dtos;
using API_asp_start_project.Domain.Interfaces;
using API_asp_start_project.Domain.Models;
using API_asp_start_project.Domain.Pagings;
using API_asp_start_project.Infrastructure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;

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

        [HttpGet]
        public IActionResult GetOwners([FromQuery] OwnerParameters ownerParams)
        {
            if (!ownerParams.ValidYearRange)
            {
                return BadRequest("Max year of birth cannot be less than min year of birth");
            }

            var owners = _repository.Owner.GetOwners(ownerParams);

            var metadata = new
            {
                owners.TotalCount,
                owners.PageSize,
                owners.CurrentPage,
                owners.HasNext,
                owners.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned {owners.Count()} owners from database");

            return Ok(owners);
        }

        [HttpGet("{id}", Name = "OwnerById")]
        public IActionResult GetOwnserById(Guid id, [FromQuery] string fields) {
            try
            {
                _logger.LogInfo("Returned Owner by Id.");

                var owner = _repository.Owner.GetOwnerById(id, fields);

                if (owner == default(ExpandoObject))
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

        [HttpPost]
        public IActionResult CreateOwner([FromBody] CreateOwnerDto owner) {
        try
            {
                if (owner is null)
                {
                    _logger.LogError("Owner object sent from client is null");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var ownerEntity = _mapper.Map<Owner>(owner);

                _repository.Owner.CreateOwner(ownerEntity);
                _repository.Save();

                var createdOwner = _mapper.Map<OwnerDto>(ownerEntity);
                return CreatedAtRoute("OwnerById", new {id = createdOwner.Id}, createdOwner);
            } 
           catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOwner(Guid id, [FromBody]UpdateOwnerDto owner) {
            try
            {
                if (owner is null)
                {
                    _logger.LogError("Owner object sent from client is null");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client");
                    return BadRequest("Invalid model object");
                }

                var ownerEntity = _repository.Owner.GetOwnerById(id);
                if(ownerEntity is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db");
                    return NotFound();
                }

                _mapper.Map(owner, ownerEntity);
                
                _repository.Owner.UpdateOwner(ownerEntity);
                _repository.Save();

                return NoContent();
            } 
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(Guid id)
        {
           try
           {
                var ownerEntity = _repository.Owner.GetOwnerById(id);
                if (ownerEntity is null) 
                { 

                    _logger.LogError("id sent by client is null");
                    return BadRequest();
                }

                if (_repository.Account.GetAccountsByOwner(id).Any())
                {
                    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Dleete those accounts first");
                    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
                }

                _repository.Owner.DeleteOwner(id);
                _repository.Save();
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }


}
