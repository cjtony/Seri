using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class CatalogsTablesController : Controller
    {
        // GET: CatalogsTables
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Job()
        {
            List<PuestosBean> listPuestosBean = new List<PuestosBean>();
            PuestosDao puestosDao = new PuestosDao();
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            listPuestosBean = puestosDao.sp_Puestos_Retrieve_Puestos(1, "Active/Desactive", 0, keyemp);
            object json = new { data = listPuestosBean };
            return Json(json);
        }

        // Carga los datos del select profesiones familia
        [HttpPost]
        public JsonResult JobFamily(int state, string type, int keyprof)
        {
            List<ProfesionesFamiliaBean> listProfFamilyBean = new List<ProfesionesFamiliaBean>();
            ProfesionesFamiliaDao profFamilyDao = new ProfesionesFamiliaDao();
            listProfFamilyBean = profFamilyDao.sp_ProfesionesFamilia_Retrieve_ProfesionesFamilia(state, type, keyprof);
            return Json(listProfFamilyBean);
        }

        // Carga los datos del select etiquetas contables
        [HttpPost]
        public JsonResult TagsCont(int state, string type, int keytag)
        {
            List<EtiquetasContablesBean> listTagContBean = new List<EtiquetasContablesBean>();
            EtiquetasContablesDao tagContDao = new EtiquetasContablesDao();
            listTagContBean = tagContDao.sp_EtiquetasContables_Retrieve_EtiquetasContables(state, type, keytag);
            return Json(listTagContBean);
        }

        //Carga los datos del select clasificacion puesto
        [HttpPost]
        public JsonResult ClasifPuest(int state, string type, int keycla, int catalog)
        {
            List<CatalogoGeneralBean> listCatGenBean = new List<CatalogoGeneralBean>();
            CatalogoGeneralDao catGenDao = new CatalogoGeneralDao();
            listCatGenBean = catGenDao.sp_CatalogoGeneral_Consulta_CatalogoGeneral(state, type, keycla, catalog);
            return Json(listCatGenBean);
        }

        //Carga los datos del select nivel jerarquico
        [HttpPost]
        public JsonResult NivJerar(int state, string type, int keyniv)
        {
            List<NivelJerarBean> listNivJerarBean = new List<NivelJerarBean>();
            NivelJerarDao nivJerarDao = new NivelJerarDao();
            listNivJerarBean = nivJerarDao.sp_NivelJerarquico_Retrieve_NivelJerarquico(state, type, keyniv);
            return Json(listNivJerarBean);
        }

        //Carga los datos del select perfomance manager
        [HttpPost]
        public JsonResult PerfManager(int state, string type, int keyper)
        {
            List<PerfomanceManagerBean> listPerfManBean = new List<PerfomanceManagerBean>();
            PerfomanceManagerDao perfManDao = new PerfomanceManagerDao();
            listPerfManBean = perfManDao.sp_PerfomanceManager_Retrieve_PerfomanceManager(state, type, keyper);
            return Json(listPerfManBean);
        }

        //Carga las empresas al momento de registrar un nuevo departamento
        [HttpPost]
        public JsonResult Business(int state, string type, int keyemp)
        {
            List<EmpresasBean> listEmpresasBean = new List<EmpresasBean>();
            EmpresasDao empresasDao = new EmpresasDao();
            int keyempresa = int.Parse(Session["IdEmpresa"].ToString());
            listEmpresasBean = empresasDao.sp_Empresas_Retrieve_Empresas(state, type, keyemp);
            return Json(listEmpresasBean);
        }

        //Carga los centros de costo al momento de registra un nuevo departamento
        [HttpPost]
        public JsonResult CentrCost(int state, string type, int keycos)
        {
            List<CentrosCostosBean> listCentrosCostosBean = new List<CentrosCostosBean>();
            CentrosCostosDao centrosCostosDao = new CentrosCostosDao();
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            listCentrosCostosBean = centrosCostosDao.sp_CentrosCostos_Retrieve_CentrosCostos(state, type, keycos, keyemp);
            return Json(listCentrosCostosBean);
        }

        //Carga los edificios al momento de registrar un nuevo departamento
        [HttpPost]
        public JsonResult Buildings(string type, int keyedi)
        {
            List<EdificiosBean> listEdificiosBean = new List<EdificiosBean>();
            EdificiosDao edificioDao = new EdificiosDao();
            listEdificiosBean = edificioDao.sp_Edificios_Retrieve_Edificios(type, keyedi);
            return Json(listEdificiosBean);
        }

        //Carga los niveles de estructura de registrar un nuevo departamento
        [HttpPost]
        public JsonResult NivEstruct(int state, string type, int keyniv)
        {
            List<NivelEstructuraBean> listNivelEstructuraBean = new List<NivelEstructuraBean>();
            NivelEstructuraDao nivelEstructuraDao = new NivelEstructuraDao();
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            listNivelEstructuraBean = nivelEstructuraDao.sp_NivelEstructura_Retrieve_NivelEstructura(state, type, keyniv, keyemp);
            return Json(listNivelEstructuraBean);
        }

        //Carga los departamentos al momento de registrar un nuevo departamento
        [HttpPost]
        public JsonResult Departaments()
        {
            List<DepartamentosBean> listDepartamentosBean = new List<DepartamentosBean>();
            DepartamentosDao departamentosDao = new DepartamentosDao();
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            listDepartamentosBean = departamentosDao.sp_Departamentos_Retrieve_Departamentos(0, "Active/Desactive", 0, keyemp);
            object json = new { data = listDepartamentosBean };
            return Json(json);
        }
        [HttpPost]
        public JsonResult DepartamentsSearch()
        {
            List<DepartamentosBean> listDepartamentosBean = new List<DepartamentosBean>();
            DepartamentosDao departamentosDao = new DepartamentosDao();
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            listDepartamentosBean = departamentosDao.sp_Departamentos_Retrieve_Departamentos(0, "Active/Desactive", 0, keyemp);
            object json = new { data = listDepartamentosBean };
            var jsonResult = Json(json, JsonRequestBehavior.DenyGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        //Carga el numero de nomina siguiente
        [HttpPost]
        public JsonResult LoadNumNomina(int keyemp)
        {
            NumeroNominaBean numeroNominaBean = new NumeroNominaBean();
            NumeroNominaDao numeroNominaDao = new NumeroNominaDao();

            numeroNominaBean = numeroNominaDao.sp_Consulta_NumeroNomina_Empresa(int.Parse(Session["IdEmpresa"].ToString()));
            return Json(numeroNominaBean);
        }

        //Carga el numero de posicion siguiente
        [HttpPost]
        public JsonResult LoadNumPosicion()
        {
            NumeroPosicionBean numeroPosicionBean = new NumeroPosicionBean();
            NumeroPosicionDao numeroPosicionDao = new NumeroPosicionDao();
            numeroPosicionBean = numeroPosicionDao.sp_Consulta_NumeroPosicion();
            return Json(numeroPosicionBean);
        }
        [HttpPost]
        public JsonResult Positions()
        {
            List<DatosPosicionesBean> posicionBean = new List<DatosPosicionesBean>();
            DatosPosicionesDao posicionDao = new DatosPosicionesDao();
            // Reemplazar por la sesion de empresa
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            string typefil = "NOT IN";
            posicionBean = posicionDao.sp_Posiciones_Retrieve_Posiciones(keyemp, typefil);
            var data = new { data = posicionBean };
            return Json(data);
        }

        [HttpPost]
        public JsonResult Regionales()
        {
            List<RegionalesBean> regionBean = new List<RegionalesBean>();
            RegionesDao regionDao = new RegionesDao();
            regionBean = regionDao.sp_Regionales_Retrieve_Regionales();
            var data = new { data = regionBean };
            return Json(data);
        }

        [HttpPost]
        public JsonResult Sucursales()
        {
            List<SucursalesBean> sucursalBean = new List<SucursalesBean>();
            SucursalesDao regionDao = new SucursalesDao();
            sucursalBean = regionDao.sp_Sucursales_Retrieve_Sucursales();
            var data = new { data = sucursalBean };
            return Json(data);
        }

        [HttpPost]
        public JsonResult SaveSucursal(string desc, string clav)
        {
            SucursalesBean sucursalBean = new SucursalesBean();
            SucursalesDao sucursalDao = new SucursalesDao();
            int usuario = int.Parse(Session["iIdUsuario"].ToString());
            sucursalBean = sucursalDao.sp_Sucursales_Insert_Sucursales(desc, clav, usuario);
            return Json(sucursalBean);
        }
    }
}