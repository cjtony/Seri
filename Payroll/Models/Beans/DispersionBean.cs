namespace Payroll.Models.Beans
{
    public class DispersionBean { }


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
        public decimal dImporte { get; set; }
    }

    public class BankDetailsBean
    {
        public int iIdBanco { get; set; }
        public string sNombreBanco { get; set; }
        public string sSufijo { get; set; }
    }

}