// See https://aka.ms/new-console-template for more information
using ConsoleApp.Models;
using ConsoleApp.Persistences;
using ConsoleApp.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

Console.WriteLine("Hello, World!");

using (var db = new AppDbContex())
{
    //Relacionamento
    Console.WriteLine("Incluir Autor");




    //Crud
    await SeendDataBase.SeedProduto(db);

    await AdicionarProduto(db);

    await AdicionarProdutos(db);

    await ListarProdutos(db);

    Console.WriteLine("Delete  primeiro registro");
    var produto = db.Produtos.First();
    db.Produtos.Remove(produto);
    await ExibirEstado(db.ChangeTracker.Entries());
    db.SaveChanges();
    await ListarProdutos(db);

    Console.WriteLine("Delete id = 4");
    var produto2 = db.Produtos.Find(4);
    db.Produtos.Remove(produto2);
    await ExibirEstado(db.ChangeTracker.Entries());
    db.SaveChanges();
    await ListarProdutos(db);


    Console.WriteLine("Delete ultimo registro");
    var produto3 = db.Produtos.Last();
    db.Produtos.Remove(produto3);
    await ExibirEstado(db.ChangeTracker.Entries());
    db.SaveChanges();
    await ListarProdutos(db);


    Console.WriteLine("Update id 3");
    var produtoUpdate = db.Produtos.First(p => p.ProdutoId == 3);
    produtoUpdate.Nome = "Borracha Grande";
    produtoUpdate.Saldo = 777;
    //db.SaveChanges();
    await ListarProdutos(db);

    try
    {
        await ExibirEstado(db.ChangeTracker.Entries());
        int resultado = db.SaveChanges();
        Console.WriteLine($"registros alterados: {resultado}");
    }
    catch (DbUpdateException dbex)
    {

        Console.WriteLine("erro no SAVEcHANGE", dbex);
    }

    Console.ReadKey();
}


static async Task ListarProdutos(AppDbContex db)
{
    var produtos = db.Produtos.ToList();

    foreach (var produto in produtos)
    {
        Console.WriteLine($"{produto.ProdutoId} {produto.Nome}");
    }

    await Task.CompletedTask;
}

static async Task AdicionarProduto(AppDbContex db)
{
    var produtoNovo = new Produto();
    produtoNovo.Nome = "borracha";
    produtoNovo.Preco = 2.99m;
    produtoNovo.Saldo = 10;

    await db.Produtos.AddAsync(produtoNovo);
    db.Entry(produtoNovo).State = EntityState.Added;
    await ExibirEstado(db.ChangeTracker.Entries());
    await db.SaveChangesAsync();
}

static async Task AdicionarProdutos(AppDbContex db)
{
    var produtos = new List<Produto>()
                {
                    new Produto{Nome ="clips",Preco= 199, Saldo=10},
                    new Produto{Nome ="grampeador",Preco= 199, Saldo=10},
                };

    await db.AddRangeAsync(produtos);
    await ExibirEstado(db.ChangeTracker.Entries());
    await db.SaveChangesAsync();
}

static async Task ExibirEstado(IEnumerable<EntityEntry> entries)
{
    foreach (var entrada in entries)
    {
        Console.WriteLine($"Estada da entidade : {entrada.State.ToString()}");
    }
}