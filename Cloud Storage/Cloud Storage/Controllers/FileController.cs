using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace Cloud_Storage.Controllers
{
    public class FileController : Controller
    {
        #region Global vars

        // object of ftp-server
        private LoadBalancerSvc.LoadBalancerClient _svc = new LoadBalancerSvc.LoadBalancerClient();
        // Extensions of files
        private const string extensions = ".docx.doc.pdf.pptx.ptx.xls.xlsx.txt";
        // Count of files in the row
        private int _countOfFiles = 5;

        #endregion

        // GET: File
        public ViewResult DisplayFiles(string folder)
        {
            //return View(new FilesNFolders());

            if (HttpContext.User.Identity.IsAuthenticated == false) { return View("~/Views/Account/Login.cshtml"); }

            if (folder != HttpContext.User.Identity.Name) { Session["path"] += $"/{folder}"; }

            // Create object for stroing types of files
            var files = new FilesNFolders();
            
            // Connecting to ftp-server and getting files and folders
            var filesAndFolders = _svc.DisplayFiles((string)Session["path"]);
            
            // Sorting file object
            var sortedFolders = filesAndFolders.Where(x => x[0] == '#').Select(x => x.Substring(1));
            var sortedFiles = filesAndFolders.Where(x => x[0] != '#');
            
            // Creating model for view 
            files.Directories = this.TakeAndFiltFolders(sortedFolders, _countOfFiles);
            files.Files = this.TakeAndFiltFiles(this.CreateFileDictionary(sortedFiles), _countOfFiles);

            return View(files);
        }

        public ActionResult DeleteAccount(string name)
        {

            var path = HttpContext.Request;
            var user = HttpContext.User.Identity.Name;
            return View();
        }

        public ActionResult CreateAccount(string name) { return null; }

        public ActionResult DownloadFile(string name) { return null; }

        public ActionResult DownloadFiles(List<string> list) { return null; }

        public ActionResult UploadFile(Stream file) { return null; }

        public ActionResult UploadFiles(HttpPostedFileBase[] files)
        {

            //Ensure model state is valid  
            /* if (ModelState.IsValid)
             {   //iterating through multiple file collection   
                 foreach (HttpPostedFileBase file in files)
                 {
                     //Checking file is available to save.  
                     if (file != null)
                     {
                         var InputFileName = Path.GetFileName(file.FileName);
                         var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                         //Save file to server folder  
                         file.SaveAs(ServerSavePath);
                         //assigning file uploaded status to ViewBag for showing message to user.  
                         ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                     }

                 }
             }*/
            return View();
        }

        public ActionResult DeleteFile(string name) { return null; }

        public ActionResult DeleteFiles(List<string> list) { return null; }

        public ActionResult CreateFile(string name) { return null; }

        public ActionResult SaveEditedFile(string name) { return null; }

        public ActionResult OpenFile(string name) { return null; }

        public ActionResult CreateFolder(string name) { return null; }

        public ActionResult DeleteFolder(string name) { return null; }


        #region Helpers

        private List<string[]> TakeAndFiltFolders(IEnumerable<string> files, int itemPerRow)
        {
            List<string[]> list = new List<string[]>();

            for (int i = 0; i < (int)Math.Ceiling((decimal)files.Count() / itemPerRow); i++)
            {
                list.Add(files.Skip(i * itemPerRow).Take(itemPerRow).ToArray());
            }

            return list;
        }

        private List<Dictionary<string, string>> TakeAndFiltFiles(Dictionary<string, string> dict, int itemPerRow)
        {
            var list = new List<Dictionary<string, string>>();

            for (int i = 0; i < (int)Math.Ceiling((decimal)dict.Count() / itemPerRow); i++)
            {
                list.Add(dict.Skip(i * itemPerRow).Take(itemPerRow).ToDictionary(x => x.Key, x => x.Value));
            }

            return list;
        }

        private Dictionary<string, string> CreateFileDictionary(IEnumerable<string> files)
        {
            var dict = new Dictionary<string, string>();

            foreach (var file in files)
            {
                var index = file.IndexOf('.');
                string extension = string.Empty;

                if (index > 0)
                {
                    if (extensions.Contains(file.Substring(index))) { extension = file.Substring(index + 1); }
                    else { extension = "unknown"; }
                }
                else { extension = "unknown"; }

                dict.Add(file, extension);
            }

            return dict;
        }

        #endregion

        //private void ЗагрузкаФайла()
        //{
        //    byte[] buffer = new byte[4096];
        //    byte[] memSize;
        //    int size = 0;
        //    var response = _svc.DonwloadFile("download/Sleeping_At_Last-Turning_Page_Instrument.mp3");

        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        while ((size = response.Read(buffer, 0, buffer.Length)) > 0)
        //        {
        //            memoryStream.Write(buffer, 0, size);
        //        }
        //        memSize = memoryStream.ToArray();
        //    }

        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + "FreeMemory.txt");
        //    Response.AddHeader("Content-Length", memSize.Length.ToString());
        //    Response.ContentType = "application/octet-stream";
        //    Response.OutputStream.Write(memSize, 0, memSize.Length);
        //    Response.End();

        //    response.Close();
        //    _svc.Close();
        //}
    }

    public class FilesNFolders
    {
        public IEnumerable<string[]> Directories { get; set; }
        public IEnumerable<Dictionary<string, string>> Files { get; set; }
    }
}