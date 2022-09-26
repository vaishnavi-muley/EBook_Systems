using EBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBook.Controllers
{
    public class HomeController : Controller
    {

        EBook_SystemEntities db = new EBook_SystemEntities();

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Books(string search)
        {
            var model = db.Books.Where(b => b.Name.StartsWith(search) || search == null).ToList();
            return View(model);
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

        [HttpPost]
        public ActionResult Feedback(Feedback f)
        {
            db.Feedbacks.Add(f);
            db.SaveChanges();
            ModelState.Clear();
            return View();
        }

        public ActionResult Feedback_Reply()
        {
            return View(db.Feedbacks.ToList());

    
        }
    }
}