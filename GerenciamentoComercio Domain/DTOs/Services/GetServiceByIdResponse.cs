namespace GerenciamentoComercio_Domain.DTOs.Services
{
    public class GetServiceByIdResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public decimal Sla { get; set; }
        public decimal Price { get; set; }
    }
}
