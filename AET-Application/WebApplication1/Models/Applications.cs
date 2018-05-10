/*
* Title     : A script for dashboard ui interactions
* Authors   : Lavius N. Motileng
* Date      : 29/11/2017
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace AETProject.Models
{
    [Table("Applications")]
    public class Applications
    {
        [Key]
        public int applicationId { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public virtual User User { get; set; }
        public int applicationType { get; set; }
        public string applicationDate { get; set; }
        public string responselicationDate { get; set; }
        public string applicationStatus { get; set; }
        public string applicationName { get; set; }
        public Boolean applicationReviewed { get; set; }
        public Boolean offerStatus { get; set; }

        public Applications() {
            applicationDate = DateTime.Today.ToString("dd-MM-yyyy");
            responselicationDate = "No respone yet";
            applicationStatus = Constant.INCOMPLETE_STATUS;
            applicationReviewed = false;
        }

        public Boolean newApplication(int _userId, int _applicationType) {
            userId = _userId;
            applicationType = _applicationType;
            string tempRole = "No role set";

            if (_applicationType == Constant.SCHOLARHIP_APPLICATION)
            {
                applicationName = "Accenture Scholarship Application";
                tempRole = "Perspective Student";
            }
            else if (_applicationType == Constant.ALUMNI_APPLICATION)
            {
                applicationName = "Alumni Role Claim";
                tempRole = "Accenture Alumni (Pending)";
            }
            else if (_applicationType == Constant.MENTORSHIP_APPLICATION)
            {
                applicationName = "Accenture Scholarship Mentorship Application";
                tempRole = "Perspective Mentor";
            }
            else if (_applicationType == Constant.STUDENTSHIP_APPLICATION) {
                applicationName = "Accenture Scholarship Studentship Application";
                tempRole = "Accenture Student (Pending)";
            }

            try {
                var db = new DBConfig();
                db.Set<Applications>().Add(this);

                //get the user from db
                User user = db.User.Where(s => s.userId == _userId).FirstOrDefault<User>();

                //update user role status
                user.role += tempRole;
                user.headline = tempRole;

                //save modified entity to the database
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                //save all changes.
                db.SaveChanges();

                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
    }

}
