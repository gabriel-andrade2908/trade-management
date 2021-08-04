namespace GerenciamentoComercio_Domain.DTOs.Employees
{
    public class GetEmployeesResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool? IsAdministrator { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
