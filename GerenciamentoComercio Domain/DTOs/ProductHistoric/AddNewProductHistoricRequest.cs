using System.ComponentModel.DataAnnotations;

namespace GerenciamentoComercio_Domain.DTOs.ProductHistoric
{
    public class AddNewProductHistoricRequest
    {
        [Required(ErrorMessage = "O campo Produto é obrigatório")]
        public int ProductId { get; set; }
        public decimal? ProductPrice { get; set; }
        public long ProductQuantity { get; set; }
    }
}
