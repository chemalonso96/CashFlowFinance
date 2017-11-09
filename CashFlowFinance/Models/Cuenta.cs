//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CashFlowFinance.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cuenta
    {
        public int CuentaId { get; set; }
        public Nullable<int> FamiliaId { get; set; }
        public Nullable<int> TasaId { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
        public string Salt { get; set; }
        public double Ahorro { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public Nullable<int> TerminosCondiciones { get; set; }
        public Nullable<int> Telefono { get; set; }
        public string Username { get; set; }
    
        public virtual Familia Familia { get; set; }
        public virtual Tasa Tasa { get; set; }
    }
}
