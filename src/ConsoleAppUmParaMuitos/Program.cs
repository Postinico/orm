// See https://aka.ms/new-console-template for more information
using ConsoleAppUmParaMuitos.Models;
using ConsoleAppUmParaMuitos.Persistences;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

using (var db = new AppDbContex())
{
    await IncluirAutor(db);

    await IncluirAutorLivro(db);

    await IncluirAutorLivroAddRange(db);

    await IncluirLivroAutor(db);

    await IncluirLivroAutorId(db, 1);

    await ExibirAutores(db);

    await ExibirAutoresLivros(db);

    await ExibirLivrosAutores(db, "Carol");

    await ExibirLivrosAutoresExplicitLoad(db, "Guilherme");
    await ExibirLivrosAutoresQueryExplicitLoad(db, "Guilherme");
    await ExibirLivrosAutoresQueryCountExplicitLoad(db, "Maria");

    await ExibirLivrosExplicitLoad(db, 1996);
}



async Task IncluirAutorLivro(AppDbContex db)
{
    var autor = new Autor
    {
        Nome = "Maria",
        Sobrenome = "Teste"
    };

    await db.Autores.AddAsync(autor);
    await db.SaveChangesAsync();

    var autorGuilherme = new Autor
    {
        Nome = "Guilherme",
        Sobrenome = "Postinico",
        Livros = new List<Livro>
        {
            new Livro{Titulo = "ASP, 1996", AnoLancamento =1996},
            new Livro{Titulo = "ASP, 2000", AnoLancamento =200},
            new Livro{Titulo = "ASP 1996-2", AnoLancamento =1996}
        }
    };

    await db.Autores.AddAsync(autorGuilherme);
    await db.SaveChangesAsync();
}

async Task IncluirAutorLivroAddRange(AppDbContex db)
{
    var autor = new Autor
    {
        Nome = "Carol",
        Sobrenome = "Postinico"
    };

    var livros = new List<Livro>
        {
            new Livro{Titulo = "ASP, ADO e banco de dados na web", AnoLancamento =2000, Autor = autor},
            new Livro{Titulo = "ASP", AnoLancamento =2000, Autor = autor},
            new Livro{Titulo = "Marketing", AnoLancamento =2000, Autor = autor}
        };

    await db.AddRangeAsync(livros);
    await db.SaveChangesAsync();
}

async Task IncluirLivroAutor(AppDbContex db)
{
    var autor = db.Autores.Find(1);

    var livro = new Livro
    {
        Titulo = "ASP, ADO e banco de dados na web",
        AnoLancamento = 1996,
        Autor = autor
    };

    await db.AddRangeAsync(livro);
    await db.SaveChangesAsync();
}

async Task IncluirLivroAutorId(AppDbContex db, int autorId)
{
    var autor = db.Autores.Find(1);

    var livro = new Livro
    {
        Titulo = "ASP - 2, ADO e banco de dados na web",
        AnoLancamento = 2000,
        AutorId = autorId
    };

    await db.AddRangeAsync(livro);
    await db.SaveChangesAsync();
}

async Task IncluirAutor(AppDbContex db)
{
    var autor = new Autor { Nome = "Guilherme - 1", Sobrenome = "Postinico" };

    await db.Autores.AddAsync(autor);
    await db.SaveChangesAsync();
}

async Task ExibirAutores(AppDbContex db)
{
    foreach (var autor in db.Autores)
    {
        Console.WriteLine($"Nome: {autor.Nome}, Sobrenome:{autor.Sobrenome}");
    }

    await Task.CompletedTask;
}

async Task ExibirAutoresLivros(AppDbContex db)
{
    foreach (var autor in db.Autores.AsNoTracking().Include("Livros"))
    {
        Console.WriteLine($"Nome: {autor.Nome}, Sobrenome:{autor.Sobrenome}");

        foreach (var livro in autor.Livros)
        {
            Console.WriteLine($"\t Titulo: {livro.Titulo}");
        }
    }

    await Task.CompletedTask;
}

async Task ExibirLivrosAutores(AppDbContex db, string nome)
{
    var resultado = await db.Autores.Where(a => a.Nome == nome)
         .Select(a => new { Autor = a, LivrosAutor = a.Livros })
         .FirstOrDefaultAsync();

    await Task.CompletedTask;
}

async Task ExibirLivrosAutoresExplicitLoad(AppDbContex db, string nome)
{
    var resultado = await db.Autores.Where(a => a.Nome == nome).FirstOrDefaultAsync();
    Console.WriteLine(resultado.Nome);

    await db.Entry(resultado).Collection(l => l.Livros).LoadAsync();

    foreach (var livro in resultado.Livros)
    {
        Console.WriteLine($"\t {livro.Titulo}");
    }
    await Task.CompletedTask;
}

async Task ExibirLivrosAutoresQueryExplicitLoad(AppDbContex db, string nome)
{
    var resultado = await db.Autores.Where(a => a.Nome == nome).FirstOrDefaultAsync();
    Console.WriteLine(resultado.Nome);

    await db.Entry(resultado).Collection(l => l.Livros)
        .Query().Where(lv => lv.AnoLancamento >= 1996).LoadAsync();

    foreach (var livro in resultado.Livros)
    {
        Console.WriteLine($"\t {livro.Titulo}");
    }
    await Task.CompletedTask;
}

async Task ExibirLivrosAutoresQueryCountExplicitLoad(AppDbContex db, string nome)
{
    var autor = await db.Autores.Where(a => a.Nome == nome).FirstOrDefaultAsync();
    Console.WriteLine(autor.Nome);

    var qtd = db.Entry(autor).Collection(l => l.Livros)
         .Query().Count();

    db.Entry(autor).Collection(l => l.Livros)
       .Query().Where(lv => lv.AnoLancamento <= 1996).Load();

    Console.WriteLine($"\t Autor {autor.Nome}  quantidade de livros: {qtd}");

    foreach (var livro in autor.Livros)
    {
        Console.WriteLine($"\t  l{livro.Titulo}");
    }
    await Task.CompletedTask;
}

async Task ExibirLivrosExplicitLoad(AppDbContex db, int ano)
{
    var resultado = await db.Livros.Where(a => a.AnoLancamento == ano).FirstOrDefaultAsync();
    Console.WriteLine(resultado.Titulo);

    await db.Entry(resultado).Reference(l => l.Autor).LoadAsync();
    Console.WriteLine($"\t {resultado.Autor.Nome} {resultado.Autor.Sobrenome}");

    await Task.CompletedTask;
}
