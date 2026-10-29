using System.ComponentModel.DataAnnotations;

namespace GerenciadorEstudo.Api.DTOs;

public class CadastroRequestDto
{
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    [MaxLength(255, ErrorMessage = "O e-mail pode ter no máximo 255 caracteres.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(8, ErrorMessage = "A senha precisa ter pelo menos 8 caracteres.")]
    [MaxLength(72, ErrorMessage = "A senha pode ter no máximo 72 caracteres.")]
    public string Senha { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirme a senha.")]
    [Compare(nameof(Senha), ErrorMessage = "As senhas não conferem.")]
    public string ConfirmarSenha { get; set; } = string.Empty;
}
