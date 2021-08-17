using GerenciamentoComercio_Domain.DTOs.ServiceHistoric;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.v1.Interfaces.Repositories;
using GerenciamentoComercio_Infra.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Services
{
    public class ServicesHistoricServices : IServicesHistoricServices
    {
        private readonly IServiceHistoricRepository _serviceHistoricRepository;

        public ServicesHistoricServices(IServiceHistoricRepository serviceHistoricRepository)
        {
            _serviceHistoricRepository = serviceHistoricRepository;
        }

        public async Task<APIMessage> GetAllServicesHistoricAsync()
        {
            IEnumerable<ServiceHistoric> historics = await _serviceHistoricRepository.GetMany();

            return new APIMessage(HttpStatusCode.OK, historics
                .Select(x => new GetAllServicesHistoricResponse
                {
                    Id = x.Id,
                    CreationDate = x.CreationDate.Value,
                    CreationUser = x.CreationUser,
                    ServiceId = x.IdService ?? 0,
                    Price = x.Price ?? 0,
                    Sla = x.Sla ?? 0
                }));
        }

        public async Task<APIMessage> GetServiceHistoricByIdAsync(int id)
        {
            ServiceHistoric historic = await _serviceHistoricRepository.GetById(id);

            if (historic == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Histórico não encontrada." });
            }

            return new APIMessage(HttpStatusCode.OK, new GetServiceHistoricByIdResponse
            {
                CreationDate = historic.CreationDate.Value,
                CreationUser = historic.CreationUser,
                ServiceId = historic.IdService ?? 0,
                Price = historic.Price ?? 0,
                Sla = historic.Sla ?? 0,
            });
        }

        public APIMessage GetHistoricByServiceAsync(int serviceId)
        {
            List<ServiceHistoric> serviceHistoric = _serviceHistoricRepository
                .GetHistoricByServiceId(serviceId);

            return new APIMessage(HttpStatusCode.OK, serviceHistoric
                .Select(x => new GetServicesHistoricByServiceResponse
                {
                    CreationDate = x.CreationDate.Value,
                    CreationUser = x.CreationUser,
                    Price = x.Price,
                    Sla = x.Sla ?? 0,
                    Id = x.Id,
                }));
        }
    }
}
