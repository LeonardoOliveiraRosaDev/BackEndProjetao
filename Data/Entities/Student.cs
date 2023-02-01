namespace BackEndProjetao.Data.Entities
{
    // Esta é a Entidade/model que determina o formato dos dados que serão manipulados pela aplicação
    // é uma representação de uma table db
    public class Student
    {
        // Definir as Props
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
    }
}
