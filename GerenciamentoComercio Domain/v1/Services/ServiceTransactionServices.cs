using GerenciamentoComercio_Domain.DTOs.ProductTransaction;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.UnitOfWork;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using GerenciamentoComercio_Infra.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Services
{
    public class ServiceTransactionServices : IServiceTransactionServices
    {
        private readonly IClientTransactionRepository _clientTransactionRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ITransactionsCommonServices _transactionsCommonServices;
        private readonly IClientTransactionServiceRepository _clientTransactionServiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceTransactionServices(IUnitOfWork unitOfWork,
            IClientTransactionRepository clientTransactionRepository,
            ITransactionsCommonServices transactionsCommonServices,
            IServiceRepository serviceRepository,
            IClientTransactionServiceRepository clientTransactionServiceRepository)
        {
            _unitOfWork = unitOfWork;
            _clientTransactionRepository = clientTransactionRepository;
            _transactionsCommonServices = transactionsCommonServices;
            _serviceRepository = serviceRepository;
            _clientTransactionServiceRepository = clientTransactionServiceRepository;
        }

        public async Task<APIMessage> AddNewServiceTransactionAsync(List<AddNewProductServiceTransactionRequest> services, int clientTransactionId, string userName)
        {
            ClientTransaction clientTransaction = await _clientTransactionRepository
                .GetById(clientTransactionId);

            if (clientTransaction == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "A transação informada não existe." });
            }

            var pricesToChangeInClientTransaction = new List<decimal>();

            foreach (AddNewProductServiceTransactionRequest serviceRequest in services)
            {
                Service service = _serviceRepository.GetById(serviceRequest.Id);

                if (service == null)
                {
                    return new APIMessage(HttpStatusCode.NotFound,
                        new List<string> { $"Serviço {serviceRequest.Id} não foi encontrado." });
                }

                if (_clientTransactionRepository.CheckIfServiceAlreadyExistInTransaction(serviceRequest.Id))
                {
                    return new APIMessage(HttpStatusCode.NotFound,
                        new List<string> { $"O serviço {serviceRequest.Id} já está inserido na transação." });
                }

                APIMessage newServiceTransaction =  _transactionsCommonServices
                    .AddNewServiceTransaction(serviceRequest, userName, clientTransactionId);

                pricesToChangeInClientTransaction.Add((decimal)newServiceTransaction.ContentObj);
            }

            UpdateClientTransaction(clientTransaction,
                pricesToChangeInClientTransaction.Sum(), true);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK,
                new List<string> { "Transação cadastrada com sucesso." });
        }

        public async Task<APIMessage> UpdateServiceTransactionAsync(int quantity, int id)
        {
            ClientTransactionService serviceTransaction = await _clientTransactionServiceRepository
                .GetById(id);

            if (serviceTransaction == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                new List<string> { "Transação não encontrada." });
            }

            ClientTransaction clientTransaction = await _clientTransactionRepository
                .GetById(serviceTransaction.IdClientTransaction.Value);

            decimal priceToChange = (serviceTransaction.Quantity.Value - quantity) *
                serviceTransaction.Price.Value;

            bool isAdd = quantity > serviceTransaction.Quantity;

            UpdateClientTransaction(clientTransaction, priceToChange, isAdd);

            serviceTransaction.Quantity = quantity;

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK,
                new List<string> { "Transação de serviço atualizada com sucesso." });
        }

        public async Task<APIMessage> DeleteServiceTransactionAsync(int id)
        {
            ClientTransactionService serviceTransaction = await _clientTransactionServiceRepository
                .GetById(id);

            if (serviceTransaction == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Transação de serviço não encontrada." });
            }

            _clientTransactionServiceRepository.Delete(serviceTransaction);

            ClientTransaction clientTransaction = await _clientTransactionRepository
                .GetById(serviceTransaction.IdClientTransaction.Value);

            decimal pricesToChangeInClientTransaction = serviceTransaction.Price.Value *
                serviceTransaction.Quantity.Value;

            UpdateClientTransaction(clientTransaction,
                pricesToChangeInClientTransaction, false);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK,
                new List<string> { "Transação de serviço excluída com sucesso." });
        }

        private static void UpdateClientTransaction(ClientTransaction clientTransaction, decimal priceToChange, bool addValue)
        {
            if (addValue)
            {
                clientTransaction.TotalPrice += priceToChange;
            }
            else
            {
                clientTransaction.TotalPrice -= priceToChange;
            }
        }
    }
}
