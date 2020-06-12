using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System.Collections.Generic;
using System.Web.Mvc;


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
        public JsonResult DataLocalitySelect(int keyLocality)
        {
            Boolean flag         = false;
            String  messageError = "none";
            LocalidadesBean localitysBean = new LocalidadesBean();
            LocalidadesDao  localitysDaoD = new LocalidadesDao();
            try {
                int keyemp    = int.Parse(Session["IdEmpresa"].ToString());
                localitysBean = localitysDaoD.sp_Dato_Localidad_Seleccionada(keyLocality, keyemp);
                if (localitysBean.sMensaje != "success") {
                    messageError = localitysBean.sMensaje;
                }
                if (localitysBean.sMensaje == "success") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = localitysBean });
        }

        [HttpPost]
        public JsonResult LoadRegPatLocalitys()
        {
            Boolean flag         = false;
            String  messageError = "none";
            List<RegistroPatronalBean2> regPatronalBean = new List<RegistroPatronalBean2>();
            RegistroPatronalDao         regPatronalDaoD = new RegistroPatronalDao();
            try {
                int keyemp = int.Parse(Session["IdEmpresa"].ToString());
                regPatronalBean = regPatronalDaoD.sp_Registro_Patronal_Retrieve_Registros_Patronales(keyemp);
                if (regPatronalBean.Count > 0) {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = regPatronalBean });
        }

        [HttpPost]
        public JsonResult LoadZoneEconomic()
        {
            Boolean flag = false;
            String messageError = "none";
            List<ZonaEconomicaBean> zoneEcoBean = new List<ZonaEconomicaBean>();
            ZonaEconomicaDao        zoneEcoDaoD = new ZonaEconomicaDao();
            try {
                int keyemp = int.Parse(Session["IdEmpresa"].ToString());
                zoneEcoBean = zoneEcoDaoD.sp_Datos_Zonas_Economicas();
                if (zoneEcoBean.Count > 0) {
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = zoneEcoBean });
        }

        [HttpPost]
        public JsonResult LoadEstatesRep()
        {
            Boolean flag = false;
            String messageError = "none";
            List<CatalogoGeneralBean> catGeneralBean = new List<CatalogoGeneralBean>();
            CatalogoGeneralDao        catGeneralDao  = new CatalogoGeneralDao();
            try {
                int keyemp = int.Parse(Session["IdEmpresa"].ToString());
                catGeneralBean = catGeneralDao.sp_CatalogoGeneral_Consulta_CatalogoGeneral(0, "Active/Desactive", 0, 1);
                if (catGeneralBean.Count > 0) {
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = catGeneralBean });
        }

        [HttpPost]
        public JsonResult SaveEditLocality(int keylocality, string desclocality, string ivalocality, int regpatlocality, int zonelocality, int estatelocality, int idreglocality, int idsuclocality)
        {
            Boolean flag = false;
            String  messageError = "none";
            LocalidadesBean2 saveEditLocBean = new LocalidadesBean2();
            LocalidadesDao   saveEditLocDaoD = new LocalidadesDao();
            try {
                saveEditLocBean = saveEditLocDaoD.sp_Update_Localidad(keylocality, desclocality.Trim().ToUpper(), ivalocality, regpatlocality, idreglocality, zonelocality, idsuclocality, estatelocality);
                if (saveEditLocBean.sMensaje != "success") {
                    messageError = saveEditLocBean.sMensaje;
                }
                if (saveEditLocBean.sMensaje == "success") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        [HttpPost]
        public JsonResult SaveDataLocality(string desclocality, string ivalocality, int regpatlocality, int zonelocality, int estatelocality, int idreglocality, int idsuclocality)
        {
            Boolean flag = false;
            String messageError = "none";
            LocalidadesBean2 saveDataLocBean = new LocalidadesBean2();
            LocalidadesDao   saveDataLocDaoD = new LocalidadesDao();
            try {
                int keyemp = int.Parse(Session["IdEmpresa"].ToString());
                saveDataLocBean = saveDataLocDaoD.sp_Insert_Localidad(desclocality.Trim().ToUpper(), ivalocality, regpatlocality, idreglocality, zonelocality, idsuclocality, estatelocality, keyemp);
                if (saveDataLocBean.sMensaje != "success") {
                    messageError = saveDataLocBean.sMensaje;
                }
                if (saveDataLocBean.sMensaje == "success") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Codigo = saveDataLocBean.iCodigoLocalidad });
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