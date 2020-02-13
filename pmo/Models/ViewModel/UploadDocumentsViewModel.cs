using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UploadDocumentsViewModel
    {
        public string ComponentName { get; set; }

        public int CurrentVersion { get; set; }

        public int ComponentId { get; set; }

        public int StageId { get; set; }

        public int ProjectId { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public string ControllerName { get; set; }
        public string Path { set; get; }

        [Required]
        public List<IFormFile> Files { get; set; }
    }
}
