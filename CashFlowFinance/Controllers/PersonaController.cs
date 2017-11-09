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

        public ActionResult AddEditPersona(Int32? PersonaId)
        {
            CashFlowEntities DB = new CashFlowEntities();
            var viewModel = new AddEditPersonaViewModel();
            viewModel.CargarDato(DB, PersonaId);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddEditPersona(AddEditPersonaViewModel model)
        {

            try
            {
                var DB = new CashFlowEntities();
                var dato = Convert.ToInt32(Session["CUENTAID"]);
                var cuenta = DB.Cuenta.FirstOrDefault(x => x.CuentaId == dato);
                if (!ModelState.IsValid)
                {
                    model.CargarDato(DB, model.PersonaId);
                    TryUpdateModel(model);
                    return View(model);
                }
                using (var ts  = new TransactionScope())
                {

                    var persona = new Persona();
                    //con la session obtienes el id de la cuenta que te va ayudar a sacar el ID
                    //de la familia (Y)
                    
                    if (model.PersonaId.HasValue)
                    {
                        persona = DB.Persona.First(x => x.PersonaId == model.PersonaId);
                    }
                    else
                    {
                        DB.Persona.Add(persona);
                    }
                    persona.Nombre = model.Nombre;
                    persona.Apellido = model.Apellido;
                    persona.FamiliaId = cuenta.FamiliaId;
                    persona.MiembroId = model.MiembroId;
                    persona.SueldoBruto = model.SueldoBruto;
                    persona.AFPId = model.AFPId;
                    persona.ImpuestoId = model.ImpuestoId;
                    persona.OcupacionId = model.OcupacionId;

                    DB.SaveChanges();
                    ts.Complete();
                }
                return RedirectToAction("Home", "Home",new { CuentaId = cuenta.CuentaId});
            }
            catch (Exception e)
            {
                var DB = new CashFlowEntities();
                model.CargarDato(DB, model.PersonaId);
                TryUpdateModel(model);
                return View(model);
            }
        }
    }
}