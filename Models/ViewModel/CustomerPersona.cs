using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasaSW.Models.ViewModel
{
    public class CustomerPersona
    {
        public int Id_ { get; set; }
        public string Username_ { get; set; }
        public string Password_ { get; set; }
        public string Name_ { get; set; }
        public string Email_ { get; set; }
        public int Denied_ { get; set; }
        public string PhoneN_ { get; set; }
        public DateTime signUpDate_ { get; set; }
        public string AdminFB_ { get; set; }

    }
}