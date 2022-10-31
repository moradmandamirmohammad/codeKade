using codeKade.DataLayer.DTOs.Account;

namespace codeKade.Application.Services.Interfaces
{
    public interface IUserService : IAsyncDisposable
    {
        Task<RegisterUserResult> Register(RegisterUserDTO register);
    }
}
