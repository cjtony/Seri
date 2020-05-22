using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Payroll.Models.Beans;
using Payroll.Models.Daos;

namespace Payroll.Controllers
{
    public class BajasEmpleadosController : Controller
    {
        // GET: BajasEmpleados
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public JsonResult SendDataDownSettlement(int keyEmployee, string dateAntiquityEmp, int idTypeDown, int idReasonsDown, string dateDownEmp, string dateReceipt, int typeDate, int typeCompensation)
        {
            Boolean flag         = false;
            String  messageError = "none";
            string  typeDateStr  = "";
            string  typeCompensationStr = "";
            string  dateDownFormat    = Convert.ToDateTime(dateDownEmp).ToString("dd/MM/yyyy");
            string  dateReceiptFormat = Convert.ToDateTime(dateReceipt).ToString("dd/MM/yyyy");
            BajasEmpleadosBean downEmployeeBean = new BajasEmpleadosBean();
            BajasEmpleadosDaoD downEmployeeDaoD = new BajasEmpleadosDaoD();
            List<BajasEmpleadosBean> listDataDownEmp = new List<BajasEmpleadosBean>();
            try {
                int keyBusiness     = int.Parse(Session["IdEmpresa"].ToString());
                typeDateStr         = (typeDate == 0) ? "Fecha Antiguedad" : "Fecha Ingreso";
                typeCompensationStr = (typeCompensation == 0) ? "Sin compensacion especial" : "Con compensacion especial";
                downEmployeeBean    = downEmployeeDaoD.sp_CNomina_Finiquito(keyBusiness, keyEmployee, dateAntiquityEmp, idTypeDown, idReasonsDown, dateDownFormat, dateReceiptFormat, typeDate, typeCompensation);
                if (downEmployeeBean.sMensaje == "SUCCESS") {
                    listDataDownEmp = downEmployeeDaoD.sp_Finiquitos_Empleado(keyEmployee, keyBusiness);
                    if (listDataDownEmp.Count > 0) {
                        flag = true;
                    } else {
                        messageError = "ERRMOSTINFO";
                    }
                } else {
                    messageError = "ERRINSFINIQ";
                }
            } catch (Exception exc) {
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, DatosFiniquito = listDataDownEmp });
        }

    }
}