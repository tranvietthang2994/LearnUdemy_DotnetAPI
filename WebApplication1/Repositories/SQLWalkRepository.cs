using Microsoft.EntityFrameworkCore;
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

		public async Task<List<Walk>> GetAllAsync()
		{
			return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();

		}
	}
}
