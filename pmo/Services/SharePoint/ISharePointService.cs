using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace pmo.Services.SharePoint {
   public interface ISharePointService {
        Task<string> Upload(IFormFile file);
        JObject Delete(string file);
    }
}
