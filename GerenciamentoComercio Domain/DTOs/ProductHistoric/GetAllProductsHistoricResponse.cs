using System;

namespace GerenciamentoComercio_Domain.DTOs.ProductHistoric
{
    public class GetAllProductsHistoricResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal? ProductPrice { get; set; }
        public long ProductQuantity { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationUser { get; set; }
    }
}
