using iTextSharp.text;
using iTextSharp.text.pdf;
using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.IO.Compression;
using System.Text;
using System.Configuration;
using System.Media;
using System.Xml;

using System.Drawing;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Ajax.Utilities;
using System.Globalization;

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
            List<EmpresasBean> LE = new List<EmpresasBean>();
            FuncionesNomina Dao = new FuncionesNomina();
            LE = Dao.sp_CEmpresas_Retrieve_Empresas();
            if (LE.Count > 0)
            {
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
        // regresa el listado de acumulado 
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
            if (Dt != null)
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
            if (Dta != null)
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
            LNH = dao.sp_TpDefinicionesNom_Retrieve_TpDefinicionNom();

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

            List<NominahdBean> TD = new List<NominahdBean>();
            FuncionesNomina dao = new FuncionesNomina();
            TD = dao.sp_DeficionNominaCancelados_Retrieve_DeficionNominaCancelados(sNombreDefinicion, iCancelado);

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
        public JsonResult UpdatePtDefinicion(string sNombreDefinicion, string sDescripcion, int iAno, int iCancelado, int iIdDefinicionhd)
        {
            NominahdBean bean = new NominahdBean();
            FuncionesNomina dao = new FuncionesNomina();
            bean = dao.sp_TpDefinicion_Update_TpDefinicion(sNombreDefinicion, sDescripcion, iAno, iCancelado, iIdDefinicionhd);
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
        public JsonResult CompruRegistroExit(int iIdDefinicionHd)
        {
            List<TpCalculosHd> LNND = new List<TpCalculosHd>();
            FuncionesNomina Dao = new FuncionesNomina();
            LNND = Dao.sp_ExiteDefinicionTpCalculo_Retrieve_ExiteDefinicionTpCalculo(iIdDefinicionHd);
            return Json(LNND);

        }
        //Guarda los datos de TpCalculos
        [HttpPost]
        public JsonResult InsertDatTpCalculos(int iIdDefinicionHd, int iNominaCerrada)
        {
            TpCalculosHd bean = new TpCalculosHd();
            FuncionesNomina dao = new FuncionesNomina();
            bean = dao.sp_TpCalculos_Insert_TpCalculos(iIdDefinicionHd, iNominaCerrada);
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
        public JsonResult ListTpCalculoln(int iIdCalculosHd, int iTipoPeriodo, int iPeriodo, int idEmpresa)
        {
            List<TpCalculosCarBean> Dta = new List<TpCalculosCarBean>();
            
            //List<NominaLnDatBean> DA = new List<NominaLnDatBean>();
            FuncionesNomina dao = new FuncionesNomina();
            Dta = dao.sp_Caratula_Retrieve_TPlantilla_Calculos(iIdCalculosHd, iTipoPeriodo, iPeriodo, idEmpresa);
            if (Dta.Count > 1) {
                for (int i = 0; i < Dta.Count; i++) {
                    Dta[i].sTotal="$ "+ string.Format(CultureInfo.InvariantCulture, "{0:#,###,##0.00}", Dta[i].dTotal);
                } 
            
            }
         
            return Json(Dta);
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
            FuncionesNomina dao = new FuncionesNomina();
            LTbProc = dao.sp_TPProcesosJobs_Retrieve_TPProcesosJobs(op1, op2, op3, CrtliIdJobs, CtrliIdTarea);
            return Json(LTbProc);
        }
        public JsonResult ListStatusProcesosJobs()
        {
            List<TPProcesos> LTbProc = new List<TPProcesos>();
            FuncionesNomina dao = new FuncionesNomina();
            LTbProc = dao.sp_EstatusJobsTbProcesos_retrieve_EstatusJobsTbProcesos();
            Startup obj = new Startup();
            obj.ActBDTbJobs();
            return Json(LTbProc);
        }
        public JsonResult ProcesosPots( int IdDefinicionHD, int anio,int iTipoPeriodo,int iperiodo)

        {
            Startup obj = new Startup();
            string NomProceso = "CNomina";
            obj.ProcesoNom(NomProceso, IdDefinicionHD, anio, iTipoPeriodo, iperiodo);

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
        public JsonResult ListPeriodoEmpresa(int IdDefinicionHD, int iperiodo, int NomCerr)
        {
            List<CInicioFechasPeriodoBean> LPe = new List<CInicioFechasPeriodoBean>();
            FuncionesNomina dao = new FuncionesNomina();
            LPe = dao.sp_PeridosEmpresa_Retrieve_CinicioFechasPeriodo(IdDefinicionHD, iperiodo,NomCerr);
            return Json(LPe);

        }
        public JsonResult UpdateCInicioFechasPeriodo(int iIdDefinicionHd, int iPerido, int iNominaCerrada)
        {
            CInicioFechasPeriodoBean bean = new CInicioFechasPeriodoBean();
            FuncionesNomina dao = new FuncionesNomina();
            bean = dao.sp_NomCerradaCInicioFechaPeriodo_Update_CInicioFechasPeriodo(iIdDefinicionHd, iPerido, iNominaCerrada);
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


        [HttpPost]
        public JsonResult Statusproc(int iIdCalculosHd, int iTipoPeriodo, int iPeriodo, int idEmpresa, int anio)
        {
            FuncionesNomina dao = new FuncionesNomina();
             
            List<TPProcesos> Dta = new List<TPProcesos>();
            string Parametro = anio+","+ iTipoPeriodo+","+ iPeriodo+","+ iIdCalculosHd+ "%";
           
            Dta = dao.sp_StatusProceso_Retrieve_TPProceso(Parametro);

            return Json(Dta);
        }

    }
}