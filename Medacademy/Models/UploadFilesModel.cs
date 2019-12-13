using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class UploadFilesModel
    {
        public string UserEmail { get; set; }
        public int FileSizeLimitMB { get; set; } = 12;
        public int MaxFiles { get; set; }
        public string ContentTypes { get; set; }
    }
}