using MusicLearningLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLearningLibrary.Application.Abstractions
{
    public interface IMediaAnalysisRepository
    {
        void add(MediaAnalysis analysis);
        MediaAnalysis GetById(Guid id);
        void Update(MediaAnalysis analysis);
    }
}
