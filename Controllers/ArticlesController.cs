using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LTQL.Models;

namespace LTQL.Controllers
{
    public class ArticlesController : Controller
    {
        private LTQLDBContext db = new LTQLDBContext();
        ExcelProcess ExcelPro = new ExcelProcess();
        SqlConnection con = new SqlConnection(ConfigurationManage.connectionStrings["LTQLDBContext"].ConnectionString);

        // GET: Articles
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string path = Server.MapPath("~/Files/");
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                file.SaveAs(path + System.IO.Path.GetFileName(file.FileName));
                ViewBag.Response = "Successful.";
            }

            return View();
        }

        // GET: Articles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArticleID,Author,ArticleContent")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleID,Author,ArticleContent")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private DataTable CopyDataFromExcelFile(HttpPostedFileBase file)
        {
            string fileExtention = file.FileName.Substring(file.FileName.IndexOf("."));
            string _FileName = "Ten_File_Muon_Luu" + fileExtention;
            string _path = Path.Combine(Server.MapPath("~/Upload/Excels"), _FileName);
            file.SaveAs(_path);
            DataTable dt = ExcelPro.ReadDataFromExcelFile(_path, false);
            return dt;
        }
        private void OverwriteFastData(int? ArticleID)
        {
            //dt là databasecos chứa dữ liệu để import vào database
            DataTable dt = new DataTable();

            //mapping các column trong database vào các column trong table ở CSDL
            SqlBulkCopy bulkcopy = new SqlBulkCopy(con);
            bulkcopy.DestinationTableName = "Article";
            bulkcopy.ColumnMappings.Add(0, "Column_Name_0");
            bulkcopy.ColumnMappings.Add(1, "Column_Name_1");
            bulkcopy.ColumnMappings.Add(2, "Column_Name_2");
            con.Open();
            bulkcopy.WriteToServer(dt);
            con.Close();
        }
    }
   

}
