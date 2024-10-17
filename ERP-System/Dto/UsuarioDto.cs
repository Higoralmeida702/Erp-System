using ERP_System.Enum;
using System.ComponentModel.DataAnnotations;

namespace ERP_System.Dto
{
    public class UsuarioDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O sobrenome é obrigatório.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O sobrenome deve ter entre 2 e 50 caracteres.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [StringLength(100, ErrorMessage = "O endereço não pode exceder 100 caracteres.")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "O número de telefone deve ter entre 10 e 15 dígitos.")]
        public string NumeroTelefone { get; set; }

        [Required(ErrorMessage = "Digite uma senha valida")]
        public string Senha { get; set; }

        [Required]
        [Compare("Senha", ErrorMessage = "Senha nao coincidem")]
        public string ConfirmacaoSenha { get; set; }

    }
}
