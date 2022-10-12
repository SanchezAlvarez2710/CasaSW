using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasaSW.Models.ViewModel
{
    public class RequestPOCP
    {
        public int id_persona { get; set; } = 0;
        //CUSTOMER-PERSONA        
        public string Password_ { get; set; } = String.Empty;
        public string Name_ { get; set; } = String.Empty;
        public string Email_ { get; set; } = String.Empty;
        public int Denied_ { get; set; } = 0;
        public string PhoneN_ { get; set; } = String.Empty;
        public DateTime signUpDate_ { get; set; } = DateTime.Now;
        public string AdminFB_ { get; set; } = String.Empty;
        //ORDER-PRODUCT        
        public string username_persona { get; set; } = string.Empty;
        public int id_order_ { get; set; }
        public string orderName_ { get; set; } = String.Empty;
        public string state_ { get; set; } = String.Empty;
        public double subtotal_ { get; set; } = 0;
        public double total_ { get; set; } = 0;
        public int id_product_ { get; set; }
        public string product_name { get; set; } = String.Empty;
        public string product_type { get; set; } = String.Empty;
        public string product_version { get; set; } = String.Empty;
        public string product_description { get; set; } = String.Empty;
    }
}