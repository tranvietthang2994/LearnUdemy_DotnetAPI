using WebApplication1.Data;
using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
	public class LocalImageRepository : IImageRepository
	{
		private readonly IWebHostEnvironment webHostEnvironment;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly WinWalksDbContext dbContext;

		public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, WinWalksDbContext dbContext)
		{
			this.webHostEnvironment = webHostEnvironment;
			this.httpContextAccessor = httpContextAccessor;
			this.dbContext = dbContext;
		}

		public async Task<Image> Upload(Image image)
		{
			var filePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",$"{ image.FileName}{image.FileExtension}" );

			// Upload image to local file system
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await image.File.CopyToAsync(stream);
			}

			// https://localhost:5000/Images/filename.extension

			var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}" +
				$"";

			image.FilePath = urlFilePath;

			// Save image to database
			await dbContext.Images.AddAsync(image);
			await dbContext.SaveChangesAsync();

			return image;
		}
	}
}
