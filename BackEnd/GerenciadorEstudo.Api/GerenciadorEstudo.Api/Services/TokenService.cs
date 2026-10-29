using System.Security.Claims;
using System.Text;
using GerenciadorEstudo.Api.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace GerenciadorEstudo.Api.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public (string Token, DateTime ExpiraEm) GerarToken(Usuario usuario)
    {
        var chave = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("Chave do JWT 'Jwt:Key' não encontrada.");

        var minutos = _configuration.GetValue<int>("Jwt:ExpiracaoMinutos");
        var expiraEm = DateTime.UtcNow.AddMinutes(minutos);

        var credenciais = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave)),
            SecurityAlgorithms.HmacSha256);

        var descricao = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email)
            }),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            Expires = expiraEm,
            SigningCredentials = credenciais
        };

        var token = new JsonWebTokenHandler().CreateToken(descricao);

        return (token, expiraEm);
    }
}
