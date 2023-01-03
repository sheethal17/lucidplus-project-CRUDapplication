using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using CRUDapplication.Models;
using System.IO;
using System.Net;
using PagedList;
using PagedList.Mvc;

namespace CRUDapplication.Controllers
{
    public class HomeController : Controller
    {

        private DemoEntities db = new DemoEntities();
        // GET: Home
        public ActionResult Index(int? page)
        {
            ViewBag.Count = db.MVCregUsers.Count();
            return View(db.MVCregUsers.OrderByDescending(x => x.ID).ToList().ToPagedList(page ?? 1, 5));
           
        }



        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MVCregUser mVCregUser = db.MVCregUsers.Find(id);
            if (mVCregUser == null)
            {
                return HttpNotFound();
            }
            return View(mVCregUser);
        }




        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(MVCregUser mVCregUser)

        {

            if (db.MVCregUsers.Any(x => x.Uname == mVCregUser.Uname))
            {
                ViewBag.Notification = "UserName already exist";
                return View();
            }
            else
            {

                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase file = Request.Files[0];
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            mVCregUser.Uimg = Path.Combine(
                                Server.MapPath("~/User-Images"), fileName);
                            file.SaveAs(mVCregUser.Uimg);

                        }

                        return View(mVCregUser);

                    }
                }

                db.MVCregUsers.Add(mVCregUser);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");

            }


        }




















       

        [HttpGet]
        public ActionResult Login()
        {

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(MVCregUser mVCregUser)
        {
            var checkLogin = db.MVCregUsers.Where(x => x.Uname.Equals(mVCregUser.Uname) && x.Upwd.Equals(mVCregUser.Upwd)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["IdUsSS"] = mVCregUser.ID.ToString();
                Session["UsernameSS"] = mVCregUser.Uname.ToString();
                return RedirectToAction("index", "Home");
            }
            else
            {
                ViewBag.Notification = "Wrong username or password";
            }
            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();
            //return RedirectToAction("Index", "Home");
            return RedirectToAction("Login");
        }


    


        [HttpPost]
    
        public ActionResult Delete(int id)
        {

          
            MVCregUser mVCregUser = db.MVCregUsers.Find(id);
            db.MVCregUsers.Remove(mVCregUser);
            db.SaveChanges();
            return RedirectToAction("Index");

        }


        







       










       



        [HttpGet]
        public ActionResult Edit(int id)
        {


            DemoEntities db = new DemoEntities();
            MVCregUser mVCregUser = db.MVCregUsers.Single(emp => emp.ID == id);

            
            return View(mVCregUser);
        }




        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(int id)

        {
            MVCregUser mVCregUser = db.MVCregUsers.FirstOrDefault(x => x.ID == id);
            UpdateModel(mVCregUser, new string[] { "ID", "Uname", "Uemail", "Ucountry", "Umob","Upwd" });
             try
            {
            using (DemoEntities db = new DemoEntities())
            {
            db.Entry(mVCregUser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            }
            return RedirectToAction("Index");
          
            }
            catch
             {
            return View();
            }


        }














    }
}