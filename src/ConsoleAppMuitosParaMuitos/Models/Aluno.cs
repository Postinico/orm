namespace ConsoleAppMuitosParaMuitos.Models
{
    public class Aluno
    {
        public int AlunoId { get; set; }

        public string Nome { get; set; }

        public ICollection<AlunoCurso> AlunoCursos { get; set; }
    }
}
