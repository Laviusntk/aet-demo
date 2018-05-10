using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AETProject.Models
{
    [Table("ProspectiveStudent")]
    public class ProspectiveStudent: Student
    {
        [Key]
        public int ProspectiveID { get; set; }
        public string latestResults { get; set; }
    }
}