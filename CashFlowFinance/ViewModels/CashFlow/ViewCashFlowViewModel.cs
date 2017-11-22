using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CashFlowFinance.Models;
using System.Globalization;

namespace CashFlowFinance.ViewModels.CashFlow
{
    public class FlujoCaja
    {
        public Double SaldoInicial { set; get; }
        public String Mes { set; get; }
        public Int32? NumeroDias { set; get; }
        public Double Tasas { set; get; }
        public Double SaldoFinal { set; get; }
    }
    public class ViewCashFlowViewModel
    {
        public List<Periodo> LstPeriodo { set; get; } = new List<Periodo>();
        public Int32? PeriodoId { set; get; }
        public List<Banco> LstBanco { set; get; } = new List<Banco>();
        public Int32? BancoId { set; get; }

        //monto con el cual va iniciar la familia
        public Double MontoInicialxFamilia { set; get; }
        //suma total de todos los ingresos
        public Double TotalIngresos { set; get; }
        //suma total de todos los gastos fijos
        public Double TotalGastosFijos { set; get; }
        //suma total de todos los sueldos de todas personas
        public Double? TotalSueldoPersonas { set; get; }
        //suma total de todos los gastos variables
        public Double TotalGastosVariables { set; get; }

        //lista de lo que quiero mostrar
        public List<FlujoCaja> LstFlujoCaja { set; get; } = new List<FlujoCaja>();


        public void FlujoDeCaja(Int32? familiaId, Int32? periodoId, Int32? bancoId)
        {
            //base de datos
            var context = new CashFlowEntities();
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var universidad = context.Gasto.Select(x => x.Nombre).Distinct();
            var gastoUniversidad = 0.0;
            //gasto que se hace por universidad
            foreach (var uni in universidad)
            {
                if (uni == "Universidad")
                {
                    gastoUniversidad = context.Gasto.Where(x => x.Nombre == "Universidad").Sum(x => x.Costo);
                }
            }
                //listo todos los bamcos
            LstBanco = context.Banco.ToList();
            //datos de la familia reconociendo a la "familiaID"
            var familia = context.Familia.First(x => x.FamiliaId == familiaId);

            //periodonombre
            var periodonombre = context.Periodo.First(x => x.PeriodoId == periodoId);
            //tasaxBanco
            var tasaxbanco = context.Banco.First(x => x.BancoId == bancoId);
            //validar
            if (context.Ingreso.Count() != 0)
            {
                TotalIngresos = context.Ingreso.Where(x => x.FamiliaId == familiaId && x.Fecha > startDate && x.Fecha < endDate).Sum(x => x.Costo);
            }
            //validar
            if (context.Gasto.Where(x=>x.Nombre =="Fijo").Count() != 0)
            {
                TotalGastosFijos = context.Gasto.Where(x => x.FaimliaId == familiaId && x.TipoGasto.Nombre == "Fijo" && x.Fecha > startDate && x.Fecha < endDate).Sum(x => x.Costo);//suma de los gastos fijo que siempre se van a pagar
            }
            //validar
            if (context.Gasto.Where(x => x.Nombre == "Variable").Count() != 0)
            {
                TotalGastosVariables = context.Gasto.Where(x => x.FaimliaId == familiaId && x.TipoGasto.Nombre == "Variable" && x.Fecha > startDate && x.Fecha < endDate).Sum(x => x.Costo);//suma de los gastos fijo que siempre se van a pagar
            }
            //validar
            if (context.Persona.Count() != 0)
            {
                TotalSueldoPersonas = context.Persona.Where(x => x.FamiliaId == familiaId).Sum(x => x.SueldoNeto);
            }
            //ENTREGA EL NOMBRE DE CADA MES 
            int monthNow = now.Month;
            DateTimeFormatInfo NombreMes = new DateTimeFormatInfo();

            //FLUJO DE CAJA
            Double Anual = 360.0;
            Double So = familia.Ahorro;
            Double Sf;//saldo final
            Double  potencia = Convert.ToDouble(periodonombre.CantDias) / Anual;
            Double numero = 1.0 + (tasaxbanco.Tasa.Value/100);
            Double Variable = Math.Pow(numero, potencia);
            Double T = (Variable - 1.0)*100.0;
            Int32 P = periodonombre.CantDias;
            for (int i = 0; i < 12; i++)
            {
                String nombre = NombreMes.GetMonthName(monthNow).ToString();

                if (nombre == "July" || nombre == "December")
                {
                    Sf = So * Math.Pow(1 + T, (30 / P)) - TotalGastosFijos - TotalGastosVariables + TotalIngresos + (2 * TotalSueldoPersonas.Value);
                    LstFlujoCaja.Add(new FlujoCaja { SaldoInicial = So, Mes = nombre, NumeroDias = periodonombre.CantDias, Tasas = T, SaldoFinal = Sf });
                    So = Sf;
                    //cuando sea julio o diciembre tengo que verificar que al sumar me agrege la grati;
                }
                else if (nombre == "January" || nombre == "February")
                {
                    Sf = So * Math.Pow(1 + T, (30 / P)) - TotalGastosFijos - TotalGastosVariables + TotalIngresos - gastoUniversidad;
                    LstFlujoCaja.Add(new FlujoCaja { SaldoInicial = So, Mes = nombre, NumeroDias = periodonombre.CantDias, Tasas = T, SaldoFinal = Sf });
                    So = Sf;
                    //considerar aca la parte de la universidad
                }
                else
                {
                    Sf = So * Math.Pow(1 + T, (30 / P)) - TotalGastosFijos - TotalGastosVariables + TotalIngresos;
                    LstFlujoCaja.Add(new FlujoCaja { SaldoInicial = So, Mes = nombre, NumeroDias = periodonombre.CantDias, Tasas = T, SaldoFinal = Sf });
                    So = Sf;
                }
                monthNow++;
                if (monthNow == 13)
                {
                    monthNow = 1;
                }
            }
        }
    }
}