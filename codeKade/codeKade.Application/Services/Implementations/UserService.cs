using codeKade.Application.PathExtentions;
using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.DTOs.Account;
using codeKade.DataLayer.Entities.Account;
using codeKade.DataLayer.Repository.Interfaces;
using codeKade.Application.Security;
using LightElectric.Application.Extensions;
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

        public async Task<bool> ResetPassword(string Code, ResetPasswordDTO reset)
        {
            var user = await _userRepository.GetEntityQuery().SingleOrDefaultAsync(j => j.ActiveCode == Code);
            if (user == null)
            {
                return false;
            }

            user.Password = _passwordHelper.EncodePasswordMd5(reset.Password);
            user.ActiveCode = Guid.NewGuid().ToString("N");
            _userRepository.EditEntity(user);
            await _userRepository.SaveChanges();
            return true;
        }

        public async Task<int> GetTodayUsers()
        {
            return await _userRepository.GetEntityQuery().CountAsync(u=>u.CreateDate.Date == DateTime.Now.Date);
        }

        public async Task<int> CountOfUsers()
        {
            return await _userRepository.GetEntityQuery().CountAsync();
        }

        public async Task<FilterUserDTO> GetUsersList(FilterUserDTO filter)
        {
            var query = _userRepository.GetEntityQuery().AsQueryable();

            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                query = query.Where(s => EF.Functions.Like(s.FirstName, $"%{filter.FirstName}%"));
            }
            if (!string.IsNullOrEmpty(filter.LastName))
            {
                query = query.Where(s => EF.Functions.Like(s.LastName, $"%{filter.LastName}%"));
            }
            if (!string.IsNullOrEmpty(filter.EmailAddress))
            {
                query = query.Where(s => EF.Functions.Like(s.Email, $"%{filter.EmailAddress}%"));
            }


            filter.SkipEntity = (filter.PageID - 1) * filter.TakeEntity;
            var entitiesCount = await query.CountAsync();
            filter.PageCount = (int)Math.Ceiling(entitiesCount / (double)filter.TakeEntity);

            var products = await query.OrderBy(s => s.ID).Skip(filter.SkipEntity).Take(filter.TakeEntity)
                .ToListAsync();
            filter.StartPage = filter.PageID - 3 > 0 ? filter.PageID - 3 : 1;
            filter.EndPage = filter.PageID + 3 <= filter.PageCount ? filter.PageID + 3 : filter.PageCount;
            filter.Users = products;
            return filter;
        }

        public async Task<RegisterUserResult> AdminRegisterUser(RegisterUserDTO register)
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
                Avatar = "usr_avatar.jpg",
                IsActive = true,
                IsAdmin = false
            };
            await _userRepository.AddEntity(user);
            await _userRepository.SaveChanges();
            return RegisterUserResult.Success;
        }

        public async Task<long> DeleteUser(long id)
        {
            var user = await _userRepository.GetByID(id);
            if (user != null)
            {
                user.IsDelete = true;
                _userRepository.EditEntity(user);
                await _userRepository.SaveChanges();
                return user.ID;
            }

            return 0;
        }

        public async Task<bool> ChangePermission(long id)
        {
            var user = await _userRepository.GetByID(id);
            if (user == null)
            {
                return false;
            }
            if (user.IsAdmin)
            {
                user.IsAdmin = false;
            }
            else
            {
                user.IsAdmin = true;
            }

            _userRepository.EditEntity(user);
            await _userRepository.SaveChanges();
            return true;
        }

        public async Task<FilterUserDTO> GetTodayUsers(FilterUserDTO filter)
        {
            var query = _userRepository.GetEntityQuery().AsQueryable();

            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                query = query.Where(s => EF.Functions.Like(s.FirstName, $"%{filter.FirstName}%"));
            }
            if (!string.IsNullOrEmpty(filter.LastName))
            {
                query = query.Where(s => EF.Functions.Like(s.LastName, $"%{filter.LastName}%"));
            }
            if (!string.IsNullOrEmpty(filter.EmailAddress))
            {
                query = query.Where(s => EF.Functions.Like(s.Email, $"%{filter.EmailAddress}%"));
            }


            filter.SkipEntity = (filter.PageID - 1) * filter.TakeEntity;
            var entitiesCount = await query.CountAsync();
            filter.PageCount = (int)Math.Ceiling(entitiesCount / (double)filter.TakeEntity);

            var products = await query.OrderBy(s => s.ID).Where(u=>u.CreateDate.Date == DateTime.Now.Date).Skip(filter.SkipEntity).Take(filter.TakeEntity)
                .ToListAsync();
            filter.StartPage = filter.PageID - 3 > 0 ? filter.PageID - 3 : 1;
            filter.EndPage = filter.PageID + 3 <= filter.PageCount ? filter.PageID + 3 : filter.PageCount;
            filter.Users = products;
            return filter;
        }

        public async Task<bool> IsUserAdmin(long userId)
        {
            var user = await _userRepository.GetByID(userId);
            if (user.IsAdmin)
            {
                return true;
            }

            return false;
        }

        public async Task<FilterUserDTO> GetDeletedUsers(FilterUserDTO filter)
        {
            var query = _userRepository.GetEntityQuery().AsQueryable();

            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                query = query.Where(s => EF.Functions.Like(s.FirstName, $"%{filter.FirstName}%"));
            }
            if (!string.IsNullOrEmpty(filter.LastName))
            {
                query = query.Where(s => EF.Functions.Like(s.LastName, $"%{filter.LastName}%"));
            }
            if (!string.IsNullOrEmpty(filter.EmailAddress))
            {
                query = query.Where(s => EF.Functions.Like(s.Email, $"%{filter.EmailAddress}%"));
            }


            filter.SkipEntity = (filter.PageID - 1) * filter.TakeEntity;
            var entitiesCount = await query.CountAsync();
            filter.PageCount = (int)Math.Ceiling(entitiesCount / (double)filter.TakeEntity);

            var products = await query.IgnoreQueryFilters().OrderBy(s => s.ID).Where(u => u.IsDelete).Skip(filter.SkipEntity).Take(filter.TakeEntity)
                .ToListAsync();
            filter.StartPage = filter.PageID - 3 > 0 ? filter.PageID - 3 : 1;
            filter.EndPage = filter.PageID + 3 <= filter.PageCount ? filter.PageID + 3 : filter.PageCount;
            filter.Users = products;
            return filter;
        }

        public async Task<bool> ReturnDeletedUser(long id)
        {
            var user = await _userRepository.GetEntityQuery().IgnoreQueryFilters().FirstOrDefaultAsync(u=>u.ID == id);
            if (user == null)
            {
                return false;
            }

            user.IsDelete = false;
            _userRepository.EditEntity(user);
            await _userRepository.SaveChanges();
            return true;
        }

        public async Task<bool> ActiveAccount(string ActiveCode)
        {
            var user = await GetUserByActiveCode(ActiveCode);
            if (user != null)
            {
                user.IsActive = true;
                user.ActiveCode = Guid.NewGuid().ToString("N");
                _userRepository.EditEntity(user);
                await _userRepository.SaveChanges();
                return true;
            }
            return false;

        }

        public async Task<User> GetUserByActiveCode(string ActiveCode)
        {
            var user = await _userRepository.GetEntityQuery().SingleOrDefaultAsync(s => s.ActiveCode == ActiveCode);
            return user;
        }

        public async Task<User> GetEntityByEmail(string email)
        {

            var user = await _userRepository.GetEntityQuery().FirstOrDefaultAsync(s => s.Email == email);
            return user;

        }

        public async Task<User> GetById(long id)
        {
            return await _userRepository.GetByID(id);
        }

        public async Task<bool> EditUser(EditProfileDTO edit)
        {
            var user = await GetById(edit.Id);
            user.FirstName = edit.FirstName;
            user.LastName = edit.LastName;
            user.Mobile = edit.Mobile;
            user.Address = edit.Address;


            if (edit.AvatarImage != null && edit.AvatarImage.IsImage())
            {
                var UserAvatarName = Guid.NewGuid().ToString("N") + Path.GetExtension(edit.AvatarImage.FileName);
                edit.AvatarImage.AddImageToServer(UserAvatarName, PathTools.UserImageUpload, 150, 100, null, user.Avatar);
                user.Avatar = UserAvatarName;
            }
            _userRepository.EditEntity(user);
            await _userRepository.SaveChanges();
            return true;
        }

        public async Task<RegisterUserResult> Register(RegisterUserDTO register)
        {
            var IsExists = await _userRepository.GetEntityQuery().AnyAsync(s => s.Email == register.Email && s.IsActive == true);
            if (IsExists == true)
            {
                return RegisterUserResult.EmailConflict;
            }

            if (register.SchoolId == null)
            {
                var user = new User
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    Mobile = register.Mobile,
                    Password = _passwordHelper.EncodePasswordMd5(register.Password),
                    ActiveCode = Guid.NewGuid().ToString("N"),
                    Avatar = "usr_avatar.jpg",
                    IsAdmin = false,
                };
                await _userRepository.AddEntity(user);
                await _userRepository.SaveChanges();
            }
            else
            {
                var user = new User
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    Mobile = register.Mobile,
                    Password = _passwordHelper.EncodePasswordMd5(register.Password),
                    ActiveCode = Guid.NewGuid().ToString("N"),
                    Avatar = "usr_avatar.jpg",
                    IsAdmin = false,
                    SchoolId = register.SchoolId
                };
                await _userRepository.AddEntity(user);
                await _userRepository.SaveChanges();
            }
            
            return RegisterUserResult.Success;
        }

        public async Task<LoginUserResult> LoginUser(LoginUserDTO login)
        {
            var user = await _userRepository.GetEntityQuery().FirstOrDefaultAsync(s => s.Email == login.Email && s.Password == _passwordHelper.EncodePasswordMd5(login.Password));
            if (user == null)
            {
                return LoginUserResult.NotFound;
            }
            if (user.IsActive == false)
            {
                return LoginUserResult.NotActive;
            }
            return LoginUserResult.Success;
        }
    }
}
