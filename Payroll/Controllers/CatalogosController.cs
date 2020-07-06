using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System;
using System.Collections.Generic;
using System.Data;
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
        public PartialViewResult GruposEmpresas()
        {
            return PartialView();
        }
        public PartialViewResult CGruposEmpresas()
        {
            return PartialView();
        }
        // POST: PETICIONES
        public PartialViewResult VistaRenglones() {
            return PartialView();
        }
        public PartialViewResult CBancos()
        {
            return PartialView();
        }
        public PartialViewResult CUsuarios()
        {
            return PartialView();
        }
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
        public JsonResult datRenglones(int IdEmpresa, int iElemntoNOm)
        {
            List<CRenglonesBean> LR = new List<CRenglonesBean>();
            ModCatalogosDao Dao = new ModCatalogosDao();
            LR = Dao.sp_CRenglones_Retrieve_CRenglones(IdEmpresa, iElemntoNOm);
            return Json(LR);
        }

        [HttpPost]
        public JsonResult LoadGruposEmpresas()
        {
            List<List<string>> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_CGruposEmpresas_Retrieve_Grupos();
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadEmpresasGrupo(int Grupo_id)
        {
            List<List<string>> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_CGruposEmpresas_Retrieve_EmpresasGrupo(Grupo_id);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadAllPuestos()
        {
            List<DataPuestosBean> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_TPuestos_Retrieve_AllPuestos();
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadPolitica(int Empresa_id, string Effdt, string Anio)
        {
            List<TabPoliticasVacacionesBean> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_CPoliticasVacaciones_Retrieve_Politica(Empresa_id, Effdt, Anio);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult UpdatePolitica(int Empresa_id, string Effdt, int Anio, int Dias, int Diasa, int Prima, int Anion)
        {
            List<string> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_CPoliticasVacaciones_Update_Politica(Empresa_id, Effdt, Anio, Dias, Diasa, Prima, Anion);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadPeriodo(int Empresa_id, int Id)
        {
            List<InicioFechasPeriodoBean> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_CInicio_Fechas_Periodo_Retrieve_Periodo(Empresa_id, Id);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult UpdatePeriodo(int Empresa_id, int editid, int editano, int editperiodo, string editfinicio, string editffinal, string editfproceso, string editfpago, int editdiaspago)
        {
            List<string> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();

            Lista = Dao.sp_CInicio_Fechas_Periodo_Update_Periodo(Empresa_id, editid, editano, editperiodo, editfinicio, editffinal, editfproceso, editfpago, editdiaspago);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadBancosEmpresa(int Empresa_id)
        {
            List<TabBancosEmpresas> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_BancosEmpresas_Retrieve_Bancos(Empresa_id);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadTipoBanco()
        {
            List<InfDomicilioBean> Lista;
            InfDomicilioDao Dao = new InfDomicilioDao();
            Lista = Dao.sp_CatalogoGeneral_Retrieve_CatalogoGeneral(31);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult SaveNewBanco(int Empresa_id, int Banco_id, int TipoBanco, int Cliente, int Plaza, int CuentaEmp, int Clabe)
        {
            List<string> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_BancosEmpresas_Insert_Banco(Empresa_id, Banco_id, TipoBanco, Cliente, Plaza, CuentaEmp, Clabe);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult UpdateBancoEmpresa(int Banco_id, int TipoBanco, int Id, int Cliente, int Plaza, string CuentaEmp, string Clabe)
        {
            List<string> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_BancosEmpresas_updatebanco_Banco(Banco_id, TipoBanco, Id, Cliente, Plaza, CuentaEmp, Clabe);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult UpdateBanco(int key, int Id)
        {
            List<string> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_BancosEmpresas_updatestatus_Banco(key, Id);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadUsers()
        {
            List<DataUsersBean> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_CUsuarios_Retrieve_Users();
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadProfiles()
        {
            List<DataProfilesBean> Lista;
            ModCatalogosDao Dao = new ModCatalogosDao();
            Lista = Dao.sp_CPerfiles_Retrieve_Perfiles();
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult Loadmainmenus(int Id)
        {
            List<MainMenuBean> Lista;
            MainMenuDao Dao = new MainMenuDao();
            Lista = Dao.sp_Retrieve_Menu_Paths(1);
            return Json(Lista);
        }
        public JsonResult Loadonemenu(int Id)
        {
            List<MainMenuBean> Lista;
            MainMenuDao Dao = new MainMenuDao();
            Lista = Dao.Bring_Main_Menus(1,Id);
            return Json(Lista);
        }
    }
}