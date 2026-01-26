using MediaProcessor;
using Microsoft.EntityFrameworkCore;
using MusicLearningLibrary.Infrastructure.DbContexts;

var builder = Host.CreateApplicationBuilder(args);

// =========================
// Database (EF Core)
// =========================
builder.Services.AddDbContext<MusicLearningLibraryDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sql =>
    {
        sql.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorNumbersToAdd: null);
    });
});

// =========================
// Background Worker
// =========================
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
