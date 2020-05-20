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
        public JsonResult SendDataDownSettlement(int keyEmployee, int idTypeDown, string dateDownEmp, int typeDate, int typeCompensation)
        {
            Boolean flag         = false;
            String  messageError = "none";
            string  typeDateStr  = "";
            string  typeCompensationStr = "";
            BajasEmpleadosBean downEmployeeBean = new BajasEmpleadosBean();
            BajasEmpleadosDaoD downEmployeeDaoD = new BajasEmpleadosDaoD();
            try {
                int keyBusiness     = int.Parse(Session["IdEmpresa"].ToString());
                typeDateStr         = (typeDate == 0) ? "Fecha Antiguedad" : "Fecha Ingreso";
                typeCompensationStr = (typeCompensation == 0) ? "Sin compensacion especial" : "Con compensacion especial";
            } catch (Exception exc) {
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, TipoFechaInt = typeDate, TipoCompensacionInt = typeCompensation, TipoFechaStr = typeDateStr, TipoCompensacionStr = typeCompensationStr });
        }

    }
}