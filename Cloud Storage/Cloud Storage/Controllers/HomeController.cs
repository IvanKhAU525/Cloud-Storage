using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cloud_Storage.Controllers
{
    public class HomeController : Controller
    {
        [RequireHttps]
        public ActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return View("~/Views/Account/Login.cshtml");
            }

            //  to correct
            var name = HttpContext.User.Identity.Name;
            Session["path"] = name;

            return RedirectToAction("DisplayFiles", "File", new { folder = name });
        }

        [RequireHttps]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [RequireHttps]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [RequireHttps]
        public ActionResult UserFiles(string userName)
        {
            ViewBag.UserName = userName;
            return View();
        }

        public ActionResult UploadFiles()
        {
            throw new NotImplementedException();
        }
    }
}