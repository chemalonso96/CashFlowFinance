using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashFlowFinance.Models;

namespace CashFlowFinance.ViewModels.Personas
{
    public class ListPersonaViewModel
    {
        public List<Persona> LstPersona = new List<Persona>();

        public void CargarDato(CashFlowEntities DB, Int32? familiaId)
        {
            LstPersona = DB.Persona.Where(x => x.FamiliaId == familiaId).ToList();
        }
    }
}