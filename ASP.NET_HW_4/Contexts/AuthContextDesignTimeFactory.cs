using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace AppAuthLesson4.Contexts
{
    public class AuthContextDesignTimeFactory : IDesignTimeDbContextFactory<AuthContext>
    {
        public AuthContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthContext>();

            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AuthDB;Trusted_Connection=True");

            
            return new AuthContext(optionsBuilder.Options);
        }
    }
}
