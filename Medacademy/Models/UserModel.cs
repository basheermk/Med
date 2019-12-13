using Medacademy.Repository.Paytm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Medacademy.Models
{
    public class UserModel
    {
        public long UserId { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [MembershipPassword(
            MinRequiredNonAlphanumericCharacters = 1,
            MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).",
            ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc).",
            MinRequiredPasswordLength = 6
        )]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name Required")]
        
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Remote("IsEmailIDExist", "User", ErrorMessage = "EmialId is already exist")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        //[Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "State Required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Zip is Required")]
        [RegularExpression(@"^\d{6}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        public string Pin { get; set; }

        [Required(ErrorMessage = "Address Districts")]
        public string Districts { get; set; }
        public HttpPostedFileBase file { get; set; }

        [Required(ErrorMessage = "Gender Required")]
        public string Gender { get; set; }

        public long CourseId { get; set; }

        public string FilesName { get; set; }
        public string SessionId { get; set; }



        public string LastName { get; set; }
        
        
        public Nullable<System.DateTime> DOB { get; set; }
        public string SchoolName { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public System.Guid UserGuid { get; set; }
        public string Location { get; set; }
        public string PostalCode { get; set; }
        public Nullable<bool> DisableStatus { get; set; }
        public string ReferenceCode { get; set; }
        public string PromoCode { get; set; }

        public List<PackageModel> LI_Pacages { get; set; }
        public PackageModel PackageModel { get; set; }
        public long TempID1 { get; set; }
        public string ReturnUrl { get; set; }
        public PaymentModel PaymentModel { get; set; }
        public PaginationModel PaginationModel { get; set; }
        public CourseModel CourseModel { get; set; }
        public LocalModel LocalModel { get; set; }
        public List<CourseModel> CourseModel_Lists { get; set; }
        public List<PackageModel> Home_Pacages_List { get; set; }

        public long TopicId { get; set; }

        public long SubtopicID { get; set; }

        public long Nonusercourseid { get; set; }  //courseid for students not logedin

        public long PackageID { get; set; }
        public List<PaymentPaytmModel> PaymentModel_Lists { get; set; }
        public PaytmResponceModel PaytmResponceModel { get; set; }
        public PaymentPaytmModel PaymentPaytmModel { get; set; }
        public string Groups { get; set; }

        public string GroupsEdit { get; set; }

        public string Profileeditimage { get; set; }

        public long GroupID { get; set; }

        public bool? Ispaid { get; set; }

    }
}