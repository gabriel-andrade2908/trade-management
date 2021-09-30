namespace GerenciamentoComercio_Domain.DTOs.Dashboard
{
    public class GetDashboardProductsTransactionsResponse
    {
        public int ProductId { get; set; }
        public int ProductCategoryId { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
