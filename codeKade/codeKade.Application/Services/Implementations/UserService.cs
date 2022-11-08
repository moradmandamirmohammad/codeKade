using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.DTOs.Account;
using codeKade.DataLayer.Entities.Account;
using codeKade.DataLayer.Repository.Interfaces;
using codeKade.Application.Security;
using Microsoft.EntityFrameworkCore;

namespace codeKade.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IPasswordHelper _passwordHelper;

        public UserService(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
        }
        public async ValueTask DisposeAsync()
        {
            if (_userRepository != null)
            {
                await _userRepository.DisposeAsync();
            }
        }

        public async Task<RegisterUserResult> Register(RegisterUserDTO register)
        {
            var IsExists = await _userRepository.GetEntityQuery().AnyAsync(s => s.Email == register.Email && s.IsActive == true);
            if (IsExists == true)
            {
                return RegisterUserResult.EmailConflict;
            }
            var user = new User
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                Mobile = register.Mobile,
                Password = _passwordHelper.EncodePasswordMd5(register.Password),
                ActiveCode = Guid.NewGuid().ToString("N"),
                Avatar = "Default.jpg"
            };
            await _userRepository.AddEntity(user);
            await _userRepository.SaveChanges();
            return RegisterUserResult.Success;
        }
    }
}
