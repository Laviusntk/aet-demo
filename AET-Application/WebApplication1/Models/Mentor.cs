/*
* Title     : A script for dashboard ui interactions
* Authors   : Lavius N. Motileng
* Date      : 29/11/2017
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace AETProject.Models
{
    [Table("Mentor")]
    public class Mentor
    {
        [Key]
        public int mentorId { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public virtual User User { get; set; }
        public string workLocation { get; set; }
        public string universityAttended { get; set; }
        public string degrees { get; set; }
        public string reasonForJoining { get; set; }
        public string reff { get; set; }

        public void mentorApplication(Mentor mnt){
          Mentor m = new Mentor();
          var db = new DBConfig();

            //db.set<Mentor>().Add(mnt);
            db.Set<Mentor>().Add(mnt); 

          User user = db.User.Where(s => s.userId == mnt.userId).FirstOrDefault<User>();
          
          //update user role status
          user.role += ", Perspective Mentor";
          user.headline += ", Perspective Mentor";

          //save modified entity to the database
          db.Entry(user).State = System.Data.Entity.EntityState.Modified;

          string msg = "Dear " + user.firstName +", Thank you for your interest in  our Mentorship Programme. All the best with your future endevours.";
            Announcement announcement = new Announcement();
            announcement.addAnnouncement(mnt.userId,"ACCENTURE (SA) EDUCATION TRUST MENTORSHIP PROGRAMME", msg, false, "AET Dev Team");

          db.SaveChanges();

          Applications aps = new Applications();
          aps.applicationStatus = Constant.PENDING_STATUS;
          aps.newApplication(user.userId, Constant.MENTORSHIP_APPLICATION);
        }
    }
}
