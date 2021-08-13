using System.ComponentModel.DataAnnotations;

namespace GerenciamentoComercio_Domain.DTOs.Services
{
    public class AddNewServiceRequest
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Sla { get; set; }
    }
}
