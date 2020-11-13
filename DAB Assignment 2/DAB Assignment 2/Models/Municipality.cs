using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAB2
{
    [Table("Municipality")]
    public class Municipality
    {
        [Key]
        public int MunicipalityID { get; set; }
        public string Name { get; set; }
        public float Population { get; set; }
        public List<Location> locations { get; set; }
        public Nation nation { get; set; }
        public List<Citizen> Citizens{ get; set; }
        public List<TestCenter> TestCenters { get; set; }
        public string nationName { get; set; }
    }
}
