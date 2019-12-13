using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class PackageModel
    {
        public long CourseID { get; set; }
        public long PackageID { get; set; }
        [Required(ErrorMessage = "Package Name Required")]
        public string PackageName { get; set; }
        [Required(ErrorMessage = "Type Required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Amount Required")]
        public decimal Amount { get; set; }

        public decimal DiscountAmount { get; set; }

        [Required(ErrorMessage = "ExpiryDays Required")]
        public int ExpiryDays { get; set; }

        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }

        public bool  Isactive { get; set; }

        public string Packageimage { get; set; }
        public string Packageeditimage { get; set; }

        //sibi


        public System.DateTime TimeStamp { get; set; }
        public string Files { get; set; }

        public List<PackageModel> LI_Pacages { get; set; }

        public PaginationModel PaginationModel { get; set; }




    }
}