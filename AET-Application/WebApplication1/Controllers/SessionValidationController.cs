using AETProject.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AETProject.Controllers
{
    public class SessionValidationController : ApiController
    {
        // GET api/<controller>
        public Boolean Get(string token)
        {
            SessionManager sm = new SessionManager();
            SessionManager currentSession = sm.getSession(1, token);
            return currentSession.isValid(currentSession);
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