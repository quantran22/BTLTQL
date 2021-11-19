using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTQL.Models;

namespace LTQL.Controllers
{
    public class StudentController : Controller
    {
        LTQLDBContext db = new LTQLDBContext();
        StringProcess genKey = new StringProcess();
        // GET: Student
        public ActionResult Index()
        {
            var model = db.Students.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            var stdID = "";
            var countStudent = db.Students.Count();
            if (countStudent == 0)
            {
                stdID = "STD001";
            }
            else
            {

                var studentID = db.Students.ToList().OrderByDescending(m => m.StudentID).FirstOrDefault().StudentID;

                stdID = genKey.GenerateKey(studentID);
            }
            ViewBag.StudentID = stdID;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student std)
        {
            if (ModelState.IsValid)
            {

                db.Students.Add(std);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(std);
        }

    }
}