using GerenciadorEstudo.Api.Models;

namespace GerenciadorEstudo.Api.Repositories;

public interface IUsuarioRepository
{
    Task<bool> EmailExisteAsync(string email);
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task AdicionarAsync(Usuario usuario);
}
