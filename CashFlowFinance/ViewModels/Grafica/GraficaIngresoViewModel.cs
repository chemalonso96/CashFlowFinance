using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashFlowFinance.Models;

namespace CashFlowFinance.ViewModels.Grafica
{
    public class ReporteIngreso
    {
        public String NombrePersona { set; get; }
        public Double TotalIngreso { set; get; }
    }
    public class GraficaIngresoViewModel
    {
        public List<Ingreso> LstIngreso { set; get; } = new List<Ingreso>();
        public List<Persona> LstPersonas { set; get; } = new List<Persona>();
        public List<ReporteIngreso> LstReporte { set; get; } = new List<ReporteIngreso>();
        public List<Int32?> LstpersonaIngreso { set; get; } = new List<Int32?>();
        public void cargarDatos(Int32? familiaId)
        {
            var context = new CashFlowEntities();
            LstpersonaIngreso = context.Ingreso.Select(x => x.PersonaId).Distinct().ToList();
            LstIngreso = context.Ingreso.ToList();

            LstPersonas = context.Persona.ToList();
            foreach (var personas in LstPersonas)
            {
                if (personas.FamiliaId == familiaId)
                {
                    foreach (var ingreso in LstpersonaIngreso)
                    {
                        if (personas.PersonaId == ingreso)
                        {
                            var TotalIngreso = context.Ingreso.Where(x => x.PersonaId == personas.PersonaId).Sum(x => x.Costo);
                            LstReporte.Add(new ReporteIngreso { NombrePersona = personas.Nombre, TotalIngreso = TotalIngreso });
                        }
                    }
                }
            }
        }
    }
}