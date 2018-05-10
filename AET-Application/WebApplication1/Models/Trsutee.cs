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

namespace AETProject.Models
{
    [Table("Admin")]
    public class Trustee
    {
        [Key]
        public int trusteId { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public virtual User User { get; set; }
    }
}
