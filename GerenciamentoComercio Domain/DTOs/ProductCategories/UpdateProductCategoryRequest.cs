using System.ComponentModel.DataAnnotations;

namespace GerenciamentoComercio_Domain.DTOs.ProductCategories
{
    public class UpdateProductCategoryRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
