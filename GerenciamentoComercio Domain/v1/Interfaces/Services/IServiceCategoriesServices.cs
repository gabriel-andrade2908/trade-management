using GerenciamentoComercio_Domain.DTOs.ServiceCategories;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface IServiceCategoriesServices
    {
        Task<APIMessage> GetAllServiceCategoriesAsync();
        Task<APIMessage> GetServiceCategoryByIdAsync(int id);
        APIMessage AddNewServiceCategoryAsync(AddNewServiceCategoryRequest request, string userName);
        Task<APIMessage> UpdateServiceCategoryAsync(UpdateServiceCategoryRequest request, int id);
        Task<APIMessage> DeleteServiceCategoryAsync(int id);
    }
}
