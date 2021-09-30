using GerenciamentoComercio_Domain.DTOs.Dashboard;
using GerenciamentoComercio_Domain.Utils.IRepository;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Infra.Context;
using GerenciamentoComercio_Infra.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GerenciamentoComercio_Domain.v1.Repositories
{
    public class ClientTransactionRepository : Repository<ClientTransaction>, IClientTransactionRepository
    {
        public ClientTransactionRepository(GerenciamentoComercioContext context) : base(context)
        {
        }

        public List<ClientTransaction> GetTransactionByClient(int clientId)
        {
            return _context.ClientTransaction
                .Where(x => x.IdClient == clientId).ToList();
        }

        public List<ClientTransaction> GetTransactionByEmployee(int employeeId)
        {
            return _context.ClientTransaction
                .Where(x => x.IdEmployee == employeeId).ToList();
        }

        public bool CheckIfServiceAlreadyExistInTransaction(int id)
        {
            return _context.ClientTransaction.Include(x => x.ClientTransactionService)
                .Any(x => x.ClientTransactionService.Select(x => x.IdService).Contains(id));
        }

        public bool CheckIfProductAlreadyExistInTransaction(int id)
        {
            return _context.ClientTransaction.Include(x => x.ClientTransactionProduct)
                .Any(x => x.ClientTransactionProduct.Select(x => x.IdProduct).Contains(id));
        }

        public List<GetDashboardProductsTransactionsResponse> GetDashboardProductsTransactions(int numberOfDays)
        {
            return (from CT in _context.ClientTransaction
                    .Where(x => x.CreationDate > DateTime.Now.AddDays(-numberOfDays))
                    join CTP in _context.ClientTransactionProduct on CT.Id equals CTP.IdClientTransaction
                    select new GetDashboardProductsTransactionsResponse
                    {
                        ClientId = CT.IdClient ?? 0,
                        EmployeeId = CT.IdEmployee ?? 0,
                        ProductId = CTP.IdProduct ?? 0,
                        TotalPrice = CT.TotalPrice ?? 0,
                        ProductCategoryId = CTP.IdProductNavigation.IdProductCategory ?? 0,
                    }).ToList();
        }

        public List<GetDashboardServicesTransactionsResponse> GetDashboardServicesTransactions(int numberOfDays)
        {
            return (from CT in _context.ClientTransaction
                    .Where(x => x.CreationDate > DateTime.Now.AddDays(-numberOfDays))
                    join CTS in _context.ClientTransactionService on CT.Id equals CTS.IdClientTransaction
                    select new GetDashboardServicesTransactionsResponse
                    {
                        ServiceId = CTS.IdService ?? 0,
                        ServiceCategoryId = CTS.IdServiceNavigation.IdServiceCategory ?? 0
                    }).ToList();
        }
    }
}
