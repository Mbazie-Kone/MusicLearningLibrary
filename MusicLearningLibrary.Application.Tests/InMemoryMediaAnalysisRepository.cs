using MusicLearningLibrary.Application.Abstractions;
using MusicLearningLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLearningLibrary.Application.Tests
{
    public class InMemoryMediaAnalysisRepository : IMediaAnalysisRepository
    {

        private readonly Dictionary<Guid, MediaAnalysis> _store = new();

        public void add(MediaAnalysis analysis)
        {
            _store[analysis.Id] = analysis;
        }

        public MediaAnalysis GetById(Guid id)
        {
            return _store[id];
        }

        public void Update(MediaAnalysis analysis)
        {
            _store[analysis.Id] = analysis;
        }

        public IEnumerable<Guid> Ids() => _store.Keys;

    }
}
