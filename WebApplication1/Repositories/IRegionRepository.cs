using System.Runtime.InteropServices;
using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
	public interface IRegionRepository
	{
		Task<List<Region>> GetAllAsync();

		Task<Region?> GetByIdAsync(Guid id);

		Task<Region?> CreateAsync(Region region);

		Task<Region?> UpdateAsync(Guid id, Region region);

		Task<Region?> DeleteAsync(Guid id);
	}
}
