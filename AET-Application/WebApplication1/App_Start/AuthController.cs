using AETProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using System.Web.SessionState;

namespace AETProject.App_Start
{
    public class AuthController : ApiController
    {
        [ResponseType(typeof(LoginStatus))]
        public IHttpActionResult GetLogin([FromUri] User user)
        {
            User temp = new User();
            User login = temp.Login(user.email, user.password);
            if (login == null)
            {
                return Ok(new LoginStatus { code = 403, message = "Login Failed", user = login });
            }
            else
            {
                
                //Response.Cookies.Add(myCookie);
                //HttpContext.Current.Session["User"] = user;
                /*HttpContext.Current.Session["userId"] = user.userId;
                HttpContext.Current.Session["firstName"] = user.firstName;
                HttpContext.Current.Session["lastName"] = user.lastName;
                HttpContext.Current.Session["startDate"] = DateTime.Now.ToString();
                HttpContext.Current.Session["endDate"] = DateTime.Now.AddDays(1).ToString();*/
                return Ok(new LoginStatus { code = 200, message = "Login Success", user = login });
            }
        }
    }
    public class LoginStatus
    {
        public int code { get; set; }
        public string message { get; set; }
        public User user { get; set; }
    }
}
