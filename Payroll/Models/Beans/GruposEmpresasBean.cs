using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Models.Beans
{
    public class GruposEmpresasBean
    {
        public int iIdGrupoEmpresa { get; set; }
        public string sNombreGrupo { get; set; }
        public int iEstadoGrupo { get; set; }
        public int iEmpresa_id { get; set; }
        public string sNombre_empresa { get; set; }
        public string sRfc { get; set; }
        public string sMensaje { get; set; }
    }
}