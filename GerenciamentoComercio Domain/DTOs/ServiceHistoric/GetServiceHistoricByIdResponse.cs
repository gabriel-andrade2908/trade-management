using System;

namespace GerenciamentoComercio_Domain.DTOs.ServiceHistoric
{
    public class GetServiceHistoricByIdResponse
    {
        public int ServiceId { get; set; }
        public decimal Sla { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationUser { get; set; }
    }
}
