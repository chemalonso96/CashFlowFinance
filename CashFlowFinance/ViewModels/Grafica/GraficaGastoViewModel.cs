using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashFlowFinance.Models;

namespace CashFlowFinance.ViewModels.Grafica
{
    public  class ReporteGasto
    {
        public String NombrePersona { set; get; }
        public Double TotalGasto { set; get; }
    }
    public class GraficaGastoViewModel
    {
        public List<Gasto> LstGastos { set; get; } = new List<Gasto>();
        public List<Persona> LstPersonas { set; get; } = new List<Persona>();
        public List<ReporteGasto> LstReporte { set; get; } = new List<ReporteGasto>();
        public List<Int32?> Lstpersonagasto { set; get; } = new List<Int32?>();
        public void cargarDatos(Int32? familiaId)
        {
            var context = new CashFlowEntities();
            Lstpersonagasto = context.Gasto.Select(x => x.PersonaId).Distinct().ToList();
            LstGastos = context.Gasto.ToList();
            
            LstPersonas = context.Persona.ToList();
            foreach (var personas in LstPersonas)
            {
                if (personas.FamiliaId == familiaId)
                {
                    foreach (var gasto in Lstpersonagasto) {
                        if (personas.PersonaId == gasto) {
                            var TotalGasto = context.Gasto.Where(x => x.PersonaId == personas.PersonaId).Sum(x => x.Costo);
                            LstReporte.Add(new ReporteGasto { NombrePersona = personas.Nombre, TotalGasto = TotalGasto });
                        }
                    }
                }
            }
        }
    }
}