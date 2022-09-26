using EBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBook.Controllers
{
    public class AdminController : Controller
    {
        EBook_SystemEntities db = new EBook_SystemEntities();
        // GET: Admin
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(AdminLogin al)
        {
            var lg = db.AdminLogins.Where(a => a.Username.Equals(al.Username) && a.Password.Equals(al.Password)).FirstOrDefault();

            if (lg != null)
            {
                return RedirectToAction("index", "Books");

            }

            else
            {
                ViewBag.MyMessage = "Invalid Login";


            }

            ModelState.Clear();

            return View();
        }

       

    }
}
