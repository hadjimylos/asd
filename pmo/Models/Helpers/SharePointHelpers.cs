using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace pmo.Models.Helpers
{
    public static class SharePointHelpers
    {
        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
