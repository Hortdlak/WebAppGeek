using WebAppGeek.Models;

namespace WebAppGeek.DTO
{
    public class ProductDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public decimal? Price { get; set; }


        public int? ProductGroupID { get; set; }
    }
}
