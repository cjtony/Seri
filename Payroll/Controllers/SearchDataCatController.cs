using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Payroll.Models.Beans;
using Payroll.Models.Daos;


namespace Payroll.Controllers
{
    public class SearchDataCatController : Controller
    {
        // GET: SearchDataCat
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SearchPuesto(string wordsearch)
        {
            List<PuestosBean> listPuestoBean = new List<PuestosBean>();
            PuestosDao puestoDao = new PuestosDao();
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            listPuestoBean = puestoDao.sp_Puestos_Retrieve_Search_Puestos(wordsearch, keyemp);
            return Json(listPuestoBean);
        }

        [HttpPost]
        public JsonResult DataSelectPuesto(int clvpuesto)
        {
            PuestosBean puestoBean = new PuestosBean();
            PuestosDao puestoDao = new PuestosDao();
            puestoBean = puestoDao.sp_Puestos_Retrieve_Puesto(clvpuesto);
            return Json(puestoBean);
        }

        [HttpPost]
        public JsonResult SearchNumPosition()
        {
            DatosPosicionesBean posicionBean = new DatosPosicionesBean();
            DatosPosicionesDao posicionesDao = new DatosPosicionesDao();
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            posicionBean = posicionesDao.sp_Posicion_Consecutivo_Posicion(keyemp);
            var data = new { result = posicionBean.sPosicionCodigo, mesage = posicionBean.sMensaje };
            return Json(data);
        }

        [HttpPost]
        public JsonResult SearchPositions(string wordsearch, string type)
        {
            List<DatosPosicionesBean> listPosicionesBean = new List<DatosPosicionesBean>();
            DatosPosicionesDao posicionesDao = new DatosPosicionesDao();
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            listPosicionesBean = posicionesDao.sp_Posiciones_Retrieve_Search_Posiciones(wordsearch, keyemp, type);
            return Json(listPosicionesBean);
        }

        [HttpPost]
        public JsonResult SearchPositionsList(string wordsearch)
        {
            List<DatosPosicionesBean> listPosicionesBean = new List<DatosPosicionesBean>();
            DatosPosicionesDao posicionesDao = new DatosPosicionesDao();
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            listPosicionesBean = posicionesDao.sp_Posiciones_Retrieve_Search_Disp_Posiciones(wordsearch, keyemp);
            return Json(listPosicionesBean);
        }

        [HttpPost]
        public JsonResult DataSelectPosition(int clvposition)
        {
            DatosPosicionesBean posicionBean = new DatosPosicionesBean();
            DatosPosicionesDao posicionesDao = new DatosPosicionesDao();
            posicionBean = posicionesDao.sp_Posiciones_Retrieve_Posicion(clvposition);
            return Json(posicionBean);
        }

        [HttpPost]
        public JsonResult SearchRegionales(string wordsearch)
        {
            List<RegionalesBean> listRegionBean = new List<RegionalesBean>();
            RegionesDao regionDao = new RegionesDao();
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            listRegionBean = regionDao.sp_Regionales_Retrieve_Search_Regionales(wordsearch, keyemp);
            return Json(listRegionBean);
        }
        
        [HttpPost]
        public JsonResult DataSelectRegional(int clvregional)
        {
            RegionalesBean regionBean = new RegionalesBean();
            RegionesDao regionDao = new RegionesDao();
            regionBean = regionDao.sp_Regionales_Retrieve_Regional(clvregional);
            return Json(regionBean);
        }

        [HttpPost]
        public JsonResult SearchOffices(string wordsearch)
        {
            List<SucursalesBean> listSucursalesBean = new List<SucursalesBean>();
            SucursalesDao sucursalesDao = new SucursalesDao();
            listSucursalesBean = sucursalesDao.sp_Sucursales_Retrieve_Search_Sucursales(wordsearch);
            return Json(listSucursalesBean);
        }

        [HttpPost]
        public JsonResult DataSelectOffices(int clvsucursal)
        {
            SucursalesBean sucursalBean = new SucursalesBean();
            SucursalesDao sucursalDao = new SucursalesDao();
            sucursalBean = sucursalDao.sp_Sucursales_Retrieve_Sucursal(clvsucursal);
            return Json(sucursalBean);
        }

        [HttpPost]
        public JsonResult SearchDepartaments(string wordsearch, string type)
        {
            List<DepartamentosBean> departamentoBean = new List<DepartamentosBean>();
            DepartamentosDao departamentoDao = new DepartamentosDao();
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            departamentoBean = departamentoDao.sp_Departamentos_Retrieve_Search_Departamentos(wordsearch, keyemp, type);
            return Json(departamentoBean);
        }

        [HttpPost]
        public JsonResult SelectDepartment(int clvdep)
        {
            DepartamentosBean departamentoBean = new DepartamentosBean();
            DepartamentosDao departamentoDao = new DepartamentosDao();
            departamentoBean = departamentoDao.sp_Departamentos_Retrieve_Departamento(clvdep);
            return Json(departamentoBean);
        }

        [HttpPost]
        public JsonResult SearchCentrCost(string wordsearch)
        {
            List<CentrosCostosBean> centrCostBean = new List<CentrosCostosBean>();
            CentrosCostosDao centrCostDao = new CentrosCostosDao();
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            centrCostBean = centrCostDao.sp_CentrosCostos_Retrieve_Search_CentrosCostos(wordsearch, keyemp);
            return Json(centrCostBean);
        }

        [HttpPost]
        public JsonResult DataSelectCentrCost(int keycentrcost)
        {
            Boolean flag         = false;
            String  messageError = "none";
            CentrosCostosBean centrCostBean = new CentrosCostosBean();
            CentrosCostosDao  centrCostDaoD = new CentrosCostosDao();
            try {
                int keyemp    = int.Parse(Session["IdEmpresa"].ToString());
                centrCostBean = centrCostDaoD.sp_Data_Centro_Costo(keyemp, keycentrcost);
                if (centrCostBean.sMensaje != "success") {
                    messageError = centrCostBean.sMensaje;
                }
                if (centrCostBean.sMensaje == "success") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = centrCostBean });
        }

        [HttpPost]
        public JsonResult SaveEditCentrCost(int keycentrcost, string ncentrocosto, string dcentrocosto)
        {
            Boolean flag         = false;
            String  messageError = "none";
            CentrosCostosBean centrCostBean = new CentrosCostosBean();
            CentrosCostosDao  centrCostDaoD = new CentrosCostosDao();
            try {
                int keyemp    = int.Parse(Session["IdEmpresa"].ToString());
                centrCostBean = centrCostDaoD.sp_Update_Centro_Costo(keycentrcost, ncentrocosto, dcentrocosto, keyemp);
                if (centrCostBean.sMensaje != "success") {
                    messageError = centrCostBean.sMensaje;
                }
                if (centrCostBean.sMensaje == "success") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError});
        }

        [HttpPost]
        public JsonResult SaveDataCentrCost(string ncentrcost, string dcentrcost)
        {
            Boolean flag         = false;
            String  messageError = "none";
            CentrosCostosBean centrCostBean = new CentrosCostosBean();
            CentrosCostosDao  centrCostDaoD = new CentrosCostosDao();
            try {
                int keyUser   = int.Parse(Session["iIdUsuario"].ToString());
                int keyEmpr   = int.Parse(Session["IdEmpresa"].ToString());
                centrCostBean = centrCostDaoD.sp_Insert_Centro_Costo(keyEmpr, ncentrcost.Trim().ToUpper(), dcentrcost.Trim().ToUpper(), keyUser);
                if (centrCostBean.sMensaje != "success") {
                    messageError = centrCostBean.sMensaje;
                }
                if (centrCostBean.sMensaje == "success") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        [HttpPost]
        public JsonResult TypesJobs(string typeJob)
        {
            Boolean flag         = false;
            String  messageError = "none";
            List<CodigoCatalogoBean> codeCatBean = new List<CodigoCatalogoBean>();
            CodigoCatalogosDao       codeCatDaoD = new CodigoCatalogosDao();
            try {
                codeCatBean = codeCatDaoD.sp_Datos_Codigo_Catalogo(typeJob);
                if (codeCatBean.Count > 0) {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = codeCatBean });
        }

        [HttpPost]
        public JsonResult SearchEdifics(string wordsearch)
        {
            List<EdificiosBean> edificioBean = new List<EdificiosBean>();
            EdificiosDao edificioDao = new EdificiosDao();
            edificioBean = edificioDao.sp_Edificios_Retrieve_Search_Edificios(wordsearch);
            return Json(edificioBean);
        }

        [HttpPost]
        public JsonResult SearchLocalitys(string wordsearch)
        {
            List<LocalidadesBean2> localBean = new List<LocalidadesBean2>();
            LocalidadesDao localDao = new LocalidadesDao();
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            localBean = localDao.sp_Localidades_Retrieve_Search_Localidades(wordsearch, keyemp);
            return Json(localBean);
        }

        [HttpPost]
        public JsonResult SearchEmploye(string wordsearch, string filtered)
        {
            List<EmpleadosBean> empleadoBean = new List<EmpleadosBean>();
            ListEmpleadosDao empleadoDao = new ListEmpleadosDao();
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            empleadoBean = empleadoDao.sp_Empleados_Retrieve_Search_Empleados(keyemp, wordsearch, filtered);
            return Json(empleadoBean);
        }

    }
}