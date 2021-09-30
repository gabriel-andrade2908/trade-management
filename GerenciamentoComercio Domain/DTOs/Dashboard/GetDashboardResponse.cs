using System.Collections.Generic;

namespace GerenciamentoComercio_Domain.DTOs.Dashboard
{
    public class GetDashboardResponse
    {
        public int TotalTransactions { get; set; }
        public decimal TotalValue { get; set; }
        public List<DashboardProductsResponse> TransactionsByProduct { get; set; }
        public List<DasboardTransactionsByProductCategoryResponse> TransactionsByProductCategory { get; set; }
        public List<DashboardServicesResponse> TransactionsByService { get; set; }
        public List<DasboardTransactionsByServiceCategoryResponse> TransactionsByServiceCategory { get; set; }
        public List<DasboardTransactionsByClientResponse> TransactionsByClient { get; set; }
        public List<DasboardTransactionsByEmployeeResponse> TransactionsByEmployee { get; set; }
    }
}
