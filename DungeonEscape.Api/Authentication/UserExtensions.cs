using System.Security.Claims;

namespace DungeonEscape.Api.Authentication;

public static class UserExtensions
{
	public static int GetUserId(this ClaimsPrincipal user)
	{
		var claim = user.Claims.First(x => x.Type == ClaimTypes.Name);
		return Convert.ToInt32(claim.Value);
	}
	public static int GetGameId(this ClaimsPrincipal user)
	{
		var claim = user.Claims.First(x => x.Type == ClaimTypes.Country);
		return Convert.ToInt32(claim.Value);
	}
}
