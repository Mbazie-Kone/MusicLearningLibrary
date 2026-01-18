using Xunit;
using System;
using MusicLearningLibrary.Domain.Entities;
using MusicLearningLibrary.Domain.Enums;

namespace MusicLearningLibrary.Domain.Tests
{
    public class MediaAnalysisTests
    {
        [Fact]
        public void MediaAnalysis_should_start_in_pending_state()
        {
            // Act
            var analysis = MediaAnalysis.Create(Guid.NewGuid());

            // Assert
            Assert.Equal(MediaAnalysisStatus.Pending, analysis.Status);
        }

        [Fact]
        public void MediaAnalysis_can_transition_from_pending_to_precessing()
        {
            var analysis = MediaAnalysis.Create(Guid.NewGuid());

            analysis.MarkProcessing();

            Assert.Equal(MediaAnalysisStatus.Processing, analysis.Status);
        }

        [Fact]
        public void MediaAnalysis_can_transition_from_processing_to_completed()
        {
            var analysis = MediaAnalysis.Create(Guid.NewGuid());
            analysis.MarkProcessing();

            analysis.MarkCompleted();

            Assert.Equal(MediaAnalysisStatus.Completed, analysis.Status);
            Assert.NotNull(analysis.CompletedAt);
        }

        [Fact]
        public void MediaAnalysis_can_transition_from_processing_to_failed_with_error()
        {
            var analysis  = MediaAnalysis.Create(Guid.NewGuid());
            analysis.MarkProcessing();

            analysis.MarkFailed("Analysis error");

            Assert.Equal(MediaAnalysisStatus.Failed, analysis.Status);
            Assert.Equal("Analysis error", analysis.FailureReason);
        }

        [Fact]
        public void MediaAnalysis_should_not_allow_invalid_state_transitions()
        {
            var analysis = MediaAnalysis.Create(Guid.NewGuid());

            Assert.Throws<InvalidOperationException>(() => analysis.MarkCompleted());
        }

        [Fact]
        public void MediaAnalysis_failed_state_requires_error_message()
        {
            var analysis = MediaAnalysis.Create(Guid.NewGuid());
            analysis.MarkProcessing();

            Assert.Throws<ArgumentException>(() => analysis.MarkFailed(string.Empty));
        }
    }
}
