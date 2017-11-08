using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashFlowFinance.Models;
using CashFlowFinance.ViewModels.Personas;
using System.Transactions;

namespace CashFlowFinance.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult ListPersona(Int32?  FamiliaId)
        {
            return View();
        }

        public ActionResult AddEditPersona(Int32? PersonaId, Int32? FamiliaId)
        {
            CashFlowEntities DB = new CashFlowEntities();
            var viewModel = new AddEditPersona();
            viewModel.CargarDato(DB, PersonaId, FamiliaId);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddEditPersona()
        {
            return View();
        }
    }
}