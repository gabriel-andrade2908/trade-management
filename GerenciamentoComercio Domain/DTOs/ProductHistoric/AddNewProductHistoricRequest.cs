namespace GerenciamentoComercio_Domain.DTOs.ProductHistoric
{
    public class AddNewProductHistoricRequest
    {
        public int ProductId { get; set; }
        public decimal? ProductPrice { get; set; }
        public long ProductQuantity { get; set; }
    }
}
