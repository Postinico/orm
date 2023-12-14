using ConsoleAppUmParaMuitos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppUmParaMuitos.Persistences
{
    public class AppDbContex : DbContext
    {
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            object value = optionsBuilder.UseInMemoryDatabase("dbCurso");


            //Exibir log do sql
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
