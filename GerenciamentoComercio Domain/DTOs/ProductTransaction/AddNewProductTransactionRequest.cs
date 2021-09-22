using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoComercio_Domain.DTOs.ProductTransaction
{
    public class AddNewProductServiceTransactionRequest
    {
        [Required(ErrorMessage = "O campo produto é obrigatório.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo quantidade é obrigatório.")]
        public int Quantity { get; set; }
    }
}
