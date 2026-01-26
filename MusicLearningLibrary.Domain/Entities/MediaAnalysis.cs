using MusicLearningLibrary.Domain.Enums;

namespace MusicLearningLibrary.Domain.Entities
{
    public class MediaAnalysis
    {
        public Guid Id { get; private set; }
        public Guid MediaId { get; private set; }

        public MediaAnalysisStatus Status { get; private set; }

        public DateTime RequestedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }

        public string? Error { get; private set; }

        private MediaAnalysis() { }

        public static MediaAnalysis Create(Guid mediaId)
        {
            return new MediaAnalysis
            {
                Id = Guid.NewGuid(),
                MediaId = mediaId,
                Status = MediaAnalysisStatus.Pending,
                RequestedAt = DateTime.UtcNow
            };
        }

        public void MarkProcessing()
        {
            /*if (Status != MediaAnalysisStatus.Pending)
                throw new InvalidOperationException("Analysis must be pending.");*/

            EnsureStatus(MediaAnalysisStatus.Pending);

            Status = MediaAnalysisStatus.Processing;
        }

        public void MarkCompleted()
        {
            /*if (Status != MediaAnalysisStatus.Processing)
                throw new InvalidOperationException("Analysis must be processing.");*/

            EnsureStatus(MediaAnalysisStatus.Processing);

            Status = MediaAnalysisStatus.Completed;
            CompletedAt = DateTime.UtcNow;
        }

        public void MarkFailed(string error)
        {
            /*if (Status != MediaAnalysisStatus.Processing)
                throw new InvalidOperationException("Only processing analysis can fail.");*/

            if (string.IsNullOrWhiteSpace(error))
                throw new ArgumentException("Error message is required.", nameof(error));

            EnsureStatus(MediaAnalysisStatus.Processing);

            Status = MediaAnalysisStatus.Failed;
            Error = error;
            CompletedAt = DateTime.UtcNow;
        }

        private void EnsureStatus(MediaAnalysisStatus expected)
        {
            if (Status != expected)
                throw new InvalidOperationException($"Invalid state transition from {Status} to {expected}");
        }
        
    }
}
