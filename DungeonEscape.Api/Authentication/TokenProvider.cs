using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DungeonEscape.Api.Authentication;

public class TokenProvider
{
	private readonly byte[] _key;

	public TokenProvider(byte[] key)
	{
		_key = key;
	}

	public string GenerateToken(int gameId, int userId)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new Claim[]
			{
						new Claim(ClaimTypes.Name, userId.ToString()),
						new Claim(ClaimTypes.Country, gameId.ToString())
			}),
			Expires = DateTime.UtcNow.AddDays(1),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha512Signature)
		};
		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}
}