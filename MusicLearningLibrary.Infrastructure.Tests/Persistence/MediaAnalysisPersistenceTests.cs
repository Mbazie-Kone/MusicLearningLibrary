using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MusicLearningLibrary.Domain.Entities;
using MusicLearningLibrary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLearningLibrary.Infrastructure.Tests.Persistence
{
    public class MediaAnalysisPersistenceTests
    {
        [Fact]
        public void Can_persist_and_load_media_analysis()
        {
            using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<MusicLearningLibraryDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new MusicLearningLibraryDbContext(options))
            {
                context.Database.EnsureCreated();

                var mediaId = Guid.NewGuid();
                var analysis = MediaAnalysis.Create(mediaId);

                context.Set<MediaAnalysis>().Add(analysis);
                context.SaveChanges();
            }

            using (var context = new MusicLearningLibraryDbContext(options))
            {
                var saved = context.Set<MediaAnalysis>().Single();

                Assert.Equal(MediaAnalysisStatus.Pending, saved.Status);
                Assert.NotEqual(Guid.Empty, saved.Id);
                Assert.NotEqual(Guid.Empty, saved.MediaId);

            }
        }

        [Fact]
        public void can_query_pending_analyses()
        {
            using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<MusicLearningLibraryDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new MusicLearningLibraryDbContext(options))
            {
                context.Database.EnsureCreated();

                var a1 = MediaAnalysis.Create(Guid.NewGuid()); // Pending
                var a2 = MediaAnalysis.Create(Guid.NewGuid());
                a2.MarkProcessing(); // Processing

                context.Set<MediaAnalysis>().AddRange(a1, a2);
                context.SaveChanges();
            }

            using (var context = new MusicLearningLibraryDbContext(options))
            {
                var pending = context.Set<MediaAnalysis>()
                    .Where(x => x.Status == MediaAnalysisStatus.Pending)
                    .ToList();

                Assert.Single(pending);
                Assert.Equal(MediaAnalysisStatus.Pending, pending[0].Status);
            }
        }
    }
}
