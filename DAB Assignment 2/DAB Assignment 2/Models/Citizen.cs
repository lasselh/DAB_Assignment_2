using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAB2
{
    [Table("Citizen")]
    public class Citizen
    {
        [Key]
        public string SocialSecurityNumber { get; set; }
        public string Sex { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<TestCenterCitizen> testCenterCitizens { get; set; }
        public List<LocationCitizen> locationCitizens { get; set; }
        public Municipality municipality { get; set; }
        public int MunicipalityID { get; set; }
    }
}
