using Microsoft.EntityFrameworkCore;
using MusicLearningLibrary.Infrastructure.DbContexts;
using MusicLearningLibrary.Application.Auth.Interfaces;
using MusicLearningLibrary.Domain.Entities;

namespace MusicLearningLibrary.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MusicLearningLibraryDbContext _db;

        public UserRepository(MusicLearningLibraryDbContext db)
        {
            _db = db;
        }

        // Alternative approach without using return
        public Task AddAsync(User user, CancellationToken ct = default) => _db.Users.AddAsync(user, ct).AsTask();

        // Alternative approach without using return
        public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) => _db.Users.FirstOrDefaultAsync(u => u.Email == email, ct);

        // Standard method implementation
        public Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return _db.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
        }

        // Standard method implementation
        public Task SaveChangeAsync(CancellationToken ct = default)
        {
            return _db.SaveChangesAsync(ct);
        }
    }
}
