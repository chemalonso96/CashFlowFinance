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
            var cuenta = context.Cuenta.FirstOrDefault(x=>x.Username == model.Username && x.Contrasenia == model.Password);

            if (cuenta != null)
            {
                Session["USERNAME"] = cuenta.Username;
                Session["CONTRASEÑA"] = cuenta.Contrasenia;
                Session["CUENTAID"] = cuenta.CuentaId;
                //ME REDIRECCIONA A ESTA PARTE 
                if (cuenta.FamiliaId.HasValue)
                {
                    return RedirectToAction("Home", "Home", new { CuentaId = cuenta.CuentaId });
                }
                else
                {
                    return RedirectToAction("Home", "Home");
                }
            }
            return View(model);
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
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
    }
}