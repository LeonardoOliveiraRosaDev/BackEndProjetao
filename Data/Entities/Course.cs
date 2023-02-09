namespace BackEndProjetao.Data.Entities
{
    // Esta é a Entidade/model que determina o formato dos dados que serão manipulados pela aplicação
    // é uma representação de uma table Course
    public class Course
    {
        // Definir as Props
        public int Id { get; set; }
        public string NomeCurso { get; set; }
        public float Mensalidade { get; set; }
    }
}
