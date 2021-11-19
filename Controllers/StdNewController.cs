﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTQL.Models;


namespace LTQL.Controllers
{

    public class StdNewController : Controller
    {
        LTQLDBContext db = new LTQLDBContext();
        StringProcess genKey = new StringProcess();
        // GET: StdNew
        public ActionResult Index()
        {

            return View(db.Students.ToList());
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
            ViewBag.StudnetID = stdID;
            return View();
        }
        [HttpPost]
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
    
