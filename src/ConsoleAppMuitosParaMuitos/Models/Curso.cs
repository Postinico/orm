namespace ConsoleAppMuitosParaMuitos.Models
{
    public class Curso
    {
        public int CursoId { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public ICollection<AlunoCurso> AlunoCursos { get; set; }
    }
}
