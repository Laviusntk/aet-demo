using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AETProject.Controllers
{
    public class ScholarhipDocsController : Controller
    {
        // GET: ScholarhipDocs
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]  
        public string UploadFile(HttpPostedFileBase  file)  
        {  
            try  
            {  
                if (file.ContentLength > 0)  
                {  
                    string _FileName = Path.GetFileName(file.FileName);  
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);  
                    file.SaveAs(_path);  
                }  
                //ViewBag.Message = "File Uploaded Successfully!!";  
                return "File Uploaded Successfully!!";  
            }  
            catch  
            {  
                //ViewBag.Message = "File upload failed!!";  
                return "File upload failed!!";  
            }  
        }
    }
}