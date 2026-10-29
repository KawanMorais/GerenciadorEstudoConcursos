using GerenciadorEstudo.Api.Models;

namespace GerenciadorEstudo.Api.Services;

public interface ITokenService
{
    (string Token, DateTime ExpiraEm) GerarToken(Usuario usuario);
}
