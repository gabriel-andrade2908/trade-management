using GerenciamentoComercio_Domain.DTOs.Clients;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.UnitOfWork;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using GerenciamentoComercio_Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Services
{
    public class ClientsServices : IClientsServices
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClientsServices(IClientRepository clientRepository,
            IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<APIMessage> GetAllClientsAsync()
        {
            IEnumerable<Client> clients = await _clientRepository.GetMany();

            return new APIMessage(HttpStatusCode.OK, clients
                .Select(x => new GetClientsResponse
                {
                    Address = x.Address,
                    Email = x.Email,
                    FullName = x.FullName,
                    Id = x.Id,
                    Phone = x.Phone,
                    Cpf = x.Cpf,
                    IsActive = x.IsActive ?? false
                }));
        }

        public async Task<APIMessage> GetClientById(int id)
        {
            Client client = await _clientRepository.GetById(id);

            if (client == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Cliente não encontrado." });
            }

            return new APIMessage(HttpStatusCode.OK, new GetClientByIdResponse
            {
                Address = client.Address,
                Email = client.Email,
                FullName = client.FullName,
                Phone = client.Phone,
                Cpf = client.Cpf,
                IsActive = client.IsActive ?? false
            });
        }

        public APIMessage AddNewClient(AddNewClientRequest request, string userName)
        {
            Client checkIfExistClientEmail = _clientRepository.GetClientByEmail(request.Email);

            if (checkIfExistClientEmail != null)
            {
                return new APIMessage(HttpStatusCode
                    .BadRequest, new List<string> { "Já existe um cliente cadastrado com este e-mail." });
            }

            var newClient= new Client
            {
                Address = request.Address,
                CreationDate = DateTime.Now,
                CreationUser = userName,
                Email = request.Email,
                FullName = request.FullName,
                Phone = request.Phone,
                Cpf = request.Cpf,
                IsActive = true
            };

            _clientRepository.AddNew(newClient);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Cliente cadastrado com sucesso." });
        }

        public async Task<APIMessage> UpdateClientAsync(UpdateClientRequest request, int id)
        {
            Client client = await _clientRepository.GetById(id);

            if (client == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Cliente não encontrado." });
            }

            Client checkIfExistClientEmail = _clientRepository.GetClientByEmail(request.Email);

            if (checkIfExistClientEmail != null && checkIfExistClientEmail.Id != id)
            {
                return new APIMessage(HttpStatusCode
                    .BadRequest, new List<string> { "Já existe um cliente cadastrado com este e-mail." });
            }

            client.Address = request.Address ?? client.Address;
            client.Email = request.Email ?? client.Email;
            client.FullName = request.FullName ?? client.FullName;
            client.Phone = request.Phone ?? client.Phone;
            client.Cpf = request.Cpf ?? client.Cpf;
            client.IsActive = request.IsActive ?? client.IsActive;

            _clientRepository.Update(client);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Cliente atualizado com sucesso." });
        }

        public async Task<APIMessage> DeleteClientAsync(int id)
        {
            Client client = await _clientRepository.GetById(id);

            if (client == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Cliente não encontrado." });
            }

            _clientRepository.Delete(client);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Cliente excluído com sucesso." });
        }
    }
}
