//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CasaSW.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ADMIN
    {
        public int id_admin { get; set; }
        public int id_persona { get; set; }
        public string rol { get; set; }
        public int state { get; set; }
        public string avatar { get; set; }
    
        public virtual PERSONA PERSONA { get; set; }
    }
}
