using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashFlowFinance.ViewModels.IngresosGastos;
using CashFlowFinance.Models;
using System.Transactions;

namespace CashFlowFinance.Controllers
{
    public class IngresosGastosController : Controller
    {
        // GET: IngresosGastos
        public ActionResult Gasto(Int32? GastosId, Int32? PersonaId)
        {
            var viewModel = new GastosViewModel();
            var context = new CashFlowEntities();
            viewModel.cargarDato(context, GastosId,PersonaId);
            Session["PERSONAID"] = viewModel.PersonaId;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Gasto(GastosViewModel model)
        {
            try
            {
                var context = new CashFlowEntities();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                using (var ts = new TransactionScope())
                {
                    var gasto = new Gasto();
                    var familia = new Familia();
                    if (model.GastoId.HasValue)
                    {
                        gasto = context.Gasto.FirstOrDefault(x => x.GastoId == model.GastoId);
                    }
                    else
                    {
                        context.Gasto.Add(gasto);
                        
                    }

                    
                    gasto.Nombre = model.Nombre;
                    gasto.Fecha = DateTime.Now;
                    gasto.TipoGastoId = model.TipoGastoId;
                    gasto.FaimliaId = Convert.ToInt32(Session["FAMILIAID"]);
                    gasto.Costo = model.Costo;
                    gasto.PersonaId = Convert.ToInt32(Session["PERSONAID"]);

                    if (gasto.FaimliaId.HasValue)
                    {
                        familia = context.Familia.First(x => x.FamiliaId == gasto.FaimliaId);
                    }
                    familia.Ahorro = familia.Ahorro - model.Costo;
                    context.SaveChanges();
                    ts.Complete();
                }
                return RedirectToAction("Gasto", new { PersonaId = Convert.ToInt32(Session["PERSONAID"])});
            }
            catch (Exception e)
            {
                return View(model);
            }
        }

        public ActionResult Ingreso(Int32? IngresoId, Int32? PersonaId)
        {
            var viewModel = new IngresosViewModel();
            var context = new CashFlowEntities();
            viewModel.cargarDato(context, IngresoId, PersonaId);
            Session["PERSONAID"] = viewModel.PersonaId;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Ingreso(IngresosViewModel model)
        {
            try
            {
                var context = new CashFlowEntities();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                using (var ts = new TransactionScope())
                {
                    var ingreso = new Ingreso();
                    var familia = new Familia();
                    if (model.IngresoId.HasValue)
                    {
                        ingreso = context.Ingreso.FirstOrDefault(x => x.IngresoId == model.IngresoId);
                    }
                    else
                    {
                        context.Ingreso.Add(ingreso);
                    }
                
                   
                    ingreso.Nombre = model.Nombre;
                    ingreso.Fecha = DateTime.Now;
                    ingreso.FamiliaId = Convert.ToInt32(Session["FAMILIAID"]);
                    ingreso.Costo = model.Costo;
                    ingreso.PersonaId = Convert.ToInt32(Session["PERSONAID"]);
                    if (ingreso.FamiliaId.HasValue)
                    {
                        familia = context.Familia.First(x => x.FamiliaId == ingreso.FamiliaId);
                    }
                    familia.Ahorro = familia.Ahorro + model.Costo;

                    context.SaveChanges();
                    ts.Complete();
                }
                return RedirectToAction("Ingreso", new { PersonaId = Convert.ToInt32(Session["PERSONAID"]) });
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
    }
}