using WebAppGeek.DTO;

namespace WebAppGeek.Abstractions.Interfaces
{
    public interface IProductGroupRepository
    {
        IEnumerable<ProductGroupDTO> GetAllProductGroups();
        int AddProduct(ProductGroupDTO productGroupDTO);
        void DeleteProduct(int id);
        string GenerateCsv();
    }
}
