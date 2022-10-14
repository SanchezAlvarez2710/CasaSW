using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasaSW.Models.ViewModel
{
    public class RequestPOCP
    {
        public int id_persona { get; set; }
        //CUSTOMER-PERSONA        
        public string Password_ { get; set; } 
        public string Name_ { get; set; } 
        public string Email_ { get; set; } 
        public int Denied_ { get; set; } 
        public string PhoneN_ { get; set; }
        public DateTime signUpDate_ { get; set; }
        public string AdminFB_ { get; set; } = string.Empty;
        //ORDER-PRODUCT        
        public string username_persona { get; set; }        
        public string orderName_ { get; set; } 
        public DateTime orderDate_ { get; set; }
        public string state_ { get; set; } 
        public double subtotal_ { get; set; } 
        public double total_ { get; set; } 
        public int id_product_ { get; set; }
        public string product_name { get; set; } 
        public string product_type { get; set; }
        public string product_version { get; set; } 
        public string product_description { get; set; } 
    }
}