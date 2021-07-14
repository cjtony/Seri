namespace Payroll.Models.Beans
{
    public class DispersionBean { }
    public class ListRenglonesGruposRestas
    {
        public int iAnio { get; set; }
        public int iPeriodo { get; set; }
        public string sEspejo { get; set; }
        public string sNombreEmpresa { get; set; }
        public string sPaterno { get; set; }
        public string sMaterno { get; set; }
        public string sNombre { get; set; }
        public string sTipoPago { get; set; }
        public int iBanco { get; set; }
        public string sNombreBanco { get; set; }
        public string sCuenta { get; set; }
        public double dRenglon481 { get; set; }
        public double dRenglon9999 { get; set; }
        public int iNomina { get; set; }
        public int iEmpresa { get; set; }
        public double dTotal { get; set; }
        public decimal decimalTotal { get; set; }
        public double  doubleTotalDispersion { get; set; }
        public decimal decimalTotalDispersion { get; set; }
    }
    public class DataErrorAccountBank
    {
        public string sBanco { get; set; }
        public string sNomina { get; set; }
        public string sEmpresa { get; set; }
        public string sCuenta { get; set; }
    }

    public class GroupBusinessDispersionBean
    {
        public int iIdGrupoEmpresa { get; set; }
        public string sNombreGrupo { get; set; }
        public string sMensaje { get; set; }
    }

    public class LoadTypePeriodPayrollBean
    {
        public int iEmpresa_id { get; set; }
        public int iAnio { get; set; }
        public int iTipoPeriodo { get; set; }
        public int iPeriodo { get; set; }
        public string sFechaInicio { get; set; }
        public string sFechaFinal { get; set; }
        public string sMensaje { get; set; }
    }

    public class PayrollRetainedEmployeesBean
    {
        public int iIdNominaRetenida { get; set; }
        public string sNombreEmpleado { get; set; }
        public int iIdEmpresa { get; set; }
        public int iIdEmpleado { get; set; }
        public int iTipoPeriodo { get; set; }
        public int iPeriodo { get; set; }
        public string sAnio { get; set; }
        public string sDescripcion { get; set; }
        public int iActivo { get; set; }
        public string sFechaAlta { get; set; }
        public int iUsuarioId { get; set; }
        public string sMensaje { get; set; }
    }

    public class SearchEmployeePayRetainedBean
    {
        public int iIdEmpleado { get; set; }
        public string sNombreEmpleado { get; set; }
        public string sNominaEmpleado { get; set; }
        public int iTipoPeriodo { get; set; }
        public string sMensaje { get; set; }
    }

    public class LoadTypePeriodBean
    {
        public int iEmpresa_id { get; set; }
        public int iAnio { get; set; }
        public int iTipoPeriodo { get; set; }
        public int iPeriodo { get; set; }
        public string sFechaInicio { get; set; }
        public string sFechaFinal { get; set; }
        public string sMensaje { get; set; }
    }

    public class DataDepositsBankingBean
    {
        public int iIdEmpresa { get; set; }
        public int iIdBanco { get; set; }
        public string sBanco { get; set; }
        public int iIdRenglon { get; set; }
        public int iDepositos { get; set; }
        public string sImporte { get; set; }
        public double dImporteSF { get; set; }
    }

    public class BankDetailsBean
    {
        public int iIdBanco { get; set; }
        public string sNombreBanco { get; set; }
        public string sSufijo { get; set; }
    }

    public class DatosEmpresaBeanDispersion
    {
        public string sNombreEmpresa { get; set; }
        public string sCalle { get; set; }
        public string sColonia { get; set; }
        public string sCodigoPostal { get; set; }
        public string sCiudad { get; set; }
        public string sRfc { get; set; }
        public int iRegimen_Fiscal_id { get; set; }
        public string sDelegacion { get; set; }
        public int iBanco_id { get; set; }
        public string sDescripcion { get; set; }
        public string sMensaje { get; set; }
    }

    public class DatosDepositosBancariosBean
    {
        public int iIdBanco { get; set; }
        public int iCantidad { get; set; }
        public string sImporte { get; set; }
    }

    public class DatosProcesaChequesNominaBean
    {
        public int iCodigoTXT { get; set; }
        public string sCodigo { get; set; }
        public int iIdBanco { get; set; }
        public string sBanco { get; set; }
        public int iIdEmpresa { get; set; }
        public string sNomina { get; set; }
        public string sCuenta { get; set; }
        public decimal dImporte { get; set; }
        public string sImporte { get; set; }
        public double doImporte { get; set; }
        public string sNombre { get; set; }
        public string sPaterno { get; set; }
        public string sMaterno { get; set; }
        public string sRfc { get; set; }
        public int iTipoPago { get; set; }
    }

    public class DatosCuentaClienteBancoEmpresaBean
    {
        public string sNumeroCliente { get; set; }
        public string sNumeroCuenta { get; set; }
        public string sVacio { get; set; }
        public int iPlaza { get; set; }
        public string sClabe { get; set; }
        public int iTipo { get; set; }
        public int iCodigo { get; set; }
        public string sMensaje { get; set; }
        public string sRFC { get; set; }
    }

    public class DatosDispersionArchivosBanamex {
        public string nameFile { get; set; }
    }

}