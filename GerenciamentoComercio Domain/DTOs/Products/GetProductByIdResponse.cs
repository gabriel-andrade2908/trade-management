namespace GerenciamentoComercio_Domain.DTOs.Products
{
    public class GetProductByIdResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
