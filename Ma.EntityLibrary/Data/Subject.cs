using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma.EntityLibrary.Data
{
    public class Subject:BaseReference
    {

        private tb_Subjects Subjects;
        public Subject() { }
        public Subject(tb_Subjects crs) { Subjects = crs; }
        public Subject(long classId) { Subjects = _Entities.tb_Subjects.FirstOrDefault(x => x.SubjectID  == classId); }
        public long SubjectID { get { return Subjects.SubjectID; } }
        public long PackageID { get { return Subjects.PackageID; } }
        public long? GroupID { get { return Subjects.GroupID; } }
        public string SubjectName { get { return Subjects.SubjectName; } }
        public long CourseID { get { return Subjects.CourseID; } }
        public List<Subject> GetSubjects()
        {
            return _Entities.tb_Subjects.Where(x => x.Isactive == true).ToList().Select(q => new Subject(q)).OrderByDescending(c => c.SubjectID).ToList();
        }
        public List<Subject> GetSubjectsbyid(long packageid)
        {
            return _Entities.tb_Subjects.Where(x => x.Isactive == true && x.PackageID == packageid).ToList().Select(q => new Subject(q)).OrderByDescending(c => c.SubjectID).ToList();
        }
    }
}
