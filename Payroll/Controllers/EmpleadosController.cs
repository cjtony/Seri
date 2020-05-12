using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class EmpleadosController : Controller
    {
        public PartialViewResult Datos_Generales()
        {
            return PartialView();
        }
        public PartialViewResult IMSS()
        {
            return PartialView();
        }
        public PartialViewResult Datos_Nomina()
        {
            return PartialView();
        }
        public PartialViewResult Estructura()
        {
            return PartialView();
        }
        public PartialViewResult RecibosNomina()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult LoadStates()
        {
            InfDomicilioDao infDomDao = new InfDomicilioDao();
            List<InfDomicilioBean> infDomBean = new List<InfDomicilioBean>();
            int type = 1;
            infDomBean = infDomDao.sp_CatalogoGeneral_Retrieve_CatalogoGeneral(type);
            var data = new { resp = "bien" };
            return Json(infDomBean);
        }
        [HttpPost]
        public JsonResult LoadInformationHome2(int codepost)
        {
            InfDomicilioDao infDomDao = new InfDomicilioDao();
            List<InfoDireccionByCPBean> listInfDomBean = new List<InfoDireccionByCPBean>();
            listInfDomBean = infDomDao.sp_Domicilio_Retrieve_Domicilio2(codepost);
            return Json(listInfDomBean);
        }
        [HttpPost]
        public JsonResult LoadInformationHome(int codepost, int state)
        {
            InfDomicilioDao infDomDao = new InfDomicilioDao();
            List<InfDomicilioBean> listInfDomBean = new List<InfDomicilioBean>();
            listInfDomBean = infDomDao.sp_Domicilio_Retrieve_Domicilio(codepost, state);
            return Json(listInfDomBean);
        }
        [HttpPost]
        public JsonResult LoadDataCatGen(int state, string type, int keycat, int keycam)
        {
            CatalogoGeneralDao catGenDao = new CatalogoGeneralDao();
            List<CatalogoGeneralBean> catGenBean = new List<CatalogoGeneralBean>();
            catGenBean = catGenDao.sp_CatalogoGeneral_Consulta_CatalogoGeneral(state, type, keycat, keycam);
            return Json(catGenBean);
        }

        [HttpPost]
        public JsonResult LoadNivelStudy(int state, string type, int keynivel)
        {
            NivelEstudiosDao nivEstDao = new NivelEstudiosDao();
            List<NivelEstudiosBean> nivEstBean = new List<NivelEstudiosBean>();
            nivEstBean = nivEstDao.sp_NivelEstudios_Retrieve_NivelEstudios(state, type, keynivel);
            return Json(nivEstBean);
        }

        [HttpPost]
        public JsonResult LoadTabs(int state, string type, int keytab)
        {
            TabuladoresDao tabDao = new TabuladoresDao();
            List<TabuladoresBean> tabBean = new List<TabuladoresBean>();
            tabBean = tabDao.sp_Tabuladores_Retrieve_Tabuladores(state, type, keytab);
            return Json(tabBean);
        }

        [HttpPost]
        public JsonResult LoadBanks(int keyban)
        {
            BancosDao banDao = new BancosDao();
            List<BancosBean> banBean = new List<BancosBean>();
            banBean = banDao.sp_Bancos_Retrieve_Bancos(keyban);
            return Json(banBean);
        }

        public JsonResult Submenus(int IdItem)
        {

            return Json("");
        }
        [HttpPost]
        public JsonResult SearchEmpleados(string txtSearch)
        {
            List<DescEmpleadoVacacionesBean> empleados = new List<DescEmpleadoVacacionesBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            empleados = Dao.sp_Retrieve_liveSearchEmpleado(int.Parse(Session["IdEmpresa"].ToString()), txtSearch);

            return Json(empleados);
        }
        [HttpPost]
        public JsonResult SearchEmpleado(int IdEmpleado)
        {
            @Session["Empleado_id"] = IdEmpleado;
            List<DescEmpleadoVacacionesBean> empleados = new List<DescEmpleadoVacacionesBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            empleados = Dao.sp_CEmpleado_Retrieve_Empleado(int.Parse(Session["Empleado_id"].ToString()), int.Parse(Session["IdEmpresa"].ToString()));
            return Json(empleados);
        }
        [HttpPost]
        public JsonResult DataTabGenEmploye(int keyemploye)
        {
            EmpleadosBean empleadoBean = new EmpleadosBean();
            ListEmpleadosDao listEmpleadoDao = new ListEmpleadosDao();
            empleadoBean = listEmpleadoDao.sp_Empleados_Retrieve_Empleado(keyemploye);
            return Json(empleadoBean);
        }

        [HttpPost]
        public JsonResult DataTabImssEmploye(int keyemploye)
        {
            ImssBean imssBean = new ImssBean();
            ListEmpleadosDao listEmpleadoDao = new ListEmpleadosDao();
            // Reemplazar por la sesion de la empresa
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            imssBean = listEmpleadoDao.sp_Imss_Retrieve_ImssEmpleado(keyemploye, keyemp);
            return Json(imssBean);
        }

        [HttpPost]
        public JsonResult DataTabNominaEmploye(int keyemploye)
        {
            DatosNominaBean datoNominaBean = new DatosNominaBean();
            ListEmpleadosDao listEmpleadoDao = new ListEmpleadosDao();
            // Reemplazar por la sesion de la empresa
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            datoNominaBean = listEmpleadoDao.sp_Nominas_Retrieve_NominaEmpleado(keyemploye, keyemp);
            return Json(datoNominaBean);
        }

        [HttpPost]
        public JsonResult DataTabStructureEmploye(int keyemploye)
        {
            DatosPosicionesBean datoPosicionBean = new DatosPosicionesBean();
            ListEmpleadosDao listEmpleadoDao = new ListEmpleadosDao();
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            datoPosicionBean = listEmpleadoDao.sp_Posiciones_Retrieve_PosicionEmpleado(keyemploye, keyemp);
            return Json(datoPosicionBean);
        }
        //Codigo nuevo
        [HttpPost]
        public JsonResult LoadTypePer()
        {
            List<TipoPeriodosBean> tipoPeriodoBean = new List<TipoPeriodosBean>();
            TipoPeriodosDao tipoPeriodoDao = new TipoPeriodosDao();
            tipoPeriodoBean = tipoPeriodoDao.sp_TipoPeriodos_Retrieve_TipoPeriodos();
            return Json(tipoPeriodoBean);
        }

        [HttpPost]
        public JsonResult LoadLocalitys()
        {
            List<LocalidadesBean2> localidadBean = new List<LocalidadesBean2>();
            LocalidadesDao localidadDao = new LocalidadesDao();
            //Reemplazar por la sesion de la empresa
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            localidadBean = localidadDao.sp_TLocalicades_Retrieve_Localidades(keyemp);
            return Json(localidadBean);
        }

        [HttpPost]
        public JsonResult LoadPositiosRep()
        {
            List<DatosPosicionesBean> posicionBean = new List<DatosPosicionesBean>();
            DatosPosicionesDao posicionDao = new DatosPosicionesDao();
            // Reemplazar por la sesion de empresa
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            string typefil = "AllPositions";
            posicionBean = posicionDao.sp_Posiciones_Retrieve_Posiciones(keyemp, typefil);
            return Json(posicionBean);
        }
        [HttpPost]
        public JsonResult LoadRegPatCla()
        {
            List<RegistroPatronalBean2> regPatronalBean = new List<RegistroPatronalBean2>();
            RegistroPatronalDao regPatronalDao = new RegistroPatronalDao();
            // Reemplazar por la sesion de la empresa
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            regPatronalBean = regPatronalDao.sp_Registro_Patronal_Retrieve_Registros_Patronales(keyemp);
            return Json(regPatronalBean);
        }

        [HttpPost]
        public JsonResult LoadNacions()
        {
            List<NacionalidadesBean> nacionBean = new List<NacionalidadesBean>();
            NacionalidadesDao nacionDao = new NacionalidadesDao();
            nacionBean = nacionDao.sp_Nacionalidades_Retrieve_Nacionalidades();
            return Json(nacionBean);
        }
        [HttpPost]
        public JsonResult UpdatePosicionAct(int clvemp)
        {
            EmpleadosBean empleadoBean = new EmpleadosBean();
            EmpleadosDao empleadoDao = new EmpleadosDao();
            // Reemplazar por la sesion de la empresa
            int keyemp = int.Parse(Session["IdEmpresa"].ToString());
            empleadoBean = empleadoDao.sp_Empleado_Update_PosicionNomina(clvemp, keyemp);
            var data = new { empleado = clvemp, result = empleadoBean.sMensaje };
            return Json(data);
        }

        [HttpPost]
        public JsonResult DataListEmpleado(int iIdEmpresa)
        {
            List<EmpleadosEmpresaBean> ListEmple = new List<EmpleadosEmpresaBean>();
            ListEmpleadosDao Dao = new ListEmpleadosDao();
            ListEmple = Dao.sp_EmpleadosDEmpresa_Retrieve_EmpleadosDEmpresa(iIdEmpresa);
            return Json(ListEmple);
        }
        [HttpPost]
        public JsonResult EmisorEmpresa(int IdEmpresa, string sNombreComple)
        {

            string[] Nombre = sNombreComple.Split(' ');
            string Idempleado = Nombre[0].ToString();
            int id = int.Parse(Idempleado);
            List<EmisorReceptorBean> ListDatEmisor = new List<EmisorReceptorBean>();
            ListEmpleadosDao Dao = new ListEmpleadosDao();
            ListDatEmisor = Dao.sp_EmisorReceptor_Retrieve_EmisorReceptor(IdEmpresa, id);

            return Json(ListDatEmisor);
        }

        //public JsonResult ListDatPeriodo(int iIdEmpresesas, int ianio, int iTipoPeriodo, int iPeriodo)
        //{
        //    List<CInicioFechasPeriodoBean> LPe = new List<CInicioFechasPeriodoBean>();
        //    ListEmpleadosDao dao = new ListEmpleadosDao();
        //    LPe = dao.sp_DatosPerido_Retrieve_DatosPerido(iIdEmpresesas, ianio, iTipoPeriodo, iPeriodo);
        //    return Json(LPe);

        //}

        [HttpPost]

        public JsonResult ReciboNomina(int iIdEmpresa, int iIdEmpleado, int iPeriodo)
        {

            List<ReciboNominaBean> LCRecibo = new List<ReciboNominaBean>();
            List<TablaNominaBean> LsTabla = new List<TablaNominaBean>();
            FuncionesNomina dao = new FuncionesNomina();
            LCRecibo = dao.sp_TpCalculoEmpleado_Retrieve_TpCalculoEmpleado(iIdEmpresa, iIdEmpleado, iPeriodo);

            if (LCRecibo != null)
            {
                if (LCRecibo.Count > 0)
                {
                    for (int i = 0; i < LCRecibo.Count; i++)
                    {
                        TablaNominaBean ls = new TablaNominaBean();
                        {
                            ls.sConcepto = LCRecibo[i].sNombre_Renglon;

                            if (LCRecibo[i].iElementoNomina == 39)
                            {
                                ls.dPercepciones = LCRecibo[i].dSaldo.ToString("#.##");
                                ls.dDeducciones = "0";
                            }
                            if (LCRecibo[i].iElementoNomina == 40)
                            {
                                ls.dPercepciones = "0";
                                ls.dDeducciones = LCRecibo[i].dSaldo.ToString();
                            }

                        }
                        ls.dSaldos = "0";
                        ls.dInformativos = "0";
                        LsTabla.Add(ls);

                    }

                }
            }
            return Json(LsTabla);

        }

        [HttpPost]

        public JsonResult XMLNomina(int IdEmpresa, string sNombreComple, int Periodo, int anios, int Tipodeperido)
        {
           
              List<EmisorReceptorBean> ListDatEmisor = new List<EmisorReceptorBean>();            
              ListEmpleadosDao Dao = new ListEmpleadosDao();
              string path = Server.MapPath("Archivos\\certificados\\");
              path = path.Replace("\\Empleados", "");
              ListDatEmisor = Dao.GXMLNOM (IdEmpresa, sNombreComple, path,Periodo, anios, Tipodeperido);
     
            return Json(ListDatEmisor);
        }

    }
}