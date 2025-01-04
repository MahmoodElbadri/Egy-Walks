using AutoMapper;
using Egy_Walks.Api.CustomActionFilter;
using Egy_Walks.Api.Data;
using Egy_Walks.Api.IRepositories;
using Egy_Walks.Api.Models.Domain;
using Egy_Walks.Api.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Egy_Walks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<RegionController> _logger;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionController(ApplicationDbContext _db, ILogger<RegionController> logger,
            IRegionRepository regionRepository, IMapper mapper)
        {
            this._db = _db;
            this._logger = logger;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _regionRepository.GetAllRegionsAsync();
            var regionResponse = _mapper.Map<List<RegionResponse>>(regions);
            _logger.LogInformation("Getting all regions");
            return Ok(regionResponse);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetRegion([FromRoute] Guid id)
        {
            _logger.LogInformation($"Getting region with id {id}");
            var region = await _regionRepository.GetRegionAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            var regionResponse = _mapper.Map<RegionResponse>(region);
            return Ok(regionResponse);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateRegion([FromBody] RegionRequest regionAddRequest)
        {
            var region = _mapper.Map<Region>(regionAddRequest);
            var addedRegion = await _regionRepository.CreateRegionAsync(region);
            var regionResponse = _mapper.Map<RegionResponse>(addedRegion);
            return CreatedAtAction(nameof(GetRegion), new { id = regionResponse.Id }, regionResponse);
        }

        [HttpPut("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id,
            [FromBody] RegionRequest regionUpdateRequest)
        {
            var regionToUpdate = _mapper.Map<Region>(regionUpdateRequest);
            var updatedRegion = await _regionRepository.UpdateRegionAsync(id, regionToUpdate);

            if (updatedRegion == null)
            {
                return NotFound($"Can't find the id u looking for");
            }

            _logger.LogInformation($"region updated successfully.");
            var regionResponse = _mapper.Map<RegionResponse>(updatedRegion);
            return Ok(regionResponse);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            try
            {
                await _regionRepository.DeleteRegionAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Region not found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}