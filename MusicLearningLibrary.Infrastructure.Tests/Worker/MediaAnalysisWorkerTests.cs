using MusicLearningLibrary.Application;
using MusicLearningLibrary.Application.Abstractions;
using MusicLearningLibrary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLearningLibrary.Infrastructure.Tests.Worker
{
    public class MediaAnalysisWorkerTests
    {
        [Fact]
        public void Worker_processes_only_pending_analyses()
        {
            // Arrange
            var repo = new InMemoryMediaAnalysisRepository();
            var orchestrator = new MediaAnalysisOrchestrator(repo);

            var pending = orchestrator.CreateAnalysis(Guid.NewGuid()); // Pending
            var processing = orchestrator.CreateAnalysis(Guid.NewGuid());
            orchestrator.MarkProcessing(processing.Id); // Processing

            var worker = new MediaAnalysisWorkerSimulator(orchestrator, repo);

            // Act
            worker.ProcessNextPending();

            // Assert
            Assert.Equal(MediaAnalysisStatus.Processing, repo.GetById(pending.Id).Status);
            Assert.Equal(MediaAnalysisStatus.Processing, repo.GetById(processing.Id).Status); // unchanged
        }

        [Fact]
        public void Worker_marks_failed_when_processing_throws()
        {
            // Arrange
            var repo = new InMemoryMediaAnalysisRepository();
            var orchestrator = new MediaAnalysisOrchestrator(repo);

            var pending = orchestrator.CreateAnalysis(Guid.NewGuid());

            var worker = new MediaAnalysisWorkerSimulator(orchestrator, repo)
            {
                ThrowOnComplete = true
            };

            // Act
            worker.ProcessNextPending();

            // Assert
            var updated = repo.GetById(pending.Id);
            Assert.Equal(MediaAnalysisStatus.Failed, updated.Status);
            Assert.False(string.IsNullOrWhiteSpace(updated.Error));
        }

        // --- helpers for test only ---
        private class MediaAnalysisWorkerSimulator
        {
            private readonly MediaAnalysisOrchestrator _orchestrator;
            private readonly IMediaAnalysisRepository _repo;

            public bool ThrowOnComplete { get; set; }

            public MediaAnalysisWorkerSimulator(MediaAnalysisOrchestrator orchestrator, IMediaAnalysisRepository repo)
            {
                _orchestrator = orchestrator;
                _repo = repo;
            }

            public void ProcessNextPending()
            {
                // Find first pending
                foreach (var id in ((InMemoryMediaAnalysisRepository)_repo).Ids())
                {
                    var analysis = _repo.GetById(id);
                    if (analysis.Status != MediaAnalysisStatus.Pending)
                        continue;

                    // Mark processing via application layer
                    _orchestrator.MarkProcessing(analysis.Id);

                    // Simulate failure *after* processing started
                    if (ThrowOnComplete)
                    {
                        var a = _repo.GetById(analysis.Id);

                        a.MarkFailed("Simulated processing error");

                        _repo.Update(a);
                    }

                    return;
                }
            }
        }
       
    }
}
