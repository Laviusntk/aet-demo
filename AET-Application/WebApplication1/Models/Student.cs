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
    [Table("Student")]
    public class Student
    {
        [Key]
        public int studentId { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public virtual User User { get; set; }
        public string nextOfKeenName{ get; set; }
        public string nextOfKeenRelationship { get; set; }
        public string nextOfKeenContacts { get; set; }
        public Boolean SACitizen{ get; set; }
        public string sex{ get; set; }
        public string secondarySchoolName{ get; set; }
        public string secondarySchoolTown{ get; set; }
        public string secondarySchoolProvince{ get; set; }
        public string nameOfUniversity{ get; set; }
        public string nameOfDegree{ get; set; }
        public string currentYearOfStudy{ get; set; }
        public string firstSemCourses{ get; set; }
        public string secSemCourses{ get; set; }
        public Boolean resStudent{ get; set; }
        public string accomodationName{ get; set; }
        public string bursryInfor{ get; set; }
        public string functionInfor { get; set; }
        public string adverstisingPlartform{ get; set; }
        public string motivEssay{ get; set; }
        public Boolean prevApplications{ get; set; }
        public string prevApplicationsDate { get; set; }
        public string motherOccupation{ get; set; }
        public string motherMonthlyIncome{ get; set; }
        public string fatherOccupation{ get; set; }
        public string fatherMonthlyIncome{ get; set; }
        public string guardianOccupation{ get; set; }
        public string guardianMonthlyIncome{ get; set; }
        public string altIncomeSources{ get; set; }
        public Boolean approvedApplication { get; set;}


        public void scholarshipApplication(Student st) {
            var db = new DBConfig();
            db.Set<Student>().Add(st);

            ProspectiveStudent ps = new ProspectiveStudent { userId = st.userId};
            db.Set<ProspectiveStudent>().Add(ps);

            Applications apps = new Applications();
            apps.newApplication(st.userId, Constant.SCHOLARHIP_APPLICATION);

            db.SaveChanges();

            User user = db.User.Where(s => s.userId == st.userId).FirstOrDefault<User>();

            string msg = "Dear " + user.firstName +". Thank you for your interest in Accenture Scholarship. To complete your application, please see the Applications tab on the AET web site to upload required documents.";
            Announcement announcement = new Announcement();
            announcement.addAnnouncement(st.userId, "Accenture Scholarship Application", msg, false, "AET Dev Team");
        }

        public void applystudentship(Student st)
        {
            var db = new DBConfig();
            db.Set<Student>().Add(st);

            AETStudent ps = new AETStudent { userId = st.userId };
            db.Set<AETStudent>().Add(ps);

            Applications apps = new Applications();
            apps.newApplication(st.userId, Constant.STUDENTSHIP_APPLICATION);

            db.SaveChanges();

            User user = db.User.Where(s => s.userId == st.userId).FirstOrDefault<User>();
            string msg = "Dear " + user.firstName +". Your information has be recieved, keep checking the applications tab for your application status.";
            Announcement announcement = new Announcement();

            announcement.addAnnouncement(st.userId, "AET Role Claimed", msg, false, "AET Dev Team");
        }

        public Boolean editEssay(int studentID, string essay)
        {
            var db = new DBConfig();
            return true;
        }

        public Boolean changeDegree()
        {
            return true;
        }
    }
}
