namespace ConsoleAppUmParaMuitos.Models
{
    public class Livro
    {
        public int LivroId { get; set; }

        public string Titulo { get; set; }

        public int AnoLancamento { get; set; }

        //relaacionamento com Autor 1 x N
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}
