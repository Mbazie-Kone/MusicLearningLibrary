using Microsoft.EntityFrameworkCore;
using MusicLearningLibrary.Domain.Entities;

namespace MusicLearningLibrary.Infrastructure.DbContexts
{
    public class MusicLearningLibraryDbContext : DbContext
    {
        public MusicLearningLibraryDbContext(DbContextOptions<MusicLearningLibraryDbContext> options) : base(options)
        {
        }

        // “This property represents the MediaItems table.”
        public DbSet<MediaItem> MediaItems { get; set; }

        // Override for additional configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional Fluent API configurations
            modelBuilder.Entity<MediaItem>(entity =>
            {
                entity.HasKey(e => e.Id); // Primary key
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.FileType)
                    .HasMaxLength(50);
                entity.Property(e => e.FileSize);
                entity.Property(e => e.UploadedAt)
                    .HasDefaultValueSql("GETDATE()"); // Default value
            });

            modelBuilder.Entity<MediaAnalysis>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Status)
                    .HasConversion<int>()
                    .IsRequired();

                entity.Property(x => x.RequestedAt).IsRequired();
                entity.Property(x => x.CompletedAt);

                entity.Property(x => x.Error)
                    .HasMaxLength(1024);

                entity.Property(x => x.MediaId).IsRequired();
            });
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<EmailConfirmationToken> EmailConfirmationTokens => Set<EmailConfirmationToken>();

        public DbSet<MediaAnalysis> MediaAnalyses => Set<MediaAnalysis>();
    }
}
