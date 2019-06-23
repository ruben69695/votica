using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Votica.EntityFrameworkCore;

namespace Votica.Data.IntegrationTests
{
    [SetUpFixture]
    public class PrepareTestingLab
    {
        public static VoticaDbContext TestContext { get; set; }

        [OneTimeSetUp]
        public void GlobalTestsSetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VoticaDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=votica_test_db;Username=rubenarrebola;Password=ruben123");

            // Create the context
            TestContext = new VoticaDbContext(optionsBuilder.Options);

            // Create the database structure or update it if exists
            TestContext.Database.Migrate();
        }

        [OneTimeTearDown]
        public void GlobalTestsTearDown()
        {
            // Delete database
            TestContext.Database.EnsureDeleted();
        }
    }
}
