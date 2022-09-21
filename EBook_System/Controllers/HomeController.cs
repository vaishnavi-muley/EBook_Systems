using EBook_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBook_System.Controllers
{

  
    public class HomeController : Controller
    {
       
        EBook_SystemEntities db1 = new EBook_SystemEntities();

        public ActionResult Index()
        {
            return View();
        }
       
        [HttpGet]
        public ActionResult Books(string search)
        {
            var model = db1.Books.Where(b => b.Name.StartsWith(search) || search == null).ToList();
            return View(model);
            //return View(db1.Books.ToList());
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Feedback()
        {

            return View();
        }

    }
}