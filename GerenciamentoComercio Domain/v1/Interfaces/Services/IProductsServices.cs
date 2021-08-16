using GerenciamentoComercio_Domain.DTOs.Products;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface IProductsServices
    {
        APIMessage GetAllProducts();
        APIMessage GetProductById(int id);
        Task<APIMessage> AddNewProductAsync(AddNewProductRequest request, string userName);
        Task<APIMessage> UpdateProductAsync(UpdateProductRequest request, int id);
        APIMessage DeleteProduct(int id);
        APIMessage GetProductByCategory(int categoryId);
    }
}
