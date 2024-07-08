using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using WebAppGeek.Abstractions.Interfaces;
using WebAppGeek.Data;
using WebAppGeek.DTO;
using WebAppGeek.Models;

namespace WebAppGeek.Repository
{

    public class ProductGroupRepository
        (ProductContext context, IMapper _mapper, IMemoryCache _memoryCache) : IProductGroupRepository
    {

        public int AddProduct(ProductGroupDTO productGroupDTO)
        {
            if (context.Products.Any(b => b.Name == productGroupDTO.Name))
                throw new Exception("Есть продукт с таким именем");

            var entity = _mapper.Map<ProductGroup>(productGroupDTO);
            context.ProductGroups.Add(entity);
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

        public IEnumerable<ProductGroupDTO> GetAllProductGroups()
        {
            if (_memoryCache.TryGetValue("products", out List<ProductGroupDTO>? listDTO)) return listDTO;
            listDTO = context.Products.Select(_mapper.Map<ProductGroupDTO>).ToList();
            _memoryCache.Set("products", listDTO, TimeSpan.FromMinutes(30));
            return listDTO;
        }
        public string GenerateCsv()
        {
            var products = GetAllProductGroups();
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("name,description");

            foreach (var product in products)
            {
                csvBuilder.AppendLine($"{product.Name},{product.Description}");
            }

            return csvBuilder.ToString();
        }
    }
}
