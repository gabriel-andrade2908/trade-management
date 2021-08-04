namespace GerenciamentoComercio_Domain.DTOs.Clients
{
    public class AddNewClientRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
