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
        public Nullable<int> id_persona { get; set; }
        public string rol { get; set; }
    
        public virtual PERSONA PERSONA { get; set; }

        public ADMIN(int? id_persona, string rol)
        {
            this.id_persona = id_persona;
            this.rol = rol;
        }
        public ADMIN()
        {
        }
    }
}
