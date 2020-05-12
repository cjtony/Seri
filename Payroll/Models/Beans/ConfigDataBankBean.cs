using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Models.Beans
{
    public class ConfigDataBankBean { }

    public class LoadDataTableBean
    {
        public int iIdBanco { get; set; }
        public string sNombreBanco { get; set; }
        public string sNumeroCliente { get; set; }
        public string sNumeroCuenta { get; set; }
        public string sNumeroPlaza { get; set; }
        public string sClabe { get; set; }
        public int iGeneraInterface { get; set; }
        public int iCodigoBanco { get; set; }
        public string sMensaje { get; set; }
    }
}