using iTextSharp.text;
using iTextSharp.text.pdf;
using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Payroll.Controllers
{
    public class NominaController : Controller
    {
        // GET: Nomina
        public PartialViewResult AltaDefinicion()
        {
            return PartialView();
        }
        public PartialViewResult BajasEmpleados()
        {
            return PartialView();
        }
        public PartialViewResult Consulta()
        {
            return PartialView();
        }
        public PartialViewResult Ejecucion()
        {

            return PartialView();
        }
        public PartialViewResult MonitorProcesos()
        {
            return PartialView();

        }

        public PartialViewResult Dispersion()
        {
            return PartialView();
        }

        public PartialViewResult ComparativoNomina()
        {

            return PartialView();
        }


        public PartialViewResult CompensacionesFijas()
        {
            return PartialView();
        }

      

        //Guarda los datos de la Definicion
        [HttpPost]
        public JsonResult DefiNomina(string sNombreDefinicion, string sDescripcion, int iAno, int iCancelado)
        {
            NominahdBean bean = new NominahdBean();
            FuncionesNomina dao = new FuncionesNomina();
            int usuario = int.Parse(Session["iIdUsuario"].ToString());
            bean = dao.sp_DefineNom_insert_DefineNom(sNombreDefinicion, sDescripcion, iAno, iCancelado, usuario);
            return Json(bean);
        }
        // llena  listado de empresas
        [HttpPost]
        public JsonResult LisEmpresas()
 
        {
            int idPerfil = int.Parse(Session["Profile"].ToString());
            int idempresa = int.Parse(Session["IdEmpresa"].ToString()); 
            List<EmpresasBean> LE = new List<EmpresasBean>();
            FuncionesNomina Dao = new FuncionesNomina(); 
            int Perfil_id = int.Parse(Session["Profile"].ToString());
            LE = Dao.sp_CEmpresas_Retrieve_Empresas(Perfil_id); 
            if (LE.Count > 0)
            {
                if (idPerfil == 32)
                {
                    LE[0].iPerfilPdf = 1;
                }
                else {
                    LE[0].iPerfilPdf = 0;
                }
                LE[0].iIdEmpresaSess = idempresa;
                for (int i = 0; i < LE.Count; i++)
                {
                    LE[i].sNombreEmpresa = LE[i].iIdEmpresa + " " + LE[i].sNombreEmpresa;
                }
            }

            return Json(LE);
        }

        // regresa el listado del periodo
        [HttpPost]
        public JsonResult LisTipPeriodo(int IdEmpresa)
        {
            List<CTipoPeriodoBean> LTP = new List<CTipoPeriodoBean>();
            FuncionesNomina Dao = new FuncionesNomina();
            LTP = Dao.sp_CTipoPeriod_Retrieve_TiposPeriodos(IdEmpresa);
            return Json(LTP);
        }
        // regresa el listado de renglon
        [HttpPost]
        public JsonResult LisRenglon(int IdEmpresa, int iElemntoNOm)
        {
            List<CRenglonesBean> LR = new List<CRenglonesBean>();
            FuncionesNomina Dao = new FuncionesNomina();
            LR = Dao.sp_CRenglones_Retrieve_CRenglones(IdEmpresa, iElemntoNOm);
            return Json(LR);
        }
        // regresa el listado de acumulado -
        [HttpPost]
        public JsonResult LisAcumulado(int iIdEmpresa, int iIdRenglon)
        {
            List<CAcumuladosRenglon> LAc = new List<CAcumuladosRenglon>();
            FuncionesNomina Dao = new FuncionesNomina();
            LAc = Dao.sp_CAcumuladoREnglones_Retrieve_CAcumuladoREnglones(iIdEmpresa, iIdRenglon);
            return Json(LAc);
        }
        // devuelve el ultimo ID
        [HttpPost]
        public JsonResult IdmaxDefiniconNom()
        {
            List<NominahdBean> Idmax = new List<NominahdBean>();
            FuncionesNomina Dao = new FuncionesNomina();
            Idmax = Dao.sp_IdDefinicionNomina_Retrieve_IdDefinicionNomina();
            return Json(Idmax);
        }
        //Devuelve el valor de la columna cancelado del ultimo regristro 
        [HttpPost]
        public JsonResult DefCancelado(int iIdFinicion)
        {
            List<NominahdBean> DefCancelado = new List<NominahdBean>();
            FuncionesNomina Dao = new FuncionesNomina();
            DefCancelado = Dao.sp_DefCancelados_Retrieve_DefCancelados(iIdFinicion);
            return Json(DefCancelado);
        }
        //Guarda los datos de la DefinicionLn
        [HttpPost]
        public JsonResult insertDefinicioNl(int iIdDefinicionHd, int iIdEmpresa, int iTipodeperiodo, int iRenglon, int iCancelado, int iElementonomina, int iEsespejo, int iIdAcumulado)
        {
            NominaLnBean bean = new NominaLnBean();
            FuncionesNomina dao = new FuncionesNomina();
            int usuario = int.Parse(Session["iIdUsuario"].ToString());
            bean = dao.sp_CDefinicionLN_insert_CDefinicionLN(iIdDefinicionHd, iIdEmpresa, iTipodeperiodo, iRenglon, iCancelado, usuario, iElementonomina, iEsespejo, iIdAcumulado);

            return Json(bean);
        }
        // Regresa el listado de periodo
        [HttpPost]
        public JsonResult ListPeriodo(int iIdEmpresesas, int ianio, int iTipoPeriodo)
        {
            List<CInicioFechasPeriodoBean> LPe = new List<CInicioFechasPeriodoBean>();
            FuncionesNomina dao = new FuncionesNomina();
            LPe = dao.sp_Cperiodo_Retrieve_Cperiodo(iIdEmpresesas, ianio, iTipoPeriodo);
            return Json(LPe);

        }
        // llena de datos en la tabla de percepciones
        [HttpPost]
        public JsonResult listdatosPercesiones(int iIdDefinicionln)
        {
            List<NominaLnDatBean> Dt = new List<NominaLnDatBean>();
            List<NominaLnDatBean> DA = new List<NominaLnDatBean>();
            FuncionesNomina dao = new FuncionesNomina();
            Dt = dao.sp_DefinicionesNomLn_Retrieve_DefinicionesNomLn(iIdDefinicionln);
            if (Dt[0].sMensaje == "success")
            {
                for (int i = 0; i < Dt.Count; i++)
                {

                    if (Dt[i].iEsespejo == "True")
                    {
                        Dt[i].iEsespejo = "Si";
                    }

                    else if (Dt[i].iEsespejo == "False")
                    {
                        Dt[i].iEsespejo = "No";
                    }

                    if (Dt[i].iIdAcumulado == "0")
                    {

                        Dt[i].iIdAcumulado = "";
                    }

                    if (Dt[i].iIdAcumulado != "0" && Dt[i].iIdAcumulado != "" && Dt[i].iIdAcumulado != " ")
                    {

                        int num = int.Parse(Dt[i].iIdAcumulado);
                        DA = dao.sp_DescripAcu_Retrieve_DescripAcu(num);
                        Dt[i].iIdAcumulado = DA[0].iIdAcumulado;

                    }

                }

            }



            return Json(Dt);
        }
        [HttpPost]
        // llena de datos en la tabla de deducciones
        public JsonResult listdatosDeducuiones(int iIdDefinicionln)
        {
            List<NominaLnDatBean> Dta = new List<NominaLnDatBean>();
            List<NominaLnDatBean> DA = new List<NominaLnDatBean>();
            FuncionesNomina dao = new FuncionesNomina();
            Dta = dao.sp_DefinicionesDeNomLn_Retrieve_DefinicionesDeNomLn(iIdDefinicionln);
            if (Dta[0].sMensaje == "success")
            {
                for (int i = 0; i < Dta.Count; i++)
                {

                    if (Dta[i].iEsespejo == "True")
                    {
                        Dta[i].iEsespejo = "Si";
                    }

                    else if (Dta[i].iEsespejo == "False")
                    {
                        Dta[i].iEsespejo = "No";
                    }

                    if (Dta[i].iIdAcumulado == "0")
                    {

                        Dta[i].iIdAcumulado = "";
                    }



                    if (Dta[i].iIdAcumulado != "0" && Dta[i].iIdAcumulado != "" && Dta[i].iIdAcumulado != " ")
                    {

                        int num = int.Parse(Dta[i].iIdAcumulado);
                        DA = dao.sp_DescripAcu_Retrieve_DescripAcu(num);
                        Dta[i].iIdAcumulado = DA[0].iIdAcumulado;

                    }

                }
            }


            return Json(Dta);
        }

        [HttpPost]
        public JsonResult ListadoNomDefinicion()
        {

            List<NominahdBean> LNND = new List<NominahdBean>();
            FuncionesNomina Dao = new FuncionesNomina();
            LNND = Dao.sp_DefinicionNombresHd_Retrieve_DefinicionNombresHd();
            return Json(LNND);
        }
        [HttpPost]
        public JsonResult TpDefinicionNomina()
        {

            List<NominahdBean> LNH = new List<NominahdBean>();
            FuncionesNomina dao = new FuncionesNomina();
            int usuarioid = int.Parse(Session["iIdUsuario"].ToString());
            LNH = dao.sp_TpDefinicionesNom_Retrieve_TpDefinicionNom(usuarioid);

            for (int i = 0; i < LNH.Count; i++)
            {

                if (LNH[i].iCancelado == "True")
                {
                    LNH[i].iCancelado = "Si";
                }

                else if (LNH[i].iCancelado == "False")
                {
                    LNH[i].iCancelado = "No";
                }

            }

            return Json(LNH);
        }
        [HttpPost]
        public JsonResult TpDefinicionNominaQry(string sNombreDefinicion, int iCancelado)
        {
            if (sNombreDefinicion == "Selecciona")
            {
                sNombreDefinicion = "";
            }
            int usuarioid = int.Parse(Session["iIdUsuario"].ToString());
            List<NominahdBean> TD = new List<NominahdBean>();
            FuncionesNomina dao = new FuncionesNomina();
            TD = dao.sp_DeficionNominaCancelados_Retrieve_DeficionNominaCancelados(sNombreDefinicion, iCancelado, usuarioid);

            if (TD != null)
            {

                for (int i = 0; i < TD.Count; i++)
                {

                    if (TD[i].iCancelado == "True")
                    {
                        TD[i].iCancelado = "Si";
                    }

                    else if (TD[i].iCancelado == "False")
                    {
                        TD[i].iCancelado = "No";
                    }
                }
            }

            return Json(TD);
        }
        [HttpPost]
        public JsonResult UpdatePtDefinicion(string sNombreDefinicion, string sDescripcion, int iAno, int iCancelado, int iIdDefinicionhd, int OptAnio)
        {
            int UsuarioId =int.Parse(Session["iIdUsuario"].ToString());
            NominahdBean bean = new NominahdBean();
            TpCalculosHd bean2 = new TpCalculosHd();
            FuncionesNomina dao  = new FuncionesNomina();
            bean = dao.sp_TpDefinicion_Update_TpDefinicion(sNombreDefinicion, sDescripcion, iAno, iCancelado, iIdDefinicionhd);
            if (OptAnio > 0) {
                bean2 = dao.sp_updateAnio_Insert_TPlantilla_Calculos_Hd(iIdDefinicionhd, UsuarioId);
            }


            return Json(bean);
        }
        [HttpPost]
        public JsonResult DeleteDefinicion(int iIdDefinicionhd)
        {
            NominahdBean bean = new NominahdBean();
            FuncionesNomina dao = new FuncionesNomina();
            bean = dao.sp_EliminarDefinicion_Delete_EliminarDefinicion(iIdDefinicionhd);
            return Json(bean);

        }
        [HttpPost]
        public JsonResult UpdatePtDefinicionNl(int iIdDefinicionln, int iIdEmpresa, int iTipodeperiodo, int iRenglon, int iEsespejo, int iIdAcumulado)
        {
            NominaLnBean bean = new NominaLnBean();
            FuncionesNomina dao = new FuncionesNomina();
            bean = dao.sp_TpDefinicionNomLn_Update_TpDefinicionNomLn(iIdDefinicionln, iIdEmpresa, iTipodeperiodo, iRenglon, iEsespejo, iIdAcumulado);
            return Json(bean);
        }
        [HttpPost]
        public JsonResult DeleteDefinicionNl(int iIdDefinicionln)
        {
            NominaLnBean Bean = new NominaLnBean();
            FuncionesNomina dao = new FuncionesNomina();
            Bean = dao.sp_EliminarDefinicionNl_Delete_EliminarDefinicionNl(iIdDefinicionln);
            return Json(Bean);
        }
        [HttpPost]
        public JsonResult CompruRegistroExit(int iIdDefinicionHd,int  iperiodo,int NomCerr,int Anio)
        {
            List<TpCalculosHd> LNND = new List<TpCalculosHd>();
            List<CTipoPeriodoBean> LTP = new List<CTipoPeriodoBean>();
            List<CInicioFechasPeriodoBean> LPe = new List<CInicioFechasPeriodoBean>();          
            FuncionesNomina Dao = new FuncionesNomina();
            LNND = Dao.sp_ExiteDefinicionTpCalculo_Retrieve_ExiteDefinicionTpCalculo(iIdDefinicionHd);
            LTP = Dao.sp_TipoPeridoTpDefinicionNomina_Retrieve_TpDefinicionNomina(iIdDefinicionHd);
            LPe = Dao.sp_PeridosEmpresa_Retrieve_CinicioFechasPeriodo(iIdDefinicionHd, iperiodo, NomCerr, Anio);
            var result = new { Result = LNND, LTP, LPe};
            return Json(result);
            //return Json(LNND);

        }
        //Guarda los datos de TpCalculos
        [HttpPost]
        public JsonResult InsertDatTpCalculos(int iIdDefinicionHd, int iNominaCerrada)
        {
            string sFolio = "";
            int iFolio = 0;
            int Idusuarios = int.Parse(Session["iIdUsuario"].ToString());
            TpCalculosHd bean = new TpCalculosHd();
            List<CInicioFechasPeriodoBean> DaFolio = new List<CInicioFechasPeriodoBean>();
            FuncionesNomina dao = new FuncionesNomina();
            DaFolio = dao.sp_DatFolioDefNomina_Retreieve(iIdDefinicionHd);
            if (DaFolio != null) {
                //iFolio = int.Parse(DaFolio[0].ianio.ToString()) * 100000 + int.Parse(DaFolio[0].iTipoPeriodo.ToString()) * 10000 +int.Parse( DaFolio[0].iPeriodo.ToString()) * 10;
                if (DaFolio[0].iPeriodo > 9)
                {
                    if (DaFolio[0].iTipoPeriodo > 0)
                    {
                        sFolio = DaFolio[0].ianio.ToString() + (DaFolio[0].iTipoPeriodo * 10) + DaFolio[0].iPeriodo + "0";
                    }
                    if (DaFolio[0].iTipoPeriodo < 1)
                    {
                        sFolio = DaFolio[0].ianio.ToString() + "00" + DaFolio[0].iPeriodo + "0";
                    }

                }
                if (DaFolio[0].iPeriodo > 0 && DaFolio[0].iPeriodo < 10)
                {
                    if (DaFolio[0].iTipoPeriodo > 0)
                    {
                        sFolio = DaFolio[0].ianio.ToString() + (DaFolio[0].iTipoPeriodo * 10) + "0" + DaFolio[0].iPeriodo + "0";
                    }
                    if (DaFolio[0].iTipoPeriodo < 1)
                    {
                        sFolio = DaFolio[0].ianio.ToString() + "00" + "0" + DaFolio[0].iPeriodo + "0";

                    }
                }

                iFolio = int.Parse(sFolio);
                bean = dao.sp_TpCalculos_Insert_TpCalculos(iIdDefinicionHd, iFolio, iNominaCerrada, Idusuarios);
            }

            return Json(bean);
        }
        // Actualiza PTCalculoshd
        [HttpPost]
        public JsonResult UpdateCalculoshd(int iIdDefinicionHd, int iNominaCerrada)
        {
            TpCalculosHd bean = new TpCalculosHd();
            FuncionesNomina dao = new FuncionesNomina();
            bean = dao.sp_TpCalculos_update_TpCalculos(iIdDefinicionHd, iNominaCerrada);
            return Json(bean);
        }
        [HttpPost]
        public JsonResult TpDefinicionnl()
        {
            List<NominaLnDatBean> Dta = new List<NominaLnDatBean>();
            List<NominaLnDatBean> DA = new List<NominaLnDatBean>();
            FuncionesNomina dao = new FuncionesNomina();
            Dta = dao.sp_TpDefinicionNomins_Retrieve_TpDefinicionNomins();
            if (Dta != null)
            {
                for (int i = 0; i < Dta.Count; i++)
                {
                    if (Dta[i].iElementonomina == "39")
                    {
                        Dta[i].iElementonomina = "Percepciones";
                    }

                    if (Dta[i].iElementonomina == "40")
                    {
                        Dta[i].iElementonomina = "Deducciones";
                    }


                    if (Dta[i].iEsespejo == "True")
                    {
                        Dta[i].iEsespejo = "Si";
                    }

                    else if (Dta[i].iEsespejo == "False")
                    {
                        Dta[i].iEsespejo = "No";
                    }

                    if (Dta[i].iIdAcumulado == "0")
                    {

                        Dta[i].iIdAcumulado = "";
                    }



                    if (Dta[i].iIdAcumulado != "0" && Dta[i].iIdAcumulado != "" && Dta[i].iIdAcumulado != " ")
                    {

                        int num = int.Parse(Dta[i].iIdAcumulado);
                        DA = dao.sp_DescripAcu_Retrieve_DescripAcu(num);
                        Dta[i].iIdAcumulado = DA[0].iIdAcumulado;

                    }

                }
            }


            return Json(Dta);
        }
        //Carga motivos de baja para select
        [HttpPost]
        public JsonResult LoadMotivoBaja()
        {
            List<MotivoBajaBean> bean;
            FuncionesNomina dao = new FuncionesNomina();
            bean = dao.sp_Cgeneral_Retrieve_MotivoBajas();
            return Json(bean);
        }
        //Carga tipos empleado para select
        [HttpPost]
        public JsonResult LoadTipoBaja()
        {
            List<TipoEmpleadoBean> bean;
            FuncionesNomina dao = new FuncionesNomina();
            bean = dao.sp_Cgeneral_Retrieve_TipoEmpleadosBajas();
            return Json(bean);
        }
        //Carga tipos empleado para select x tipo de empleado
        [HttpPost]
        public JsonResult LoadMotivoBajaxTe()
        {
            List<MotivoBajaBean> bean;
            FuncionesNomina dao = new FuncionesNomina();
            bean = dao.sp_Cgeneral_Retrieve_MotivoBajasxTe();
            return Json(bean);
        }
        [HttpPost]
        public JsonResult LoadDatosBaja()
        {
            List<string> bean;
            FuncionesNomina dao = new FuncionesNomina();
            var Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            var Empleado_id = int.Parse(Session["Empleado_id"].ToString());
            bean = dao.sp_TEmpleado_Nomina_Retrieve_DatosBaja(Empresa_id, Empleado_id);
            return Json(bean);
        }

        [HttpPost]
        public JsonResult ListTpCalculoln(int iIdCalculosHd, int iTipoPeriodo, int iPeriodo, int idEmpresa, int Anio)
        {
            List<TpCalculosCarBean> Dta = new List<TpCalculosCarBean>();
            List<TPProcesos> LProce= new List<TPProcesos>();
            List<EmpresasBean> LisEmpreCal = new List<EmpresasBean>();
            //List<EmpleadosEmpresaBean> ListEmple = new List<EmpleadosEmpresaBean>();
            FuncionesNomina dao = new FuncionesNomina();
            dao.sp_EstatusTpProcesosJobs_Update_EstatusTpProcesosJobs();
            Dta = dao.sp_Caratula_Retrieve_TPlantilla_Calculos(iIdCalculosHd, iTipoPeriodo, iPeriodo, idEmpresa, Anio);           
         
            LProce = Statusproc(iIdCalculosHd, iTipoPeriodo, iPeriodo,  idEmpresa,  Anio);
            LisEmpreCal = dao.sp_Empresa_Retrieve_TpCalculosLN(iIdCalculosHd, iTipoPeriodo, 0);

            if (Dta[0].sMensaje == "No hay datos") {

                for (int i = 0; i < LisEmpreCal.Count ; i++) {           
                    Dta = dao.sp_Caratula_Retrieve_TPlantilla_Calculos(0, iTipoPeriodo, iPeriodo, LisEmpreCal[i].iIdEmpresa, Anio);
                    if (Dta[0].sMensaje == "success") {
                        i = Dta.Count + 5;

                    }
                };           
            };
            if (Dta.Count > 1)
            {
                for (int i = 0; i < Dta.Count; i++)
                {
                    Dta[i].sTotal = "$ " + string.Format(CultureInfo.InvariantCulture, "{0:#,###,##0.00}", Dta[i].dTotal);
                }

            }

            var result = new { Result = Dta, LProce, LisEmpreCal };
            return Json(result);
            
        }

        [HttpPost]
        public JsonResult EmpresaCal(int iIdCalculosHd, int iTipoPeriodo, int iPeriodo)
        {
            List<EmpresasBean> Dta = new List<EmpresasBean>();
            //List<NominaLnDatBean> DA = new List<NominaLnDatBean>();
            FuncionesNomina dao = new FuncionesNomina();
            Dta = dao.sp_Empresa_Retrieve_TpCalculosLN(iIdCalculosHd, iTipoPeriodo, iPeriodo);

            return Json(Dta);

        }
        public JsonResult ListTBProcesosJobs()
        {
            int op1 = 0, op2 = 0, op3 = 0, CrtliIdJobs = 0, CtrliIdTarea = 0;
            List<TPProcesos> LTbProc = new List<TPProcesos>();
            // entra en monitor 
            FuncionesNomina dao = new FuncionesNomina();
            LTbProc = dao.sp_TPProcesosJobs_Retrieve_TPProcesosJobs(op1, op2, op3, CrtliIdJobs, CtrliIdTarea);
            return Json(LTbProc);
        }
        public JsonResult ListStatusProcesosJobs()
        {
            List<TPProcesos> LTbProc = new List<TPProcesos>();
            FuncionesNomina dao = new FuncionesNomina();
            dao.sp_EstatusTpProcesosJobs_Update_EstatusTpProcesosJobs();
            LTbProc = dao.sp_EstatusJobsTbProcesos_retrieve_EstatusJobsTbProcesos();
            Startup obj = new Startup();
            obj.ActBDTbJobs();
            return Json(LTbProc);
        }
        public JsonResult ProcesosPots(int IdDefinicionHD, int anio, int iTipoPeriodo, int iperiodo, int iIdempresa, int iCalEmpleado)
        {
            string Nameuse = Session["Susuario"].ToString();
            int UsuarioId = int.Parse(Session["iIdUsuario"].ToString());
            string Path = Server.MapPath("Archivos\\porlotes\\");
            List<TPProcesos> Exist = new List<TPProcesos>();
            string sFolio="";
            int iFolio = 0;
            Path = Path.Replace("\\Nomina", "");
            Path = Path + "prueba.bat";

            Startup obj = new Startup();
            string NomProceso = "CNomina";
            FuncionesNomina Dao2 = new FuncionesNomina();

            if (iperiodo > 9)
            {
                if (iTipoPeriodo > 0)
                {
                    sFolio = anio.ToString() + (iTipoPeriodo * 10) + iperiodo.ToString() + "0";
                }
                if (iTipoPeriodo < 1)
                {
                    sFolio = anio.ToString() + "00" + iperiodo.ToString() + "0";
                }

            }
            if (iperiodo > 0 && iperiodo < 10)
            {
                if (iTipoPeriodo > 0)
                {
                    sFolio = anio.ToString() + (iTipoPeriodo * 10) + "0" + iperiodo.ToString() + "0";
                }
                if (iTipoPeriodo < 1)
                {
                    sFolio = anio.ToString() + "00" + "0" + iperiodo.ToString() + "0";

                }
            }
            iFolio = int.Parse(sFolio);

            Exist = Dao2.sp_ExistUsuProcesJobs_Retrieve_Tp_Usuario_ProcesJobs(UsuarioId);
            Dao2.sp_Usuario_Update_TplantillaCalculosHd(IdDefinicionHD, iFolio, UsuarioId);
            if (Exist[0].iExistUsuario == 0) {
                UsuarioId = 0;
                Nameuse = "IPSNet";
            }
            obj.ProcesoNom(NomProceso, IdDefinicionHD, anio, iTipoPeriodo, iperiodo, iIdempresa, iCalEmpleado, Path, Nameuse);
            return null;
        }
        [HttpPost]
        public JsonResult TipoPeriodo(int IdDefinicionHD)
        {

            List<CTipoPeriodoBean> LTP = new List<CTipoPeriodoBean>();
            FuncionesNomina Dao = new FuncionesNomina();
            LTP = Dao.sp_TipoPeridoTpDefinicionNomina_Retrieve_TpDefinicionNomina(IdDefinicionHD);
            return Json(LTP);
        }
        [HttpPost]
        public JsonResult ListPeriodoEmpresa(int IdDefinicionHD, int iperiodo, int NomCerr, int Anio)
        {
            List<CInicioFechasPeriodoBean> LPe = new List<CInicioFechasPeriodoBean>();
            FuncionesNomina dao = new FuncionesNomina();
            LPe = dao.sp_PeridosEmpresa_Retrieve_CinicioFechasPeriodo(IdDefinicionHD, iperiodo, NomCerr, Anio);
            return Json(LPe);

        }
        public JsonResult UpdateCInicioFechasPeriodo(int iIdDefinicionHd, int iPerido, int iNominaCerrada, int Anio, int IdTipoPeriodo, int IdEmpresa)
        {
            CInicioFechasPeriodoBean bean = new CInicioFechasPeriodoBean();
            FuncionesNomina dao = new FuncionesNomina();
            bean = dao.sp_NomCerradaCInicioFechaPeriodo_Update_CInicioFechasPeriodo(iIdDefinicionHd, iPerido, iNominaCerrada, Anio, IdTipoPeriodo, IdEmpresa);
            return Json(bean);
        }
        [HttpPost]
        public JsonResult ExiteRenglon(int iIdDefinicionHd, int iIdEmpresa, int iRenglon, int iElementonomina)
        {

            List<NominaLnBean> Exte = new List<NominaLnBean>();
            FuncionesNomina dao = new FuncionesNomina();
            Exte = dao.sp_ExitReglon_Retrieve_TpDefinicionNominaLn(iIdEmpresa, iRenglon, iIdDefinicionHd, iElementonomina);
            return Json(Exte);
        }
        [HttpPost]
        public JsonResult UpdateRenglonDefNl(int iIdDefinicion)
        {
            NominaLnBean ListDef = new NominaLnBean();
            FuncionesNomina dao = new FuncionesNomina();
            ListDef = dao.sp_RenglonesDefinicionNL_Update_TplantillaDefinicionNL(iIdDefinicion);
            return Json(ListDef);
        }
        [HttpPost]
        public ActionResult PDFCaratula()
        {
            string Fecha = DateTime.Now.ToString("dd/MM/yyyy ");
            string path = Server.MapPath("Archivos\\certificados\\PDF\\Caratula.pdf");
            path = path.Replace("\\Nomina", "");
            FileStream pdf = new FileStream(path, FileMode.Create);
            Document documento = new Document(iTextSharp.text.PageSize.LETTER, 0, 0, 0, 0);
            PdfWriter PW = PdfWriter.GetInstance(documento, pdf);
            documento.Open();

            documento.Add(new Paragraph("Fecha: " + Fecha));
            PdfPTable table1 = new PdfPTable(4);



            documento.Close();
            return null;
        }
        [HttpPost]
        public JsonResult ExitPerODedu(int iIdDefinicionHd) {
            List<int> op = new List<int>();
            FuncionesNomina dao = new FuncionesNomina();
            op = dao.sp_ExitPercepODeduc_Retrieve_TPlantilla_Definicion_Nomina_Ln(iIdDefinicionHd);

            return Json(op);
        }
        [HttpPost]
        public JsonResult QryDifinicionPeriodoCerrado()
        {

            List<NominahdBean> TD = new List<NominahdBean>();
            FuncionesNomina dao = new FuncionesNomina();
            TD = dao.sp_DefinicionConNomCe_Retrieve_TpDefinicionNominaHd();

            if (TD != null)
            {

                for (int i = 0; i < TD.Count; i++)
                {

                    if (TD[i].iCancelado == "True")
                    {
                        TD[i].iCancelado = "Si";
                    }

                    else if (TD[i].iCancelado == "False")
                    {
                        TD[i].iCancelado = "No";
                    }
                }
            }

            return Json(TD);
        }

        public List<TPProcesos> Statusproc(int iIdCalculosHd, int iTipoPeriodo, int iPeriodo, int idEmpresa, int anio)
        {
            FuncionesNomina dao = new FuncionesNomina();
            List<TPProcesos> Dta = new List<TPProcesos>();
            List<TPProcesos> Dta2 = new List<TPProcesos>();
            string Folio = "";
            if (iPeriodo > 9)
            {
                if (iTipoPeriodo < 1)
                {
                    Folio = anio.ToString() + "00" + iPeriodo + "0";
                }
                else {
                    Folio = anio.ToString() + (iTipoPeriodo * 10) + iPeriodo + "0";
                }
               
            }
            if (iPeriodo < 10) {
                if (iTipoPeriodo < 1) {
                    Folio = anio.ToString() + "00" + "0" + iPeriodo + "0";
                }
                else{
                    Folio = anio.ToString() + (iTipoPeriodo * 10) + "0" + iPeriodo + "0";

                }
            }
            string Parametro = anio + "," + iTipoPeriodo + "," + iPeriodo + "," + iIdCalculosHd + "%";
            Dta2 = dao.sp_CalculosHdFinProces_Retrieve_TPlantillaCalculosHd(Convert.ToInt32(Folio), iIdCalculosHd);
            Dta = dao.sp_StatusProceso_Retrieve_TPProceso(Parametro);
            if (Dta2[0].sEstatusJobs != "No hay datos")
            {
                if (Dta[0].sMensaje != "No hay datos")
                {
                    if (Dta[0].sMensaje == Dta2[0].sMensaje)
                    {
                        if (Dta[0].sEstatusJobs == "Terminado" && Dta2[0].sEstatusJobs == "Procesando")
                        {
                            Dta[0].sEstatusJobs = "Procesando";
                        }
                    }
                }
            }
            else if (Dta2[0].sEstatusJobs == "No hay datos") {
                Dta[0].sMensaje = "No hay datos";

            }
            return Dta;
        }

        // Lista Empleado
        [HttpPost]
        public JsonResult ListEmplados(int iIdEmpresa)
        {
            List<EmpleadosEmpresaBean> LTEmp = new List<EmpleadosEmpresaBean>();
            FuncionesNomina Dao = new FuncionesNomina();
            LTEmp = Dao.sp_EmpleadosDeEmpresa_Retreive_Templeados(iIdEmpresa);
            return Json(LTEmp);
        }

        // Lista Empleado conid
        [HttpPost]
        public JsonResult ListConIDEmplados(int iIdEmpresa)
        {
            List<EmpleadosEmpresaBean> LTEmp = new List<EmpleadosEmpresaBean>();
            FuncionesNomina Dao = new FuncionesNomina();
            LTEmp = Dao.sp_EmpleadosDeEmpresa_Retreive_Templeados(iIdEmpresa);
            if (LTEmp != null) {
                for (int i = 0; i < LTEmp.Count; i++) {
                    LTEmp[i].sNombreCompleto = LTEmp[i].iIdEmpleado + " " + LTEmp[i].sNombreCompleto.ToString();
                }
            }
            return Json(LTEmp);
        }

        //Guarda Lista de Empleado en la tabla Lista_empleados_Nomina
        [HttpPost]
        public JsonResult SaveEmpleados(int IdEmpresa, string sIdEmpleados, int iAnio, int TipoPeriodo, int iPeriodo)
        {
            int IdEmpleado = 0;
            int iExite = 0;
            string[] IdEmpleados = sIdEmpleados.Split(',');
            int numsId = IdEmpleados.Count();
            ListEmpleadoNomBean bean = new ListEmpleadoNomBean();
            FuncionesNomina dao = new FuncionesNomina();
            for (int i = 0; i < numsId - 1; i++) {
                IdEmpleado = Convert.ToInt32(IdEmpleados[i].ToString());
                bean = dao.sp_LisEmpleados_InsertUpdate_TlistaEmpladosNomina(IdEmpresa, IdEmpleado, iAnio,
                      TipoPeriodo, iPeriodo, iExite);
                if (bean.sMensaje == "error") { i = numsId + 2; }
            }

            return Json(bean);
        }

        // No de Empleado de Empresas
        [HttpPost]
        public JsonResult NoEmpleados(String IdEmpresas)
        {
            List<EmpresasBean> LE = new List<EmpresasBean>();
            FuncionesNomina Dao = new FuncionesNomina();
            if (IdEmpresas != "")
            {

                int idEmpresa = 0, Noempresa = 0, rows = 0;
                string[] valores = IdEmpresas.Split(' ');
                rows = valores.Length - 1;
                for (int i = 0; i < rows; i++)
                {
                    idEmpresa = Convert.ToInt32(valores[i]);
                    LE = Dao.sp_NoEmpleadosEmpresa_Retrieve_TempleadoNomina(idEmpresa, 0);
                    if (LE.Count > 0)
                    {
                        Noempresa = Noempresa + LE[0].iNoEmpleados;
                    }

                }


                LE[0].iNoEmpleados = Noempresa;

            }
            else {
                EmpresasBean ls = new EmpresasBean
                {
                    iNoEmpleados = 0,
                };
                LE.Add(ls);
            }


            return Json(LE);
        }

        // Tipo de Perido y Perido de empresas Emision facturas
        [HttpPost]
        public JsonResult TipoPPeriodoEmision(String IdEmpresas, int OP)
        {

            string Validacion = " ";
            List<CInicioFechasPeriodoBean> LE = new List<CInicioFechasPeriodoBean>();
            List<CInicioFechasPeriodoBean> LE2 = new List<CInicioFechasPeriodoBean>();
            List<CInicioFechasPeriodoBean> LPe = new List<CInicioFechasPeriodoBean>();
            List<CTipoPeriodoBean> LE3 = new List<CTipoPeriodoBean>();
            LE = null;
            LE3 = null;
            FuncionesNomina Dao = new FuncionesNomina();
            if (IdEmpresas != "")
            {
                if (OP == 0) {
                    int idEmpresa = 0, rows = 0;
                    string[] valores = IdEmpresas.Split(' ');
                    rows = valores.Length - 1;
                    for (int i = 0; i < rows; i++)
                    {
                        idEmpresa = Convert.ToInt32(valores[i]);
                        LE = Dao.sp_TipoPPEmision_Retrieve_CInicioPeriodo(idEmpresa, OP);
                        if (LE.Count > 0)
                        {
                            Validacion = "Correcta";
                            for (int x = 1; x < valores.Length - 1; x++)
                            {
                                idEmpresa = Convert.ToInt32(valores[x]);
                                LE2 = Dao.sp_TipoPPEmision_Retrieve_CInicioPeriodo(idEmpresa, OP);
                                for (int a = 0; a < LE.Count - 1; a++)
                                {
                                    int correc = 0;

                                    for (int b = 0; b < 0; b++)
                                    {
                                        if (LE[i].iTipoPeriodo == LE2[x].iTipoPeriodo)
                                        {
                                            correc = correc + 1;
                                        }
                                    }

                                    if (correc > 0)
                                    {
                                        Validacion = "Correcta";

                                    }
                                    else
                                    {

                                        Validacion = "Incorrecta";
                                    }
                                }
                            }
                        }
                        if (Validacion == "Correcta")
                        {
                            idEmpresa = Convert.ToInt32(valores[0]);
                            LE3 = Dao.sp_CTipoPeriod_Retrieve_TiposPeriodos(idEmpresa);
                        };
                    }
                }
                if (OP == 1) {

                    int idEmpresa = 0, periodo = 0;
                    string[] valores = IdEmpresas.Split(' ');
                    int i = 0;
                    idEmpresa = Convert.ToInt32(valores[i]);
                    LE = Dao.sp_TipoPPEmision_Retrieve_CInicioPeriodo(idEmpresa, OP);
                    if (LE.Count > 0) {
                        for (int a = 0; a < LE.Count; a++) {
                            if (valores.Length - 1 == 1)
                            {
                                LPe = LE;
                            }
                            else {
                                periodo = 0;
                                for (int x = 1; x < valores.Length - 1; x++)
                                {

                                    idEmpresa = Convert.ToInt32(valores[x]);
                                    LE2 = Dao.sp_TipoPPEmision_Retrieve_CInicioPeriodo(idEmpresa, OP);
                                    if (LE2.Count > 0)
                                    {
                                        if (a < LE2.Count) {
                                            if (LE[a].iPeriodo == LE2[a].iPeriodo)
                                            {
                                                Validacion = "Correcta";

                                            }
                                            else
                                            {
                                                Validacion = "incorrecta";
                                                x = 99;
                                            }

                                        };
                                    };


                                };
                                if (Validacion == "Correcta")
                                {
                                    CInicioFechasPeriodoBean ls = new CInicioFechasPeriodoBean();
                                    ls.iIdEmpresesas = LE[a].iIdEmpresesas;
                                    ls.iTipoPeriodo = LE[a].iTipoPeriodo;
                                    ls.iPeriodo = LE[a].iPeriodo;
                                    ls.sFechaInicio = LE[a].sFechaInicio;
                                    ls.sFechaFinal = LE[a].sFechaFinal;
                                    LPe.Add(ls);
                                }
                            }

                        }
                    }



                }
            }
            else
            {

                CTipoPeriodoBean ls = new CTipoPeriodoBean();
                {
                    ls.iId = 0;
                    ls.sValor = "Nodatos";

                };
                LE3.Add(ls);

            }
            if (OP == 0)
            {
                return Json(LE3);
            }
            if (OP == 1)
            {
                return Json(LPe);
            }
            else {
                return Json(LE3);
            }

        }

        /// Consulta su Exite 

        public JsonResult ExitCalculos(string Idempresas, int anio, int Tipodeperido, int Periodo, int IdDefinicionHD) {
            List<TpCalculosHd> list = new List<TpCalculosHd>();
            List<TpCalculosHd> LiExit = new List<TpCalculosHd>();
            FuncionesNomina Dao = new FuncionesNomina();
            string Correcto = "success";
            if (Idempresas != "" && Idempresas != null) {
                int idEmpresa = 0;
                string[] valores = Idempresas.Split(',');
                for (int i = 1; i < valores.Length - 1; i++)
                {
                    idEmpresa = Convert.ToInt32(valores[i]);
                    list = Dao.sp_ExitCalculo_Retreve_TPlantillaCalculos(idEmpresa, anio, Tipodeperido, Periodo);
                    if (list[0].sMensaje == "success")
                    {
                        if (list[0].iIdDefinicionHd != IdDefinicionHD) {
                            Correcto = "error";
                        }
                    }

                    if (Correcto == "error") { i = valores.Length + 2; }
                }
            }
            TpCalculosHd ls = new TpCalculosHd();
            {
                ls.sMensaje = "success";

            };
            LiExit.Add(ls);
            return Json(LiExit);
        }

        // consulta direccion  el path de los archivos bat 
        public string path()
        {
            string path = " ";
            path = Server.MapPath("Archivos\\porlotes\\");
            path = path.Replace("\\Nomina", "");
            path = path + "prueba.bat";

            return path;
        }

        // Muestra el listado de periodo en la  pantalla comparativo nominas

        [HttpPost]
        public JsonResult ListPeriodoComp(int iIdEmpresesas, int ianio, int iTipoPeriodo)
        {
            List<CInicioFechasPeriodoBean> LPe = new List<CInicioFechasPeriodoBean>();
            FuncionesNomina dao = new FuncionesNomina();
            LPe = dao.sp_CIncioPeriodo_Retrieve_Periodo(iIdEmpresesas, ianio, iTipoPeriodo);
            return Json(LPe);

        }

        // Muestra la diferencias de nomina de una empresa o varias empresas 

        [HttpPost]
        public JsonResult NomiaDiferenciaxEmpresa(int CrtliIdEmpresa, int CrtliAnio, int CrtliTipoPeriodo, int CtrliPeriodo, int CtrliPeriodo2)
        {
            List<CompativoNomBean> LPe = new List<CompativoNomBean>();
            FuncionesNomina dao = new FuncionesNomina();
            LPe = dao.sp_ComparativoNominaXEmpresa_Retrieve_TpCalculosLN(CrtliIdEmpresa, CrtliAnio, CrtliTipoPeriodo, CtrliPeriodo, CtrliPeriodo2);
            return Json(LPe);
        }
   
        // Muestra la diferencias de nomina de una empresa o varias empresas 

        [HttpPost]
        public JsonResult NomiaDiferenciaxEmpleado(int CrtliIdEmpresa, int CrtliAnio, int CrtliTipoPeriodo, int CtrliPeriodo, int CtrliPeriodo2,int CtrliTipoPAgo,int recibo)
        {
            List<CompativoNomBean> LPe = new List<CompativoNomBean>();
            FuncionesNomina dao = new FuncionesNomina();
            LPe = dao.sp_ComparativoNomXEmpleado_Retrieve_TpCalculosLN(CrtliIdEmpresa, CrtliAnio, CrtliTipoPeriodo, CtrliPeriodo, CtrliPeriodo2, CtrliTipoPAgo, recibo);
            return Json(LPe);
        }
        //Muestra el listado de renglones de la diferencia de nomina de una empresa


       [HttpPost]
        public JsonResult NomiaDiferencia(int CrtliIdEmpresa, int CrtliAnio, int CrtliTipoPeriodo, int CtrliPeriodo,  int CtrliPeriodoAnte, int CtrliIdEmpleado, int CtrliEspejo)
        {
            List<CompativoNomBean> LPe = new List<CompativoNomBean>();
            FuncionesNomina dao = new FuncionesNomina();
            LPe = dao.sp_CompativoNomina_Retrieve_TPCalculosln(CrtliIdEmpresa, CrtliAnio, CrtliTipoPeriodo, CtrliPeriodo, CtrliPeriodoAnte, CtrliIdEmpleado, CtrliEspejo);
            return Json(LPe);
        }

        /// lista de perido por empleado 
        [HttpPost]
        public JsonResult PeriodosEmpleados(int Idempresa, int Anio, int TipoPeriodo, int idEmpleado)
        {
            List<CInicioFechasPeriodoBean> LE = new List<CInicioFechasPeriodoBean>();
            FuncionesNomina Dao = new FuncionesNomina();
            LE = Dao.sp_PeriodoEmpleado_Retrieve_TPCalculosLN(Idempresa, Anio, TipoPeriodo, idEmpleado);
            return Json(LE);
        }

        /// consulta la tabla compensacionesfijas 
       
        public JsonResult CompFijasEmpre()
        {
            Boolean flag = false;
            List<CompensacionFijaBean> LComp = new List<CompensacionFijaBean>();
            FuncionesNomina Dao = new FuncionesNomina();
            LComp = Dao.sp_Compensacionfija_Retrieve_CCompensacionfija();
            if (LComp.Count > 0)
            {
                flag = true;
            }
            return Json(new { Bandera = flag, Datos = LComp });
        }

        /// Listado de puesto por Empresa 
        [HttpPost]
        public JsonResult LisPuestosEmpresa(int iIdEmpresa)
        {
            List<PuestosNomBean> LPuesto = new List<PuestosNomBean>();
            FuncionesNomina Dao = new FuncionesNomina();
            LPuesto = Dao.sp_PuestosXEmpresa_Retrieve_Tpuestos(iIdEmpresa);
            return Json(LPuesto);
        }

        //Guarda los datos de la compensacion nueva
        [HttpPost]
        public JsonResult NewCompFija(int iIdempresa, int iPyA,int iIdpuesto,int iIdRenglon,double iImporte, string sDescripcion)
        {
            List<CompensacionFijaBean> bean = new List<CompensacionFijaBean>();
            FuncionesNomina dao = new FuncionesNomina();
            int usuario = int.Parse(Session["iIdUsuario"].ToString());
            bean = dao.sp_Compensacion_Insert_CCompensacionFija(iIdempresa, iPyA, iIdpuesto, iIdRenglon, iImporte, sDescripcion, usuario);
            return Json(bean);
        }

        //Actualiza los datos de la compensacion nueva
        [HttpPost]
        public JsonResult UpdateCompFija(int iIDComp,int iIdempresa, int iPyA, int iIdpuesto, int iIdRenglon, double iImporte, string sDescripcion ,int iCancel)
        {
            CompensacionFijaBean bean = new CompensacionFijaBean();
            FuncionesNomina dao = new FuncionesNomina();
            int usuario = int.Parse(Session["iIdUsuario"].ToString());
            bean = dao.Sp_CCompensacion_update_CCompensacion(iIDComp, iIdempresa, iPyA, iIdpuesto, iIdRenglon, iImporte, sDescripcion, usuario, iCancel);
            return Json(bean);
        }

        //  verifica si hay una ejecucion en procesos 
        [HttpPost]
        public JsonResult ProcesEjecuEsta()
        {
            string Nameuse = Session["Susuario"].ToString();
            int Idusuario = int.Parse(Session["iIdUsuario"].ToString());
            List<TPProcesos> LPro = new List<TPProcesos>();
            List<TPProcesos> Exist = new List<TPProcesos>();
            FuncionesNomina Dao = new FuncionesNomina();
            Exist = Dao.sp_ExistUsuProcesJobs_Retrieve_Tp_Usuario_ProcesJobs(Idusuario);
            if (Exist[0].iExistUsuario == 0)
            {
                Idusuario = 0;
            }
           


            LPro = Dao.sp_ProcesEje_Retrieve_TpProcesosJobs(Idusuario);
            return Json(LPro);
        }

        // verifica que exite el id de la definicion en calculos hd

        [HttpPost]
        public JsonResult CompruRegistroExitdef(int iIdDefinicionHd)
        {
            List<TpCalculosHd> LNND = new List<TpCalculosHd>();         
            FuncionesNomina Dao = new FuncionesNomina();
            LNND = Dao.sp_ExiteDefinicionTpCalculo_Retrieve_ExiteDefinicionTpCalculo(iIdDefinicionHd);         
            return Json(LNND);

        }
   
    }
}