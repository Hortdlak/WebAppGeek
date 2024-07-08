using WebAppGeek.DTO;


namespace WebAppGeek.Abstractions.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<ProductDTO> GetAllProducts();
        int AddProduct(ProductDTO productDTO);
        void DeleteProduct(int id);
        string GenerateCsv();
    }
}
