/*
* Title     : A script for dashboard ui interactions
* Authors   : Lavius N. Motileng
* Date      : 29/11/2017
*/
using AETProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DBConfig : DbContext
    {
        public DBConfig() : base("AETFinalVer5.1") { }
        public DbSet<User> User { get; set; }
        public DbSet<Address> Adress { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<SessionManager> SessionManager { get; set; }
        public DbSet<Alumni> Alumni { get; set; }
        public DbSet<Mentor> Mentor { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<ProspectiveStudent> PStudent { get; set; }
        public DbSet<Applications> Applications { get; set; }
        public DbSet<Announcement> Announcement { get; set; }
        public DbSet<Student> AETStudent { get; set; }
    }
}
