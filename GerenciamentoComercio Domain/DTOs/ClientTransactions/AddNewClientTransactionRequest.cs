using GerenciamentoComercio_Domain.DTOs.ProductTransaction;
using System.Collections.Generic;

namespace GerenciamentoComercio_Domain.DTOs
{
    public class AddNewClientTransactionRequest
    {
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public int DiscountPrice { get; set; }
        public int DiscountPercentage { get; set; }
        public string Observations { get; set; }
        public List<AddNewProductServiceTransactionRequest> Products { get; set; }
        public List<AddNewProductServiceTransactionRequest> Services { get; set; }
    }
}
