using codeKade.DataLayer.DTOs.Account;
using codeKade.DataLayer.Entities.Account;

namespace codeKade.Application.Services.Interfaces
{
    public interface IUserService : IAsyncDisposable
    {
        Task<RegisterUserResult> Register(RegisterUserDTO register);

        Task<LoginUserResult> LoginUser(LoginUserDTO login);

        Task<User> GetEntityByEmail(string email);

        Task<User> GetById(long id);

        Task<bool> EditUser(EditProfileDTO edit);

        Task<bool> ActiveAccount(string ActiveCode);

        Task<User> GetUserByActiveCode(string ActiveCode);

        Task<bool> ResetPassword(string Code, ResetPasswordDTO reset);
    }
}
