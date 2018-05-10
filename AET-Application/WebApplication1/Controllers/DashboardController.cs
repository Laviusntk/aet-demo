/*
* Title     : A script for dashboard ui interactions
* Authors   : Lavius N. Motileng
* Date      : 29/11/2017
*/
using AETProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace AET_WebApp.Controllers
{
    public class DashboardController : Controller
    {
        SessionManager userSession = null;
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult DashCurrStdProfile() {
            return View();
        }

        public ActionResult DashChat()
        {
            return View();
        }
  
        public ActionResult DashAdminProfile()
        {
            return View();
        }

        public ActionResult DashFileUpload()
        {
            return View();
        }
        public ActionResult DashTrusteeProfile()
        {
            return View();
        }
        public ActionResult DashTrustees()
        {
            return View();
        }
        public void Logout() {
            try {
                var db = new DBConfig();
                db.SessionManager.Remove(userSession);
                db.SaveChanges();
            }
            catch (Exception e) {
            }
            Session["sessionKey"] = null;
            Response.Redirect("../Home");
        }

        public ActionResult applyscholarship(Student st) {
            Student temp = new Student();
            string session = Session["sessionKey"] + "";
            string[] s = session.Split('\\');
            int _userId = Int32.Parse(s[0]);

            st.userId = _userId;
            temp.scholarshipApplication(st);

            return RedirectToAction("../Dashboard");
        }

        public ActionResult applyMentorship(Mentor mnt) {
            Mentor temp = new Mentor();
            string session = Session["sessionKey"] + "";
            string[] s = session.Split('\\');
            int _userId = Int32.Parse(s[0]);

            mnt.userId = _userId;
            temp.mentorApplication(mnt);

            return RedirectToAction("../Dashboard");
        }


        public ActionResult Cancel(int appId)
        {
            var db = new DBConfig();

            //get the user from db
            Applications apps = db.Applications.Where(s => s.applicationId == appId).FirstOrDefault<Applications>();

            //update user role status
            apps.applicationStatus = Constant.CANCELLED_STATUS;

            //save modified entity to the database
            db.Entry(apps).State = System.Data.Entity.EntityState.Modified;

            //save all changes.
            db.SaveChanges();

            string msg = "Thank you for updating you application status. We wish you all the best with your future endeavours.";
            Announcement announcement = new Announcement();
            announcement.addAnnouncement(apps.userId, "Cancelled Application: " + apps.applicationName, msg, false, "AET Dev Team");

            return RedirectToAction("../Dashboard");
        }


        public ActionResult Accept(int appId)
        {
            var db = new DBConfig();

            //get the user from db
            Applications apps = db.Applications.Where(s => s.applicationId == appId).FirstOrDefault<Applications>();

            //update user role status
            apps.applicationStatus = Constant.ACCEPTED_STATUS;

            //save modified entity to the database
            db.Entry(apps).State = System.Data.Entity.EntityState.Modified;

            //save all changes.
            db.SaveChanges();

            string msg = "Thank you for updating you application status. We wish you all the best with your future endeavours.";
            Announcement announcement = new Announcement();
            announcement.addAnnouncement(apps.userId, "Accepted Offer: " + apps.applicationName, msg, false, "AET Dev Team");

            return RedirectToAction("../Dashboard");
        }

        public ActionResult reapply(int appId)
        {
            var db = new DBConfig();

            //get the user from db
            Applications apps = db.Applications.Where(s => s.applicationId == appId).FirstOrDefault<Applications>();

            //update user role status
            apps.applicationStatus = Constant.PENDING_STATUS;
            apps.applicationDate += ", "+ DateTime.Today.ToString("dd-MM-yyyy");

            //save modified entity to the database
            db.Entry(apps).State = System.Data.Entity.EntityState.Modified;

            //save all changes.
            db.SaveChanges();
            string msg = "Thank you for updating you application status. We wish you all the best with your future endeavours.";
            Announcement announcement = new Announcement();
            announcement.addAnnouncement(apps.userId, "Re-application: "+apps.applicationName, msg, false, "AET Dev Team");
            return RedirectToAction("../Dashboard");
        }

        public ActionResult Decline(int appId)
        {
            var db = new DBConfig();

            //get the user from db
            Applications apps = db.Applications.Where(s => s.applicationId == appId).FirstOrDefault<Applications>();

            //update user role status
            apps.applicationStatus = Constant.DECLINED_STATUS;

            //save modified entity to the database
            db.Entry(apps).State = System.Data.Entity.EntityState.Modified;

            //save all changes.
            db.SaveChanges();

            string msg = "Thank you for updating you application status. We wish you all the best with your future endeavours.";
            Announcement announcement = new Announcement();
            announcement.addAnnouncement(apps.userId, "Offer Decline", msg, false, "AET Dev Team");


            return RedirectToAction("../Dashboard");
        }

        public ActionResult applystudentship(Student st) {
            Student temp = new Student();
            string session = Session["sessionKey"] + "";
            string[] s = session.Split('\\');
            int _userId = Int32.Parse(s[0]);

            st.userId = _userId;
            temp.applystudentship(st);

            return RedirectToAction("../Dashboard");
        }

        [HttpPost]
        public void UploadScholarshipFiles(string applicationId, HttpPostedFileBase idDocumentFile, HttpPostedFileBase academicTranscriptFile, HttpPostedFileBase midyearReportFile, HttpPostedFileBase proofOfEarningsFile, HttpPostedFileBase cvFile)
        {
            uploadFile(idDocumentFile,"scholarship-idDoc",applicationId);
            uploadFile(cvFile, "scholarship-cvDoc", applicationId);
            uploadFile(academicTranscriptFile, "scholarship-transcriptsDoc", applicationId);
            uploadFile(midyearReportFile, "scholarship-midYearReportDoc", applicationId);
            uploadFile(proofOfEarningsFile, "scholarship-earningsDoc", applicationId);

            var db = new DBConfig();

            //get the user from db
            string sess = Session["sessionKey"] + "";
            int _userId = Int32.Parse(sess.Split('\\')[0]);
            Applications apps = db.Applications.Where(s => s.userId == _userId && s.applicationId == Int32.Parse(applicationId)).FirstOrDefault<Applications>();

            //update application status
            apps.applicationStatus = Constant.PENDING_STATUS;

            //save modified entity to the database
            db.Entry(apps).State = System.Data.Entity.EntityState.Modified;

            //save all changes.
            db.SaveChanges();


            Response.Redirect("../Dashboard");
        }


        public void uploadFile(HttpPostedFileBase file, string DOC_CODE, string appId) {
            string s = Session["sessionKey"] + "";
            int _userId = Int32.Parse(s.Split('\\')[0]);

            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = DOC_CODE +"-"+_userId +"-"+appId+"-"+Path.GetFileName(file.FileName).Replace(" ", "-");
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                }
            }
            catch
            {
            }
        }
        public ActionResult Portal(string session) {
            if (session != null)
            {
                string[] s = session.Split('\\');
                int _userId = Int32.Parse(s[0]);
                string sessionToken = s[1];
                SessionManager sm = new SessionManager();
                SessionManager currentSession = sm.getSession(_userId, sessionToken);

                if (currentSession.isValid(currentSession))
                {
                    userSession = currentSession;
                    return View();
                }
                else
                    Response.Redirect("../Home");
            }
            else {
                Response.Redirect("../Home");
            }

            return null;
        }

        public ActionResult Index(string session)
        {
            if (session != null)
            {
                string[] s = session.Split('\\');
                int _userId = Int32.Parse(s[0]);
                string sessionToken = s[1];
                SessionManager sm = new SessionManager();
                SessionManager currentSession = sm.getSession(_userId, sessionToken);

                if (currentSession.isValid(currentSession))
                {
                    userSession = currentSession;
                    return View();
                }
                else
                    Response.Redirect("../Home");
            }
            else if(session == null){
                if (Session["sessionKey"] != null)
                {
                    Response.Redirect("/Dashboard/Portal?session="+ Session["sessionKey"]);
                }
                else {
                    Response.Redirect("../Home");
                }
            }
            return null;
        }
        public string dashboard(string data)
        {
            return data;
        }

        public void SetSID(string _email, string _password)
        {
            User temp = new User();
            User login = temp.Login(_email, _password);
            if (login == null)
            {
                Response.Redirect("../Home/Index");
            }
            else
            {
                SessionManager sm = new SessionManager();
                SessionManager session = sm.newSession(login.userId);
                string sessionKey = login.userId + "\\" + session.sessionToken;
                Session["sessionKey"] = sessionKey;
                Response.Redirect("../Dashboard?session=" + sessionKey);
            }
        }
    }

}
