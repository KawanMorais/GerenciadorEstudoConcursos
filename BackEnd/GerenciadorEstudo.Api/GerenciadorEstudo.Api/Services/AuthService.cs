using GerenciadorEstudo.Api.DTOs;
using GerenciadorEstudo.Api.Models;
using GerenciadorEstudo.Api.Repositories;

namespace GerenciadorEstudo.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IUsuarioRepository usuarioRepository, ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
    }

    public async Task<bool> CadastrarAsync(CadastroRequestDto dto)
    {
        var email = NormalizarEmail(dto.Email);

        if (await _usuarioRepository.EmailExisteAsync(email))
        {
            return false;
        }

        var usuario = new Usuario
        {
            Email = email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
        };

        await _usuarioRepository.AdicionarAsync(usuario);
        return true;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        var email = NormalizarEmail(dto.Email);
        var usuario = await _usuarioRepository.ObterPorEmailAsync(email);

        if (usuario is null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
        {
            return null;
        }

        var (token, expiraEm) = _tokenService.GerarToken(usuario);

        return new AuthResponseDto
        {
            Token = token,
            Email = usuario.Email,
            ExpiraEm = expiraEm
        };
    }

    private static string NormalizarEmail(string email)
    {
        return email.Trim().ToLowerInvariant();
    }
}
