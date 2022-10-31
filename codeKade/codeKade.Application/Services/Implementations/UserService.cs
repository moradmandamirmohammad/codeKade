using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.DTOs.Account;

namespace codeKade.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RegisterUserResult> Register(RegisterUserDTO register)
        {
            throw new NotImplementedException();
        }
    }
}
