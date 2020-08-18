using System;

namespace Payroll.Models.Beans
{
    public class NominaBean
    {



    }

    public class NominahdBean
    {

        public int iIdDefinicionhd { get; set; }
        public string sNombreDefinicion { get; set; }
        public string sDescripcion { get; set; }
        public int iAno { get; set; }
        public string iCancelado { get; set; }
        public int iUsuarioAlta { get; set; }
        public string sFechaAlta { get; set; }

        public string sMensaje { get; set; }

    }

    public class NominaLnBean
    {
        public int iIdDefinicionln { get; set; }
        public int iIdDefinicionHd { get; set; }
        public int iIdEmpresa { get; set; }
        public int iTipodeperiodo { get; set; }
        public int iIdperiodo { get; set; }
        public int iRenglon { get; set; }
        public int iCancelado { get; set; }
        public int iIdusuarioalta { get; set; }
        public string sFechaalta { get; set; }
        public int iElementonomina { get; set; }
        public int iEsespejo { get; set; }

        public int iIdAcumulado { get; set; }

        public string sMensaje { get; set; }

    }

    public class CTipoPeriodoBean
    {
        public int iId { get; set; }
        public string sValor { get; set; }
       
        public string sMensaje { get; set; }
    }

    public class CRenglonesBean
    {
        public int iIdEmpresa { get; set; }
        public string sIdEmpresa { get; set; }
        public int iIdRenglon { get; set; }
        public string sNombreRenglon { get; set; }
        public int iIdElementoNomina { get; set; }
        public string sIdElementoNomina { get; set; }
        public int iIdSeccionReporte { get; set; }
        public string sIdSeccionReporte { get; set; }
        public int iIdAcumulado { get; set; }
        public string sIdAcumulado { get; set; }
        public int iCancelado { get; set; }
        public string sCancelado { get; set; }
        public int iTipodeRenglon { get; set; }
        public string sTipodeRenglon { get; set; }
        public int iEspejo { get; set; }
        public string sEspejo { get; set; }
        public int ilistCalclos { get; set; }
        public string slistCalculos { get; set; }
        public string sCuentaCont { get; set; }
        public string sDespCuCont { get; set; }
        public string sCargAbCuenta { get; set; }
        public int iIdSat { get; set; }
        public string sIdSat { get; set; }
        public string sMensaje { get; set; }
    }


    public class CAcumuladosRenglon
    {
        public int iIdEmpresa { get; set; }
        public int iIdRenglon { get; set; }
        public int iIdAcumulado { get; set; }
        public string sDesAcumulado { get; set; }
        public string sCuentaContable { get; set; }
        public string sDesConcepto { get; set; }
        public string sMensaje { get; set; }

    }

    public class CInicioFechasPeriodoBean
    {
        public int iId { get; set; }
        public int iIdEmpresesas { get; set; }
        public int ianio { get; set; }
        public int iTipoPeriodo { get; set; }
        public int iPeriodo { get; set; }
        public string sNominaCerrada { get; set; }
        public string sFechaInicio { get; set; }
        public string sFechaFinal { get; set; }
        public string sFechaProceso { get; set; }
        public string sFechaPago { get; set; }
        public int iDiasEfectivos { get; set; }
        public string sMensaje { get; set; }

    }

    public class NominaLnDatBean
    {
        public string iIdDefinicionln { get; set; }
        public string iIdDefinicionHd { get; set; }
        public string IdEmpresa { get; set; }
        public string iTipodeperiodo { get; set; }
        public string iIdperiodo { get; set; }
        public string iRenglon { get; set; }
        public string iCancelado { get; set; }
        public string iIdusuarioalta { get; set; }
        public string sFechaalta { get; set; }
        public string iElementonomina { get; set; }
        public string iEsespejo { get; set; }

        public string iIdAcumulado { get; set; }

        public string sMensaje { get; set; }

    }

    public class NominasHdDatBean
    {
        public string iIdDefinicionhd { get; set; }
        public string sNombreDefinicion { get; set; }
        public string sDescripcion { get; set; }
        public string iAno { get; set; }
        public string iCancelado { get; set; }
        public string iUsuarioAlta { get; set; }
        public string sFechaAlta { get; set; }
        public string sMensaje { get; set; }


    }

    public class TpCalculosHd
    {
        public int iIdCalculosHd { get; set; }
        public int iIdDefinicionHd { get; set; }
        public int iInicioCalculos { get; set; }
        public int iFinCalculos { get; set; }
        public int iNominaCerrada { get; set; }
        public string sMensaje { get; set; }
    }

    public class TpCalculosLn
    {

        public int iIdCalculosLn { get; set; }
        public int iIdCalculosHd { get; set; }
        public int iIdEmpresa { get; set; }
        public int iIdEmpleado { get; set; }
        public int iAnio { get; set; }
        public int iIdTipoPeriodo { get; set; }
        public int iPeriodo { get; set; }
        public int iConsecutivo { get; set; }
        public int iIdRenglon { get; set; }
        public string iImporte { get; set; }
        public string iSaldo { get; set; }
        public string iGravado { get; set; }
        public string iExcento { get; set; }
        public string sFecha { get; set; }
        public string iInactivo { get; set; }
        public int iTipoEmpleado { get; set; }
        public int iIdDepartamento { get; set; }
        public string EsEspejo { get; set; }
        public string sMensaje { get; set; }

    }

    public class TpCalculosCarBean
    {
        public string sValor { get; set; }
        public int iIdRenglon { get; set; }
        public string sNombreRenglon { get; set; }
        public decimal dTotal { get; set; }
        public string sTotal { get; set; }
        public string sMensaje { get; set; }
    }
    public class TPProcesos
    {
        public int iIdTarea { get; set; }
        public int iIdJobs { get; set; }
        public string sEstatusJobs { get; set; }
        public string sNombre { get; set; }
        public string sParametros { get; set; }
        public string sMensaje { get; set; }

    }

    public class HangfireJobs
    {
        public int iId { get; set; }
        public int iStateldId { get; set; }
        public string sArguments { get; set; }
        public string sInvocacionData { get; set; }
        public string sCreatedAt { get; set; }
        public string sMensaje { get; set; }

    }

    public class EmisorReceptorBean
    {
        public string sNombreEmpresa { get; set; }
        public string sCalle { get; set; }
        public string sColonia { get; set; }
        public string sCiudad { get; set; }
        public string sRFC { get; set; }
        public string sAfiliacionIMSS { get; set; }
        public string sNombreComp { get; set; }
        public string sRFCEmpleado { get; set; }
        public int iIdEmpleado { get; set; }
        public string sDescripcionDepartamento { get; set; }
        public string sNombrePuesto { get; set; }
        public string sFechaIngreso { get; set; }
        public string sTipoContrato { get; set; }
        public string sCentroCosto { get; set; }
        public decimal dSalarioMensual { get; set; }
        public string sRegistroImss { get; set; }
        public string sCURP { get; set; }
        public string sDescripcion { get; set; }
        public String sCtaCheques { get; set; }
        public int iRegimenFiscal { get; set; }
        public int iIdNomina { get; set; }
        public string sUrl {get;set;}
        public string sMensaje { get; set; }

    }

    public class EmpleadosEmpresaBean
    {
        public int iIdEmpleado { get; set; }
        public string sNombreCompleto { get; set; }
        public string sMensaje { get; set; }
    }

    public class ReciboNominaBean
    {
        public int iIdFiniquito { get; set; } 
        public int iIdEmpleado { get; set; }
        public int iIdRenglon { get; set; }
        public int iIdTipoPeriodo { get; set; }
        public string sNombre_Renglon { get; set; }
        public decimal dSaldo { get; set; }
        public int iConsecutivo { get; set; }
        public int iIdCalculoshd { get; set; }
        public int iElementoNomina { get; set; }
        public string sValor { get; set; }
       public string sEspejo { get; set; }
       public string sMensaje { get; set; }

    }

    public class TablaNominaBean
    {
        public string sConcepto { get; set; }
        public string dPercepciones { get; set; }
        public string dDeducciones { get; set; }
        public string dSaldos { get; set; }
        public string dInformativos { get; set; }
        public string sMensaje { get; set; }
    }

    public class XMLBean
    {

        public string sfilecer { get; set; }
        public string sfilekey { get; set; }
        public string stransitorio { get; set; }
        public int ifolio { get; set; }
        public string sMensaje { get; set; }
    }

    public class SelloSatBean {
        public int iIdCalculosHd { get; set; }
        public int iIdEmpresa { get; set; }
        public int iIdEmpleado { get; set; }
        public int ianio { get; set; }
        public int iTipoPeriodo { get; set; }
        public int iPeriodo { get; set; }
        public string sRecibos { get; set; }
        public string sSelloSat { get; set; }
        public string sUUID { get; set; }
        public string sSelloCFD { get; set; }
        public string Rfcprov { get; set; }
        public string sNoCertificado { get; set; }
        public string Fechatimbrado { get; set; }
        public string Fecha { get; set; }
        public string sMensaje { get; set; }
    }
    public class ElementoNominaBean
    {
        public int iIdValor { get; set; }
        public string sValor { get; set; }
        public string sMensaje { get; set; }
    }

    public class ListSatBean { 
       public int idSat { get; set; }
       public string sSat { get; set; }
       public string sMensaje { get; set; }
    }

    public class ListEmpleadoNomBean { 
       public int iIdEmpresa { get; set; }
       public int iIdEmpleado { get; set; }
       public int ianio { get; set; }
       public int TipoPeriodo { get; set; }
       public int Periodo { get; set; }
       public string sMensaje { get; set; }

    }

    public class ControlEjecucionBean { 
      public int iIdContro { get; set; }
      public int iIdempresa { get; set; }
      public int iInactivo { get; set; }
      public int sfecha { get; set; }
      public string sMensaje { get; set; }
    }

    public class TipoFiniquito {

        public int iIdTipoFiniquito { get; set; }
        public string sNombreFiniquito { get; set; }
     
        public string sMensaje { get; set; }

    }
    
}