using GerenciamentoComercio_Domain.DTOs.Services;
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
    public class ServicesServices : IServicesServices
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceCategoryRepository _serviceCategoriesRepository;
        private readonly IServiceHistoricRepository _serviceHistoricRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ServicesServices(IUnitOfWork unitOfWork,
            IServiceRepository serviceRepository,
            IServiceCategoryRepository serviceCategoriesRepository,
            IServiceHistoricRepository serviceHistoricRepository)
        {
            _unitOfWork = unitOfWork;
            _serviceRepository = serviceRepository;
            _serviceCategoriesRepository = serviceCategoriesRepository;
            _serviceHistoricRepository = serviceHistoricRepository;
        }

        public APIMessage GetAllServices()
        {
            IEnumerable<Service> services = _serviceRepository.GetMany();

            return new APIMessage(HttpStatusCode.OK, services
                .Select(x => new GetAllServicesResponse
                {
                    Name = x.Name,
                    CategoryId = x.IdServiceCategory ?? 0,
                    Description = x.Description,
                    CategoryName = x.IdServiceCategoryNavigation == null ? null : x.IdServiceCategoryNavigation.Title,
                    Id = x.Id,
                    IsActive = x.IsActive ?? false,
                    Sla = x.ServiceHistoric.FirstOrDefault(p => p.IdService == x.Id) == null ? 0 : x.ServiceHistoric.FirstOrDefault(p => p.IdService == x.Id).Sla ?? 0,
                    Price = x.ServiceHistoric.FirstOrDefault(p => p.IdService == x.Id) == null ? 0 : x.ServiceHistoric.FirstOrDefault(p => p.IdService == x.Id).Price ?? 0,
                }));
        }

        public APIMessage GetServiceById(int id)
        {
            Service service = _serviceRepository.GetById(id);

            if (service == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Serviço não encontrado." });
            }

            return new APIMessage(HttpStatusCode.OK, new GetServiceByIdResponse
            {
                Name = service.Name,
                CategoryId = service.IdServiceCategory ?? 0,
                Description = service.Description,
                CategoryName = service.IdServiceCategoryNavigation == null ? null : service.IdServiceCategoryNavigation.Title,
                IsActive = service.IsActive ?? false,
                Sla = service.ServiceHistoric.FirstOrDefault(p => p.IdService == service.Id) == null ? 0 : service.ServiceHistoric.FirstOrDefault(p => p.IdService == service.Id).Sla ?? 0,
                Price = service.ServiceHistoric.FirstOrDefault(p => p.IdService == service.Id) == null ? 0 : service.ServiceHistoric.FirstOrDefault(p => p.IdService == service.Id).Price ?? 0,
            });
        }

        public APIMessage GetServicesByCategory(int categoryId)
        {
            IEnumerable<Service> services = _serviceRepository.GetServiceByCategory(categoryId);

            return new APIMessage(HttpStatusCode.OK, services
                .Select(x => new GetAllServicesResponse
                {
                    Name = x.Name,
                    CategoryId = x.IdServiceCategory ?? 0,
                    Description = x.Description,
                    CategoryName = x.IdServiceCategoryNavigation == null ? null : x.IdServiceCategoryNavigation.Title,
                    Id = x.Id,
                    IsActive = x.IsActive ?? false,
                    Sla = x.ServiceHistoric.FirstOrDefault(p => p.IdService == x.Id) == null ? 0 : x.ServiceHistoric.FirstOrDefault(p => p.IdService == x.Id).Sla ?? 0,
                    Price = x.ServiceHistoric.FirstOrDefault(p => p.IdService == x.Id) == null ? 0 : x.ServiceHistoric.FirstOrDefault(p => p.IdService == x.Id).Price ?? 0,
                }));
        }

        public async Task<APIMessage> AddNewServiceAsync(AddNewServiceRequest request, string userName)
        {
            ServiceCategory category = await _serviceCategoriesRepository
                .GetById(request.CategoryId ?? 0);

            if (category == null && request.CategoryId != null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Categoria de serviço não encontrada." });
            }

            var newService = new Service
            {
                Description = request.Description,
                IsActive = true,
                Name = request.Name,
                IdServiceCategory = request.CategoryId,
                CreationDate = DateTime.Now,
                CreationUser = userName
            };

            _serviceRepository.AddNew(newService);

            AddServiceHistoric(userName, request.Sla, request.Price);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Serviço cadastrado com sucesso." });
        }

        public async Task<APIMessage> UpdateServiceAsync(UpdateServiceRequest request, int id, string userName)
        {
            ServiceCategory category = await _serviceCategoriesRepository
                .GetById(request.CategoryId ?? 0);

            if (category == null && request.CategoryId != null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Categoria de serviço não encontrada." });
            }

            Service service = _serviceRepository.GetById(id);

            if (service == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Serviço não encontrado." });
            }

            service.Name = request.Name ?? service.Name;
            service.Description = request.Description ?? service.Description;
            service.IdServiceCategory = request.CategoryId ?? service.IdServiceCategory;
            service.IsActive = request.IsActive ?? service.IsActive;

            _serviceRepository.Update(service);

            AddServiceHistoric(userName, request.Sla, request.Price);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Serviço atualizado com sucesso." });
        }

        public APIMessage DeleteService(int id)
        {
            Service service = _serviceRepository.GetById(id);

            if (service == null)
            {
                return new APIMessage(HttpStatusCode.NotFound,
                    new List<string> { "Serviço não encontrado." });
            }

            _serviceRepository.Delete(service);

            _unitOfWork.Commit();

            return new APIMessage(HttpStatusCode.OK, new List<string> { "Serviço excluído com sucesso." });
        }

        private void AddServiceHistoric(string userName, int? sla, decimal? price)
        {
            var newHistoric = new ServiceHistoric
            {
                CreationDate = DateTime.Now,
                CreationUser = userName,
                Price = price,
                Sla = sla
            };

            _serviceHistoricRepository.AddNew(newHistoric);
        }
    }
}
