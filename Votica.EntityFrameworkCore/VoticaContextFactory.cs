using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Votica.EntityFrameworkCore
{
    /// <summary>
    /// Class needed to create migrations with ef core
    /// </summary>
    public class VoticaContextFactory : IDesignTimeDbContextFactory<VoticaDbContext>
    {
        public VoticaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VoticaDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=votica_db;Username=votica;Password=votica123");

            return new VoticaDbContext(optionsBuilder.Options);
        }
    }
}