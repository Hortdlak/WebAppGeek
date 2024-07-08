using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebAppGeek.Abstractions.Interfaces;
using WebAppGeek.DTO;
using WebAppGeek.Models;

namespace WebAppGeek.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController()
        {
            
        }
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet(template: "get")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                var product = _productRepository.GetAllProducts();
                return Ok(product);
            }
            catch (Exception)
            {

                return StatusCode(404);
            }

            /* Реализация
               using (ProductContext storage = new())
            {
                var list = storage.Products.Select(b => new Product()
                { Name = b.Name, Description = b.Description, Price = b.Price }).ToList();
                return Ok(list);
            };
            */
        }

        [HttpPost (template: "post")]
        public ActionResult<int> AddProduct(ProductDTO productDTO) 
        {

            try
            {
                var product = _productRepository.AddProduct(productDTO);
                return Ok(product);
            }
            catch (Exception)
            {

                return StatusCode(404);
            }

            /* Реализация
            using (ProductContext storage = new())
            {
                if (storage.Products.Any(b => b.Name == name))
                    return StatusCode(409);
                var product = new Product() { Name = name, Description = discretional, Price = price };
                storage.Products.Add(product);
                storage.SaveChanges();
                return Ok(product.Id);
            };
            */
        }

        [HttpDelete(template: "delete")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                _productRepository.DeleteProduct(id);
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(404);
            }

            /* Реализация
            using (ProductContext storage = new())
            {
                var product = storage.Products.Find(id);
                if (product == null)
                    return NotFound();

                storage.Products.Remove(product);
                storage.SaveChanges();
                return Ok();

            }
             */
        }
        [HttpGet(template: "csv")]
        public IActionResult GetProductsCsv()
        {
            try
            {
                var csv = _productRepository.GenerateCsv();
                var bytes = Encoding.UTF8.GetBytes(csv);
                var result = new FileContentResult(bytes, "text/csv")
                {
                    FileDownloadName = "products.csv"
                };

                return result;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
