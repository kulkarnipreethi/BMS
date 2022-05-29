using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BMS.models
{
    public class tblBMS
    {
        [Key]//Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//auto generat
        public int ISBM { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }

        public string Publication { get; set; }

    }
}
