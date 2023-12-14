using ConsoleAppMuitosParaMuitos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppMuitosParaMuitos.Persistences
{
    public class AppDbContex : DbContext
    {
        //mapeando entidades para tabelas

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<AlunoCurso> AlunoCursos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("dbCurso");


            //Exibir log do sql
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Difinindo chave estrangeira
            modelBuilder.Entity<AlunoCurso>()
                .HasKey(ac => new { ac.AlunoId, ac.CursoId });
        }
    }
}
