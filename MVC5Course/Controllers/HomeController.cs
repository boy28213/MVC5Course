﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Unknown()
        {
            return View();
        }

        public ActionResult SomeAction()
        {
            //Response.Write("<script>alert('建立成功!'); location.href='/';</script>");
            //return "<script>alert('建立成功!'); location.href='/';</script>";
            //return Content("<script>alert('建立成功!'); location.href='/';</script>");

            return PartialView("SuccessRedirect", "/");
        }

        public ActionResult GetFile()
        {
            return File(Server.MapPath("~/Images/WannaCry.png"), "image/png", "WannaCry.png");
        }

        public ActionResult GetJson()
        {
            db.Configuration.LazyLoadingEnabled = false;

            return Json(db.Product.Take(5), JsonRequestBehavior.AllowGet);
        }

        [SharedViewBag]
        public ActionResult About()
        {
            throw new ArgumentException("Error Handled!!");

            return View();
        }

        [SharedViewBag(MyProperty = "")]
        public ActionResult PartialAbout()
        {
            //ViewBag.Message = "Your application page.";

            if (Request.IsAjaxRequest())
            {
                return PartialView("About");
            }
            else
            {
                return View("About");
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult VT()
        {
            ViewBag.IsEnabled = true;

            return View();
        }

        public ActionResult RazorTest()
        {
            int[] data = new int[] { 1, 2, 3, 4, 5 };

            return PartialView(data);
        }
    }
}