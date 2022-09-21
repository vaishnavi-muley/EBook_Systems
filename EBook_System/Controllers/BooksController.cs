using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EBook_System.Models;

namespace EBook_System.Controllers
{
    public class BooksController : Controller
    {
         EBook_SystemEntities db = new EBook_SystemEntities();

        // GET: Books
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        public ActionResult Create()
        // GET: Books/Create
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid == true)
            {
                string fileName = Path.GetFileNameWithoutExtension(book.ImageFile.FileName);
                string PdfName = Path.GetFileNameWithoutExtension(book.PdfFile.FileName);



                string extension = Path.GetExtension((book.ImageFile.FileName));
                string pdfextension = Path.GetExtension((book.PdfFile.FileName));



                HttpPostedFileBase postedfile = book.ImageFile;
                HttpPostedFileBase postedfile_pdf = book.PdfFile;




                int length = postedfile.ContentLength;


                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png" || extension.ToLower() == ".pdf")
                {
                    if (length <= 1550000000)
                    {
                        fileName = fileName + extension;
                        book.Image_path = "~/images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                        book.ImageFile.SaveAs(fileName);

                        PdfName = PdfName + pdfextension;
                        book.Pdf_path = "~/Pdf/"+PdfName;
                        PdfName = Path.Combine(Server.MapPath("~/Pdf/"), PdfName);
                        book.PdfFile.SaveAs(PdfName);
                        db.Books.Add(book);
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            TempData["CreateMessage"] = "<script>alert('Data Inserted Successfully.')</script>";
                            ModelState.Clear();
                            return RedirectToAction("Index", "Books");
                        }
                        else
                        {
                            TempData["CreateMessage"] = "<script>alert('Data Not Inserted.')</script>";
                        }
                    }
                    else
                    {
                        TempData["SizeMessage"] = "<script>alert('Image Size should be less than 1 MB')</script>";
                    }
                }
                else
                {
                    TempData["ExtensionMessage"] = "<script>alert('Format Not Supported')</script>";
                }             

            }

            return View();
        }















        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
            var BookRow = db.Books.Where(model => model.id == id).FirstOrDefault();

            Session["Image"] = BookRow.Image_path;
            Session["Pdfs"] = BookRow.Pdf_path;

            return View(BookRow);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Book book)
        {
            if (ModelState.IsValid == true)
            {


                if (book.ImageFile != null)
                {

                    string fileName = Path.GetFileNameWithoutExtension(book.ImageFile.FileName);



                    string extension = Path.GetExtension(book.ImageFile.FileName);



                    HttpPostedFileBase postedFile = book.ImageFile;


                    int length = postedFile.ContentLength;

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (length <= 1550000000)
                        {
                            fileName = fileName + extension;
                            book.Image_path = "~/images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                            book.ImageFile.SaveAs(fileName);
                            ;


                            db.Entry(book).State = EntityState.Modified;
                            int a = db.SaveChanges();
                            if (a > 0)
                            {
                                string ImagePath = Request.MapPath(Session["Image"].ToString());
                                if (System.IO.File.Exists(ImagePath))
                                {
                                    System.IO.File.Delete(ImagePath);
                                }




                                TempData["UpdateMessage"] = "<script>alert('Data Updated Successfully.')</script>";
                                ModelState.Clear();
                                //return RedirectToAction("Create", "Books");
                            }
                            else
                            {
                                TempData["UpdateMessage"] = "<script>alert('Data Not Updated.')</script>";
                            }
                        }
                        else
                        {
                            TempData["SizeMessage"] = "<script>alert('Image Size should be less than 1 MB')</script>";
                        }
                    }
                    else
                    {
                        TempData["ExtensionMessage"] = "<script>alert('Format Not Supported')</script>";
                    }


                }


                if (book.PdfFile != null)
                {


                    string PdfName = Path.GetFileNameWithoutExtension(book.PdfFile.FileName);



                    string pdfextension = Path.GetExtension((book.PdfFile.FileName));


                    HttpPostedFileBase postedfile_pdf = book.PdfFile;

                    int length = postedfile_pdf.ContentLength;

                    if (pdfextension.ToLower() == ".jpg" || pdfextension.ToLower() == ".jpeg" || pdfextension.ToLower() == ".png" || pdfextension.ToLower() == ".pdf")
                    {
                        if (length <= 1000000)
                        {

                          /*  PdfName = PdfName + pdfextension;
                            book.Pdf_path = "~/Pdf/" + PdfName;
                            PdfName = Path.Combine(Server.MapPath("~/Pdf/"), PdfName);
                            book.PdfFile.SaveAs(PdfName);
                            db.Books.Add(book);

                            int a = db.SaveChanges();*/



                            PdfName = PdfName + pdfextension;
                            book.Pdf_path = "~/Pdf/" + PdfName;
                            book.Pdf_Filename = PdfName;
                            PdfName = Path.Combine(Server.MapPath("~/Pdf/"), PdfName);
                            book.PdfFile.SaveAs(PdfName);


                            db.Entry(book).State = EntityState.Modified;

                            int a = db.SaveChanges();
                            if (a > 0)
                            {

                                String virtual_path = Server.MapPath("~/Pdf/" + book.Pdf_Filename);
                                string PdfPath = Request.MapPath(book.Pdf_path);
                                if (System.IO.File.Exists(PdfPath))
                                {
                                    System.IO.File.Delete(PdfPath);
                                }



                                TempData["UpdateMessage"] = "<script>alert('Data Updated Successfully.')</script>";
                                ModelState.Clear();
                                return RedirectToAction("Create", "Books");
                            }
                            else
                            {
                                TempData["UpdateMessage"] = "<script>alert('Data Not Updated.')</script>";
                            }
                        }
                        else
                        {
                            TempData["SizeMessage"] = "<script>alert('Image Size should be less than 1 MB')</script>";
                        }
                    }
                    else
                    {
                        TempData["ExtensionMessage"] = "<script>alert('Format Not Supported')</script>";
                    }


                }

                else


                {
                    book.Image_path = Session["Image"].ToString();
                    book.Pdf_path = book.Pdf_path.ToString();
                    db.Entry(book).State = EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data Updated Successfully.')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Create", "Books");
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script>alert('Data Not Updated.')</script>";
                    }


                }








              
               
                
                
                
            

            }
            return View();
        }














        [HttpGet]
        public FileResult DownLoadFile(int id)
        {
            List<Book> ObjFiles = db.Books.ToList();

            var FileById = (from FC in ObjFiles
                            where FC.id.Equals(id)
                            select new { FC.Pdf_path }).ToList().FirstOrDefault();

            //Build the File Path.
            string path = Server.MapPath(FileById.Pdf_path);

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream","testfile.pdf");

        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }


        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
    }
}
