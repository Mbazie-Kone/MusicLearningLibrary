using MusicLearningLibrary.Application;
using MusicLearningLibrary.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaProcessor.Services
{
    public class MediaAnalysisJobRunner
    {
        private readonly MediaAnalysisOrchestrator _orchestrator;
        private readonly MediaAnalysisRepository _repository;

        public MediaAnalysisJobRunner(MediaAnalysisOrchestrator orchestrator, MediaAnalysisRepository repository)
        {
            _orchestrator = orchestrator;
            _repository = repository;
        }

        public void ProcessNextPending()
        {
            var pending = _repository.GetPending(1);
            if (pending.Count == 0) return;

            var analysis = pending[0];

            _orchestrator.MarkProcessing(analysis.Id);

            try
            {
                // placeholder: no audio logic yet
                _orchestrator.MarkCompleted(analysis.Id);
            }
            catch (Exception ex)
            {
                // Failed is allowed only from Processing (domain rule)
                var a = _repository.GetById(analysis.Id);
                a.MarkFailed(ex.Message);
                _repository.Update(a);
            }
        }
    }
}
