using GerenciadorEstudo.Api.DTOs;
using GerenciadorEstudo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorEstudo.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Cadastrar(CadastroRequestDto dto)
    {
        var cadastrou = await _authService.CadastrarAsync(dto);

        if (!cadastrou)
        {
            return Conflict(new { mensagem = "E-mail já cadastrado." });
        }

        return StatusCode(StatusCodes.Status201Created, new { mensagem = "Conta criada com sucesso." });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginRequestDto dto)
    {
        var resposta = await _authService.LoginAsync(dto);

        if (resposta is null)
        {
            return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });
        }

        return Ok(resposta);
    }
}
