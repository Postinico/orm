using ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Persistences
{
    public class AppDbContex : DbContext
    {
        //mapeando entidades para tabelas

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("dbCurso");


            //Exibir log do sql
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // popular tabela itilizando add-migration
            modelBuilder.Entity<Produto>().HasData(new Produto { ProdutoId = 6, Nome = "papel A4", Preco = 2.99M, Saldo = 10 });
        }
    }
}
