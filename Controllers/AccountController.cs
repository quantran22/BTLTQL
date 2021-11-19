using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LTQL.Models;

namespace LTQL.Controllers
{
    public class AccountController : Controller
    {
        Encrytion encry = new Encrytion();
        LTQLDBContext db = new LTQLDBContext();
        StringProcess strPro = new StringProcess();
        

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(Account acc)
        {
            if (ModelState.IsValid)
            {
                //mã hóa mật khẩu trước khi lưu database
                acc.Password = encry.PasswordEncrytion(acc.Password);
                db.Accounts.Add(acc);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View(acc);
        }
        [HttpGet]
        [AllowAnonymous]
        
        public ActionResult Login(string returnUrl)
        {
            if (checkSession() !=0)
            {
                return RedirectToLocal(returnUrl);
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(Account acc, string returnUrl)
        {
            try
            {
                if(!string.IsNullOrEmpty(acc.Username)&&!string.IsNullOrEmpty(acc.Password))
                {
                    using (var db = new LTQLDBContext())
                    {
                        var passToMD5 = strPro.GetMD5(acc.Password);
                        var account = db.Accounts.Where(m => m.Username.Equals(acc.Username) && m.Password.Equals(passToMD5)).Count();
                        if (account == 1)
                        {
                            FormsAuthentication.SetAuthCookie(acc.Username, false);
                            Session["idUser"] = acc.Username;
                            Session["roleUser"] = acc.RoleID;
                            return RedirectToLocal(returnUrl);
                        }
                        ModelState.AddModelError("", "Thông tin đăng nhập không chính xác");
                    }
                }
                ModelState.AddModelError("", "Username and password is requied.");
            }
            catch
            {
                ModelState.AddModelError("", "Hệ thống đang được bảo trì, vui lòng liên hệ với quản trị viên.");
            }
            return View(acc);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl)|| returnUrl =="/")
            {
                if(checkSession()==1)
                {
                    return RedirectToAction("Index", "HomeAdmin", new { Areas = "Admins" });
                }  
                else if(checkSession() ==2)
                {
                    return RedirectToAction("Index", "HomeEmp", new { Areas = "Employees" });
                }
            }    
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home_ad");
            }
        }
        //kiểm tra ng dùng đăng nhaaph theo quyền gì
        private int checkSession()
        {
            using (var db = new LTQLDBContext())
            {
                var user = HttpContext.Session["idUsser"];
                if (user != null)
                {
                    var role = db.Accounts.Find(user.ToString()).RoleID;
                    if (user != null)
                    {
                        if (role.ToString() == "admin")
                        {
                            return 1;
                        }
                        else if (role.ToString() == "NV")
                        {
                            return 2;
                        }
                    }
                }
            }
            return 0;
        }

    }
}