using CashFlowFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashFlowFinance.ViewModels.LoginRegister
{
    public class RegisterViewModel
    {
        public Int32? CuentaId { set; get; }
        public String Username { set; get; }
        public String Correo { set; get; }
        public String Contraseña { set; get; }
        public Int32? Telefono { set; get; }
        public Int32 TerminosyCondiciones { set; get; }


        public void cargarDatos(Cuenta cuenta)
        {
            CuentaId = cuenta.CuentaId;
            Correo = cuenta.Correo;
            Contraseña = cuenta.Contrasenia;
            Telefono = cuenta.Telefono;
            TerminosyCondiciones = cuenta.TerminosCondiciones.Value;
            Username = cuenta.Username;
        }
    }
}