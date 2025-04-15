using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
	public interface IWalkRepository
	{
		Task<Walk> CreateAsync(Walk walk);
		Task<List<Walk>> GetAllAsync();
	}
}
