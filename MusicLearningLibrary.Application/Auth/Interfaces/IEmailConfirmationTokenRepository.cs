using MusicLearningLibrary.Domain.Entities;

namespace MusicLearningLibrary.Application.Auth.Interfaces
{
    public interface IEmailConfirmationTokenRepository
    {
        Task AddAsync(EmailConfirmationToken token, CancellationToken ct = default);
        Task<EmailConfirmationToken?> GetByTokenAsync(string token, CancellationToken ct = default);
        Task SaveChangeAsync(CancellationToken ct = default);
    }
}
