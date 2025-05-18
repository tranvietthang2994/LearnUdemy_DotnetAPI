using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
	public interface IImageRepository
	{
		Task<Image> Upload(Image image);
	}
}
