using GerenciadorEstudo.Api.DTOs;

namespace GerenciadorEstudo.Api.Services;

public interface IAuthService
{
    // false = o e-mail já está cadastrado
    Task<bool> CadastrarAsync(CadastroRequestDto dto);

    // null = e-mail ou senha inválidos
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto dto);
}
