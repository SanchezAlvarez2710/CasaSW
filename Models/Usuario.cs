﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasaSW.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string ConfirmarPassword { get; set; }  
    }
}