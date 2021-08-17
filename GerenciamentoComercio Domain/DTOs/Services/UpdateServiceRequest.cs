namespace GerenciamentoComercio_Domain.DTOs.Services
{
    public class UpdateServiceRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsActive { get; set; }
        public int? Sla { get; set; }
        public decimal? Price { get; set; }
    }
}
