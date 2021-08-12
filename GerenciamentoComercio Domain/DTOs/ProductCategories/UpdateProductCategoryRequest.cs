using System.ComponentModel.DataAnnotations;

namespace GerenciamentoComercio_Domain.DTOs.ProductCategories
{
    public class UpdateProductCategoryRequest
    {
        [Required(ErrorMessage = "O campo Título é obrigatório")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        public string Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
