using Medacademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Medacademy.Repository;

namespace Medacademy.Controllers
{
    public class LocalsController : Controller
    {
        public PartialViewResult pv_Local_packages(PackageModel models)
        {           
            var datas = (UserModel)Session["Localsession"];
            try
            {
                if (datas != null)
                {
                    PaginationRepository PaginationRepository = new PaginationRepository();

                    LocalModel LocalModel = new LocalModel();
                    LocalModel.LI_Pacages = PaginationRepository.PakagesLists(models.PaginationModel);

                    datas.LocalModel = LocalModel;

                    Session["Localsession"] = datas;
                    return PartialView("~/Views/Locals/pv_Local_packages.cshtml", datas);

                }

            }
            catch (Exception ex)
            {
                return PartialView("~/Views/User/SessionExpired.cshtml");
            }
            return PartialView("~/Views/User/SessionExpired.cshtml");


        }

        public PartialViewResult pv_Local_courses(PackageModel models)
        {
            var datas = (UserModel)Session["Localsession"];
            try
            {
                if (datas != null)
                {
                    PaginationRepository PaginationRepository = new PaginationRepository();

                    LocalModel LocalModel = new LocalModel();
                    LocalModel.CourseModel_Lists = PaginationRepository.CoursesLists(models.PaginationModel);

                    datas.LocalModel = LocalModel;

                    Session["Localsession"] = datas;
                    return PartialView("~/Views/Locals/pv_Local_courses.cshtml", datas);

                }

            }
            catch (Exception ex)
            {
                return PartialView("~/Views/User/SessionExpired.cshtml");
            }
            return PartialView("~/Views/User/SessionExpired.cshtml");


        }
    }
}