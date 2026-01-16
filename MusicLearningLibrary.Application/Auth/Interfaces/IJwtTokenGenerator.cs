using MusicLibrary.Domain.Entities;

namespace MusicLearningLibrary.Application.Auth.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
