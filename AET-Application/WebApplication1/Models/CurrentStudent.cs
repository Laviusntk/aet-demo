using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AETProject.Models
{
    

    [Table("CurrentStudent")]
    public class CurrentStudent: Student
    {
        [Key]
        public string studentNumber { get; set; }
        public string academicRecord { get; set; }
    }
}