 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using AETProject.Models;
using System.Web.Script.Serialization;
using System.Runtime.InteropServices;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        protected string googleplus_client_id = "99159428431-pikng4pvhu0itrgp5jgmrsgu8port3j4.apps.googleusercontent.com";    // Replace this with your Client ID
        protected string googleplus_client_sceret = "sM89BQtloqMXXYFPry1AHZre";                                                // Replace this with your Client Secret
        protected string googleplus_redirect_url = "http://localhost:60409/Home/oauthcallback";                                         // Replace this with your Redirect URL; Your Redirect URL from your developer.google application should match this URL.
        protected string dashboard_url = "http://localhost:60409/Dashboard/dashboard?data=";
        protected string Parameters;

        public ActionResult Index()
        {
            if (Session["sessionKey"] != null)
            {
                string session = Session["sessionKey"] + "";
                string[] s = session.Split('\\');
                int _userId = Int32.Parse(s[0]);
                string sessionToken = s[1];
                SessionManager sm = new SessionManager();
                SessionManager currentSession = sm.getSession(_userId, sessionToken);

                if (currentSession.isValid(currentSession))
                    Response.Redirect("/Dashboard?session=" + Session["sessionKey"]);
            }
            return View();
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

        public ActionResult Conflict()
        {
            ViewBag.Message = "Account alread exists";
            return View();
        }

        public ActionResult PasswordError()
        {
            ViewBag.Message = "Passwords Do Not Match.";
            return View();
        }

        public void SetSID(string _email, string _password)
        {

            User temp = new User();
            User login = temp.Login(_email, _password);
            if (login == null)
            {
                Response.Redirect("Index");
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

        public void loginWithGoogle() {
            var Googleurl = "https://accounts.google.com/o/oauth2/auth?response_type=code&redirect_uri=" + googleplus_redirect_url + "&scope=https://www.googleapis.com/auth/userinfo.email%20https://www.googleapis.com/auth/userinfo.profile&client_id=" + googleplus_client_id;
            Session["loginWith"] = "google";
            Response.Redirect(Googleurl);
        }

        [HttpPost]
        public void Register(string titleForm, string firstName, string surname, string email, string cell, string password, string ConfirmPassword, string province, string streetName, string township, string city, string citycode)
        {
            User user = user = new User { firstName = firstName, lastName = firstName, email = email, title = titleForm, cellPhoneNumber = cell, password = password };
            if (password.Equals(ConfirmPassword))
            {
                Address registerAddress = new Address { streetName = streetName, city = city, province = province, suburbTownship = township, poastalCode = citycode };
                User tmp = user.Register(user, registerAddress);
                if (tmp == null)
                {
                    Response.Redirect("../Home/Conflict");
                }
                else {
                    Response.Redirect("../Home");
                }
            }
            else {
                Response.Redirect("../Home/PasswordError");
            }
            //return RedirectToAction("../Dashboard/Index");
        }

        public void LinkedinLogin(
            String _firstName,
            String _email,
            String _headline,
            String _industry,
            String _lastName,
            String _location,
            String _pf,
            String _summary) {

            User tmp = new AETProject.Models.User { profilePicture = _pf, firstName = _firstName, lastName = _lastName, email = _email, headline = _headline, industry = _industry, location = _location, summary = _summary };
            User authenticatedUser = tmp.LinkedinLogin(tmp);

            SessionManager sm = new SessionManager();
            SessionManager session = sm.newSession(authenticatedUser.userId);
            string sessionKey = authenticatedUser.userId + "\\" + session.sessionToken;
            Session["sessionKey"] = sessionKey;
            Response.Redirect("../Dashboard?session=" + sessionKey);
            //RedirectToAction("../Dashboard");
        }

        public void oauthcallback()
        {
            GoogleUserOutputData data = null;
            var url = Request.Url.Query;
            if (url != "")
            {
                string queryString = url.ToString();
                char[] delimiterChars = { '=' };
                string[] words = queryString.Split(delimiterChars);
                string code = words[1];

                if (code != null)
                {
                    //get the access token 
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
                    webRequest.Method = "POST";
                    Parameters = "code=" + code + "&client_id=" + googleplus_client_id + "&client_secret=" + googleplus_client_sceret + "&redirect_uri=" + googleplus_redirect_url + "&grant_type=authorization_code";
                    byte[] byteArray = Encoding.UTF8.GetBytes(Parameters);
                    webRequest.ContentType = "application/x-www-form-urlencoded";
                    webRequest.ContentLength = byteArray.Length;
                    Stream postStream = webRequest.GetRequestStream();
                    // Add the post data to the web request
                    postStream.Write(byteArray, 0, byteArray.Length);
                    postStream.Close();

                    WebResponse response = webRequest.GetResponse();
                    postStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(postStream);
                    string responseFromServer = reader.ReadToEnd();
                    //return responseFromServer;

                    GooglePlusAccessToken serStatus = JsonConvert.DeserializeObject<GooglePlusAccessToken>(responseFromServer);

                    if (serStatus != null)
                    {
                        string accessToken = string.Empty;
                        accessToken = serStatus.access_token;

                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            Session["accessToken"] = accessToken;
                            string userdetails = getuserinfor(accessToken);
                            Session["userinfor"] = userdetails;
                            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                            GoogleUserOutputData profile = JsonConvert.DeserializeObject<GoogleUserOutputData>(userdetails);
                            User user = new User();
                            User tmp = user.GoogleLogin(profile);


                            SessionManager sm = new SessionManager();
                            SessionManager session = sm.newSession(tmp.userId);
                            string sessionKey = tmp.userId + "\\" + session.sessionToken;
                            Session["sessionKey"] = sessionKey;
                            Response.Redirect("../Dashboard?session=" + sessionKey);
                        }
                    }
                }
            }
        }

        public string getuserinfor(string access_token) {
            var urlProfile = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + access_token;
            //get the access token 
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(urlProfile);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            // Add the post data to the web request

            WebResponse response = webRequest.GetResponse();
            Stream postStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(postStream);
            string responseFromServer = reader.ReadToEnd();
            return responseFromServer;
        }

        private async void getgoogleplususerdataSer(string access_token, GoogleUserOutputData data)
        {
            try
            {
                HttpClient client = new HttpClient();
                var urlProfile = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + access_token;

                client.CancelPendingRequests();
                HttpResponseMessage output = await client.GetAsync(urlProfile);

                if (output.IsSuccessStatusCode)
                {
                    string outputData = await output.Content.ReadAsStringAsync();
                    GoogleUserOutputData serStatus = JsonConvert.DeserializeObject<GoogleUserOutputData>(outputData);

                    if (serStatus != null)
                    {
                        // You will get the user information here.
                        data = serStatus;
                        Response.Redirect(dashboard_url + data.email);
                    }
                }
            }
            catch (Exception ex)
            {
                data = null;
            }
        }
    }
    public class GooglePlusAccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string id_token { get; set; }
        public string refresh_token { get; set; }
    }
    public class GoogleUserOutputData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string email { get; set; }
        public string picture { get; set; }
        public string gender { get; set; }
        public string family_name { get; set; }
    }
}