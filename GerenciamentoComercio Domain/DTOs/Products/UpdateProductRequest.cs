﻿using System.ComponentModel.DataAnnotations;

namespace GerenciamentoComercio_Domain.DTOs.Products
{
    public class UpdateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsActive { get; set; }
    }
}
