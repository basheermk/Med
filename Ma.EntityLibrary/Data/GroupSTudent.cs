using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ma.EntityLibrary.Data
{
    public class GroupSTudent : BaseReference
    {
        private tb_GroupStudent Groups;
        public GroupSTudent() { }
        public GroupSTudent(tb_GroupStudent crs) { Groups = crs; }
        public GroupSTudent(long groupid) { Groups = _Entities.tb_GroupStudent.FirstOrDefault(x => x.GroupID == groupid); }
        public long ID { get { return Groups.ID; } }
        public long GroupID { get { return Groups.GroupID; } }
        public long StudentID { get { return Groups.StudentID; } }
        public bool IsActive { get { return Groups.IsActive; } }
        public long Checkstudentgroup(long userid)
        {
            return _Entities.tb_GroupStudent.Where(x => x.IsActive == true && x.StudentID == userid).ToList().Select(q => new GroupSTudent(q)).OrderBy(c => c.GroupID).Count();
        }
    }
}
