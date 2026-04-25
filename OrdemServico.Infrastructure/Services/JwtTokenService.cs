using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace OrdemServico.Infrastructure.Services;

public interface IJwtTokenService
{
    string GenerateToken(string username);
}

public class JwtTokenService : IJwtTokenService
{
    private const string SecretKey = "sua-chave-super-secreta-com-mais-de-32-caracteres-aqui-12345";
    private const string Issuer = "OrdemServico";
    private const string Audience = "OrdemServicoApp";
    public string GenerateToken(string username)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
          {
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, username),
            new Claim("role", "user")
        };
        var token = new JwtSecurityToken(
              issuer: Issuer,
              audience: Audience,
              claims: claims,
              expires: DateTime.UtcNow.AddHours(24),
              signingCredentials: credentials
          );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}