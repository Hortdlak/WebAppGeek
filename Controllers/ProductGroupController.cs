using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebAppGeek.Abstractions.Interfaces;
using WebAppGeek.DTO;
using WebAppGeek.Models;
using WebAppGeek.Repository;

namespace WebAppGeek.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductGroupController : ControllerBase
    {
        private readonly IProductGroupRepository _productGroupRepository;
        public ProductGroupController(IProductGroupRepository productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;
        }
        public ProductGroupController()
        {
            
        }

        [HttpGet(template: "get")]
        public ActionResult<IEnumerable<ProductGroup>> GetAllProductGroups()
        {

            try
            {
                var product = _productGroupRepository.GetAllProductGroups();
                return Ok(product);
            }
            catch (Exception)
            {

                return StatusCode(404);
            }

            /*
            using (ProductContext storage = new())
            {
                var list = storage.ProductGroups
                    .Select(pg => new ProductGroup
                    {Id = pg.Id,Name = pg.Name,Description = pg.Description,Products = pg.Products})
                    .ToList();

                return Ok(list);
            }
            */
        }

        [HttpPost(template: "post")]
        public ActionResult<int> AddProductGroup(ProductGroupDTO productGroupDTO)
        {

            try
            {
                var product = _productGroupRepository.AddProduct(productGroupDTO);
                return Ok(product);
            }
            catch (Exception)
            {

                return StatusCode(404);
            }

            /*
            using (ProductContext storage = new())
            {
                /*
                if (storage.ProductGroups.Any(pg => pg.Name == name))
                    return StatusCode(409);

                var productGroup = new ProductGroup
                {Name = name,Description = description};

                storage.ProductGroups.Add(productGroup);
                storage.SaveChanges();
                return Ok(productGroup.Id);
                
            }
            */
        }


        [HttpDelete(template: "delete")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                _productGroupRepository.DeleteProduct(id);
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
                var csv = _productGroupRepository.GenerateCsv();
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
