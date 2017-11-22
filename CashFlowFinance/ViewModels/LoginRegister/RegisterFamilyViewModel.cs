using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashFlowFinance.Models;

namespace CashFlowFinance.ViewModels.LoginRegister
{
    public class RegisterFamilyViewModel
    {
        public Int32? FamiliaId { set; get; }
        public String NombreCompleto { set; get; }
        public String CantIntegrantes { set; get; }
        public String Picture { set; get; }
        public Double Ahorro { set; get; }
        public Int32? CuentaId { set; get; }
        public void cargarDato(CashFlowEntities context,Int32? familiaId, String username)
        {
            var cuenta = context.Cuenta.FirstOrDefault(x => x.Username == username);
            CuentaId = cuenta.CuentaId;
            Familia familia = new Familia();
            if (familiaId.HasValue)
            {
                FamiliaId = familia.FamiliaId;
                NombreCompleto = familia.NombreGeneral;
                CantIntegrantes = Convert.ToString(familia.CantidadIntegrantes);
                Picture = familia.Picture;
            }
        }
    }
}