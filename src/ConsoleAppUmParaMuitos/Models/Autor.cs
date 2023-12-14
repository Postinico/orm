namespace ConsoleAppUmParaMuitos.Models
{
    public class Autor
    {
        public int AutorId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        //Relacionamento com livros
        public ICollection<Livro> Livros { get; set; }
    }
}
