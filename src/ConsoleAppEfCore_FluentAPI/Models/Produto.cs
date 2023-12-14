namespace ConsoleAppEfCore_FluentAPI.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal Preco { get; set; }
        public bool Estoque { get; set; }
    }
}
