using MusicLibrary.Application.Auth.Interfaces;
using System.Security.Cryptography;

namespace MusicLearningLibrary.Infrastructure.Tokens
{
    public class ConfirmationTokenGenerator : IConfirmationTokenGenerator
    {
        public string Generate()
        {
            var bytes = RandomNumberGenerator.GetBytes(32);
            return Convert.ToBase64String(bytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .TrimEnd('=');
        }
    }
}
