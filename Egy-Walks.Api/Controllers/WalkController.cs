using AutoMapper;
using Egy_Walks.Api.CustomActionFilter;
using Egy_Walks.Api.IRepositories;
using Egy_Walks.Api.Models.Domain;
using Egy_Walks.Api.Models.DTOs.WalkDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Egy_Walks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly ILogger<WalkController> _logger;
        private readonly IMapper _mapper;

        public WalkController(IWalkRepository walkRepository, ILogger<WalkController> logger, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? sortOrder,[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 2)
        {
            _logger.LogInformation("Fetching all walks");
            var walks = await _walkRepository.GetAllWalksAsync(filterOn, filterQuery, sortBy, sortOrder,pageNumber, pageSize);
            var walksResponse = _mapper.Map<List<WalkResponse>>(walks);
            return Ok(walksResponse);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalk([FromBody] WalkRequest walkRequest)
        {
            _logger.LogInformation("Creating new walk");
            var walk = _mapper.Map<Walk>(walkRequest);
            var createdWalk = await _walkRepository.CreateWalkAsync(walk);
            var walkResponse = _mapper.Map<WalkResponse>(createdWalk);
            return CreatedAtAction(nameof(GetWalk), new { id = walkResponse.Id }, walkResponse);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<WalkResponse>> GetWalk([FromRoute] Guid id)
        {
            _logger.LogInformation("Fetching walk with {id}", id);
            if (id == Guid.Empty)
            {
                return NotFound("Invalid walk id");
            }

            var walk = await _walkRepository.GetWalkAsync(id);
            if (null == walk)
            {
                return NotFound($"Walk with id {id} not found");
            }

            var walkResponse = _mapper.Map<WalkResponse>(walk);
            return Ok(walkResponse);
        }

        [HttpPut("{id:guid}")]
        [ValidateModel]
        public async Task<ActionResult<WalkResponse>> UpdateWalk([FromRoute] Guid id,
            [FromBody] WalkRequest walkUpdateRequest)
        {
            try
            {
                var walk = _mapper.Map<Walk>(walkUpdateRequest);
                walk.Id = id;
                var updatedWalk = await _walkRepository.UpdateWalkAsync(id, walk);
                var walkResponse = _mapper.Map<WalkResponse>(updatedWalk);
                return Ok(walkResponse);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Walk not found for update with ID {WalkId}.", id);
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating walk");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating walk");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            try
            {
                await _walkRepository.DeleteWalkAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Walk not found for deletion with ID {WalkId}.", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting walk with ID {WalkId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting walk");
            }
        }
    }
}