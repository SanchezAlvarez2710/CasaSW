using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasaSW.Models.ViewModel
{
    public class UserAdmin
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public string ConfirmarPassword { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }
        public int state { get; set; }
    }
}