using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAB2
{
    [Table("TestCenterManagement")]
    public class TestCenterManagement
    {
        [Key]
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public TestCenter testcenter { get; set; }
        public int TestCenterID { get; set; }
    }
}
