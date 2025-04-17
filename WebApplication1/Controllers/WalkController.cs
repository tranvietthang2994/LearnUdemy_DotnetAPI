using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
	// api.walk
	[Route("api/[controller]")]
	[ApiController]
	public class WalkController : ControllerBase
	{
		private readonly IMapper mapper;
		private readonly IWalkRepository walkRepository;

		public WalkController(IMapper mapper, IWalkRepository walkRepository) {
			this.mapper = mapper;
			this.walkRepository = walkRepository;
		}


		// CREATE Walk
		// POST: /api/walk
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
		{
			// Map DTO to Domain Model
			var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

			await walkRepository.CreateAsync(walkDomainModel);

			return Ok(mapper.Map<WalkDto>(walkDomainModel));
		}

		// GET ALL Walks
		// GET: /api/walk?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
			[FromQuery] string? sortBy, [FromQuery] bool? isAscending)
		{
			var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true);

			var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);

			return Ok(walksDto);
		}

		// GET Walk by Id
		// GET: /api/walk/{id}
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var walkDomainModel = await walkRepository.GetByIdAsync(id);
			
			if (walkDomainModel == null)
			{
				return NotFound();
			}

			var walkDto = mapper.Map<WalkDto>(walkDomainModel);

			return Ok(walkDto);	
		}

		// UPDATE Walk by Id
		// PUT: /api/Walk
		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
		{
			//Map Dto to Domain model
			var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

			walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

			if (walkDomainModel == null)
			{
				return NotFound();
			}

			// Map Domain Model to DTO
			return Ok(mapper.Map<WalkDto>(walkDomainModel));
		}

		// DELETE Walk by Id
		// DELETE: /api/Walk/{id}
		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var walkDomainModel = await walkRepository.DeleteAsync(id);

			if (walkDomainModel == null)
			{
				return NotFound();
			}

			return Ok(mapper.Map<WalkDto>(walkDomainModel));
		}
	}
}
