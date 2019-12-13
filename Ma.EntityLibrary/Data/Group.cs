using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ma.EntityLibrary.Data
{
     public  class Group : BaseReference
    {
        private tb_Group Groups;
        public Group() { }
        public Group(tb_Group crs) { Groups = crs; }
        public Group(long classId) { Groups = _Entities.tb_Group.FirstOrDefault(x => x.Group_ID == classId); }
        public long? PackageID { get { return Groups.PackageID; } }
        public long? CourseID { get { return Groups.Course_ID; } }
        public long GroupID { get { return Groups.Group_ID; } }
        public string GroupName { get { return Groups.Groupname; } }
        public List<Group> GetGroups()
        {
            return _Entities.tb_Group.Where(x => x.Isactive == true).ToList().Select(q => new Group(q)).OrderBy(c => c.GroupID).ToList();     
        }
        public long GetGroupsbyuserid(long userid)
        {
            return _Entities.tb_GroupStudent.Where(x => x.IsActive == true && x.StudentID == userid).FirstOrDefault().GroupID;
        }
       
    }
}
