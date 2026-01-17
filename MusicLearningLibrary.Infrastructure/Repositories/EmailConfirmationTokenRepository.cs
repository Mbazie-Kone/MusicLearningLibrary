using Microsoft.EntityFrameworkCore;
using MusicLearningLibrary.Infrastructure.DbContexts;
using MusicLearningLibrary.Application.Auth.Interfaces;
using MusicLearningLibrary.Domain.Entities;

namespace MusicLearningLibrary.Infrastructure.Repositories
{

    public class EmailConfirmationTokenRepository : IEmailConfirmationTokenRepository
    {
        private readonly MusicLibraryDbContext _db;

        public EmailConfirmationTokenRepository(MusicLibraryDbContext db)
        {
            _db = db;
        }

        public Task AddAsync(EmailConfirmationToken token, CancellationToken ct = default)
        {
            return _db.EmailConfirmationTokens.AddAsync(token, ct).AsTask();
        }

        public Task<EmailConfirmationToken?> GetByTokenAsync(string token, CancellationToken ct = default)
        {
            return _db.EmailConfirmationTokens.FirstOrDefaultAsync(t => t.Token == token, ct);
        }

        public Task SaveChangeAsync(CancellationToken ct = default)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
