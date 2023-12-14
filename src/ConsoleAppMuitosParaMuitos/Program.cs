// See https://aka.ms/new-console-template for more information
using ConsoleAppMuitosParaMuitos.Models;
using ConsoleAppMuitosParaMuitos.Persistences;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

using (var db = new AppDbContex())
{
    await CarregarDb(db);

    await ExibirCuros(db);

    var aluno = db.Alunos.First();

    await ExibirCurosAluno(db, aluno);
}
Console.ReadKey();

async Task ExibirCuros(AppDbContex db)
{
    var alunos = db.Alunos.Include(a => a.AlunoCursos)
                          .ThenInclude(c => c.Curso).ToList();

    foreach (var aluno in alunos)
    {
        Console.WriteLine($"Aluno: {aluno.Nome}");

        foreach (var curso in aluno.AlunoCursos.Select(e => e.Curso))
        {
            Console.WriteLine($"\t Curso {curso.Nome}");
        }
    }
}

async Task ExibirCurosAluno(AppDbContex db, Aluno aluno)
{
    var alunoCursos = db.AlunoCursos.Where(a => a.AlunoId == aluno.AlunoId)
                                   .Include(c => c.Curso);

    Console.WriteLine($"Aluno Filtrado {aluno.Nome}");

    foreach (var curso in alunoCursos)
    {
        Console.WriteLine($"\t Curso {curso.Curso.Nome}");
    }
}

static async Task CarregarDb(AppDbContex db)
{
    var cursos = new[]
    {
        new Curso{Nome = "Visual C#", Descricao = "Curso de C#"},
        new Curso{Nome = "Docker", Descricao = "Curso de Docker"},
        new Curso{Nome = "ASP .NET Core", Descricao = "Curso de EF Core"},
        new Curso{Nome = "Angular", Descricao = "Curso de Angular"}
    };

    var Alunos = new[]
    {
        new Aluno{Nome = "Guilherme"},
        new Aluno{Nome = "Carol"},
        new Aluno{Nome = "Bruno"},
        new Aluno{Nome = "João"}
    };

    await db.AlunoCursos.AddRangeAsync(
         new AlunoCurso { Curso = cursos[0], Aluno = Alunos[0] },
         new AlunoCurso { Curso = cursos[3], Aluno = Alunos[0] },
         new AlunoCurso { Curso = cursos[1], Aluno = Alunos[0] },
         new AlunoCurso { Curso = cursos[0], Aluno = Alunos[1] },
         new AlunoCurso { Curso = cursos[1], Aluno = Alunos[2] },
         new AlunoCurso { Curso = cursos[1], Aluno = Alunos[3] },
         new AlunoCurso { Curso = cursos[2], Aluno = Alunos[0] },
         new AlunoCurso { Curso = cursos[2], Aluno = Alunos[1] },
         new AlunoCurso { Curso = cursos[3], Aluno = Alunos[2] },
         new AlunoCurso { Curso = cursos[3], Aluno = Alunos[3] }
         );

    await db.SaveChangesAsync();
}