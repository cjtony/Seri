using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Models.Beans
{
    public class CodigoCatalogoBean
    {

        public int iId { get; set; }
        public string sCatalogo { get; set; }
        public string sCodigo { get; set; }
        public string sDescripcion { get; set; }
        public string sCancelado { get; set; }
        public int iConsecutivo { get; set; }
        public string sMensaje { get; set; }

    }
}