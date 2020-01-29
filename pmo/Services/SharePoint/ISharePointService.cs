using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace pmo.Services.SharePoint
{
   public interface ISharePointService
    {
        Task<string> GetUserInfo();
        Task<string> Upload(IFormFile file);
        bool BreakFileRoleInheritance(string file);
        bool RemoveFilePermissions(string file);
        bool Delete(string file);
    }
}
