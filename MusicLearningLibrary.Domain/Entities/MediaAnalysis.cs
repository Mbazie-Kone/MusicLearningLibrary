using MusicLearningLibrary.Domain.Enums;

namespace MusicLearningLibrary.Domain.Entities
{
    public class MediaAnalysis
    {
        public Guid MediaId { get; private set; }
        public MediaAnalysisStatus Status { get; private set; }
        public DateTime? CompletedAt { get; private set; }
       
        public string? FailureReason { get; private set; }
        public DateTime? FailedAt { get; private set; }

        private MediaAnalysis() { }

        public static MediaAnalysis Create(Guid mediaId)
        {
            return new MediaAnalysis
            {
                MediaId = mediaId,
                Status = MediaAnalysisStatus.Pending
            };
        }

        public void MarkProcessing()
        {
            if (Status != MediaAnalysisStatus.Pending)
                throw new InvalidOperationException("Analysis must be pending.");

            Status = MediaAnalysisStatus.Processing;
        }

        public void MarkCompleted()
        {
            if (Status != MediaAnalysisStatus.Processing)
                throw new InvalidOperationException("Analysis must be processing.");

            Status = MediaAnalysisStatus.Completed;
            CompletedAt = DateTime.UtcNow;
        }

        public void MarkFailed(string reason)
        {
            if (Status != MediaAnalysisStatus.Processing)
                throw new InvalidOperationException("Only processing analysis can fail.");

            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Failure reason is required.", nameof(reason));

            Status = MediaAnalysisStatus.Failed;
            FailureReason = reason;
            FailedAt = DateTime.UtcNow;
        }
    }
}
