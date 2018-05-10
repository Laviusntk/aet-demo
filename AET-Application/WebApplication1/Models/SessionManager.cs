/*
* Title     : A script for dashboard ui interactions
* Authors   : Lavius N. Motileng
* Date      : 29/11/2017
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using WebApplication1.Models;

namespace AETProject.Models
{
    [Table("SessionManager")]
    public class SessionManager
    {
        [Key]
        public int sessionId { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public virtual User User { get; set; }
        public string sessionToken { get; set; }
        public string sessionDate { get; set; }
        public string sessionTime { get; set; }

        public SessionManager newSession(int _userId) {
            var db = new DBConfig();

            DateTime currentDayTime = DateTime.Now;
            DateTime x15MinsLater = currentDayTime.AddMinutes(15);

            string sessionEndDate = DateTime.Today.ToString("dd-MM-yyyy");
            string sessionEndTime = x15MinsLater.ToString("HH:mm:ss");

            SessionManager sm = new SessionManager {userId = _userId, sessionToken = RandomString(), sessionDate = DateTime.Today.ToString("dd-MM-yyyy"), sessionTime = sessionEndTime };
            db.Set<SessionManager>().Add(sm);
            db.SaveChanges();
            return sm;
        }

        public SessionManager renewSession(string session) {
            var db = new DBConfig();
            int _userId = Int32.Parse(session.Split('\\')[0]);
            string sessionId = session.Split('\\')[1];

            SessionManager currentSession = getSession(userId, sessionId);

            if (currentSession != null && isValid(currentSession)) {
                return newSession(_userId);
            }
            else {
                return new SessionManager();
            }
        }

        public Boolean isSessionValid(string session) {
            var db = new DBConfig();
            int _userId = Int32.Parse(session.Split('\\')[0]);
            string sessionId = session.Split('\\')[1];

            SessionManager currentSession = getSession(userId, sessionId);

            if (currentSession != null && isValid(currentSession))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean isValid(SessionManager currentSession)
        {
            if (currentSession != null)
            {
                string date1 = currentSession.sessionDate;
                string pattern = "dd-MM-yyyy";
                DateTime parsedDate1;
                DateTime.TryParseExact(date1, pattern, null, DateTimeStyles.None, out parsedDate1);

                DateTime time1 = DateTime.ParseExact(currentSession.sessionTime, "HH:mm:ss", null);

                var combinedDateTime1 = parsedDate1.AddTicks(time1.TimeOfDay.Ticks);
                //combinedDateTime1.ToString("dd-MM-yyyy HH:mm:ss")
                int result = DateTime.Compare(DateTime.Now, combinedDateTime1);

                if (result <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else {
                return false;
            }
        }
        public SessionManager getSession(int _userId, string _sessionToken)
        {
            var db = new DBConfig();
            SessionManager currentSession = (from sessions in db.SessionManager
                                             where sessions.userId == _userId && sessions.sessionToken == _sessionToken
                                             select new
                                             {
                                                 userId = sessions.userId,
                                                 sessionId = sessions.sessionId,
                                                 sessionDate = sessions.sessionDate,
                                                 sessionTime = sessions.sessionTime,
                                                 sessionToken = sessions.sessionToken
                                             }
                                            )
                                            .ToList()
                                            .Select(x => new SessionManager
                                            {
                                                userId = x.userId,
                                                sessionId = x.sessionId,
                                                sessionDate = x.sessionDate,
                                                sessionTime = x.sessionTime,
                                                sessionToken = x.sessionToken
                                            })
                                            .FirstOrDefault();
            return currentSession;
        }

        public Boolean isValid() {

            return true;
        }
        public string RandomString()
        {
            int length = 25;
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
