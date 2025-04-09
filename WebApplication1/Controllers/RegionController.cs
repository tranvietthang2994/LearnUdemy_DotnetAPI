using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
	// https://localhost:9999/api/...
	[Route("api/[controller]")]
	[ApiController]
	public class RegionController : ControllerBase
	{
		private readonly WinWalksDbContext dbContext;
		private readonly IRegionRepository regionRepository;

		public RegionController(WinWalksDbContext _dbContext, IRegionRepository regionRepository)
        {
			this.dbContext = _dbContext;
			this.regionRepository = regionRepository;
		}

		//-------------------------------------------------------------------------------------------------------------------
		// GET ALL REGION
		// GET: https://localhost:9999/api/region
        [HttpGet]
		public async Task<IActionResult> GetAll()
		{
			//Get Data from Database - Domain models
			var regionsDomain = await regionRepository.GetAllAsync();

			//Map Domain Models to DTOs
			var regionsDto = new List<RegionDto>();
			foreach (var regionDomain in regionsDomain)
			{
				regionsDto.Add(new RegionDto()
				{
					Id = regionDomain.Id,
					Code = regionDomain.Code,
					Name = regionDomain.Name,
					RegionImgUrl = regionDomain.RegionImgUrl
				});
			}

			//Return DTOs
			return Ok(regionsDto);
		}

		//-------------------------------------------------------------------------------------------------------------------
		// GET SINGLE REGION (Get region by id)
		// GET: https://localhost:9999/api/region/{id}
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			//Chỉ dùng được khi tìm bằng khoá chính
			//var region = dbContext.Regions.Find(id);

			//Get Data from Database - Domain models
			var regionDomain = await regionRepository.GetByIdAsync(id);

			if (regionDomain == null)
			{
				return NotFound();
			}

			//Map Domain Models to DTOs
			var regionDto = new RegionDto
			{
				Id = regionDomain.Id,
				Code = regionDomain.Code,
				Name = regionDomain.Name,
				RegionImgUrl = regionDomain.RegionImgUrl
			};

			return Ok(regionDto);
		}

		//-------------------------------------------------------------------------------------------------------------------
		// POST To create new region
		// POST: https://localhost:9999/api/region
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
			// Map or Convert DTO to Domain Model
			var regionsDomainModel = new Region
			{
				Code = addRegionRequestDto.Code,
				Name = addRegionRequestDto.Name,
				RegionImgUrl = addRegionRequestDto.RegionImgUrl
			};

			// Use Domain Model to create Region
			regionsDomainModel = await regionRepository.CreateAsync(regionsDomainModel);

			// Map Domain model back to DTO
			var regionDto = new RegionDto
			{
				Id = regionsDomainModel.Id,
				Code = regionsDomainModel.Code,
				Name = regionsDomainModel.Name,
				RegionImgUrl = regionsDomainModel.RegionImgUrl
			};

			return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
			
		}

		//-------------------------------------------------------------------------------------------------------------------
		// Update region
		// PUT: https://localhost:9999/api/region/{id}
		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
		{
			// Map DTO to Domain model
			var regionDomainModel = new Region
			{
				Code = updateRegionRequestDto.Code,
				Name = updateRegionRequestDto.Name,
				RegionImgUrl = updateRegionRequestDto.RegionImgUrl
			};

			//Check if region existed
			regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

			if (regionDomainModel == null)
			{
				return NotFound();
			}

			// Convert Domain to DTO
			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImgUrl = regionDomainModel.RegionImgUrl
			};

			return Ok(regionDto);
		}

		//-------------------------------------------------------------------------------------------------------------------
		// Delete region
		// DELETE: https://localhost:9999/api/region/{id}
		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id) { 
			var regionDomainModel = await regionRepository.DeleteAsync(id);

			if (regionDomainModel == null)
			{
				return NotFound();
			}

			// Convert Domain to DTO
			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImgUrl = regionDomainModel.RegionImgUrl
			};

			return Ok(regionDto);
		}
	}
}
