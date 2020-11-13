using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAB2
{
    public class TestCenterCitizen
    {
        [Key]
        public string SocialSecurityNumber { get; set; }
        public int TestCenterID { get; set; }

        public Citizen citizen { get; set; }
        public TestCenter testCenter { get; set; }

        public bool result { get; set; }
        public string status { get; set; }
        public string date { get; set; }
    }
}
