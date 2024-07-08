namespace WebAppGeek.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }

        
        public int? ProductGroupID { get; set; }
        public virtual ProductGroup? ProductGroup { get; set; }
        public List<Storage>? Storages { get; set; }
    }
}
