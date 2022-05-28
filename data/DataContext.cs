using Microsoft.EntityFrameworkCore;
using Product;
using BuyItem;
using OrderItem;
namespace Product.Data
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<ProductItems>? ProductoItem { get; set; }
        public DbSet<BuyItems>? BuyItem { get; set; }
        public DbSet<OrderItems>? PedidoItem { get; set; }
    }
}