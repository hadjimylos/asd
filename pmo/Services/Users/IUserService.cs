using dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace pmo.Services.Users
{
    public interface IUserService
    {
        bool AddNewUser(UserViewModel userViewModel);
        User GetUserById(int userId);
    }
}
