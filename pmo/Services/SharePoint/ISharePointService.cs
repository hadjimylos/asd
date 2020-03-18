using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace pmo.Services.SharePoint {
   public interface ISharePointService {
        Task<string> Upload(IFormFile file, int projectId);
        void Delete(string file);
        byte[] Download(string filename);
        public string GetFileNameFromUrl(string url);

    }
}
