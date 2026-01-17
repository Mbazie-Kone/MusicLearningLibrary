using Microsoft.Extensions.Logging;
using MusicLearningLibrary.Application.Auth.Interfaces;

namespace MusicLearningLibrary.Infrastructure.Email
{
    public class FakeEmailSender : IEmailSender
    {
        private readonly ILogger<FakeEmailSender> _logger;
        public FakeEmailSender(ILogger<FakeEmailSender> logger)
        {
            _logger = logger;
        }
        public Task SendRegistrationConfirmationAsync(string toEmail, string confirmationLink, CancellationToken ct = default)
        {
            _logger.LogInformation("FAKE EMAIL to {Email}: {Link}", toEmail, confirmationLink);
            return Task.CompletedTask;
        }
    }
}
