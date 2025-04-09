using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Domain;

namespace WebApplication1.Data
{
	public class WinWalksDbContext : DbContext
	{
		//ctor
		public WinWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
		{

		}

		//prop
        public DbSet<Difficulty> Difficulties { get; set; }
		public DbSet<Region> Regions { get; set; }
		public DbSet<Walk> Walks { get; set; }

	}
}
