using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAB2
{
    [Table("LocationCitizen")]
    public class LocationCitizen
    {
        [Key]
        public string SocialSecurityNumber { get; set; }
        public string Address { get; set; }

        public Citizen citizen { get; set; }
        public Location location { get; set; }

        public string date { get; set; }
    }
}
