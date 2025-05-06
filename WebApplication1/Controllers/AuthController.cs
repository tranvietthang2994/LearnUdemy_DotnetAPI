using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private UserManager<IdentityUser> userManager;

		public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
		}

        // POST: /api/Auth/Register
        [HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
		{
			var identityUser = new IdentityUser
			{
				UserName = registerRequestDto.Username,
				Email = registerRequestDto.Username,
			};

			var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

			if (identityResult.Succeeded)
			{
				// Add roles to this User
				if (registerRequestDto.Roles == null || registerRequestDto.Roles.Any())
				{
					identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

					if (identityResult.Succeeded)
					{
						return Ok(new { message = "User registered successfully" });
					}
				}
			}

			return BadRequest("Something went wrong");
		}
		//user@gmail.com
		//User@123

		// POST: /api/Auth/Login
		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
		{
			var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
			
			if (user != null)
			{
				var checkPassResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
				
				if (checkPassResult)
				{
					// Create token

					return Ok();
				}
			}

			return BadRequest("Username or password is incorrect.");
		}
	}
}
