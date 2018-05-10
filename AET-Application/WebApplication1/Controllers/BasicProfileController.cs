using AETProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace AETProject.Controllers
{
    public class BasicProfileController : ApiController
    {
        // GET api/<controller>
        public User Get(string sessionid)
        {
            if (sessionid != null)
            {
                int _userId = Int32.Parse(sessionid.Split('\\')[0]);
                string sessionKey = sessionid.Split('\\')[1];
                SessionManager sm = new SessionManager();
                SessionManager currentSession = sm.getSession(_userId, sessionKey);
                User user = null;
                if (currentSession.isValid(currentSession))
                {
                    var db = new DBConfig();
                    user = (from u in db.User
                            where u.userId == currentSession.userId
                            select new
                            {
                                email = u.email,
                                userId = u.userId,
                                firstName = u.firstName,
                                lastName = u.lastName,
                                headline = u.headline,
                                title = u.title,
                                profilePicture = u.profilePicture
                            }
                  )
                  .ToList()
                  .Select(x => new User
                  {
                      email = x.email,
                      userId = x.userId,
                      firstName = x.firstName,
                      lastName = x.lastName,
                      headline = x.headline,
                      title = x.title,
                      profilePicture = x.profilePicture
                  })
                  .FirstOrDefault();
                }
                return user;

            }
            else {
                return null;
            }
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}