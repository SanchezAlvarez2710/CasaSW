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
    
    public partial class ORDER
    {
        public int id_product { get; set; }
        public int id_persona { get; set; }
        public string orderName { get; set; }
        public System.DateTime orderDate { get; set; }
        public string state { get; set; }
        public double subtotal { get; set; }
        public double total { get; set; }
    
        public virtual PERSONA PERSONA { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}
