using Microsoft.EntityFrameworkCore;
using WebAppGeek.Models;

namespace WebAppGeek.Data
{
    public class ProductContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGroup> ProductsGroup { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer("Data Source= .\\SQLEXPRESS; Initial Catalog = Products;" +
            "Trusted_Connection=True; TrustServerCertificate=True")
            .UseLazyLoadingProxies().LogTo(Console.WriteLine);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.HasKey(pg => pg.Id)
                .HasName("product_group_pk");

                entity.ToTable("Category");

                entity.Property(pg => pg.Name)
                .HasColumnName("name")
                .HasMaxLength(255);
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id)
                .HasName("product_pk");   

                entity.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(255);

                entity.HasOne(p =>p.ProductGroup)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.ProductGroupID);
            });
            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(s => s.Id)
                .HasName("storage_pk");


                entity.HasOne(s => s.ProductName)
                .WithMany(s => s.Storages).HasForeignKey(s => s.ProductId);
            });
        }
    }
}
