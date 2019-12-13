using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma.EntityLibrary.Data
{
   public  class Package:BaseReference
    {
        private tb_Package packages;
        public Package() { }
        public Package(tb_Package crs) { packages = crs; }
        public Package(long Packageid) { packages = _Entities.tb_Package.FirstOrDefault(x => x.PackageID == Packageid); }
        public long CourseID { get { return packages.CourseID; } }
        public long PackageID { get { return packages.PackageID; } }

        public string PackageName { get { return packages.Name; } }

        public string Type { get { return packages.Type; } }


        public decimal Amount { get { return packages.Amount; } }

        public decimal DiscountAmount { get { return Convert.ToDecimal(packages.DiscountAmount); } }


        public int ExpiryDays { get { return packages.ExpiryDays; } }

        public string Description { get { return packages.Description; } }

        public string Files { get { return packages.Files; } }
        public bool IsActive { get { return packages.Isactive; } }
        public System.DateTime TimeStamp { get { return packages.TimeStamp; } }

        public System.DateTime ExpiryDate { get { return packages.ExpiryDate; } }

        public List<Package> GetPackages()
        {
            var xx = _Entities.tb_Package.Where(x => x.Isactive == true).OrderByDescending(c => c.PackageID).ToList().Select(q => new Package(q)).ToList();
            return xx;
        }

        public List<Subject> GetSubjects()
        {
            return packages.tb_Subjects.Where(x => x.PackageID == packages.PackageID && x.Isactive == true).OrderByDescending(c => c.SubjectID).ToList().Select(q => new Subject(q)).ToList();
            
        }

        public List<Package> GetPackagesbyid(long courseid)
        {
            return _Entities.tb_Package.Where(x => x.Isactive == true && x.CourseID == courseid).OrderByDescending(c => c.PackageID).ToList().Select(q => new Package(q)).ToList();
            
        }

        public List<Package> GetPackagesusingpackageid(long packageid)
        {
            var xx = _Entities.tb_Package.Where(x => x.Isactive == true && x.PackageID == packageid).OrderByDescending(c => c.PackageID).ToList().Select(q => new Package(q)).ToList();
            return xx;
        }


    }
}
