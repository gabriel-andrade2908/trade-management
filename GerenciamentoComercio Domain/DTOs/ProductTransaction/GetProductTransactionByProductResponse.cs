using System;

namespace GerenciamentoComercio_Domain.DTOs.ProductTransaction
{
    public class GetProductTransactionByProductResponse
    {
        public int Id { get; set; }
        public int ClientTransactionId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationUser { get; set; }
    }
}
