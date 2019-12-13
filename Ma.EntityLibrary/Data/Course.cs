using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ma.EntityLibrary.Data
{
  public  class Course :BaseReference
    {
        private tb_Course Courses;
        public Course() { }
        public Course(tb_Course crs) { Courses = crs; }
        public Course(long classId) { Courses = _Entities.tb_Course.FirstOrDefault(x => x.CourseId == classId); }
        public long CourseId { get { return Courses.CourseId; } }
        public string CourseName { get { return Courses.CourseName; } }
        public bool IsActive { get { return Courses.IsActive; } }
        public System.DateTime TimeStamp { get { return Courses.TimeStamp; } }
        public string CourseSubjectName { get { return Courses.CourseSubjectName; } }
        public decimal? Price { get { return Courses.Price; } }
        public string Duration { get { return Courses.Duration; } }
        public string Details { get { return Courses.Details; } }
        public string Files { get { return Courses.Files; } }

        public List<Course> GetCourses()
        {
            var xx= _Entities.tb_Course.Where(x=> x.IsActive == true).OrderByDescending(x => x.CourseId).ToList().Select(q => new Course(q)).ToList();
            return xx;
        }
        public List<Course> GetUserviewCourses()
        {
            return _Entities.tb_Course.Where(x => x.IsActive == true).OrderBy(x => x.CourseId).ToList().Select(q => new Course(q)).ToList();
           
        }
        public List<Package> GetPackages()
        {
            return Courses.tb_Package.Where(z => z.CourseID == Courses.CourseId && z.Isactive).OrderByDescending(z => z.PackageID).ToList().Select(q => new Package(q)).ToList();
        }
        public List<Course> GetUserviewCoursesbyID(long courseid)
        {
            return _Entities.tb_Course.Where(x => x.IsActive == true && x.CourseId == courseid).OrderBy(x => x.CourseId).ToList().Select(q => new Course(q)).ToList();

        }
        public List<Course> Getcoursesforuserid(long courseid)
        {
            return _Entities.tb_Course.Where(x => x.IsActive == true && x.CourseId == courseid).OrderBy(x => x.CourseId).ToList().Select(q => new Course(q)).ToList();

           
          
        }

   
    }
}
