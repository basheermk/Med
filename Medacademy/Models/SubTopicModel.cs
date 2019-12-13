using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medacademy.Models
{
    public class SubTopicModel
    {
        [Required(ErrorMessage = "Topic Required")]
        public long TopicID { get; set; }
           
        public long SubTopicID { get; set; }

        [Required(ErrorMessage = "SubTopic Name Required")]
        public string SubTopicName { get; set; }

        public string Pdfpath { get; set; }

        public string Videopath { get; set; }

        public string extension { get; set; }

        /////////////Sibi
        public long TopicID_Edit { get; set; }
        public decimal ID { get; set; }
        public string Name { get; set; }
        public Nullable<long> FileSize { get; set; }
        public string FilePath { get; set; }
        public List<SelectListItem> SelectListItems { get; set; }
        public List<SubTopicModel> SubTopicModel_List { get; set; }
        public string PDFName { get; set; }
        public string YouTubeVideo { get; set; }


        public long CourseID { get; set; }

        public long PackageID { get; set; }

        public long SubjectID { get; set; }




    }
}