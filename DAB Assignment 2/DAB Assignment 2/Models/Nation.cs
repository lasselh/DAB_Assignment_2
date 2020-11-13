using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAB2
{
    [Table("Nation")]
    public class Nation
    {
        [Key]
        public string nationName { get; set; }
        public List<Municipality> municipalities { get; set; }

        //public string NationID { get; set; }
    }
}
