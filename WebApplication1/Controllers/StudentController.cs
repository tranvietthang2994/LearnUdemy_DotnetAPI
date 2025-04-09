using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
	// https://localhost:portnumber/api/students
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		// GET: https://localhost:portnumber/api/students
		[HttpGet]
		public IActionResult GetAllStudent()
		{
			string[] studentNames = new string[] { "A", "B", "C" };
			return Ok(studentNames);
		}
	}
}
