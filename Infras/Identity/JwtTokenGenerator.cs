using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infras.Interfaces;

namespace Infras.Identity;

public class JwtTokenGenerator : IIdentityService
{
  private readonly IConfiguration _config;

  public JwtTokenGenerator(IConfiguration config)
  {
    _config = config;
  }

  public string GenerateToken(string userId, string email, IEnumerable<string> roles)
  {
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
          new Claim(JwtRegisteredClaimNames.Sub, userId),
          new Claim(JwtRegisteredClaimNames.Email, email),
          new Claim("role", "Admin")
      };

    var token = new JwtSecurityToken(
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(120),
        signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
