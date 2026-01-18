using MusicLearningLibrary.Application.Abstractions;
using MusicLearningLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLearningLibrary.Application
{
    public class MediaAnalysisOrchestrator
    {
        private readonly IMediaAnalysisRepository _repository;

        public MediaAnalysisOrchestrator(IMediaAnalysisRepository repository)
        {
            _repository = repository;
        }

        public MediaAnalysis CreateAnalysis(Guid mediaId)
        {
            var analysis = MediaAnalysis.Create(mediaId);
            
            _repository.add(analysis);

            return analysis;
        }

        public void MarkProcessing(Guid analysisId)
        {
            var analysis = _repository.GetById(analysisId);

            analysis.MarkProcessing();

            _repository.Update(analysis);
        }

        public void MarkCompleted(Guid analysisId)
        {
            var analysis = _repository.GetById(analysisId);

            analysis.MarkCompleted();

            _repository.Update(analysis);
        }
    }
}
