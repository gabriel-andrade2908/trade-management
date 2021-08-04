using System.ComponentModel.DataAnnotations;

namespace GerenciamentoComercio_Domain.DTOs.Employees
{
    public class AddNewEmployeeRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Access { get; set; }
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool GeneratePassword { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public bool IsAdministrator { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Address { get; set; }
    }
}
