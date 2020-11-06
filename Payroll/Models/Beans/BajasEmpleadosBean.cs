using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Antlr.Runtime.Tree;

namespace Payroll.Models.Beans
{

    public class PeriodoActualBean
    {
        public int iEmpresa_id { get; set; }
        public int iAnio { get; set; }
        public int iTipoPeriodo { get; set; }
        public string sFecha_Inicio { get; set; }
        public string sFecha_Final { get; set; }
        public int iPeriodo { get; set; }
        public string sMensaje { get; set; }
    }
    public class BajasEmpleadosBean
    {

        public int iIdFiniquito { get; set; }
        public int iEmpresa_id { get; set; }
        public string sEmpresa { get; set; }
        public int iEmpleado_id { get; set; }
        public string sEffdt { get; set; }
        public string sFecha_antiguedad { get; set; }
        public string sFecha_ingreso { get; set; }
        public string sFecha_baja { get; set; }
        public int iAnios { get; set; }
        public string sDias { get; set; }
        public string sRFC { get; set; }
        public int iCentro_costo_id { get; set; }
        public string sCentro_costo { get; set; }
        public int iPuesto_id { get; set; }
        public string sPuesto { get; set; }
        public string sPuesto_codigo { get; set; }
        public string sDepartamento { get; set; }
        public string sDepto_codigo { get; set; }
        public int iAnioPeriodo { get; set; }
        public int iPeriodo { get; set; }
        public int iDias_Pendientes { get; set; }
        public string sSalario_mensual { get; set; }
        public string sSalario_diario { get; set; }
        public int iTipo_finiquito_id { get; set; }
        public string sFiniquito_valor { get; set; }
        public string sFecha_recibo { get; set; }
        public string sFecha { get; set; }
        public int iInactivo { get; set; }
        public string sCancelado { get; set; }
        public string sRegistroImss { get; set; }
        public string sCta_Cheques { get; set; }
        public string sFecha_Pago_Inicio { get; set; }
        public string sFecha_Pago_Fin { get; set; }
        public int iban_fecha_ingreso { get; set; }
        public int iban_compensacion_especial { get; set; }
        public int iEstatus { get; set; }
        public string sMensaje { get; set; }
    }

    public class DatosPDFCancelado
    {
        public string sNombrePDF { get; set; }
        public string sNombreFolder { get; set; }
    }
    public class DatosFiniquito
    {
        public int iIdValor { get; set; }
        public string sTipo { get; set; }
        public int iRenglon_id { get; set; }
        public string sNombre_Renglon { get; set; }
        public string sGravado { get; set; }
        public string sExcento { get; set; }
        public string sSaldo { get; set; }
        public int iEmpresa { get; set; }
        public int iNomina { get; set; }
        public string sNombre { get; set; }
        public string sLeyenda { get; set; }

    }

    public class TipoDeEmpleadoBean { 
        public int iIdTipoEmpleado { get; set; }
        public string sTipodeEmpleado { get; set; }
        public string sMensaje { get; set; }
    }
}