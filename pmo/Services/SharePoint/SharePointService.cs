using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using pmo.Controllers;
using pmo.Models.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace pmo.Services.SharePoint
{
    public class SharePointService : BaseController, ISharePointService
    {
        static string siteUrl = $"{Config.AppSettings["Sharepoint:SPFarm"]}/{Config.AppSettings["Sharepoint:SPSite"]}";
        static string domain = $"{Config.AppSettings["NetworkCredentials:domain"]}";
        static string username = $"{Config.AppSettings["NetworkCredentials:username"]}";
        static string password = $"{Config.AppSettings["NetworkCredentials:password"]}";
        static string documentLibrary = $"{Config.AppSettings["Sharepoint:DocumentLibrary"]}";
        static string subsite = $"{Config.AppSettings["Sharepoint:SPSite"]}";

        public SharePointService(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {

        }
        public string GetFileNameFromUrl(string url)
        {
            return url.Replace(siteUrl, "").Replace("/" + documentLibrary + "/", ""); 
        }
        public async Task<string> Upload(IFormFile file, string projectName)
        {
            if (file == null || file.Length == 0) return "file not selected";

            string projectNameStripped = projectName
                .Replace(" ", "-")
                .ToLower();

            string timeStamp = DateTime.Now.ToString("ddMMyyy-hhmmss");
            string fileName = $"{projectNameStripped}_{timeStamp}_{file.FileName}";

            string result = string.Empty;
            string pathToUpload = $"{siteUrl}/_api/Web/GetFolderByServerRelativeUrl('{documentLibrary}')/Files/add(url='{fileName}',overwrite=true)";
            HttpWebRequest wreq = HttpWebRequest.Create(pathToUpload) as HttpWebRequest;
            wreq.UseDefaultCredentials = false;
            //credential who has edit access on document library
            NetworkCredential credentials = new System.Net.NetworkCredential(username, password, domain);
            wreq.Credentials = credentials;

            //Get formdigest value from site
            string formDigest = GetFormDigestValue(siteUrl, credentials);

            wreq.Headers.Add("X-RequestDigest", formDigest);
            wreq.Method = "POST";
            wreq.Timeout = 1000000; //timeout should be large in order to upload file which are of large size
            wreq.Accept = "application/json; odata=verbose";
            wreq.ContentLength = file.Length;
            using (System.IO.Stream requestStream = wreq.GetRequestStream())
            {
                Byte[] bytes = await file.GetBytes();
                requestStream.Write(bytes, 0, (int)file.Length);
            }

            try
            {
                WebResponse wresp = wreq.GetResponse();
                using (StreamReader sr = new StreamReader(wresp.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    JObject resultObj = JObject.Parse(result);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public byte[] Download(string filename) {

            using (var client = new WebClient()) {
                client.Headers.Add(HttpRequestHeader.Accept, "application/octet-stream");
                client.Headers.Add("binaryStringRequestBody", "true");
                client.UseDefaultCredentials = false;
                NetworkCredential credentials = new System.Net.NetworkCredential(username, password, domain);
                client.Credentials = credentials;
                string siteUrl = $"{Config.AppSettings["Sharepoint:SPFarm"]}/{Config.AppSettings["Sharepoint:SPSite"]}";
                string formDigest = GetFormDigestValue(siteUrl, credentials);
                client.Headers.Add("X-RequestDigest", formDigest);

                var endpointUri = new Uri($"{siteUrl}/_api/web/getfilebyserverrelativeurl('/{subsite}/{documentLibrary}/{filename}')/OpenBinaryStream");
                var result = client.DownloadData(endpointUri);
                using (MemoryStream stream = new MemoryStream(result))
                {
                    return stream.ToArray();
                }
            }
        }

        public void Delete(string url) {
            HttpWebRequest wreq = HttpWebRequest.Create(url) as HttpWebRequest;
            wreq.UseDefaultCredentials = false;
            NetworkCredential credentials = new NetworkCredential(username, password, domain);
            wreq.Credentials = credentials;
            wreq.Method = "DELETE";
            WebResponse wresp = wreq.GetResponse();
        }

        private static string GetFormDigestValue(string siteurl, NetworkCredential credentials) {
            string newFormDigest = "";
            HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(siteurl + "/_api/contextinfo");
            endpointRequest.Method = "POST";
            endpointRequest.ContentLength = 0;
            endpointRequest.Credentials = credentials;
            endpointRequest.Accept = "application/json;odata=verbose";
            try {
                //HttpWebResponse endpointResponse = (HttpWebResponse)endpointRequest.GetResponse();

                WebResponse webResp = endpointRequest.GetResponse();
                Stream webStream = webResp.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                var j = JObject.Parse(response);
                var jObj = (JObject)JsonConvert.DeserializeObject(response);
                foreach (var item in jObj["d"].Children()) {
                    newFormDigest = item.First()["FormDigestValue"].ToString();
                }
                responseReader.Close();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return newFormDigest;
        }
    }

}
