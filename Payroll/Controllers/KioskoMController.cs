using Payroll.Models.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class KioskoMController : Controller
    {
        // GET: KioscoM
        public PartialViewResult ConsultaRecibo()
        {
            return PartialView();
        }
        public PartialViewResult AutorizacionVacaciones()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult getSolicitudesSinAprobar()
        {
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            List<List<string>> list = Dao.sp_TPeriodosVacaciones_retrieve_solicitudes_pendientes(int.Parse(Session["iIdUsuario"].ToString()));
            return Json(list);
        }
        [HttpPost]
        public JsonResult getSolicitudesAprobadas()
        {
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            List<List<string>> list = Dao.sp_TPeriodosVacaciones_retrieve_solicitudes_aprobadas(int.Parse(Session["iIdUsuario"].ToString()));
            return Json(list);
        }
        [HttpPost]
        public JsonResult getSolicitudesRechazadas()
        {
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            List<List<string>> list = Dao.sp_TPeriodosVacaciones_retrieve_solicitudes_rechazadas(int.Parse(Session["iIdUsuario"].ToString()));
            return Json(list);
        }
    }
}