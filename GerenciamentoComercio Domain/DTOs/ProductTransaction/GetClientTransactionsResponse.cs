using System;

namespace GerenciamentoComercio_Domain.DTOs.ProductTransaction
{
    public class GetClientTransactionsResponse
    {
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public decimal DiscountPrice { get; set; }
        public int DiscountPercentage { get; set; }
        public decimal TotalPrice { get; set; }
        public string Observations { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationUser { get; set; }
    }
}
