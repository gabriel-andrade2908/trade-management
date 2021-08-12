using GerenciamentoComercio_Domain.DTOs.ProductCategories;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface IProductCategoriesServices
    {
        Task<APIMessage> GetAllProductCategoriesAsync();
        Task<APIMessage> GetProductCategoryByIdAsync(int id);
        APIMessage AddNewProductCategoryAsync(AddNewProductCategoryRequest request, string userName);
        Task<APIMessage> UpdateProductCategoryAsync(UpdateProductCategoryRequest request, int id);
        Task<APIMessage> DeleteProductCategoryAsync(int id);
    }
}
