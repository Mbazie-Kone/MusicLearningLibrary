namespace MusicLearningLibrary.Application.Auth.Interfaces
{
    public interface IEmailSender
    {
        Task SendRegistrationConfirmationAsync(string toEmail, string confirmationLink, CancellationToken ct = default);
    }
}
