using CashFlowFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CashFlowFinance.ViewModels.LoginRegister;
using System.Transactions;

namespace CashFlowFinance.Controllers
{
    public class LoginRegisterController : Controller
    {
        //MUESTRA
        public ActionResult Login()
        {
            var viewModel = new LoginViewModel();
            return View(viewModel);
        }

        //HACE EL POST (ENVIA EL USER Y PASS PARA VALIDAR E INGRESAR)
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //VERIFICA QUE EL TIPO DE DATO INGRESADO ES CORRECTO
                return View(model);
            }
            //BASE DE DATOS
            var context = new CashFlowEntities();
            var cuenta = context.Cuenta.First(x=>x.Username == model.Username && x.Contrasenia == model.Password);
            var familia = context.Familia.First(x => x.CuentaId == cuenta.CuentaId);
            if (cuenta != null)
            {
                Session["USERNAME"] = cuenta.Username;
                Session["CONTRASEÑA"] = cuenta.Contrasenia;
                Session["CUENTAID"] = cuenta.CuentaId;
                Session["FAMILIAID"] = familia.FamiliaId;
            }
            return RedirectToAction("Home", "Home", new { FamiliaId = familia.FamiliaId });
        }

        public ActionResult Register()
        {
            var viewModel = new RegisterViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var context = new CashFlowEntities();

                using (var transaction = new TransactionScope())
                {
                    var cuenta = new Cuenta();
                    if (model.CuentaId.HasValue)
                    {
                        cuenta = context.Cuenta.FirstOrDefault(x => x.CuentaId == model.CuentaId);
                    }
                    else
                    {
                        context.Cuenta.Add(cuenta);
                    }

                    //TABLA CUENTA
                    cuenta.Username = model.Username;
                    cuenta.Contrasenia = model.Contraseña;
                    cuenta.Correo = model.Correo;
                    cuenta.Telefono = model.Telefono;
                    cuenta.Salt = "123";
                    cuenta.FechaCreacion = DateTime.Now;
                    cuenta.TerminosCondiciones = model.TerminosyCondiciones;

                    context.SaveChanges();
                    transaction.Complete();
                    
                }
                return RedirectToAction("RegisterFamily", new { Username = model.Username});
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        public ActionResult RegisterFamily(Int32? FamiliaId, String Username)
        {
            var viewModel = new RegisterFamilyViewModel();
            var context = new CashFlowEntities();
            viewModel.cargarDato(context,FamiliaId, Username);
            Session["CUENTAIDREGISTER"] = viewModel.CuentaId;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult RegisterFamily(RegisterFamilyViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var context = new CashFlowEntities();
                using (var ts = new TransactionScope()){
                    var familia = new Familia();
                    var cuenta = new Cuenta();
                    if (model.FamiliaId.HasValue)
                    {
                        familia = context.Familia.FirstOrDefault(x => x.FamiliaId == model.FamiliaId);
                    }
                    else
                    {
                        context.Familia.Add(familia);
                    }
                    
                    familia.NombreGeneral = model.NombreCompleto;
                    familia.CantidadIntegrantes = Convert.ToInt32(model.CantIntegrantes);
                    familia.CuentaId = Convert.ToInt32(Session["CUENTAIDREGISTER"]);
                    familia.Ahorro = 0;
                    //tasa
                    //imagen
                    context.SaveChanges();
                    ts.Complete();
                }
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
    }
}