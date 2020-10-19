using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Models.Beans
{

    public class RenglonesHCBean
    {
        public int iIdRenglon        { get; set; }
        public string sNombreRenglon { get; set; }
        public int iConsecutivo      { get; set; }
    }
    public class ReporteNominaBean
    {

        public int iIdEmpleado { get; set; }
        public int iIdEmpresa  { get; set; }

    }

    public class DatosGeneralesHC
    {
        public int iAnio { get; set; }
        public int iPeriodo { get; set; }
        public int iEmpresa { get; set; }
        public string sEmpresa { get; set; }
        public int iNomina { get; set; }
        public string sPaterno { get; set; }
        public string sMaterno { get; set; }
        public string sNombreE { get; set; }
        public string sRegImss { get; set; }
        public string sRfc { get; set; }
        public string sCurp { get; set; }
        public string sPuesto { get; set; }
        public string sNombrePuesto { get; set; }
        public string sNivelJerarquico { get; set; }
        public string sDepto { get; set; }
        public string sNombreDepto { get; set; }
        public string sCentrCosto { get; set; }
        public string sDescCentrCosto { get; set; }
        public int iRegional { get; set; }
        public string sClvRegional { get; set; }
        public string sDescRegional { get; set; }
        public int iSucursal { get; set; }
        public string sClvSucursal { get; set; }
        public string sDescSucursal { get; set; }
        public string sFechaAnt { get; set; }
        public string sFechaIng { get; set; }
        public decimal dSueldo { get; set; }
        public int iVacanteC { get; set; }
        public int iUltimaPos { get; set; }
        public decimal dUltSdi { get; set; }
    }

    public class DetallesRenglon
    {
        public decimal dSaldo { get; set; }
        public int iRenglon { get; set; }
    }

}