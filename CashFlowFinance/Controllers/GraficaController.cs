using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashFlowFinance.ViewModels.Grafica;
using CashFlowFinance.Models;
using System.Transactions;


namespace CashFlowFinance.Controllers
{
    public class GraficaController : Controller
    {
        // GET: Grafica
        public ActionResult GraficaGasto(Int32? FamiliaId)
        {
            var viewModel = new GraficaGastoViewModel();
            viewModel.cargarDatos(FamiliaId);
            return View(viewModel);
        }
        public ActionResult GraficaIngreso(Int32? FamiliaId)
        {
            var viewModel = new GraficaIngresoViewModel();
            viewModel.cargarDatos(FamiliaId);
            return View(viewModel);
        }
    }
}