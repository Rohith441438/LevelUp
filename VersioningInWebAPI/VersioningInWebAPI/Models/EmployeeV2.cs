using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VersioningInWebAPI.Models
{
    public class EmployeeV2
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeAge { get; set; }
    }
}