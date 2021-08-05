using System.ComponentModel.DataAnnotations;

namespace GerenciamentoComercio_Domain.DTOs.Employees
{
    public class UpdateEmployeeRequest
    {
        public string FullName { get; set; }

        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Formato do campo Email inválido")]
        public string? Email { get; set; }
        public string Access { get; set; }
        public bool? IsAdministrator { get; set; }

        [RegularExpression(@"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$", ErrorMessage = "Formato do campo Telefone inválido")]
        public string? Phone { get; set; }
        public string Address { get; set; }
    }
}
