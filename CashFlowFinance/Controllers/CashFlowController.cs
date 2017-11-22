using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashFlowFinance.Models;
using CashFlowFinance.ViewModels.CashFlow;
using System.Transactions;

namespace CashFlowFinance.Controllers
{
    public class CashFlowController : Controller
    {
        // GET: CashFlow
        public ActionResult CashFlow(Int32? FamiliaId)
        {
            var viewModel = new CashFlowViewModel();
            viewModel.cargarData(FamiliaId);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CashFlow(CashFlowViewModel model)
        {
            try
            {
                    return RedirectToAction("ViewCashFlow", new { FamiliaId = Convert.ToInt32(Session["FAMILIAID"]), PeriodoId = model.PeriodoId, BancoId = model.BancoId });
            }
            catch (Exception e)
            {
                return View(model);
            }
       }

        public ActionResult ViewCashFlow(Int32 FamiliaId, Int32 PeriodoId, Int32 BancoId)
        {
            var viewModel = new ViewCashFlowViewModel();
            viewModel.FlujoDeCaja(FamiliaId, PeriodoId, BancoId);
            return View(viewModel);
        }
    }
}