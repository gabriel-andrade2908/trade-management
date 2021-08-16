using GerenciamentoComercio_Domain.DTOs.ProductHistoric;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using System.Threading.Tasks;

namespace GerenciamentoComercio_Domain.v1.Interfaces.Services
{
    public interface IProductsHistoricServices
    {
        Task<APIMessage> GetAllProductsHistoricAsync();
        Task<APIMessage> GetProductHistoricByIdAsync(int id);
        APIMessage GetHistoricByProductAsync(int productId);
        APIMessage AddNewProductHistoricAsync(AddNewProductHistoricRequest request, string userName);
        Task<APIMessage> UpdateProductHistoricAsync(UpdateProductHistoricRequest request, int id);
        Task<APIMessage> DeleteProductHistoricAsync(int id);
    }
}
