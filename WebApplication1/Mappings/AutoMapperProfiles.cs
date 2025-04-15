using AutoMapper;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;

namespace WebApplication1.Mappings
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles() {
			CreateMap <Region, RegionDto>().ReverseMap(); // Region => RegionDTO (if no reverse)
			CreateMap<AddRegionRequestDto, Region>().ReverseMap();
			CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
			CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
			CreateMap<Walk, WalkDto>().ReverseMap();
			CreateMap<Difficulty, DifficultyDto>().ReverseMap();

		}
	}


}
