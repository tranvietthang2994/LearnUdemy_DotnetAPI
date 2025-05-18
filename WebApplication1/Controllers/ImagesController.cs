using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageRepository imageRepository;

		public ImagesController(IImageRepository imageRepository)
        {
			this.imageRepository = imageRepository;
		}

        // POST: /api/Images/Upload
        [HttpPost]
		[Route("Upload")]
		public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto requestDto)
		{
			ValidateFileUpLoadRequestDto(requestDto);

            if (ModelState.IsValid)
            {
				// Convert dto to domain model
				var imageDomainModel = new Image
				{
					File = requestDto.File,
					FileName = requestDto.FileName,
					FileDescription = requestDto.FileDescription,
					FileExtension = Path.GetExtension(requestDto.File.FileName),
					FileSizeInBytes = requestDto.File.Length,
				};

				// User respository to upload image
				await imageRepository.Upload(imageDomainModel);

				return Ok(imageRepository);
			}

			return BadRequest(ModelState);
		}

		private void ValidateFileUpLoadRequestDto(ImageUploadRequestDto requestDto)
		{
			var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };

			if (!allowedExtensions.Contains(Path.GetExtension(requestDto.File.FileName).ToLower()))
			{
				ModelState.AddModelError("file", "Invalid file type. Only .jpg, .jpeg, .png, and .gif are allowed.");
			}

			if (requestDto.File.Length > 10 * 1024 * 1024) // 10 MB limit
			{
				ModelState.AddModelError("file", "File size exceeds the limit of 5 MB.");
			}
		}
	}
}
