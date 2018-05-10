using AETProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AETProject.Controllers
{
    public class DashboardAPIController : ApiController
    {
        // GET api/<controller>
        public SessionManager Get()
        {
            SessionManager sm = new SessionManager();
            //SessionManager currentSession = sm.getSession(1, "FX9C9U68JNL143OHLN05J6SPTTC7I8W9QD2WCP71HHU7J");
            return sm.newSession(1);
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