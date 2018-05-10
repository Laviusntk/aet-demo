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
    public class MyAnnouncementsController : ApiController
    {
        public List<Announcement> Get(string sessionid)
        {
            if (sessionid != null)
            {
                int _userId = Int32.Parse(sessionid.Split('\\')[0]);
                string sessionKey = sessionid.Split('\\')[1];
                SessionManager sm = new SessionManager();
                SessionManager currentSession = sm.getSession(_userId, sessionKey);

                if (currentSession.isValid(currentSession))
                {
                    var db = new DBConfig();
                    var anns = db.Announcement.Where(s => s.userId == _userId);
                    return anns.ToList<Announcement>();
                }
                return null;
            }
            else
            {
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