using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTQL.Areas.Employees.Controllers
{
    public class HomeEmpController : Controller
    {
        [Authorize(Roles ="NV")]
        // GET: Employees/HomeEmp
        public ActionResult Index()
        {
            return View();
        }
    }
}