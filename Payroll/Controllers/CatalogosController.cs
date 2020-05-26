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
    }
}