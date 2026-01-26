using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MusicLearningLibrary.Infrastructure.DbContexts
{
    public class MusicLearningLibraryDbContextFactory : IDesignTimeDbContextFactory<MusicLearningLibraryDbContext>
    {
        public MusicLearningLibraryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MusicLearningLibraryDbContext>();

            // Connection string USED ONLY for dotnet ef commands
            var connectionString =
                "Server=localhost,1433;Database=MusicLearningLibraryDb;User Id=sa;Password=12345678aA!;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);

            return new MusicLearningLibraryDbContext(optionsBuilder.Options);
        }

    }
}
