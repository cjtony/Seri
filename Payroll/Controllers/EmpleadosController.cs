using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Xml;

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

        //public PartialViewResult TimbradosXML()
        //{
        //    return PartialView();
        //}

        public ActionResult TimbradosXML()
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
            Boolean flag         = false;
            String  messageError = "none";
            EmpleadosBean empleadoBean       = new EmpleadosBean();
            ListEmpleadosDao listEmpleadoDao = new ListEmpleadosDao();
            try {
                int keyemp   = int.Parse(Session["IdEmpresa"].ToString());
                empleadoBean = listEmpleadoDao.sp_Empleados_Retrieve_Empleado(keyemploye, keyemp);
                if (empleadoBean.sMensaje != "success") {
                    messageError = empleadoBean.sMensaje;
                } else {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = empleadoBean });
        }

        [HttpPost]
        public JsonResult DataTabImssEmploye(int keyemploye)
        {
            Boolean flag         = false;
            String  messageError = "none";
            ImssBean imssBean                = new ImssBean();
            ListEmpleadosDao listEmpleadoDao = new ListEmpleadosDao();
            try {
                int keyemp = int.Parse(Session["IdEmpresa"].ToString());
                imssBean   = listEmpleadoDao.sp_Imss_Retrieve_ImssEmpleado(keyemploye, keyemp);
                if (imssBean.sMensaje != "success") {
                    messageError = imssBean.sMensaje.ToString();
                } else {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = imssBean });
        }

        [HttpPost]
        public JsonResult DataTabNominaEmploye(int keyemploye)
        {
            Boolean flag         = false;
            String  messageError = "none";
            DatosNominaBean datoNominaBean   = new DatosNominaBean();
            ListEmpleadosDao listEmpleadoDao = new ListEmpleadosDao();
            try {
                int keyemp     = int.Parse(Session["IdEmpresa"].ToString());
                datoNominaBean = listEmpleadoDao.sp_Nominas_Retrieve_NominaEmpleado(keyemploye, keyemp);
                if (datoNominaBean.sMensaje != "success") {
                    messageError = datoNominaBean.sMensaje;
                } else {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = datoNominaBean });
        }

        [HttpPost]
        public JsonResult DataTabStructureEmploye(int keyemploye)
        {
            Boolean flag         = false;
            String  messageError = "none";
            DatosPosicionesBean datoPosicionBean = new DatosPosicionesBean();
            ListEmpleadosDao listEmpleadoDao     = new ListEmpleadosDao();
            try {
                int keyemp = int.Parse(Session["IdEmpresa"].ToString());
                datoPosicionBean = listEmpleadoDao.sp_Posiciones_Retrieve_PosicionEmpleado(keyemploye, keyemp);
                if (datoPosicionBean.sMensaje != "success") {
                    messageError = datoPosicionBean.sMensaje;
                }
                if (datoPosicionBean.sMensaje == "success") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = true, MensajeError = messageError, Datos = datoPosicionBean });
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
        public JsonResult DataListEmpleado(int iIdEmpresa, int TipoPeriodo,int periodo)
        {
            List<EmpleadosEmpresaBean> ListEmple = new List<EmpleadosEmpresaBean>();
            ListEmpleadosDao Dao = new ListEmpleadosDao();
            ListEmple = Dao.sp_EmpleadosDEmpresa_Retrieve_EmpleadosDEmpresa(iIdEmpresa, TipoPeriodo, periodo);
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

                            if (LCRecibo[i].sValor == "Percepciones")
                            {
                                ls.dPercepciones = LCRecibo[i].dSaldo.ToString("#.##");
                                ls.dDeducciones = "0";
                            }
                            if (LCRecibo[i].sValor == "Deducciones")
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
            ListDatEmisor = Dao.GXMLNOM(IdEmpresa, sNombreComple, path, Periodo, anios, Tipodeperido);
            return Json(ListDatEmisor);
        }

        [HttpPost]
        public JsonResult TimbXML(int Anio, int TipoPeriodo, int Perido, int Version, string NomArchivo)
        {
            var fileName = NomArchivo;
            string PathZip = Server.MapPath("Archivos\\certificados\\");
            PathZip = PathZip.Replace("\\Empleados", "");
            PathZip = PathZip + NomArchivo;
            string path = Server.MapPath("Archivos\\certificados\\XmlZip\\");
            path = path.Replace("\\Empleados", "");

            //ZipFile.ExtractToDirectory(PathZip, path);

            DirectoryInfo di = new DirectoryInfo(path);
            foreach (var fi in di.GetFiles())
            {
                String pathxml = path + fi.Name;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(pathxml);
                XmlNode nodo = xmlDoc.GetElementsByTagName("cfdi:Comprobante").Item(0);
                string ValorAtributo = nodo.Attributes.GetNamedItem("Folio").Value;
                XmlNode nodo2 = xmlDoc.GetElementsByTagName("tfd:TimbreFiscalDigital").Item(0);
                string ValorAtributo2 = nodo2.Attributes.GetNamedItem("UUID").Value;
                XmlNode nodo3 = xmlDoc.GetElementsByTagName("tfd:TimbreFiscalDigital").Item(0);
                string ValorAtributo3 = nodo3.Attributes.GetNamedItem("SelloSAT").Value;
            };

            List<EmisorReceptorBean> ListDatEmisor = new List<EmisorReceptorBean>();

            return Json(ListDatEmisor);
        }


        [HttpPost]
        public ActionResult LoadFile(HttpPostedFileBase fileUpload)
        {
            try
            {
                string path = Server.MapPath("~/Archivos/certificados/");
                if (Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileUpload.SaveAs(path + Path.GetFileName(fileUpload.FileName));

            }
            catch (Exception e)
            {
                return Json(new { Value = false, Message = e.Message }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { Value = true, Message = "Archivo cargado se procedera el timbrado" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TotalesRecibo(int iIdEmpresa, int iIdEmpleado, int iPeriodo) 
        {
            List<ReciboNominaBean> ListTotales = new List<ReciboNominaBean>();
            ListEmpleadosDao Dao = new ListEmpleadosDao();
            ListTotales = Dao.sp_SaldosTotales_Retrieve_TPlantillasCalculos(iIdEmpresa, iIdEmpleado, iPeriodo);
            return Json(ListTotales);
        }
    }
}