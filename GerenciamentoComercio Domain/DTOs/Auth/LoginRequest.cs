using System.ComponentModel.DataAnnotations;

namespace GerenciamentoComercio_Domain.DTOs.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "O campo Acesso é obrigatório.")]
        public string Access { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Password { get; set; }
    }
}
