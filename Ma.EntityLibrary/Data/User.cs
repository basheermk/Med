using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma.EntityLibrary.Data
{
    public class User : BaseReference
    {
        private tb_Login Users;

        public User() { }
        public User(tb_Login crs) { Users = crs; }
        public User(long topicid) { Users = _Entities.tb_Login.FirstOrDefault(x => x.UserId == UserId); }

        public long UserId { get { return Users.UserId; } }
        public string Password { get { return Users.Password; } }
        public string FirstName { get { return Users.FirstName; } }
        public string LastName { get { return Users.LastName; } }
        public string Gender { get { return Users.Gender; } }
        public string Email { get { return Users.Email; } }
        public string ContactNo { get { return Users.ContactNo; } }
        public Nullable<System.DateTime> DOB { get { return Users.DOB; } }
        public string SchoolName { get { return Users.SchoolName; } }
        public int? RoleId { get { return Users.RoleId; } }
        public bool? IsActive { get { return Users.IsActive; } }
        public System.DateTime? TimeStamp { get { return Users.TimeStamp; } }
        public System.Guid? UserGuid { get { return Users.UserGuid; } }
        public string Location { get { return Users.Location; } }
        public string State { get { return Users.State; } }
        public string Address { get { return Users.Address; } }
        public string PostalCode { get { return Users.PostalCode; } }
        public Nullable<bool> DisableStatus { get { return Users.DisableStatus; } }
        public string ReferenceCode { get { return Users.ReferenceCode; } }
        public string PromoCode { get { return Users.PromoCode; } }
        public string Pin { get { return Users.Pin; } }
        public string Districts { get { return Users.Districts; } }
        public long? CourseId { get { return Users.CourseId; } }
        public string FilesName { get { return Users.FilesName; } }
        public string SessionId { get { return Users.SessionId; } }
    }
}
