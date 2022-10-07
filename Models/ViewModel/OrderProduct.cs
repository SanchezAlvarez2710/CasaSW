using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasaSW.Models.ViewModel
{
    public class OrderProduct
    {
        public int id_persona_ { get; set; } = 0;
        public string username_persona { get; set; }
        public int id_order_ { get; set; } = 0;
        public string orderName_ { get; set; }        
        public string state_ { get; set; }
        public double subtotal_ { get; set; } = 0;
        public double total_ { get; set; } = 0;
        public int id_product_ { get; set; } = 0;
        public string product_name { get; set; }
        public string product_type { get; set; }
        public string product_version { get; set; }
        public string product_description { get; set; }
    }
}