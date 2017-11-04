using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CashFlowFinance.ViewModels.LoginRegister
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Debe completarse este campo")]
        public String Username { get; set; }

        [Required(ErrorMessage ="Debe completarse este campo")]
        [DataType(DataType.Password)]
        [Display(Name ="Contraseña")]
        public String Password { get; set; }
    }
}