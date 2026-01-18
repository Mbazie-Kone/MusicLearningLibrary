using MusicLearningLibrary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLearningLibrary.Application.Tests
{
    public class MediaAnalysisOrchestratorTests
    {
        [Fact]
        public void When_media_is_uploaded_analysis_is_craeted_in_pending_state()
        {
            // Arrange
            var repository = new InMemoryMediaAnalysisRepository();
            var orchestrator = new MediaAnalysisOrchestrator(repository);

            var mediaId = Guid.NewGuid();

            // Act
            var analysis = orchestrator.RequestAnalysis(mediaId);

            // Assert
            Assert.NotNull(analysis);
            Assert.Equal(mediaId, analysis.MediaId);
            Assert.Equal(MediaAnalysisStatus.Pending, analysis.Status);
        }

        [Fact]
        public void When_analysis_is_taken_in_charge_it_moves_to_processing()
        {
            // Arrange
            var repository = new InMemoryMediaAnalysisRepository();
            var orchestrator = new MediaAnalysisOrchestrator(repository);

            var mediaId = Guid.NewGuid();
            var analysis = orchestrator.RequestAnalysis(mediaId);

            // Act
            orchestrator.MarkAnalysisProcessing(analysis.Id);

            // Assert
            var updatedAnalysis = repository.GetById(analysis.Id);
            Assert.Equal(MediaAnalysisStatus.Processing, updatedAnalysis.Status);
        }

        [Fact]
        public async Task Orchestrator_must_not_bypass_domain_rules()
        {
            // Arrange
            var repository = new InMemoryMediaAnalysisRepository();
            var orchestrator = new MediaAnalysisOrchestrator(repository);

            var mediaId = Guid.NewGuid();
            var analysis = orchestrator.CreateAnalysis(mediaId);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => orchestrator.MarkCompleted(analysis.Id));
        }
    }
}
