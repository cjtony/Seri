using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class CatalogosController : Controller
    {
        // GET: PETICIONES
        public PartialViewResult VistaCatalogos()
        {
            return PartialView();
        }
        public PartialViewResult Empleados()
        {
            return PartialView();
        }
        public PartialViewResult CEmpleados()
        {
            return PartialView();
        }
        public PartialViewResult Localidades()
        {
            return PartialView();
        }
        public PartialViewResult CLocalidades()
        {
            return PartialView();
        }
        public PartialViewResult Puestos()
        {
            return PartialView();
        }
        public PartialViewResult CPuestos()
        {
            return PartialView();
        }
        public PartialViewResult Regionales()
        {
            return PartialView();
        }
        public PartialViewResult CRegionales()
        {
            return PartialView();
        }
        public PartialViewResult Sucursales()
        {
            return PartialView();
        }
        public PartialViewResult CSucursales()
        {
            return PartialView();
        }
        public PartialViewResult CentrosCostos()
        {
            return PartialView();
        }
        public PartialViewResult CCentrosCostos()
        {
            return PartialView();
        }
        public PartialViewResult PoliticasVacaciones()
        {
            return PartialView();
        }
        public PartialViewResult CPoliticasVacaciones()
        {
            return PartialView();
        }
        public PartialViewResult RegistroPatronal()
        {
            return PartialView();
        }
        public PartialViewResult CRegistroPatronal()
        {
            return PartialView();
        }
        public PartialViewResult FechasPeriodos()
        {
            return PartialView();
        }
        public PartialViewResult CFechasPeriodos()
        {
            return PartialView();
        }
        // POST: PETICIONES
        [HttpPost]
        public JsonResult LoadFechasPeriodos()
        {
            List<InicioFechasPeriodoBean> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
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
        public JsonResult SaveNewEffdt(int Empresa_id, string Effdt)
        {
            List<string> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();

            Lista = Dao.sp_CPoliticasVacaciones_Insert_Effdt_Futura(Empresa_id, Effdt);
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
        [HttpPost]
        public JsonResult DeletePolitica(int Empresa_id, string Effdt, int Anios)
        {
            List<string> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();

            Lista = Dao.sp_CPoliticasVacaciones_Delete_Politica(Empresa_id, Effdt, Anios);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult SaveNewPolitica(string inEmpresa_id, string inEffdt, string inano, string indias, string inprimav, string indiasa)
        {
            List<string> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();

            Lista = Dao.sp_CPoliticasVacaciones_Insert_Politica(inEmpresa_id, inEffdt, inano, indias, inprimav, indiasa);
            return Json(Lista);
        }


        [HttpPost]
        public JsonResult LoadPoliticasVacaciones()
        {
            List<TabPoliticasVacacionesBean> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_Retrieve_CPoliticasVacaciones();
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadPoliticasVacacionesFuturas()
        {
            List<TabPoliticasVacacionesBean> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_Retrieve_CPoliticasVacaciones_Futuras();
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadPoliticasVacacionesDetalle(int Empresa_id)
        {
            List<TabPoliticasVacacionesBean> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_Retrieve_CPoliticasVacaciones_Detalle(Empresa_id);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadPoliticasVacaciones_Futuras_Detalle(int Empresa_id, string Effdt)
        {
            List<TabPoliticasVacacionesBean> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_Retrieve_CPoliticasVacaciones_Futuras_Detalle(Empresa_id, Effdt);
            return Json(Lista);
        }

        [HttpPost]
        public JsonResult LoadPuestos()
        {
            List<TabPoliticasVacacionesBean> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_Retrieve_CPoliticasVacaciones();
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadEmpresasNEmpleados()
        {
            List<EmpleadosxEmpresaBean> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_CEmpresas_Retrieve_NoEmpleados();
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadPuestosxEmpresa(int Empresa_id)
        {
            List<string> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_TPuestos_Retrieve_Puestos_Empresa(Empresa_id);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadPuestosEmpresas()
        {
            List<List<string>> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_TPuestos_Retrieve_Empresas();
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult SearchPuesto(int Empresa_id, string Search)
        {
            List<DataPuestosBean> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_Tpuestos_Search_Puesto(Empresa_id, Search);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadPuesto(int Empresa_id, string Puesto_id)
        {
            List<DataPuestosBean> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_TPuestos_Retrieve_Puesto(Empresa_id, Puesto_id);
            return Json(Lista);
        }
    }
}