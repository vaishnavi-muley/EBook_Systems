using EBook_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace EBook_System.Controllers
{

    public class UserController : Controller
    {

        EBook_SystemEntities3 db3 = new EBook_SystemEntities3();
        // GET: User
        [HttpGet]
        public ActionResult Feedback()
        {
            return View();
        }
        
        
        [HttpPost]
        public ActionResult Feedback(Feedback f) {

            db3.Feedbacks.Add(f);
            db3.SaveChanges();
            return View();

        }


        public ActionResult Feedback_Reply()
        {
            return View(db3.Feedbacks.ToList());
        }



    }
}
