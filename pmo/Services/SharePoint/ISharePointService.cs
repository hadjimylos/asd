using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace pmo.Services.SharePoint
{
   public interface ISharePointService
    {
        Task<string> Upload(IFormFile file);
        bool BreakFileRoleInheritance(string file);
        bool RemoveFilePermissions(string file, string id);
        bool AddFilePermissions(string file, string id);
        bool Delete(string file);
        Task<string> GetUserPrincipalId(string userEmail);
        void InsertOneToMany(string type, int id, string file, string Url);
    }
}
