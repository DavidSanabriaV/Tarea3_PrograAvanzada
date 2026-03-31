using Tarea3.Models;

namespace Tarea3.Services
{
    public interface IAccountService
    {
        Task<(bool Succeeded, IEnumerable<string> Errors)> RegisterAsync(RegisterViewModel model);
        Task<(bool Succeeded, string? ErrorMessage)> LoginAsync(string email, string password, bool rememberMe);
        Task LogoutAsync();
    }
}
