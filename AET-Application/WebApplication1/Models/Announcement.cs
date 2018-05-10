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
    [Table("Announcement")]
    public class Announcement
    {
        [Key]
        public int announcementId { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public virtual User User { get; set; }
        public string announcementSubject { get; set; }
        public string announcementMessage { get; set; }
        public Boolean seen { get; set; }
        public Boolean isGeneral { get; set; }
        public string announcementDate { get; set; }
        public string announcementAuthor { get; set; }

        public Announcement()
        {
            announcementMessage = "Announcement message";
            seen = false;
            announcementDate = DateTime.Today.ToString("dd-MM-yyyy");
        }

        public void addAnnouncement(int _userId, string _subject, string _message, Boolean _general, string _authorName)
        {
            Announcement anmt = new Announcement();
            anmt.announcementSubject = _subject;
            anmt.announcementAuthor = _authorName;
            anmt.announcementMessage = _message;
            anmt.isGeneral = _general;
            anmt.userId = _userId;

            var db = new DBConfig();
            db.Set<Announcement>().Add(anmt);
            db.SaveChanges();
        }
    }
}
