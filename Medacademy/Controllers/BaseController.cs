using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ma.ClassLibrary;
using Ma.EntityLibrary;


namespace Medacademy.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public MAEntities Entities = new MAEntities();
    }
}