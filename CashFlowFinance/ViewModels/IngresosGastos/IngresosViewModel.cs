using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashFlowFinance.Models;

namespace CashFlowFinance.ViewModels.IngresosGastos
{
    public class IngresosViewModel
    {
        public Int32? IngresoId { set; get; }
        public String Nombre { set; get; }
        public Double Costo { set; get; }
        public List<Ingreso> LstIngreso { set; get; } = new List<Ingreso>();
        public Int32? PersonaId { set; get; }

        public void cargarDato(CashFlowEntities context, Int32? ingresoId, Int32? personaId)
        {
            this.IngresoId = ingresoId;
            var ingreso = context.Ingreso.FirstOrDefault(x => x.IngresoId == ingresoId);
           
            LstIngreso = context.Ingreso.ToList();
            this.PersonaId = personaId;
            if (ingresoId.HasValue)
            {
                Costo = ingreso.Costo;
                Nombre = ingreso.Nombre;
            }
        }
    }
}