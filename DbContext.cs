using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace WpfApp1
{
    public class KotyaDbContext : DbContext
    {
        public KotyaDbContext(DbContextOptions<KotyaDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost; port=5432 ; user id =postgres ; password =kotya; database = ft_bd_1; ");
            }
        }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
