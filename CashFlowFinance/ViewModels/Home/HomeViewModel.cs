using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashFlowFinance.Models;

namespace CashFlowFinance.ViewModels.Home
{
    public class HomeViewModel
    {
        public Int32? FamiliaId { set; get; }
        public String ImagenFamilia { set; get; }
        public Double? Ahorro { set; get; }
        public String NombreFamilia { set; get; }
        public Int32? Telefono { set; get; }
        public String Correo { set; get; }
        public Int32 Integrantes { set; get; }
        public List<Persona> LstPersona { set; get; } = new List<Persona>();
        public void CargarDatos(CashFlowEntities BD, Int32? familiaId)
        {
            BD = new CashFlowEntities();
            this.FamiliaId = familiaId;
            LstPersona = BD.Persona.ToList();

            if (familiaId.HasValue)
            {
                var familia = BD.Familia.First(x => x.FamiliaId == familiaId);
                Telefono = familia.Cuenta.Telefono;
                Correo = familia.Cuenta.Correo;
                Ahorro = familia.Ahorro;
                NombreFamilia = familia.NombreGeneral;
                Integrantes = familia.CantidadIntegrantes;
            }
        }
    }
}