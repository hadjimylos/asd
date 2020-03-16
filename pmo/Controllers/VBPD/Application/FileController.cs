using AutoMapper;
using dbModels;
using forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using pmo.Services.SharePoint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using ViewModels;
using ViewModels.Helpers;

namespace pmo.Controllers {
    [Route("projects/{projectId}/stages/{stageNumber}/files")]
    public class FileController : BaseStageComponentController {
        private readonly string documentLibraryToSharepointPath = $"{Config.AppSettings["Sharepoint:documentLibraryToSharepointPath"]}";
        private readonly ISharePointService _SharePointService;
        private readonly string path = "~/Views/VBPD/Application/Files";

        public FileController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, ISharePointService SharePointService) : base(context, mapper, httpContextAccessor) {
            _SharePointService = SharePointService;
        }

        public IActionResult Detail() {
            var model = _context.StageFiles.Where(w => w.StageId == _stageId).ToList();
            return View($"{path}/Detail.cshtml", model);
        }

        [Route("edit")]
        public IActionResult Edit() {
            List<FileForm> model = new List<FileForm>();

            var uploadedFiles = _context.StageFiles.Where(
                w =>
                    w.StageId == _stageId
            ).Select(
                s =>
                    s.FileTagId
            ).ToList();

            var requiredFiles = !_isLite ?
                _context.StageFileConfigs.IncludeAll().Where(
                    w =>
                        w.StageConfigId == _stageConfigId &&
                        !uploadedFiles.Contains(w.RequiredFileTagId)
                ).GetRequiredFiles() :
                _context.LiteStageFileConfigs.IncludeAll().Where(
                    w =>
                        w.StageConfigId == _stageConfigId &&
                        !uploadedFiles.Contains(w.RequiredFileTagId)
                ).GetRequiredFiles();

            requiredFiles.ForEach(requiredFile => {
                model.Add(
                    new FileForm {
                        TagDescription = requiredFile.Name,
                        TagId = requiredFile.Id,
                    });
            });

            // list of uploaded files
            ViewBag.Uploaded = _context.StageFiles
                .IncludeAll()
                .Where(
                    w =>
                        w.StageId == _stageId
                ).ToList();

            return View($"{path}/Edit.cshtml", model);
        }

        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Edit(List<FileForm> files) {
            var saveFiles = files.Where(w => w.File != null).ToList();
            var projectName = _context.Projects.Find(_projectId).Name;

            saveFiles.ForEach(f => {
                var upload = _SharePointService.Upload(f.File, projectName);
                var result = JObject.Parse(upload.Result);
                string relative = result["d"]["ServerRelativeUrl"].ToString();
                string name = result["d"]["Name"].ToString();
                string savePath = $"{Config.AppSettings["Sharepoint:SPFarm"]}{relative}";
                _context.StageFiles.Add(new StageFile {
                    Url = savePath,
                    Description = f.Description,
                    StageId = _stageId,
                    FileTagId = f.TagId,
                });
            });

            _context.SaveChanges();
            return this._editAction;
        }

        [Route("download")]
        public IActionResult Download() {
            using (var client = new WebClient()) {
                client.Headers.Add(HttpRequestHeader.Accept, "application/octet-stream");
                client.Headers.Add("binaryStringRequestBody", "true");
                client.UseDefaultCredentials = false;
                NetworkCredential credentials = new System.Net.NetworkCredential("svc-gbl-PMOPortalT", "t3YzY61htj63FQK", "Global");
                client.Credentials = credentials;
                string siteUrl = $"{Config.AppSettings["Sharepoint:SPFarm"]}/{Config.AppSettings["Sharepoint:SPSite"]}";
                string formDigest = GetFormDigestValue(siteUrl, credentials);
                client.Headers.Add("X-RequestDigest", formDigest);

                var endpointUri = new Uri("https://testshareit.itt.com/sites/pmo-staging/_api/web/getfilebyserverrelativeurl('/sites/pmo-staging/PMO_UAT/light-bulb_16032020-105039_ContentEditorHTML.txt')/OpenBinaryStream");
                var result = client.DownloadData(endpointUri);
                MemoryStream stream = new MemoryStream(result);
                System.IO.File.WriteAllBytes(@"C:\Users\efthimios.dellis\source\repos\PMO",result);
                return Ok();
            }
        }
            
     

            //    string result = string.Empty;
            //    string pathToUpload = "https://testshareit.itt.com/sites/pmo-staging/_api/Web/GetFileByServerRelativeUrl(/sites/pmo-staging/PMOTestLibrary/"+filename+")/$value";
            //    HttpWebRequest wreq = HttpWebRequest.Create(pathToUpload) as HttpWebRequest;
            //    wreq.UseDefaultCredentials = false;
            //    //credential who has edit access on document library
            //    NetworkCredential credentials = new System.Net.NetworkCredential("svc-gbl-PMOPortalT", "t3YzY61htj63FQK", "Global");
            //    wreq.Credentials = credentials;
            //    //Get formdigest value from site
            //    string formDigest = GetFormDigestValue(siteUrl, credentials);

            //    wreq.Headers.Add("X-RequestDigest", formDigest);
            //    wreq.Method = "GET";
            //    wreq.Timeout = 1000000; //timeout should be large in order to upload file which are of large size
            //                            //wreq.Accept = "application/json; odata=verbose";
            //    wreq.Accept = "application/octet-stream";
            //    wreq.ContentType = "application/octet-stream";
            //    //wreq.ContentLength = file.Length;
            //    //using (System.IO.Stream requestStream = wreq.GetRequestStream())
            //    //{
           

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

        [Route("delete")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Delete(string url, int fileid) {
            _SharePointService.Delete(url);
            _context.Remove(_context.StageFiles.Find(fileid));
            _context.SaveChanges();
            return RedirectToAction("Edit", new { projectId = _projectId, stageNumber = _stageNumber });
        }
    }
}