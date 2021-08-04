using System.ComponentModel.DataAnnotations;

namespace GerenciamentoComercio_Domain.DTOs.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Access { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Password { get; set; }
    }
}
