using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAB2
{
    [Table("TestCenter")]
    public class TestCenter
    {
        [Key]
        public int TestCenterID { get; set; }
        public string Hours { get; set; }
        public List<TestCenterCitizen> testCenterCitizens { get; set; }
        public TestCenterManagement testcentermanagement { get; set; }
        public Municipality municipality { get; set; }
        public int MunicipalityID { get; set; }
    }
}
