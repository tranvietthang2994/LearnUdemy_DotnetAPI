using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Repositories
{
	public interface ITokenRepository
	{
		string CreatJWTToken(IdentityUser user, IList<string> roles);
	}
}
