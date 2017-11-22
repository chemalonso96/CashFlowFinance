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
                Int32 dato = Convert.ToInt32(Session["FAMILIAID"]);
                var familia = DB.Familia.FirstOrDefault(x => x.FamiliaId == dato);
                if (!ModelState.IsValid)
                {
                    model.CargarDato(DB, model.PersonaId);
                    TryUpdateModel(model);
                    return View(model);
                }
                using (var ts  = new TransactionScope())
                {

                    var persona = new Persona();
                    var family = new Familia();
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

                    var afp = DB.AFP.First(x=>x.AFPId == model.AFPId);
                    var impuesto = DB.Impuesto.First(x => x.ImpuestoId == model.ImpuestoId);

                    persona.Nombre = model.Nombre;
                    persona.Apellido = model.Apellido;
                    persona.MiembroId = model.MiembroId;
                    persona.FamiliaId = Convert.ToInt32(Session["FAMILIAID"]);
                    persona.SueldoBruto = model.SueldoBruto;
                    //calculula mos el sueldo neto
                    persona.SueldoNeto = model.SueldoBruto - (afp.Porcentaje * model.SueldoBruto) - (afp.Seguro * model.SueldoBruto) - (afp.Comision * model.SueldoBruto) - (impuesto.Porcentaje * model.SueldoBruto);
                    persona.SueldoAnio = (model.SueldoBruto - (afp.Porcentaje * model.SueldoBruto) - (afp.Seguro * model.SueldoBruto) - (afp.Comision * model.SueldoBruto) - (impuesto.Porcentaje * model.SueldoBruto)) * 14;
                    persona.AFPId = model.AFPId;
                    persona.ImpuestoId = model.ImpuestoId;
                    persona.OcupacionId = model.OcupacionId;
                    persona.Essalud = 0.09;

                    //verificando si la familia tiene un id para hacer un EDITAR
                    if (persona.FamiliaId.HasValue)
                    {
                        family = DB.Familia.First(x => x.FamiliaId == persona.FamiliaId);
                    }
                    family.CantidadIntegrantes = family.CantidadIntegrantes + 1;

                    DB.SaveChanges();
                    ts.Complete();
                }
                return RedirectToAction("Home", "Home", new { FamiliaId = familia.FamiliaId });
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