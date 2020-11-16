using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAB2
{
    [Table("Location")]
    public class Location
    {
        [Key]
        public string Address { get; set; }
        public List<LocationCitizen> locationCitizens { get; set; }
        public Municipality municipality { get; set; }

        public int MunicipalityID { get; set; }
    }
}
