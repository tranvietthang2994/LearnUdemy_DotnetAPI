using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
	// https://localhost:9999/api/...
	[Route("api/[controller]")]
	[ApiController]
	public class RegionController : ControllerBase
	{
		private readonly WinWalksDbContext dbContext;

		public RegionController(WinWalksDbContext _dbContext)
        {
			this.dbContext = _dbContext;
		}

		//-------------------------------------------------------------------------------------------------------------------
		// GET ALL REGION
		// GET: https://localhost:9999/api/region
        [HttpGet]
		public async Task<IActionResult> GetAll()
		{
			//Get Data from Database - Domain models
			var regionsDomain = await dbContext.Regions.ToListAsync();

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
			var regionDomain = await dbContext.Regions.SingleOrDefaultAsync(r => r.Id == id);

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
			await dbContext.Regions.AddAsync(regionsDomainModel);
			await dbContext.SaveChangesAsync();

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
			var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

			if (regionDomainModel == null)
			{
				return NotFound();
			}

			// Map DTO to Domain model
			regionDomainModel.Code = updateRegionRequestDto.Code;
			regionDomainModel.Name = updateRegionRequestDto.Name;
			regionDomainModel.RegionImgUrl = updateRegionRequestDto.RegionImgUrl;

			await dbContext.SaveChangesAsync();

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
			var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(y => y.Id == id);
			
			if (regionDomainModel == null)
			{
				return NotFound();
			}

			dbContext.Regions.Remove(regionDomainModel);
			await dbContext.SaveChangesAsync();

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
