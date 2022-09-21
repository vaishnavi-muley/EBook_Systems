using EBook_System.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;

namespace EBook_System.Controllers
{
    public class AdminController : Controller
    {
       /// <summary>
       /// EBook_SystemEntities db = new EBook_SystemEntities();
       /// </summary>
        EBook_SystemEntities1 db1 = new EBook_SystemEntities1();
        // GET: Admin
        public ActionResult AdminLogin()
        {


            return View();
        }


        [HttpPost]
        public ActionResult AdminLogin(AdminLogin al)
        {
            var lg = db1.AdminLogins.Where(a => a.Username.Equals(al.Username) && a.Password.Equals(al.Password)).FirstOrDefault();

            if (lg != null)
            {
                return RedirectToAction("Create", "Books");

            }
          
            else
            {
                ViewBag.MyMessage = "Invalid Login";
               

            }

            return View();
        }

     
    }
}