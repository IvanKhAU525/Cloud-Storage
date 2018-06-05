using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cloud_Storage.Controllers
{
    public class MobileServerController : Controller
    {
        // GET: MobileServer
        public string Upload(IEnumerable<HttpPostedFileBase> uploads)
        {
            if (uploads != null)
            {
                //uploads.First().
                //return service.PushFiles(uploads.ToArray());
            }

            return "nothing";
        }
        public string GetFile()
        {
            return "Success!";
        }
    }
}