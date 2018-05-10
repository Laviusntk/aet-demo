/*
* Title     : A script for dashboard ui interactions
* Authors   : Lavius N. Motileng
* Date      : 29/11/2017
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AETProject.Models
{
    public class RegisterViewModel
    {
        [Key]
        public int registrationId { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Compare("password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string cellPhoneNumber { get; set; }
        [Required]
        public string streetName { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string province { get; set; }
        [Required]
        public string suburbTownship { get; set; }
        [Required]
        public string poastalCode { get; set; }
    }
}
