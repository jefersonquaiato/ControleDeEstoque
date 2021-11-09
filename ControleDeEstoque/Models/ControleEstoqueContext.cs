using Microsoft.EntityFrameworkCore;

namespace ControleDeEstoque.Models
{
    public class ControleEstoqueContext : DbContext
    {
        public ControleEstoqueContext(DbContextOptions<ControleEstoqueContext> options) : base(options) {  }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ControleEstoque;Trusted_Connection=true;");
        }

        public DbSet<Produto> Produtos { get; set; }
    }
}