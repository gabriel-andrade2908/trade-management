using System.ComponentModel.DataAnnotations;

namespace GerenciamentoComercio_Domain.DTOs.Clients
{
    public class AddNewClientRequest
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Formato do campo Email inválido")]
        public string Email { get; set; }

        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "Formato do campo CPF inválido")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório")]
        [RegularExpression(@"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$", ErrorMessage = "Formato do campo Telefone inválido")]
        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
