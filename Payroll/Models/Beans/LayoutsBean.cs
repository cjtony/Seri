using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Models.Beans
{
    public class LayoutsBean { }

    public class ErroresLayoutBean
    {
        public string sNomina            { get; set; }
        public string sEmpresa           { get; set; }
        public string sErroresInsercion  { get; set; }
        public string sErroresValidacion { get; set; }
        public int iFila                 { get; set; }
        public string sTipoInsercion      { get; set; }
        public string sMensaje { get; set; }
    }
    public class LayoutSalarioMasivoBean
    {
        public int iBanderaFecha { get; set; }
        public int iBandera1     { get; set; }
        public int iBandera2     { get; set; }
        public int iBandera3     { get; set; }
        public string sMensaje   { get; set; }
        public int iCantidad     { get; set; }
    }

}