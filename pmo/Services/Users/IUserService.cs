using dbModels;
using System.ComponentModel.DataAnnotations;
using ViewModels;

namespace pmo.Services.Users
{
    public interface IUserService
    {
        bool AddNewUser(UserViewModel userViewModel);
        bool UpdateUser(UserViewModel userViewModel);
        User GetUserById(int userId);
        ValidationResult AD_GetUser(string networkUsername);
    }
}
