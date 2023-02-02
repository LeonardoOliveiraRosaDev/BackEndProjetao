using Microsoft.EntityFrameworkCore;
namespace BackEndProjetao.Data
{
    // 1º passo: Praticar o mecanismo de Herança com a super classe DbContext
    public class MyDbContext : DbContext
    {
        // 2º passo: definir o contrutor da classe para que seja praticado a injeção de dependencia com a superclasse DbContextOption
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        // 3º passo: Fazer a referência necessária para que a table Student - representada pela Entity Student
        public DbSet<Entities.Student> Student
        {
            get; set;
        }
    }
}
