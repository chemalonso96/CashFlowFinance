﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using CashFlowFinance.Models;
using CashFlowFinance.ViewModels.Home;

namespace CashFlowFinance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home(Int32? FamiliaId)
        {
            CashFlowEntities DB = new CashFlowEntities();
            var viewModel = new HomeViewModel();
            viewModel.CargarDatos(DB, FamiliaId);
            return View(viewModel);
        }   
    }
}