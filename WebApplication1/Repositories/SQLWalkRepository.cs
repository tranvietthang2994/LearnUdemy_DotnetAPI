using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using WebApplication1.Data;
using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
	public class SQLWalkRepository : IWalkRepository
	{
		private readonly WinWalksDbContext dbContext;

		public SQLWalkRepository(WinWalksDbContext dbContext)
        {
			this.dbContext = dbContext;
		}
        public async Task<Walk> CreateAsync(Walk walk)
		{
			await dbContext.Walks.AddAsync(walk);
			await dbContext.SaveChangesAsync();
			return walk;
		}


		public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null)
		{
			var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

			// Filter
			if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
			{
				if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
					walks = walks.Where(x => x.Name == filterQuery);
				}	
			}

			return await walks.ToListAsync();
			//return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
		}

		public async Task<Walk?> GetByIdAsync(Guid id)
		{
			return await dbContext.Walks
				.Include("Difficulty")
				.Include("Region")
				.FirstOrDefaultAsync(x => x.Id == id);		
		}

		public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
		{
			var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
			if (existingWalk == null)
			{
				return null;
			}

			existingWalk.Name = walk.Name;
			existingWalk.Description= walk.Description;
			existingWalk.LengthInKm = walk.LengthInKm;
			existingWalk.WalkImgUrl = walk.WalkImgUrl;
			existingWalk.DifficultyId = walk.DifficultyId;
			existingWalk.RegionId = walk.RegionId;

			await dbContext.SaveChangesAsync();

			return existingWalk;
		}

		public async Task<Walk?> DeleteAsync(Guid id)
		{
			var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
			if (existingWalk == null)
			{
				return null;
			}
			
			dbContext.Walks.Remove(existingWalk);
			await dbContext.SaveChangesAsync();

			return existingWalk;
		}

	}
}
