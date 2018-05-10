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

namespace AETProject.Models
{
    public class Constant
    {
        public const int SCHOLARHIP_APPLICATION = 1;
        public const int MENTORSHIP_APPLICATION = 2;
        public const int STUDENTSHIP_APPLICATION = 3;
        public const int ALUMNI_APPLICATION = 4;
        public const string INCOMPLETE_STATUS = "Incomplete";
        public const string PENDING_STATUS = "Pending";
        public const string SUCCESS_STATUS = "Successful";
        public const string UNSUCCESS_STATUS = "UnSuccessful";
        public const string CANCELLED_STATUS = "Cancelled";
        public const string ACCEPTED_STATUS = "Accepted";
        public const string DECLINED_STATUS = "Declined";
    }
}
