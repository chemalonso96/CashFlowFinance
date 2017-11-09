using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashFlowFinance.Models;

namespace CashFlowFinance.ViewModels.Personas
{
    public class AddEditPersonaViewModel
    {
        public Int32? PersonaId { set; get; }
        public String Nombre { set; get; }
        public String Apellido { set; get; }
        public Int32? FamiliaId { set; get; }
        public Int32? MiembroId { set; get; }
        public String Miembro { set; get; }
        public Double? SueldoAnio { set; get; }
        public Double? SueldoNeto { set; get; }
        public Double? SueldoBruto { set; get; }
        public Int32? AFPId { set; get; }
        public Int32? ImpuestoId { set; get; }
        public Int32? OcupacionId { set; get; }

        public List<Miembro> LstMiembro {set; get;}= new List<Miembro>();
        public List<AFP> LstAFP { set; get; } = new List<AFP>();
        public List<Ocupacion> LstOcupacion { set; get; } = new List<Ocupacion>();
        public List<Impuesto> LstImpuesto { set; get; } = new List<Impuesto>();

        public void CargarDato(CashFlowEntities DB, Int32? personaId)
        {

            LstMiembro = DB.Miembro.ToList();
            LstAFP = DB.AFP.ToList();
            LstImpuesto = DB.Impuesto.ToList();
            LstOcupacion = DB.Ocupacion.ToList();

            var Persona = DB.Persona.FirstOrDefault(x=>x.PersonaId == personaId);
            if (personaId.HasValue)
            {
                this.PersonaId = personaId;
                this.Nombre = Persona.Nombre;
                this.Apellido = Persona.Apellido;
                this.FamiliaId = Persona.FamiliaId;
                this.MiembroId = Persona.MiembroId;
                //this.SueldoAnio = Persona.SueldoAnio;
                //this.SueldoNeto = Persona.SueldoNeto;
                this.SueldoBruto = Persona.SueldoBruto;
                this.AFPId = Persona.AFPId;
                this.ImpuestoId = Persona.ImpuestoId;
                this.OcupacionId = Persona.OcupacionId;
            }
        }


    }
}