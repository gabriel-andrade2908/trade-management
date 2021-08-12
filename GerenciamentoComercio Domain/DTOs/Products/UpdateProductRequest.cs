using System.ComponentModel.DataAnnotations;

namespace GerenciamentoComercio_Domain.DTOs.Products
{
    public class UpdateProductRequest
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsActive { get; set; }
    }
}
