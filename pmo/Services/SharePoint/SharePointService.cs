using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        static string siteUrl = $"{Config.AppSettings["Sharepoint:SPSite"]}";
        static string domain = $"{Config.AppSettings["NetworkCredentials:domain"]}";
        static string username = $"{Config.AppSettings["NetworkCredentials:username"]}";
        static string password = $"{Config.AppSettings["NetworkCredentials:password"]}";
        static string documentLibrary = $"{Config.AppSettings["Sharepoint:DocumentLibrary"]}";
        static string ReadRoleDefinition = $"{Config.AppSettings["Sharepoint:ReadRoleDefinition"]}";
        static string documentLibraryToSharepointPath = $"{Config.AppSettings["Sharepoint:documentLibraryToSharepointPath"]}";

        public SharePointService(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {

        }
        public async Task<string> GetUserPrincipalId(string userEmail)
        {
            string Id = "0";
            var content = "";
            using (var handler = new HttpClientHandler { Credentials = new NetworkCredential(username, password, domain) })
            using (var _client = new HttpClient(handler))
            {
                _client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                var result = await _client.GetAsync(siteUrl + "/_api/web/SiteUsers/getByEmail('" + userEmail + "')/Id");
                if (!result.IsSuccessStatusCode)
                {
                    Console.WriteLine("Error");
                }
                content = await result.Content.ReadAsStringAsync();
                JObject UserData = JObject.Parse(content);
                Id = (string)UserData["d"]["Id"];
            }
            return Id;
        }
        public async Task<string> Upload(IFormFile file)
        {


            if (file == null || file.Length == 0) return "file not selected";
            bool status = false;
            string result = string.Empty;
            string pathToUpload = siteUrl + "/_api/Web/GetFolderByServerRelativeUrl('" + documentLibrary + "')/Files/add(url='" + file.FileName + "',overwrite=true)";
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
                    status = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
                throw;
            }
        }
        private static string GetFormDigestValue(string siteurl, NetworkCredential credentials)
        {
            string newFormDigest = "";
            HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create(siteurl + "/_api/contextinfo");
            endpointRequest.Method = "POST";
            endpointRequest.ContentLength = 0;
            endpointRequest.Credentials = credentials;
            endpointRequest.Accept = "application/json;odata=verbose";
            try
            {
                //HttpWebResponse endpointResponse = (HttpWebResponse)endpointRequest.GetResponse();

                WebResponse webResp = endpointRequest.GetResponse();
                Stream webStream = webResp.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                var j = JObject.Parse(response);
                var jObj = (JObject)JsonConvert.DeserializeObject(response);
                foreach (var item in jObj["d"].Children())
                {
                    newFormDigest = item.First()["FormDigestValue"].ToString();
                }
                responseReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return newFormDigest;
        }


        public bool BreakFileRoleInheritance(string fileName)
        {
            bool status = false;
            string result = string.Empty;
            string resourceUrl = siteUrl + "/_api/Web/GetFileByServerRelativeUrl('/" + documentLibraryToSharepointPath + "/" + fileName + "')/ListItemAllFields/breakroleinheritance(true)";
            HttpWebRequest wreq = HttpWebRequest.Create(resourceUrl) as HttpWebRequest;
            wreq.UseDefaultCredentials = false;
            NetworkCredential credentials = new NetworkCredential(username, password, domain);
            wreq.Credentials = credentials;
            string formDigest = GetFormDigestValue(siteUrl, credentials);
            wreq.Headers.Add("X-RequestDigest", formDigest);
            wreq.Method = "POST";
            //wreq.Timeout = 1000000; //timeout should be large in order to upload file which are of large size
            wreq.Accept = "application/json; odata=verbose";
            try
            {
                WebResponse wresp = wreq.GetResponse();
                using (System.IO.StreamReader sr = new System.IO.StreamReader(wresp.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    status = true;
                    return status;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return status;
                throw;
            }
        }
        public bool RemoveFilePermissions(string fileName, string id)
        {
            bool status = false;
            string result = string.Empty;
            //string resourceUrl = siteUrl + "/_api/Web/GetFileByServerRelativeUrl('" + documentLibraryToSharepointPath + "/" + fileName + "')/ListItemAllFields/roleassignments/removeroleassignment(principalid=" + 3 + ",roledefid=" + ReadRoleDefinition + ")";
            string resourceUrl = siteUrl + "/_api/Web/GetFileByServerRelativeUrl('" + documentLibraryToSharepointPath + "/" + fileName + "')/ListItemAllFields/roleassignments/removeroleassignment(principalid=" + id + ")";
            HttpWebRequest wreq = HttpWebRequest.Create(resourceUrl) as HttpWebRequest;
            wreq.UseDefaultCredentials = false;
            NetworkCredential credentials = new NetworkCredential(username, password, domain);
            wreq.Credentials = credentials;
            string formDigest = GetFormDigestValue(siteUrl, credentials);
            wreq.Method = "POST";
            wreq.Accept = "application/json; odata=verbose";
            wreq.Headers.Add("X-RequestDigest", formDigest);
            wreq.Headers.Add("X-HTTP", "DELETE");
            //wreq.Timeout = 1000000; //timeout should be large in order to upload file which are of large size
            try
            {
                WebResponse wresp = wreq.GetResponse();
                using (StreamReader sr = new StreamReader(wresp.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    status = true;
                    return status;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return status;
                throw;
            }
        }

        public bool AddFilePermissions(string fileName, string id)
        {
            bool status = false;
            string result = string.Empty;
            string resourceUrl = siteUrl + "/_api/Web/GetFileByServerRelativeUrl('" + documentLibraryToSharepointPath + "/" + fileName + "')/ListItemAllFields/roleassignments/addroleassignment(principalid=" + id + ",roledefid=" + ReadRoleDefinition + ")";
            //string resourceUrl = siteUrl + "/_api/Web/GetFileByServerRelativeUrl('" + documentLibraryToSharepointPath + "/" + fileName + "')/ListItemAllFields/roleassignments/removeroleassignment(principalid=7)";
            HttpWebRequest wreq = HttpWebRequest.Create(resourceUrl) as HttpWebRequest;
            wreq.UseDefaultCredentials = false;
            NetworkCredential credentials = new NetworkCredential(username, password, domain);
            wreq.Credentials = credentials;
            string formDigest = GetFormDigestValue(siteUrl, credentials);
            wreq.Method = "POST";
            wreq.Accept = "application/json; odata=verbose";
            wreq.Headers.Add("X-RequestDigest", formDigest);
            //wreq.Headers.Add("X-HTTP", "DELETE");
            //wreq.Timeout = 1000000; //timeout should be large in order to upload file which are of large size
            try
            {
                WebResponse wresp = wreq.GetResponse();
                using (StreamReader sr = new StreamReader(wresp.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    status = true;
                    return status;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return status;
                throw;
            }
        }

        public bool Delete(string file)
        {

            string pathToDelete = siteUrl + documentLibraryToSharepointPath + "/" + file;
            HttpWebRequest wreq = HttpWebRequest.Create(pathToDelete) as HttpWebRequest;
            wreq.UseDefaultCredentials = false;
            NetworkCredential credentials = new NetworkCredential(username, password, domain);
            wreq.Credentials = credentials;
            wreq.Method = "DELETE";

            try
            {
                var resp = wreq.GetResponse();
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
                throw;
            }
        }
    }

}
