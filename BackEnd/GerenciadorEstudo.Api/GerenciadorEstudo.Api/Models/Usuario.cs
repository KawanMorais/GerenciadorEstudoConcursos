namespace GerenciadorEstudo.Api.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public string FusoHorario { get; set; } = "America/Sao_Paulo";
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}
