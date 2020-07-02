using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class DispersionController : Controller
    {
        // GET: Dispersion

        // Muestra la informacion del periodo actual de la nomina
        [HttpPost]
        public JsonResult LoadInfoPeriodPayroll(string yearAct)
        {
            Boolean flag = false;
            String messageError = "none";
            LoadTypePeriodPayrollBean periodBean = new LoadTypePeriodPayrollBean();
            LoadTypePeriodPayrollDaoD periodDaoD = new LoadTypePeriodPayrollDaoD();
            try
            {
                int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                periodBean = periodDaoD.sp_Load_Info_Periodo_Empr(keyBusiness, Convert.ToInt32(yearAct.ToString().Trim()));
                flag = (periodBean.sMensaje == "success") ? true : false;
            }
            catch (Exception exc)
            {
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, InfoPeriodo = periodBean, MensajeError = messageError });
        }

        // Muestra los datos de los empleados con nomina retenida
        [HttpPost]
        public JsonResult PayrollRetainedEmployees()
        {
            Boolean flag = false;
            String messageError = "none";
            List<PayrollRetainedEmployeesBean> payRetainedBean = new List<PayrollRetainedEmployeesBean>();
            PayrollRetainedEmployeesDaoD payRetainedDaoD = new PayrollRetainedEmployeesDaoD();
            try
            {
                int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                payRetainedBean = payRetainedDaoD.sp_Retrieve_NominasRetenidas(keyBusiness);
            }
            catch (Exception exc)
            {
                messageError = exc.Message.ToString();
            }
            var data = new { data = payRetainedBean };
            return Json(data);
        }

        // Muestra los empleados de la empresa a retener nomina
        [HttpPost]
        public JsonResult SearchEmployeesRetainedPayroll(string searchEmployee, string filter)
        {
            Boolean flag = false;
            String messageError = "none";
            Char[] charactersClear = { ' ', '*', '.', '<', '>', '=', '?', '|', '(', ')', '!', '%', '#', '@', '$', '/', '^' };
            string searchClear = searchEmployee.ToString().Trim(charactersClear);
            List<SearchEmployeePayRetainedBean> employeePayRetBean = new List<SearchEmployeePayRetainedBean>();
            SearchEmployeePayRetainedDaoD employeePayRetDaoD = new SearchEmployeePayRetainedDaoD();
            try
            {
                int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                employeePayRetBean = employeePayRetDaoD.sp_SearchEmploye_Ret_Nomina(keyBusiness, searchClear, filter);
                flag = (employeePayRetBean.Count > 0) ? true : false;
            }
            catch (Exception exc)
            {
                messageError = exc.Message.ToString();
            }
            return Json(employeePayRetBean);
        }

        // Carga el periodo actual
        [HttpPost]
        public JsonResult LoadTypePeriod(int year, int typePeriod)
        {
            Boolean flag = false;
            String messageError = "none";
            LoadTypePeriodBean loadTypePerBean = new LoadTypePeriodBean();
            LoadTypePeriodDaoD loadTypePerDaoD = new LoadTypePeriodDaoD();
            try
            {
                int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                loadTypePerBean = loadTypePerDaoD.sp_Load_Type_Period_Empresa(keyBusiness, year, typePeriod);
                flag = (loadTypePerBean.sMensaje == "success") ? true : false;
            }
            catch (Exception exc)
            {
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = loadTypePerBean });
        }

        // Guarda la retencion de nomina del empleado
        [HttpPost]
        public JsonResult RetainedPayrollEmployee(int keyEmployee, int typePeriod, int periodPayroll, int yearRetained, string descriptionRetained)
        {
            Boolean flag = false;
            String messageError = "none";
            PayrollRetainedEmployeesBean retPayEmployeeBean = new PayrollRetainedEmployeesBean();
            PayrollRetainedEmployeesDaoD retPayEmployeeDaoD = new PayrollRetainedEmployeesDaoD();
            try
            {
                int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                int keyUser = Convert.ToInt32(Session["iIdUsuario"].ToString());
                retPayEmployeeBean = retPayEmployeeDaoD.sp_Insert_Empleado_Retenida_Nomina(keyBusiness, keyEmployee, typePeriod, periodPayroll, yearRetained, descriptionRetained, keyUser);
                flag = (retPayEmployeeBean.sMensaje == "success") ? true : false;
            }
            catch (Exception exc)
            {
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        // Remueve la nomina retenida al empleado
        [HttpPost]
        public JsonResult RemovePayrollRetainedEmployee(int keyPayrollRetained)
        {
            Boolean flag = false;
            String messageError = "none";
            PayrollRetainedEmployeesBean retPayEmployeeBean = new PayrollRetainedEmployeesBean();
            PayrollRetainedEmployeesDaoD retPayEmployeeDaoD = new PayrollRetainedEmployeesDaoD();
            try
            {
                retPayEmployeeBean = retPayEmployeeDaoD.sp_Update_Remove_Nomina_Retenida(keyPayrollRetained);
                flag = (retPayEmployeeBean.sMensaje == "success") ? true : false;
            }
            catch (Exception exc)
            {
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        // Muestra informacion al desplegar la dispersion
        [HttpPost]
        public JsonResult ToDeployDispersion(int yearDispersion, int typePeriodDisp, int periodDispersion, string dateDispersion, string type)
        {
            Boolean flag1 = false, flag2 = false;
            String messageError = "none";
            List<DataDepositsBankingBean> daDepBankingBean = new List<DataDepositsBankingBean>();
            DataDispersionBusiness daDiBusinessDaoD = new DataDispersionBusiness();
            List<BankDetailsBean> bankDetailsBean = new List<BankDetailsBean>();
            try
            {
                int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                daDepBankingBean = daDiBusinessDaoD.sp_Obtiene_Depositos_Bancarios(keyBusiness, yearDispersion, typePeriodDisp, periodDispersion, type);
                if (daDepBankingBean.Count > 0)
                {
                    flag1 = true;
                    bankDetailsBean = daDiBusinessDaoD.sp_Datos_Banco(daDepBankingBean);
                    flag2 = (bankDetailsBean.Count > 0) ? true : false;
                }
            }
            catch (Exception exc)
            {
                messageError = exc.Message.ToString();
            }
            return Json(new { BanderaDispersion = flag1, BanderaBancos = flag2, MensajeError = messageError, DatosDepositos = daDepBankingBean, DatosBancos = bankDetailsBean });
        }

    }
}