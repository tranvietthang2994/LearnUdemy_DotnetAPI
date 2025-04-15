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
		// GET: /api/walk
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var walksDomainModel = await walkRepository.GetAllAsync();

			var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);

			return Ok(walksDto);
		}
	}
}
