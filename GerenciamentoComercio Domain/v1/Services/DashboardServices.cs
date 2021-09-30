using GerenciamentoComercio_Domain.DTOs.Dashboard;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace GerenciamentoComercio_Domain.v1.Services
{
    public class DashboardServices : IDashboardServices
    {
        private readonly IClientTransactionRepository _clientTransactionRepository;

        public DashboardServices(IClientTransactionRepository clientTransactionRepository)
        {
            _clientTransactionRepository = clientTransactionRepository;
        }

        public APIMessage GetDashboard(int numberOfDays)
        {
            List<GetDashboardProductsTransactionsResponse> transactions = _clientTransactionRepository
                .GetDashboardProductsTransactions(numberOfDays);

            List<GetDashboardServicesTransactionsResponse> servicesTransactions = _clientTransactionRepository
                .GetDashboardServicesTransactions(numberOfDays);

            return new APIMessage(HttpStatusCode.OK, transactions
                .Select(x => new GetDashboardResponse
                {
                    TotalTransactions = transactions.Count(),
                    TotalValue = transactions.Sum(x => x.TotalPrice),
                    TransactionsByClient = transactions.GroupBy(x => x.ClientId).Select(x => new DasboardTransactionsByClientResponse
                    {
                        ClientId = x.Key,
                        TransactionsCount = x.Count()
                    }).ToList(),
                    TransactionsByEmployee = transactions.GroupBy(x => x.EmployeeId).Select(x => new DasboardTransactionsByEmployeeResponse
                    {
                        EmployeeId = x.Key,
                        TransactionsCount = x.Count()
                    }).ToList(),
                    TransactionsByProduct = transactions.GroupBy(x => x.ProductId)
                   .Select(x => new DashboardProductsResponse
                    {
                        ProductId = x.Key,
                        TransactionsCount = x.Count()
                    }).ToList(),
                    TransactionsByProductCategory = transactions.GroupBy(x => x.ProductCategoryId).Select(x => new DasboardTransactionsByProductCategoryResponse
                    {
                        ProductCategoryId = x.Key,
                        TransactionsCount = x.Count()
                    }).ToList(),
                    TransactionsByService = servicesTransactions.GroupBy(x => x.ServiceId).Select(x => new DashboardServicesResponse
                    {
                        ServiceId = x.Key,
                        TransactionsCount = x.Count()
                    }).ToList(),
                    TransactionsByServiceCategory = servicesTransactions.GroupBy(x => x.ServiceCategoryId).Select(x => new DasboardTransactionsByServiceCategoryResponse
                    {
                        ServiceCategoryId = x.Key,
                        TransactionsCount = x.Count()
                    }).ToList(),
                }).FirstOrDefault());
        }
    }
}
