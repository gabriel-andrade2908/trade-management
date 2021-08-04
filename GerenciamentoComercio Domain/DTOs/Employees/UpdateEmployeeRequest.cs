namespace GerenciamentoComercio_Domain.DTOs.Employees
{
    public class UpdateEmployeeRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Access { get; set; }
        public bool? IsAdministrator { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
