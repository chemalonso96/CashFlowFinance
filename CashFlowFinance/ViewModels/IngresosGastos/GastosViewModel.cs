using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashFlowFinance.Models;

namespace CashFlowFinance.ViewModels.IngresosGastos
{
    public class GastosViewModel
    {
        public Int32? GastoId { set; get; }
        public Int32 TipoGastoId { set; get; }
        public String Nombre { set; get; }
        public List<TipoGasto> LstTipoGasto { set; get; }
        public Double Costo { set; get; }
        public List<Gasto> LstGasto { set; get; } = new List<Gasto>();
        public Int32? PersonaId { set; get; }

        public void cargarDato(CashFlowEntities context, Int32? gastoId, Int32? personaId) {
            this.GastoId = gastoId;
            var gasto = context.Gasto.FirstOrDefault(x => x.GastoId == gastoId);
            LstTipoGasto = context.TipoGasto.ToList();
            LstGasto = context.Gasto.ToList();
            this.PersonaId = personaId;
            if (gastoId.HasValue)
            { 
                TipoGastoId = gasto.TipoGastoId;
                Costo = gasto.Costo;
                Nombre = gasto.Nombre;
            }
        }
    }
}