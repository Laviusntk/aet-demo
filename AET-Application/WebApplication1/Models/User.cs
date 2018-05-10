/*
* Title     : A script for dashboard ui interactions
* Authors   : Lavius N. Motileng
* Date      : 29/11/2017
*/
using System.Web.Mvc;
using System.Json;
using System.Web.Script.Serialization;
using WebApplication1.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.IO;
using System.Web;
using WebApplication1.Controllers;

namespace AETProject.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string race { get; set; }
        public string role { get; set; }
        public string password { get; set; }
        public string location { get; set; }
        public string industry { get; set; }
        public string profilePicture { get; set; }
        public string summary { get; set; }
        public string headline { get; set; }
        public string linkedinurl { get; set; }
        public string cellPhoneNumber { get; set; }
        public virtual System.Collections.Generic.ICollection<Address> Addresses { get; set; }

        public User() {
            profilePicture = "/Media Content/pf.png";
            summary = "No summary";
            headline = "No Role Selected";
        }

        public User Login(string _email, string _password)
        {
            var db = new DBConfig();
            User user = (from u in db.User where u.email == _email && u.password == _password
                         select new
                         {
                             email = u.email,
                             userId = u.userId,
                             firstName = u.firstName,
                             lastName = u.lastName,
                             profilePicture = u.profilePicture
                         }
                         )
                         .ToList()
                         .Select(x => new User {
                             email = x.email,
                             userId = x.userId,
                             firstName = x.firstName,
                             lastName = x.lastName,
                             profilePicture = x.profilePicture
                         })
                         .FirstOrDefault();

            if (user == null) {
                return null;
            }
            else {
                return user;
            }
        }

        public User Register(User _user, Address address)
        {
            var db = new DBConfig();
            User user = null;
            try
            {
                user = (from u in db.User
                        where u.email == _user.email
                        select new
                        {
                            email = u.email,
                            userId = u.userId,
                            firstName = u.firstName,
                            lastName = u.lastName,
                        }
                              )
                              .ToList()
                              .Select(x => new User
                              {
                                  email = x.email,
                                  userId = x.userId,
                                  firstName = x.firstName,
                                  lastName = x.lastName,
                              })
                              .FirstOrDefault();

                if (user == null)
                {
                    User temp = new User {firstName = _user.firstName, lastName = _user.lastName, email = _user.email, title = _user.title, cellPhoneNumber = _user.cellPhoneNumber, password = _user.password};
                    db.Set<User>().Add(temp);
                    db.SaveChanges();

                    User existing_user = (from u in db.User
                                 where u.email == _user.email
                                 select new
                                 {
                                     email = u.email,
                                     userId = u.userId,
                                     firstName = u.firstName,
                                     lastName = u.lastName,
                                 }
                              )
                              .ToList()
                              .Select(x => new User
                              {
                                  email = x.email,
                                  userId = x.userId,
                                  firstName = x.firstName,
                                  lastName = x.lastName,
                              })
                              .FirstOrDefault();
                    int id = existing_user.userId;
                    address.userId = id;
                    //Address registerAddress = new Address { userId = id, streetName = address.streetName, city = address.city, province = address.province, suburbTownship = address.suburbTownship, poastalCode = address.poastalCode};
                    db.Set<Address>().Add(address);
                    db.SaveChanges();
                    return temp;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                User temp = new User {  firstName = _user.firstName, lastName = _user.lastName, email = _user.email, title = _user.title, cellPhoneNumber = _user.cellPhoneNumber, password = _user.password };
                db.Set<User>().Add(temp);
                db.SaveChanges();

                User existing_user = (from u in db.User
                                      where u.email == _user.email
                                      select new
                                      {
                                          email = u.email,
                                          userId = u.userId,
                                          firstName = u.firstName,
                                          lastName = u.lastName,
                                      }
          )
          .ToList()
          .Select(x => new User
          {
              email = x.email,
              userId = x.userId,
              firstName = x.firstName,
              lastName = x.lastName,
          })
          .FirstOrDefault();
                int id = existing_user.userId;
                Address registerAddress = new Address { userId = id, streetName = address.streetName, city = address.city, province = address.province, suburbTownship = address.suburbTownship, poastalCode = address.poastalCode };
                db.Set<Address>().Add(registerAddress);
                db.SaveChanges();

                return temp;
            }

        }
        public User GoogleLogin(GoogleUserOutputData googleData)
        {
            var db = new DBConfig();
            User user = null;
            try
            {
               user = (from u in db.User
                             where u.email == googleData.email
                             select new
                             {
                                 email = u.email,
                                 userId = u.userId,
                                 firstName = u.firstName,
                                 lastName = u.lastName,
                             }
                             )
                             .ToList()
                             .Select(x => new User
                             {
                                 email = x.email,
                                 userId = x.userId,
                                 firstName = x.firstName,
                                 lastName = x.lastName,
                             })
                             .FirstOrDefault();

                if (user == null)
                {
                    User temp = new User { profilePicture = googleData.picture, firstName = googleData.given_name, lastName = googleData.family_name, email = googleData.email, gender = googleData.gender };
                    db.Set<User>().Add(temp);
                    db.SaveChanges();
                    return temp;
                }
                else
                {
                    return user;
                }
            }
            catch
            {
                User temp = new User { profilePicture = googleData.picture, firstName = googleData.given_name, lastName = googleData.family_name, email = googleData.email, gender = googleData.gender };
                db.Set<User>().Add(temp);
                db.SaveChanges();
                return temp;
            }

        }

        public User LinkedinLogin(User linkedinData)
        {
            var db = new DBConfig();
            User user = (from u in db.User
                         where u.email == linkedinData.email
                         select new
                         {
                             email = u.email,
                             userId = u.userId,
                             firstName = u.firstName,
                             lastName = u.lastName,
                         }
                         )
                         .ToList()
                         .Select(x => new User
                         {
                             email = x.email,
                             userId = x.userId,
                             firstName = x.firstName,
                             lastName = x.lastName,
                         })
                         .FirstOrDefault();

            if (user == null)
            {
                User temp = new User { profilePicture = linkedinData.profilePicture, firstName = linkedinData.firstName, lastName = linkedinData.lastName, email = linkedinData.email, headline = linkedinData.headline, industry = linkedinData.industry, location = linkedinData.location, summary = linkedinData.summary };
                db.Set<User>().Add(temp);
                db.SaveChanges();

                User currentUser = (from users in db.User
                                                 where users.email == linkedinData.email
                                                 select new
                                                 {
                                                     userId = users.userId
                                                 }
                                                )
                                                .ToList()
                                                .Select(x => new User
                                                {
                                                    userId = x.userId
                                                })
                                                .FirstOrDefault();
                return currentUser;
            }
            else
            {
                return user;
            }
        }
    }
}
