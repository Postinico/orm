namespace ConsoleAppMuitosParaMuitos.Models
{
    public class AlunoCurso
    {
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}
