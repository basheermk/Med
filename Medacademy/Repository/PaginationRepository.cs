using Ma.EntityLibrary;
using Medacademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medacademy.Repository
{
    public class PaginationRepository
    {
        public MAEntities _Entities = new MAEntities();
        // Passing TotalItems And Divide Number
        public PaginationModel CalculatePage(PaginationModel model)
        {
            float TotalItems = model.TotalItems;

            string pageSize_String = System.Configuration.ConfigurationManager.AppSettings["Pakage_PageSize"];
            model.DividNumber = Convert.ToInt32(pageSize_String);

            float divid = model.DividNumber;
            string calcilateItems = Convert.ToString(TotalItems / divid);
            int reslts;
            bool res = Int32.TryParse(calcilateItems, out reslts);
            if (res == true)
            {
                model.NumberOfPages = Convert.ToInt32(calcilateItems);
            }
            else
            {
                string[] a1 = calcilateItems.Split('.');
                int a2 = Convert.ToInt32(a1[0]);
                model.NumberOfPages = Convert.ToInt32(a2 + 1);
            }
            return model;
        }
        public List<PackageModel> PakagesLists(PaginationModel model)
        {
            List<PackageModel> LI_PackageModel = new List<PackageModel>();
            try
            {
                var total = _Entities.tb_Package.Where(x => x.Isactive == true).Count();
                if (model.CourseID != 0)
                {
                    total = _Entities.tb_Package.Where(x => x.CourseID == model.CourseID && x.Isactive == true).Count();
                }


                string pageSize_String = System.Configuration.ConfigurationManager.AppSettings["Pakage_PageSize"];
                var pageSize = Convert.ToInt32(pageSize_String); // set your page size, which is number of records per page

                var page = model.PageNumber; // set current page number, must be >= 1 (ideally this value will be passed to this logic/function from outside)

                var skip = pageSize * (page - 1);

                var canPage = skip < total;

                if (!canPage) // do what you wish if you can page no further
                    return null;

                var datas = _Entities.tb_Package.Where(x => x.Isactive == true).OrderByDescending(x => x.PackageID).Skip(skip).Take(pageSize).ToList();

                if (model.CourseID != 0)
                {
                    datas = _Entities.tb_Package.Where(x => x.CourseID == model.CourseID && x.Isactive == true).OrderByDescending(x => x.PackageID).Skip(skip).Take(pageSize).ToList();

                }

                //var datas = _Entities.tb_Package.Where(x => x.Isactive == true).ToList();


                foreach (var a1 in datas)
                {
                    PackageModel PackageModel = new PackageModel();
                    PackageModel.PackageID = a1.PackageID;
                    PackageModel.CourseID = a1.CourseID;
                    PackageModel.PackageName = a1.Name;
                    PackageModel.Type = a1.Type;
                    PackageModel.Amount = a1.Amount;
                    PackageModel.DiscountAmount = (decimal)a1.DiscountAmount;
                    PackageModel.ExpiryDays = a1.ExpiryDays;
                    PackageModel.Description = a1.Description;
                    PackageModel.TimeStamp = a1.TimeStamp;
                    PackageModel.Files = a1.Files;

                    LI_PackageModel.Add(PackageModel);

                }
            }
            catch (Exception ex)
            {

            }



            return LI_PackageModel;
        }


        public PaginationModel Courses_CalculatePage(PaginationModel model)
        {
            float TotalItems = model.TotalItems;

            string pageSize_String = System.Configuration.ConfigurationManager.AppSettings["Courses_PageSize"];
            model.DividNumber = Convert.ToInt32(pageSize_String);

            float divid = model.DividNumber;
            string calcilateItems = Convert.ToString(TotalItems / divid);
            int reslts;
            bool res = Int32.TryParse(calcilateItems, out reslts);
            if (res == true)
            {
                model.NumberOfPages = Convert.ToInt32(calcilateItems);
            }
            else
            {
                string[] a1 = calcilateItems.Split('.');
                int a2 = Convert.ToInt32(a1[0]);
                model.NumberOfPages = Convert.ToInt32(a2 + 1);
            }
            return model;
        }

        public List<CourseModel> CoursesLists(PaginationModel model)
        {
            List<CourseModel> CourseModel_Lists = new List<CourseModel>();
            try
            {
                var total = _Entities.tb_Course.Where(x => x.IsActive == true).Count();
                if (model.CourseID != 0)
                {
                    total = _Entities.tb_Course.Where(x => x.CourseId == model.CourseID && x.IsActive == true).Count();
                }


                string pageSize_String = System.Configuration.ConfigurationManager.AppSettings["Courses_PageSize"];
                var pageSize = Convert.ToInt32(pageSize_String); // set your page size, which is number of records per page

                var page = model.PageNumber; // set current page number, must be >= 1 (ideally this value will be passed to this logic/function from outside)

                var skip = pageSize * (page - 1);

                var canPage = skip < total;

                if (!canPage) // do what you wish if you can page no further
                    return null;

                var datas = _Entities.tb_Course.Where(x => x.IsActive == true).OrderByDescending(x => x.CourseId).Skip(skip).Take(pageSize).ToList();

                if (model.CourseID != 0)
                {
                    datas = _Entities.tb_Course.Where(x => x.CourseId == model.CourseID && x.IsActive == true).OrderByDescending(x => x.CourseId).Skip(skip).Take(pageSize).ToList();

                }

                //var datas = _Entities.tb_Package.Where(x => x.Isactive == true).ToList();


                foreach (var a1 in datas)
                {
                    CourseModel CourseModel = new CourseModel();
                    CourseModel.CourseId = a1.CourseId;
                    CourseModel.CourseName = a1.CourseName;
                    CourseModel.Isactive = a1.IsActive;
                    CourseModel.TimeStamp = a1.TimeStamp;
                    CourseModel.CourseSubjectName = a1.CourseSubjectName;
                    if (a1.Price != null)
                    {
                        CourseModel.Price = (decimal)a1.Price;
                    }
                    CourseModel.Duration = a1.Duration;
                    CourseModel.Details = a1.Details;
                    CourseModel.Files = a1.Files;

                    CourseModel_Lists.Add(CourseModel);

                }
            }
            catch (Exception ex)
            {

            }



            return CourseModel_Lists;
        }

        public List<PackageModel> Home_PakagesLists(PaginationModel model)
        {
            List<PackageModel> LI_PackageModel = new List<PackageModel>();
            try
            {
                var total = _Entities.tb_Package.Where(x => x.Isactive == true).Count();
                if (model.CourseID != 0)
                {
                    total = _Entities.tb_Package.Where(x => x.CourseID == model.CourseID && x.Isactive == true).Count();
                }


                string pageSize_String = "4";
                var pageSize = Convert.ToInt32(pageSize_String); // set your page size, which is number of records per page

                var page = model.PageNumber; // set current page number, must be >= 1 (ideally this value will be passed to this logic/function from outside)

                var skip = pageSize * (page - 1);

                var canPage = skip < total;

                if (!canPage) // do what you wish if you can page no further
                    return null;

                var datas = _Entities.tb_Package.Where(x => x.Isactive == true).OrderByDescending(x => x.PackageID).Skip(skip).Take(pageSize).ToList();

                if (model.CourseID != 0)
                {
                    datas = _Entities.tb_Package.Where(x => x.CourseID == model.CourseID && x.Isactive == true).OrderByDescending(x => x.PackageID).Skip(skip).Take(pageSize).ToList();

                }

                //var datas = _Entities.tb_Package.Where(x => x.Isactive == true).ToList();


                foreach (var a1 in datas)
                {
                    PackageModel PackageModel = new PackageModel();
                    PackageModel.PackageID = a1.PackageID;
                    PackageModel.CourseID = a1.CourseID;
                    PackageModel.PackageName = a1.Name;
                    PackageModel.Type = a1.Type;
                    PackageModel.Amount = a1.Amount;
                    PackageModel.DiscountAmount = (decimal)a1.DiscountAmount;
                    PackageModel.ExpiryDays = a1.ExpiryDays;
                    PackageModel.Description = a1.Description;
                    PackageModel.TimeStamp = a1.TimeStamp;
                    PackageModel.Files = a1.Files;

                    LI_PackageModel.Add(PackageModel);

                }
            }
            catch (Exception ex)
            {

            }



            return LI_PackageModel;
        }


    }
}