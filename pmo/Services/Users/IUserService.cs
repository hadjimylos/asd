using dbModels;
using System.ComponentModel.DataAnnotations;
using ViewModels;

namespace pmo.Services.Users
{
    public interface IUserService
    {
        bool AddNewUser(forms.UserForm userViewModel);
        bool UpdateUser(forms.UserForm userViewModel);
        int GetCurrentUserId();
        User GetUserById(int userId);
        ValidationResult AD_GetUser(string networkUsername);
    }
}
