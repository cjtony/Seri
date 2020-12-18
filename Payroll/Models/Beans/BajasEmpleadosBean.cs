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
        public string sTipo_Operacion { get; set; }
        public int iMotivo_baja { get; set; }
        public string sMotivo_baja { get; set; }
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
        public int iIdFiniquito { get; set; }
        public string sEffdt { get; set; }
        public string sFechaAntiguedad { get; set; }
        public string sFechaIngreso { get; set; }
        public string sFechaBaja { get; set; }
        public int iAnios { get; set; }
        public int iDias { get; set; }
        public string sRfc { get; set; }
        public int iCentroId { get; set; }
        public int iPuestoId { get; set; }
        public double dSalarioMensual { get; set; }
        public double dSalarioDiario { get; set; }
        public int iTipoFiniquitoId { get; set; }
        public string sFechaRecibo { get; set; }
        public int iInactivo { get; set; }
        public int iBanFechaIngreso { get; set; }
        public int iBanCompEspecial { get; set; }
        public int iEstatus { get; set; }
        public int iAnio           { get; set; }
        public int iPeriodo        { get; set; }
        public int iDiasPendientes { get; set; }
        public int iCancelado      { get; set; }
        public int iMotivoBajaId   { get; set; }
        public int iTipoOperacion  { get; set; }
        public int iIdValor { get; set; }
        public string sTipo { get; set; }
        public int iRenglon_id { get; set; }
        public string sNombre_Renglon { get; set; }
        public string sFechaPagoInicio { get; set; }
        public string sFechaPInicio { get; set; }
        public string sFechaPFin { get; set; }
        public string sGravado { get; set; }
        public string sExcento { get; set; }
        public string sSaldo { get; set; }
        public int iEmpresa { get; set; }
        public int iNomina { get; set; }
        public string sNombre { get; set; }
        public string sLeyenda { get; set; }
        public string sMensaje { get; set; }

    }

    public class TipoDeEmpleadoBean { 
        public int iIdTipoEmpleado { get; set; }
        public string sTipodeEmpleado { get; set; }
        public string sMensaje { get; set; }
    }
}