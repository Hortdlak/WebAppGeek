using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using System.Text;
using WebAppGeek.Abstractions.Interfaces;
using WebAppGeek.Data;
using WebAppGeek.DTO;
using WebAppGeek.Models;

namespace WebAppGeek.Repository
{
    public class ProductRepository
        (ProductContext context, IMapper _mapper, IMemoryCache _memoryCache) : IProductRepository
    {
        public int AddProduct(ProductDTO productDTO)
        {
            if (context.Products.Any(b => b.Name == productDTO.Name))
                throw new Exception("Есть продукт с таким именем");

            var entity = _mapper.Map<Product>(productDTO);
            context.Products.Add(entity);
            context.SaveChanges();

            _memoryCache.Remove("products");

            return entity.Id;
        }

        public void DeleteProduct(int id)
        {
            var product = context.Products.Find(id) ??
                throw new Exception("Не найдено");

            context.Products.Remove(product);
            context.SaveChanges();
        }
        
        public IEnumerable<ProductDTO> GetAllProducts()
        {
            if (_memoryCache.TryGetValue("products", out List<ProductDTO>? listDTO)) return listDTO;
            listDTO = context.Products.Select(_mapper.Map<ProductDTO>).ToList();
            _memoryCache.Set("products", listDTO, TimeSpan.FromMinutes(30));
            return listDTO;
        }

        public string GenerateCsv()
        {
            var products = GetAllProducts();
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("name,description,price");

            foreach (var product in products)
            {
                csvBuilder.AppendLine($"{product.Name},{product.Description},{product.Price}");
            }

            return csvBuilder.ToString();
        }
    }
}
