//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CashFlowFinance.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Gasto
    {
        public int GastoId { get; set; }
        public int TipoGastoId { get; set; }
        public string Nombre { get; set; }
        public System.DateTime Fecha { get; set; }
        public Nullable<int> FaimliaId { get; set; }
        public double Costo { get; set; }
        public Nullable<int> PersonaId { get; set; }
    
        public virtual Familia Familia { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual TipoGasto TipoGasto { get; set; }
    }
}
