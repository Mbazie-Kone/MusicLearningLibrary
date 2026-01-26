using MusicLearningLibrary.Application.Abstractions;
using MusicLearningLibrary.Domain.Entities;
using MusicLearningLibrary.Domain.Enums;
using MusicLearningLibrary.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLearningLibrary.Infrastructure.Repositories
{
    public class MediaAnalysisRepository : IMediaAnalysisRepository
    {
        private readonly MusicLearningLibraryDbContext _db;

        public MediaAnalysisRepository(MusicLearningLibraryDbContext db)
        {
            _db = db;
        }

        public void add(MediaAnalysis analysis)
        {
            _db.MediaAnalyses.Add(analysis);
            _db.SaveChanges();
        }

        public MediaAnalysis GetById(Guid id)
        {
            return _db.MediaAnalyses.Single(x => x.Id == id);
        }

        public void Update(MediaAnalysis analysis)
        {
            _db.MediaAnalyses.Update(analysis);
            _db.SaveChanges();
        }

        // Helper useful for workers (if needed in tests/workers)
        public List<MediaAnalysis> GetPending(int take = 10)
        {
            return _db.MediaAnalyses
                .Where(x => x.Status == MediaAnalysisStatus.Pending)
                .OrderBy(x => x.RequestedAt)
                .Take(take)
                .ToList();
        }
    }
}
