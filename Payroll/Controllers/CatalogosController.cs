using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class CatalogosController : Controller
    {
        // GET: Catalogos
        public PartialViewResult VistaCatalogos()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult LoadFechasPeriodos()
        {
            List<InicioFechasPeriodoBean> Lista;
            ModCatalogosDao  Dao = new ModCatalogosDao();
            Lista = Dao.sp_Retrieve_CInicio_Fechas_Periodo();
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadFechasPeriodosDetalle(int Empresa_id)
        {
            List<InicioFechasPeriodoBean> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_Retrieve_CInicio_Fechas_Periodo_Detalle(Empresa_id);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult SaveNewPeriodo(int inEmpresa_id, int inano, int inperiodo, string infinicio, string inffinal, string infproceso, string infpago, int indiaspago)
        {
            List<string> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            
            Lista = Dao.sp_CInicio_Fechas_Periodo_Insert_Fecha_Periodo(inEmpresa_id, inano, inperiodo, infinicio, inffinal, infproceso, infpago, indiaspago);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult DeletePeriodo(int Empresa_id, int Id)
        {
            List<string> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            
            Lista = Dao.sp_CInicio_Fechas_Periodo_Delete_Fecha_Periodo(Empresa_id, Id);
            return Json(Lista);
        }
    }
}