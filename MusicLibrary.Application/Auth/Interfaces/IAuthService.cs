using MusicLibrary.Application.Auth.Commands;

namespace MusicLibrary.Application.Auth.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterUserCommand command, CancellationToken ct = default);
        Task ConfirmEmailAsync(ConfirmEmailCommand command, CancellationToken ct = default);
        Task LoginAsync(LoginCommand command, CancellationToken ct = default);
    }
}
