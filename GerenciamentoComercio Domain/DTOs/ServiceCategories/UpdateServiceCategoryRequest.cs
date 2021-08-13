namespace GerenciamentoComercio_Domain.DTOs.ServiceCategories
{
    public class UpdateServiceCategoryRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
