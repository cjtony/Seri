using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Models.Utilerias
{
    public class Validaciones
    {

        public string ValidationsInts (string value)
        {
            return (value.Trim() != "") ? value.Trim() : "0";
        }

    }
}