using Microsoft.AspNetCore.Mvc;
using OrdemServico.API.Dtos;
using OrdemServico.Infrastructure.Services;

namespace OrdemServico.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenService _jwtService;
    public AuthController(IJwtTokenService jwtService) => _jwtService = jwtService;
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
            return BadRequest("Username e password são obrigatórios");
        var token = _jwtService.GenerateToken(dto.Username);
        return Ok(new TokenResponseDto { Token = token, Username = dto.Username });
    }
}