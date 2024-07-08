using Microsoft.AspNetCore.Mvc;
using WebAppGeek.Data;
using WebAppGeek.Models;

namespace WebAppGeek.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductGroupController : ControllerBase
    {
        [HttpGet(template: "get")]
        public ActionResult<IEnumerable<ProductGroup>> GetAllProductGroups()
        {
            using (ProductContext storage = new())
            {
                var list = storage.ProductsGroup
                    .Select(pg => new ProductGroup
                    {Id = pg.Id,Name = pg.Name,Description = pg.Description,Products = pg.Products})
                    .ToList();

                return Ok(list);
            }
        }

        [HttpPost(template: "post")]
        public ActionResult<int> AddProductGroup(string name, string description)
        {
            using (ProductContext storage = new())
            {
                if (storage.ProductsGroup.Any(pg => pg.Name == name))
                    return StatusCode(409);

                var productGroup = new ProductGroup
                {Name = name,Description = description};

                storage.ProductsGroup.Add(productGroup);
                storage.SaveChanges();
                return Ok(productGroup.Id);
            }
        }

        [HttpDelete(template: "delete")]
        public ActionResult DeleteProductGroup(int id)
        {
            using (ProductContext storage = new())
            {
                var productGroup = storage.ProductsGroup.Find(id);
                if (productGroup == null)
                    return NotFound();

                storage.ProductsGroup.Remove(productGroup);
                storage.SaveChanges();
                return Ok();
            }
        }
    }
}
