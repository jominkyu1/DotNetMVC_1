using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebOne.Models
{
    /**
      TABLE : LS101T0
      
      JOIN  
        COD_CONT from LS901T0
        BRA_NAME from LS801T0
     */

    public class TransList
    {
        public string bra_name { get; set; }
        public string cod_cont { get; set; }
        public string scann_date { get; set; }
        public string scann_time { get; set; }
        public string trs_name { get; set; }
    }
}