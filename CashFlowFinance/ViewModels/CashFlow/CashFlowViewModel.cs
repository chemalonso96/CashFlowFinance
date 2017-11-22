using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashFlowFinance.Models;
using System.Globalization;

namespace CashFlowFinance.ViewModels.CashFlow
{
   

    public class CashFlowViewModel
    {
        public List<Periodo> LstPeriodo { set; get; } = new List<Periodo>();
        public Int32? PeriodoId { set; get; }
        public List<Banco> LstBanco { set; get; } = new List<Banco>();
        public Int32? BancoId { set; get; }
        //lo que quiero que se muestre
        public void cargarData(Int32? familiaId)
        {
            var context = new CashFlowEntities();
            LstBanco = context.Banco.ToList();
            LstPeriodo = context.Periodo.ToList();
            var familia = context.Familia.First(x => x.FamiliaId == familiaId);
        }

        
    }
}