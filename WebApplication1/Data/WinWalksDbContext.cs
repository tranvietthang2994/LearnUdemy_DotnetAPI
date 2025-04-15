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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Seed data for Difficulties

			var diffuculties = new List<Difficulty>()
			{
				new Difficulty()
				{
					Id = Guid.Parse("2063b4d2-a031-4ce7-903e-7a3aed191864"),
					Name = "Easy"
				},
				new Difficulty()
				{
					Id = Guid.Parse("a805c2c7-fda8-45c7-8f9d-2b2bb46f2918"),
					Name = "Medium"
				},
				new Difficulty()
				{
					Id = Guid.Parse("a00d3707-e0d4-40c8-9591-71da0b0d1012"),
					Name = "Hard"
				}
			};

			// Seed difficulties to the database
			modelBuilder.Entity<Difficulty>().HasData(diffuculties);

			// Seed data for Regions
			var regions = new List<Region>
			{
				new Region
				{
					Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
					Name = "Auckland",
					Code = "AKL",
					RegionImgUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
				},
				new Region
				{
					Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
					Name = "Northland",
					Code = "NTL",
					RegionImgUrl = null
				},
				new Region
				{
					Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
					Name = "Bay Of Plenty",
					Code = "BOP",
					RegionImgUrl = null
				},
				new Region
				{
					Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
					Name = "Wellington",
					Code = "WGN",
					RegionImgUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
				},
				new Region
				{
					Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
					Name = "Nelson",
					Code = "NSN",
					RegionImgUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
				},
				new Region
				{
					Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
					Name = "Southland",
					Code = "STL",
					RegionImgUrl = null
				},
			};

			modelBuilder.Entity<Region>().HasData(regions);

		}

	}
}
