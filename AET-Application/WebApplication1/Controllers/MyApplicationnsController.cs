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
    public class MyApplicationnsController : ApiController
    {
        // GET api/<controller>
        public List<Applications> Get(string sessionid)
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
                    var apps = (from u in db.Applications
                                where u.userId == currentSession.userId
                                select new
                                {
                                    applicationId = u.applicationId,
                                    applicationType = u.applicationType,
                                    applicationDate = u.applicationDate,
                                    applicationStatus = u.applicationStatus,
                                    applicationName = u.applicationName
                                }
                  )
                  .ToList()
                  .Select(x => new Applications
                  {
                      applicationId = x.applicationId,
                      applicationType = x.applicationType,
                      applicationDate = x.applicationDate,
                      applicationStatus = x.applicationStatus,
                      applicationName = x.applicationName
                  });
                  return apps.ToList<Applications>();
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