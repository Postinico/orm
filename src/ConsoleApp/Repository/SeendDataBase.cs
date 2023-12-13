using ConsoleApp.Models;
using ConsoleApp.Persistences;

namespace ConsoleApp.Repository
{
    public class SeendDataBase
    {
        public static async Task SeedProduto(AppDbContex context)
        {
            if (!context.Produtos.Any())
            {
                var produtos = new List<Produto>()
                {
                    new Produto{Nome ="caneta",Preco= 199, Saldo=10},
                    new Produto{Nome ="lapis",Preco= 199, Saldo=10},
                };

                await context.AddRangeAsync(produtos);
                await context.SaveChangesAsync();
            }
        }
    }
}
