using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashFlowFinance.Models;

namespace CashFlowFinance.ViewModels.Home
{
    public class HomeViewModel
    {
        public Int32? CuentaId { set; get; }
        public String ImagenFamilia { set; get; }
        public Double Ahorro { set; get; }
        public String NombreFamilia { set; get; }
        public Int32? Telefono { set; get; }
        public String Correo { set; get; }
        public Int32 Integrantes { set; get; }
        public List<Miembro> LstMiembro { set; get; }
        public void CargarDatos(CashFlowEntities BD, Int32? cuentaId)
        {
            BD = new CashFlowEntities();
            LstMiembro = BD.Miembro.ToList();
            this.CuentaId = cuentaId;


            var cuenta = BD.Cuenta.First(x=>x.CuentaId == cuentaId);
            Ahorro = cuenta.Ahorro;
            Telefono = cuenta.Telefono;
            Correo = cuenta.Correo;
            Ahorro = cuenta.Ahorro;

            var familia = BD.Familia.First(x=>x.FamiliaId == cuenta.FamiliaId);
            NombreFamilia = familia.NombreGeneral;
            Integrantes = familia.CantidadIntegrantes;

        }
    }
}