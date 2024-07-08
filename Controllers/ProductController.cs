using Microsoft.AspNetCore.Mvc;
using WebAppGeek.Data;
using WebAppGeek.Models;

namespace WebAppGeek.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet(template: "get")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            using (ProductContext storage = new())
            {
                var list = storage.Products.Select(b => new Product()
                { Name = b.Name, Description = b.Description, Price = b.Price }).ToList();
                return Ok(list);
            };
        }

        [HttpPost (template: "post")]
        public ActionResult<int> AddProduct(string name, string discretional, decimal price) 
        {
            using (ProductContext storage = new())
            {
                if (storage.Products.Any(b => b.Name == name))
                    return StatusCode(409);
                var product = new Product() { Name = name,Description = discretional,Price = price};
                storage.Products.Add(product);
                storage.SaveChanges();
                return Ok(product.Id);
            };  
        }
        
        [HttpDelete(template: "delete")]
        public ActionResult DeleteProduct(int id)
        {
            using (ProductContext storage = new())
            {
                var product = storage.Products.Find(id);
                if (product == null)
                    return NotFound();

                storage.Products.Remove(product);
                storage.SaveChanges();
                return Ok();
            }
        }
    }
}
