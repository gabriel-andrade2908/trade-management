using System.ComponentModel.DataAnnotations;

namespace GerenciamentoComercio_Domain.DTOs.Products
{
    public class AddNewProductRequest
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}
