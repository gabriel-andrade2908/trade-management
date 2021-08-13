namespace GerenciamentoComercio_Domain.DTOs.ServiceCategories
{
    public class GetAllServiceCategoriesResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
