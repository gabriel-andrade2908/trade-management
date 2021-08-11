namespace GerenciamentoComercio_Domain.DTOs.Employees
{
    public class GetEmployeeByIdResponse
    {
        public string FullName { get; set; }
        public string Access { get; set; }
        public string Email { get; set; }
        public bool? IsAdministrator { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
