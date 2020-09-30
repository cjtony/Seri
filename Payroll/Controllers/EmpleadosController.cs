﻿using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.IO.Compression;
using System.Text;
using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Security.Policy;

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

        public ActionResult EmisionRecibos()
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
        public JsonResult SearchEmpleadosM(string txtSearch, string Empresa_id)
        {
            List<DescEmpleadoVacacionesBean> empleados = new List<DescEmpleadoVacacionesBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            empleados = Dao.sp_Retrieve_liveSearchEmpleado(int.Parse(Empresa_id), txtSearch);

            return Json(empleados);
        }
        [HttpPost]
        public JsonResult SearchEmpleado(int IdEmpleado)
        {
            @Session["Empleado_id"] = IdEmpleado;
            List<DescEmpleadoVacacionesBean> empleados = new List<DescEmpleadoVacacionesBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            empleados = Dao.sp_CEmpleado_Retrieve_Empleado(IdEmpleado, int.Parse(Session["IdEmpresa"].ToString()));
            return Json(empleados);
        }
        [HttpPost]
        public JsonResult DataTabGenEmploye(int keyemploye)
        {
            Boolean flag = false;
            String messageError = "none";
            EmpleadosBean empleadoBean = new EmpleadosBean();
            ListEmpleadosDao listEmpleadoDao = new ListEmpleadosDao();
            try {
                int keyemp = int.Parse(Session["IdEmpresa"].ToString());
                empleadoBean = listEmpleadoDao.sp_Empleados_Retrieve_Empleado(keyemploye, keyemp);
                if (empleadoBean.sMensaje != "success") {
                    messageError = empleadoBean.sMensaje;
                } else {
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = empleadoBean });
        }

        [HttpPost]
        public JsonResult DataTabImssEmploye(int keyemploye)
        {
            Boolean flag = false;
            String messageError = "none";
            ImssBean imssBean = new ImssBean();
            ListEmpleadosDao listEmpleadoDao = new ListEmpleadosDao();
            try {
                int keyemp = int.Parse(Session["IdEmpresa"].ToString());
                imssBean = listEmpleadoDao.sp_Imss_Retrieve_ImssEmpleado(keyemploye, keyemp);
                if (imssBean.sMensaje != "success") {
                    messageError = imssBean.sMensaje.ToString();
                } else {
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = imssBean });
        }

        [HttpPost]
        public JsonResult DataTabNominaEmploye(int keyemploye)
        {
            Boolean flag = false;
            String messageError = "none";
            DatosNominaBean datoNominaBean = new DatosNominaBean();
            ListEmpleadosDao listEmpleadoDao = new ListEmpleadosDao();
            try {
                int keyemp = int.Parse(Session["IdEmpresa"].ToString());
                datoNominaBean = listEmpleadoDao.sp_Nominas_Retrieve_NominaEmpleado(keyemploye, keyemp);
                if (datoNominaBean.sMensaje != "success") {
                    messageError = datoNominaBean.sMensaje;
                } else {
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = datoNominaBean });
        }

        [HttpPost]
        public JsonResult DataTabStructureEmploye(int keyemploye)
        {
            Boolean flag = false;
            String messageError = "none";
            DatosPosicionesBean datoPosicionBean = new DatosPosicionesBean();
            ListEmpleadosDao listEmpleadoDao = new ListEmpleadosDao();
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
                flag = false;
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
        public JsonResult DataListEmpleado(int iIdEmpresa, int TipoPeriodo, int periodo, int Anio)
        {
            List<EmpleadosEmpresaBean> ListEmple = new List<EmpleadosEmpresaBean>();
            ListEmpleadosDao Dao = new ListEmpleadosDao();
            ListEmple = Dao.sp_EmpleadosDEmpresa_Retrieve_EmpleadosDEmpresa(iIdEmpresa, TipoPeriodo, periodo, Anio);
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

        public JsonResult ReciboNomina(int iIdEmpresa, int iIdEmpleado, int ianio, int iTipodePerido, int iPeriodo, int iespejo)
        {
            int idRenglon = 0;
            List<ReciboNominaBean> LCRecibo = new List<ReciboNominaBean>();
            List<TablaNominaBean> LsTabla = new List<TablaNominaBean>();
            FuncionesNomina dao = new FuncionesNomina();
            LCRecibo = dao.sp_TpCalculoEmpleado_Retrieve_TpCalculoEmpleado(iIdEmpresa, iIdEmpleado, iPeriodo, iTipodePerido, ianio, iespejo);

            if (LCRecibo != null)
            {
                if (LCRecibo.Count > 0)
                {
                    for (int i = 0; i < LCRecibo.Count; i++)
                    {
                        if (LCRecibo[i].iIdRenglon == 1) {
                            idRenglon = i;
                        }

                        TablaNominaBean ls = new TablaNominaBean();
                        {
                            ls.sConcepto = LCRecibo[i].iIdRenglon+" "+ LCRecibo[i].sNombre_Renglon;

                            if (LCRecibo[i].sValor == "Percepciones")
                            {
                                ls.dPercepciones = LCRecibo[i].dSaldo.ToString("#.##");
                                ls.dDeducciones = "0";
                            }
                            if (LCRecibo[i].sValor == "Deducciones")
                            {
                                if (LCRecibo[i].iIdRenglon == 1013)
                                {
                                    LsTabla[idRenglon].dDeducciones = "-"+LCRecibo[i].dSaldo.ToString();

                                }
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

        public JsonResult XMLNomina(int IdEmpresa, string sNombreComple, int Periodo, int anios, int Tipodeperido, int Masivo)
        {

            List<EmisorReceptorBean> ListDatEmisor = new List<EmisorReceptorBean>();
            ListEmpleadosDao Dao = new ListEmpleadosDao();
            string path = Server.MapPath("Archivos\\certificados\\XmlZip\\");
            path = path.Replace("\\Empleados", "");
            ListDatEmisor = Dao.GXMLNOM(IdEmpresa, sNombreComple, path, Periodo, anios, Tipodeperido, Masivo);
            return Json(ListDatEmisor);
        }

        [HttpPost]
        public JsonResult TimbXML(int Anio, int TipoPeriodo, int Perido, int Version, string NomArchivo)
        {
            string CadeSat, UUID, RfcEmi, RfcRep, SelloCF, RfcProv, Nomcer, fechatem, selloemisor;
            int NumEmpleado, anios = Anio, Tipodeperido = TipoPeriodo;
            var fileName = NomArchivo;
            string PathPDF = Server.MapPath("Archivos\\certificados\\PDF\\");
            string PathZip = Server.MapPath("Archivos\\certificados\\");
            PathPDF = PathPDF.Replace("\\Empleados", "");
            PathZip = PathZip.Replace("\\Empleados", "");
            PathZip = PathZip + NomArchivo;
            string path = Server.MapPath("Archivos\\certificados\\XmlZip\\");
            path = path.Replace("\\Empleados", "");

            ZipFile.ExtractToDirectory(PathZip, path);

            DirectoryInfo di = new DirectoryInfo(path);
            foreach (var fi in di.GetFiles())
            {  // nombre y ubicacion del archivo del xml que se va a crear
                String pathxml = path + fi.Name;
                //nombre y unicacion del PDF 
                string Nombrearc = PathPDF + fi.Name;
                Nombrearc = Nombrearc.Replace(".xml", ".pdf");
                // crecion del archivo PDF
                FileStream Fs = new FileStream(Nombrearc, FileMode.Create);
                Document documento = new Document(iTextSharp.text.PageSize.LETTER, 5, 10, 10, 5);
                PdfWriter pw = PdfWriter.GetInstance(documento, Fs);

                documento.Open();

                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
                iTextSharp.text.Font TTexNeg = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font TexNom = new iTextSharp.text.Font(bf, 7, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font TexNeg = new iTextSharp.text.Font(bf, 7, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font TTexNegCuerpo = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font TexNegCuerpo = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(pathxml);
                ////////Cabecera 
                XmlNode nodo = xmlDoc.GetElementsByTagName("cfdi:Emisor").Item(0);
                string Palabra = nodo.Attributes.GetNamedItem("Nombre").Value;
                Paragraph Empresa = new Paragraph(50, Palabra, TTexNeg);
                Empresa.IndentationLeft = 90;

                Paragraph Trfc = new Paragraph("R.F.C.:", TexNeg);
                Trfc.IndentationLeft = 90;
                Palabra = nodo.Attributes.GetNamedItem("Rfc").Value;
                RfcEmi = Palabra;
                Paragraph Rfc = new Paragraph(-1, Palabra, TexNom);
                Rfc.IndentationLeft = 112;
                Paragraph Rfcpatron = new Paragraph(-1, Palabra, TexNom);
                Rfcpatron.IndentationLeft = 132;

                Paragraph TrfcPatron = new Paragraph("R.F.C. Patron:", TexNeg);
                TrfcPatron.IndentationLeft = 90;
                nodo = xmlDoc.GetElementsByTagName("nomina12:Emisor").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("RegistroPatronal").Value;
                Paragraph TRegPat = new Paragraph("Reg.Pat:", TexNeg);
                TRegPat.IndentationLeft = 90;
                Paragraph RegPat = new Paragraph(-1, Palabra, TexNom);

                Paragraph TfolioFis = new Paragraph(-50, "Folio Fiscal:", TexNeg);
                TfolioFis.IndentationLeft = 412;
                nodo = xmlDoc.GetElementsByTagName("tfd:TimbreFiscalDigital").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("UUID").Value;
                CadeSat = "||" + Palabra + "|";
                UUID = Palabra;
                Paragraph folioFis = new Paragraph(-1, Palabra, TexNom);
                folioFis.IndentationLeft = 450;
                RegPat.IndentationLeft = 115;
                nodo = xmlDoc.GetElementsByTagName("cfdi:Comprobante").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("NoCertificado").Value;

                Paragraph TNumCertEmi = new Paragraph("No. de serie del Emisor:", TexNeg);
                TNumCertEmi.IndentationLeft = 380;
                Paragraph NumCertEmi = new Paragraph(-1, Palabra, TexNom);
                NumCertEmi.IndentationLeft = 450;
                Paragraph TFechaEmisior = new Paragraph("Fecha y hora de emisión:", TexNeg);
                TFechaEmisior.IndentationLeft = 377;
                Palabra = nodo.Attributes.GetNamedItem("Fecha").Value;
                Paragraph FechaEmisior = new Paragraph(-1, Palabra, TexNom);
                FechaEmisior.IndentationLeft = 450;
                Paragraph TFechaCertifi = new Paragraph("Fecha y hora de Certificación:", TexNeg);
                TFechaCertifi.IndentationLeft = 363;
                nodo = xmlDoc.GetElementsByTagName("tfd:TimbreFiscalDigital").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("FechaTimbrado").Value;
                CadeSat = CadeSat + Palabra + "|";
                Paragraph FechaCertifi = new Paragraph(-1, Palabra, TexNom);
                FechaCertifi.IndentationLeft = 450;
                Paragraph TRegimenFis = new Paragraph("Regimen fiscal:", TexNeg);
                TRegimenFis.IndentationLeft = 403;
                nodo = xmlDoc.GetElementsByTagName("cfdi:Emisor").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("RegimenFiscal").Value;
                if (Palabra == "601") { Palabra = Palabra + "-General De Ley Personas Morales"; }
                Paragraph RegimenFis = new Paragraph(-1, Palabra, TexNom);
                RegimenFis.IndentationLeft = 450;
                Paragraph TTipoCDFI = new Paragraph("Tipo de CDFI:", TexNeg);
                TTipoCDFI.IndentationLeft = 406;
                Paragraph TipoCDFI = new Paragraph(-1, "Recibo de Nomina", TexNom);
                TipoCDFI.IndentationLeft = 450;
                Paragraph TSerieFolio = new Paragraph("Serie y Folio:", TexNeg);
                TSerieFolio.IndentationLeft = 409;
                nodo = xmlDoc.GetElementsByTagName("cfdi:Comprobante").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("Folio").Value;
                Paragraph SerieFolio = new Paragraph(-1, Palabra, TexNom);
                SerieFolio.FirstLineIndent = 450;

                ////////// Info Personal
                Paragraph Espacio = new Paragraph(20, " ");
                Paragraph table1 = new Paragraph();
                table1.IndentationLeft = 50;
                PdfPTable table = new PdfPTable(1);
                table.HorizontalAlignment = 0;
                table.PaddingTop = 10;
                table.TotalWidth = 200;
                table.LockedWidth = true;
                BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font TexHatable = new iTextSharp.text.Font(bf2, 8, 1, BaseColor.WHITE);
                // Esta es la primera fila
                PdfPCell Cell = new PdfPCell();
                Cell.BackgroundColor = BaseColor.BLACK;
                Cell.AddElement(new Chunk("INFORMACION PERSONAL DEL TRABAJADOR", TexHatable));
                table.AddCell(Cell);
                table1.Add(table);

                nodo = xmlDoc.GetElementsByTagName("nomina12:Receptor").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("NumEmpleado").Value;
                NumEmpleado = int.Parse(Palabra);
                Paragraph TNoEmpleado = new Paragraph(10, "No.Empleado :", TTexNegCuerpo);
                TNoEmpleado.IndentationLeft = 50;
                Paragraph NoEmpleado = new Paragraph(-1, Palabra, TexNegCuerpo);
                NoEmpleado.IndentationLeft = 108;
                Paragraph TNommbre = new Paragraph("Nombre:", TTexNegCuerpo);
                TNommbre.IndentationLeft = 50;
                nodo = xmlDoc.GetElementsByTagName("cfdi:Receptor").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("Nombre").Value;
                Paragraph Nommbre = new Paragraph(-1, Palabra, TexNegCuerpo);
                Nommbre.IndentationLeft = 85;

                Paragraph TCurp = new Paragraph("Curp:", TTexNegCuerpo);
                TCurp.IndentationLeft = 50;
                nodo = xmlDoc.GetElementsByTagName("nomina12:Receptor").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("Curp").Value;
                Paragraph Curp = new Paragraph(-1, Palabra, TexNegCuerpo);
                Curp.IndentationLeft = 73;

                Paragraph TrfcEmp = new Paragraph("R.F.C.:", TTexNegCuerpo);
                TrfcEmp.IndentationLeft = 50;
                nodo = xmlDoc.GetElementsByTagName("cfdi:Receptor").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("Rfc").Value;
                RfcRep = Palabra;
                Paragraph rfcEmp = new Paragraph(-1, Palabra, TexNegCuerpo);
                rfcEmp.IndentationLeft = 78;

                Paragraph TNSS = new Paragraph("NSS:", TTexNegCuerpo);
                TNSS.IndentationLeft = 50;
                nodo = xmlDoc.GetElementsByTagName("nomina12:Receptor").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("NumSeguridadSocial").Value;
                Paragraph NSS = new Paragraph(-1, Palabra, TexNegCuerpo);
                NSS.IndentationLeft = 73;

                Paragraph TRegimen = new Paragraph("Regimen:", TTexNegCuerpo);
                TRegimen.IndentationLeft = 50;
                Palabra = nodo.Attributes.GetNamedItem("TipoRegimen").Value;
                if (Palabra == "02") { Palabra = Palabra + "-Sueldos"; }
                Paragraph Regimen = new Paragraph(-1, Palabra, TexNegCuerpo);
                Regimen.IndentationLeft = 83;

                Paragraph Espacio2 = new Paragraph(-80, " ");
                Paragraph table3 = new Paragraph();
                table3.IndentationLeft = 350;

                PdfPTable table2 = new PdfPTable(1);
                table2.HorizontalAlignment = 0;
                table2.PaddingTop = 10;
                table2.TotalWidth = 200;
                table2.LockedWidth = true;
                ///////// Info Laboral

                PdfPCell Cell2 = new PdfPCell();
                Cell2.BackgroundColor = BaseColor.BLACK;
                Cell2.AddElement(new Chunk("INFORMACION LABORAL", TexHatable));
                table2.AddCell(Cell2);
                table3.Add(table2);

                Paragraph TPuesto = new Paragraph(10, "Puesto:", TTexNegCuerpo);
                TPuesto.IndentationLeft = 350;
                nodo = xmlDoc.GetElementsByTagName("nomina12:Receptor").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("Puesto").Value;
                Paragraph Puesto = new Paragraph(-1, Palabra, TexNegCuerpo);
                Puesto.IndentationLeft = 375;

                Paragraph TContrato = new Paragraph("Contrato:", TTexNegCuerpo);
                TContrato.IndentationLeft = 350;
                Palabra = nodo.Attributes.GetNamedItem("TipoContrato").Value;
                Paragraph Contrato = new Paragraph(-1, Palabra, TexNegCuerpo);
                Contrato.IndentationLeft = 383;

                Paragraph TSalarioB = new Paragraph("Salario Base:", TTexNegCuerpo);
                TSalarioB.IndentationLeft = 350;
                Palabra = nodo.Attributes.GetNamedItem("SalarioBaseCotApor").Value;
                Paragraph SalarioB = new Paragraph(-1, Palabra, TexNegCuerpo);
                SalarioB.IndentationLeft = 395;

                Paragraph TAntiguedad = new Paragraph("Antigüedad:", TTexNegCuerpo);
                TAntiguedad.IndentationLeft = 350;
                Palabra = nodo.Attributes.GetNamedItem("Antigüedad").Value;
                Paragraph Antiguedad = new Paragraph(-1, Palabra + "(Semanas)", TexNegCuerpo);
                Antiguedad.IndentationLeft = 390;

                Paragraph TJornada = new Paragraph("Jornada:", TTexNegCuerpo);
                TJornada.IndentationLeft = 350;
                Palabra = nodo.Attributes.GetNamedItem("TipoJornada").Value;
                Paragraph Jornada = new Paragraph(-1, Palabra, TexNegCuerpo);
                Jornada.IndentationLeft = 379;

                Paragraph TRiesgopu = new Paragraph("Riesgo Puesto:", TTexNegCuerpo);
                TRiesgopu.IndentationLeft = 350;
                Palabra = nodo.Attributes.GetNamedItem("RiesgoPuesto").Value;
                Paragraph Riesgopu = new Paragraph(-1, Palabra, TexNegCuerpo);
                Riesgopu.IndentationLeft = 400;


                /////////////// tipo de pago 
                

                Paragraph Espacio3 = new Paragraph(20, " ");
                Paragraph table6 = new Paragraph();
                table6.IndentationLeft = 50;
                PdfPTable table7 = new PdfPTable(1);
                table7.HorizontalAlignment = 0;
                table7.PaddingTop = 10;
                table7.TotalWidth = 500;
                table7.LockedWidth = true;

                PdfPCell Cell3 = new PdfPCell();
                Cell3.BackgroundColor = BaseColor.BLACK;
                Cell3.AddElement(new Chunk("INFORMACION DE PAGO", TexHatable));
                table7.AddCell(Cell3);
                table6.Add(table7);

                Paragraph TFecPago = new Paragraph("Fecha de Pago:", TTexNegCuerpo);
                TFecPago.IndentationLeft = 50;
                nodo = xmlDoc.GetElementsByTagName("nomina12:Nomina").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("FechaPago").Value;
                Paragraph FecPago = new Paragraph(-1, Palabra, TexNegCuerpo);
                FecPago.IndentationLeft = 100;

                Paragraph TClaveb = new Paragraph("Clave:", TTexNegCuerpo);
                TClaveb.IndentationLeft = 200;
                nodo = xmlDoc.GetElementsByTagName("nomina12:Receptor").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("CuentaBancaria").Value;
                Paragraph Claveb = new Paragraph(-1, Palabra, TexNegCuerpo);
                Claveb.IndentationLeft = 223;
                Paragraph TBanco = new Paragraph("Banco:", TTexNegCuerpo);
                TBanco.IndentationLeft = 50;
                string sbanco;
                if (Palabra.Length >= 7 && Palabra.Length < 18)
                {
                    nodo = xmlDoc.GetElementsByTagName("nomina12:Receptor").Item(0);
                    Palabra = nodo.Attributes.GetNamedItem("Banco").Value;
                    sbanco = Palabra;
                }
                else {
                    sbanco = Palabra.Substring(0, 3);
                }
                List<EmisorReceptorBean> ListEmisor = new List<EmisorReceptorBean>();
                ListEmpleadosDao Dao = new ListEmpleadosDao();
                ListEmisor = Dao.sp_EmisorReceptor_Retrieve_EmisorReceptor(0, NumEmpleado);
                if (ListEmisor != null) {

                    sbanco = sbanco + " " + ListEmisor[0].sDescripcion;
                }

                Paragraph Banco = new Paragraph(-1, sbanco, TexNegCuerpo);
                Banco.IndentationLeft = 75;

                Paragraph TPeriodo = new Paragraph(-22, "Periodo:", TTexNegCuerpo);
                TPeriodo.IndentationLeft = 200;
                nodo = xmlDoc.GetElementsByTagName("nomina12:Receptor").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("PeriodicidadPago").Value;
                Paragraph Periodo = new Paragraph(-1, Palabra, TexNegCuerpo);
                Periodo.IndentationLeft = 227;


                Paragraph TLugarExp = new Paragraph(-10, "Lugar de Expedicion:", TTexNegCuerpo);
                TLugarExp.IndentationLeft = 380;
                nodo = xmlDoc.GetElementsByTagName("cfdi:Comprobante").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("LugarExpedicion").Value;
                Paragraph LugarExp = new Paragraph(-1, " Cp: " + Palabra, TexNegCuerpo);
                LugarExp.IndentationLeft = 453;

                Paragraph TDiasPag = new Paragraph("Dias pagados:", TTexNegCuerpo);
                TDiasPag.IndentationLeft = 50;
                nodo = xmlDoc.GetElementsByTagName("nomina12:Nomina").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("NumDiasPagados").Value;
                Paragraph DiasPag = new Paragraph(-1, Palabra, TexNegCuerpo);
                DiasPag.IndentationLeft = 98;
                Paragraph espacio4 = new Paragraph(50, " ", TexNegCuerpo);

                Paragraph table8 = new Paragraph();
                table8.IndentationLeft = 50;

                PdfPTable table9 = new PdfPTable(1);
                table9.HorizontalAlignment = 0;
                table9.PaddingTop = 10;
                table9.TotalWidth = 350;
                table9.LockedWidth = true;

                PdfPCell Cell4 = new PdfPCell();
                Cell4.BackgroundColor = BaseColor.BLACK;
                Cell4.AddElement(new Chunk("LEYENDA", TexHatable));
                table9.AddCell(Cell4);
                table8.Add(table9);

                Paragraph Espacio5 = new Paragraph(-16, " ");
                Paragraph table10 = new Paragraph();
                table10.IndentationLeft = 350;
                PdfPTable table11 = new PdfPTable(1);
                table11.HorizontalAlignment = 0;
                table11.PaddingTop = 10;
                table11.TotalWidth = 150;
                table11.LockedWidth = true;

                PdfPCell Cell5 = new PdfPCell();
                Cell5.BackgroundColor = BaseColor.BLACK;
                Cell5.AddElement(new Chunk("PERCEPCIONES", TexHatable));
                table11.AddCell(Cell5);
                table10.Add(table11);

                Paragraph Espacio6 = new Paragraph(-16, " ");
                Paragraph table12 = new Paragraph();
                table12.IndentationLeft = 450;
                PdfPTable table13 = new PdfPTable(1);
                table13.HorizontalAlignment = 0;
                table13.PaddingTop = 10;
                table13.TotalWidth = 100;
                table13.LockedWidth = true;

                PdfPCell Cell6 = new PdfPCell();
                Cell6.BackgroundColor = BaseColor.BLACK;
                Cell6.AddElement(new Chunk("DEDUCIONES", TexHatable));
                table13.AddCell(Cell6);
                table12.Add(table13);




                /// imprime en documento
                documento.Add(Empresa);
                documento.Add(Trfc);
                documento.Add(Rfc);
                documento.Add(TrfcPatron);
                documento.Add(Rfcpatron);
                documento.Add(TRegPat);
                documento.Add(RegPat);
                documento.Add(TfolioFis);
                documento.Add(folioFis);
                documento.Add(TNumCertEmi);
                documento.Add(NumCertEmi);
                documento.Add(TFechaEmisior);
                documento.Add(FechaEmisior);
                documento.Add(TFechaCertifi);
                documento.Add(FechaCertifi);
                documento.Add(TRegimenFis);
                documento.Add(RegimenFis);
                documento.Add(TTipoCDFI);
                documento.Add(TipoCDFI);
                documento.Add(TSerieFolio);
                documento.Add(SerieFolio);
                documento.Add(Espacio);
                documento.Add(table1);
                documento.Add(TNoEmpleado);
                documento.Add(NoEmpleado);
                documento.Add(TNommbre);
                documento.Add(Nommbre);
                documento.Add(TCurp);
                documento.Add(Curp);
                documento.Add(TrfcEmp);
                documento.Add(rfcEmp);
                documento.Add(TNSS);
                documento.Add(NSS);
                documento.Add(TRegimen);
                documento.Add(Regimen);
                documento.Add(Espacio2);
                documento.Add(table3);
                documento.Add(TPuesto);
                documento.Add(Puesto);
                documento.Add(TContrato);
                documento.Add(Contrato);
                documento.Add(TSalarioB);
                documento.Add(SalarioB);
                documento.Add(TAntiguedad);
                documento.Add(Antiguedad);
                documento.Add(TJornada);
                documento.Add(Jornada);
                documento.Add(TRiesgopu);
                documento.Add(Riesgopu);
                documento.Add(Espacio3);
                documento.Add(table6);
                documento.Add(TFecPago);
                documento.Add(FecPago);
                documento.Add(TBanco);
                documento.Add(Banco);
                documento.Add(TDiasPag);
                documento.Add(DiasPag);
                documento.Add(TPeriodo);
                documento.Add(Periodo);
                documento.Add(TClaveb);
                documento.Add(Claveb);
                documento.Add(TLugarExp);
                documento.Add(LugarExp);
                documento.Add(espacio4);
                documento.Add(table8);
                documento.Add(Espacio5);
                documento.Add(table10);
                documento.Add(Espacio5);
                documento.Add(table12);

                int a = 0;
                string Palabra2;
                decimal valor;
                decimal per = 0;
                decimal ded = 0;
                decimal total;
                for (int i = 0; i < 4;) { nodo = xmlDoc.GetElementsByTagName("nomina12:Percepcion").Item(a);
                    if (nodo != null) {
                        Palabra = nodo.Attributes.GetNamedItem("Concepto").Value;
                        Palabra2 = nodo.Attributes.GetNamedItem("ImporteGravado").Value;
                        valor = decimal.Parse(Palabra2);
                        per = per + valor;
                        Paragraph TLeyenda = new Paragraph(Palabra, TexNegCuerpo);
                        TLeyenda.IndentationLeft = 75;
                        Paragraph TPercep = new Paragraph(-1, Palabra2, TexNegCuerpo);
                        TPercep.IndentationLeft = 375;
                        documento.Add(TLeyenda);
                        documento.Add(TPercep);
                        a = a + 1;
                    }
                    else {
                        i = 5;
                    }
                }
                a = 0;
                for (int i = 0; i < 4;)
                {
                    nodo = xmlDoc.GetElementsByTagName("nomina12:Deduccion").Item(a);

                    if (nodo != null)
                    {
                        Palabra = nodo.Attributes.GetNamedItem("Concepto").Value;
                        Palabra2 = nodo.Attributes.GetNamedItem("Importe").Value;
                        valor = decimal.Parse(Palabra2);
                        ded = ded + valor;
                        Paragraph TLeyenda = new Paragraph(Palabra, TexNegCuerpo);
                        TLeyenda.IndentationLeft = 75;
                        Paragraph TDedu = new Paragraph(-1, Palabra2, TexNegCuerpo);
                        TDedu.IndentationLeft = 475;

                        documento.Add(TLeyenda);
                        documento.Add(TDedu);
                        a = a + 1;
                    }
                    else
                    {
                        i = 5;
                    }

                }
                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0f, 100f, BaseColor.BLACK, Element.ALIGN_LEFT, 0.2f)));
                p.IndentationLeft = 50;
                p.IndentationRight = 50;
                documento.Add(p);
                Paragraph Ttotal = new Paragraph("Total:", TTexNegCuerpo);
                Ttotal.IndentationLeft = 350;
                string perp = per.ToString();
                Paragraph Tper = new Paragraph(-1, perp, TTexNegCuerpo);
                Tper.IndentationLeft = 375;

                Paragraph Tper2 = new Paragraph(-1, perp, TTexNegCuerpo);
                Tper2.IndentationLeft = 475;

                string deduc = ded.ToString();
                Paragraph Tdedu = new Paragraph(-1, deduc, TTexNegCuerpo);
                Tdedu.IndentationLeft = 475;

                Paragraph Tdedu2 = new Paragraph(-1, deduc, TTexNegCuerpo);
                Tdedu2.IndentationLeft = 475;

                total = per - ded;
                string Rtotal = total.ToString();
                Paragraph Ttotal2 = new Paragraph(-1, Rtotal, TTexNegCuerpo);
                Ttotal2.IndentationLeft = 475;


                Paragraph Espacio7 = new Paragraph(10, " ");
                Paragraph TSbtotal = new Paragraph("Subtotal:", TTexNegCuerpo);
                TSbtotal.IndentationLeft = 430;
                Paragraph TDes = new Paragraph("Descuento:", TTexNegCuerpo);
                TDes.IndentationLeft = 430;

                Paragraph TTotal2 = new Paragraph("Total:", TTexNegCuerpo);
                TTotal2.IndentationLeft = 430;

                Paragraph Espacio8 = new Paragraph(5, " ");
                Paragraph table14 = new Paragraph();
                table14.IndentationLeft = 50;

                PdfPTable table15 = new PdfPTable(1);
                table15.HorizontalAlignment = 0;
                table15.PaddingTop = 10;
                table15.TotalWidth = 250;
                table15.LockedWidth = true;

                PdfPCell Cell7 = new PdfPCell();
                Cell7.BackgroundColor = BaseColor.BLACK;
                Cell7.AddElement(new Chunk("SELLO DIGITAL EMISOR", TexHatable));
                table15.AddCell(Cell7);

                nodo = xmlDoc.GetElementsByTagName("cfdi:Comprobante").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("Sello").Value;
                string selloemi = Palabra;
                selloemisor = Palabra;
                Paragraph SelloEmi = new Paragraph(Palabra, TexNegCuerpo);

                Paragraph TSello = new Paragraph();
                table15.AddCell(SelloEmi);
                table14.Add(table15);



                Paragraph table16 = new Paragraph();
                table16.IndentationLeft = 50;

                PdfPTable table17 = new PdfPTable(1);
                table17.HorizontalAlignment = 0;
                table17.PaddingTop = 10;
                table17.TotalWidth = 250;
                table17.LockedWidth = true;

                PdfPCell Cell8 = new PdfPCell();
                Cell8.BackgroundColor = BaseColor.BLACK;
                Cell8.AddElement(new Chunk("SELLO DIGITAL DEL SAT", TexHatable));
                table17.AddCell(Cell8);

                nodo = xmlDoc.GetElementsByTagName("tfd:TimbreFiscalDigital").Item(0);
                Palabra = nodo.Attributes.GetNamedItem("SelloCFD").Value;
                SelloCF = Palabra;
                Palabra = nodo.Attributes.GetNamedItem("SelloSAT").Value;

                string SelloSat2 = Palabra;
                Paragraph SelloSAT = new Paragraph(Palabra, TexNegCuerpo);
                table17.AddCell(SelloSAT);
                table16.Add(table17);

                //--
                Paragraph table18 = new Paragraph();
                table18.IndentationLeft = 50;

                PdfPTable table19 = new PdfPTable(1);
                table19.HorizontalAlignment = 0;
                table19.PaddingTop = 10;
                table19.TotalWidth = 250;
                table19.LockedWidth = true;

                PdfPCell Cell9 = new PdfPCell();
                Cell9.BackgroundColor = BaseColor.BLACK;
                Cell9.AddElement(new Chunk("Cadena Original del complemento de certificación digital del SAT", TexHatable));
                table19.AddCell(Cell9);

                nodo = xmlDoc.GetElementsByTagName("tfd:TimbreFiscalDigital").Item(0);
                RfcProv = nodo.Attributes.GetNamedItem("RfcProvCertif").Value;
                Nomcer = nodo.Attributes.GetNamedItem("NoCertificadoSAT").Value;
                fechatem = nodo.Attributes.GetNamedItem("FechaTimbrado").Value;

                CadeSat = CadeSat + nodo.Attributes.GetNamedItem("RfcProvCertif").Value + "|" + selloemi + "|" + nodo.Attributes.GetNamedItem("NoCertificadoSAT").Value + "||";
                Paragraph CaSelloSAT = new Paragraph(CadeSat, TexNegCuerpo);
                table19.AddCell(CaSelloSAT);
                table18.Add(table19);


                documento.Add(Ttotal);
                documento.Add(Tper);
                documento.Add(Tdedu);
                documento.Add(Espacio7);
                documento.Add(TSbtotal);
                documento.Add(Tper2);
                documento.Add(TDes);
                documento.Add(Tdedu2);
                documento.Add(TTotal2);
                documento.Add(Ttotal2);
                documento.Add(Espacio8);
                documento.Add(table14);
                documento.Add(table16);
                documento.Add(table18);

                /// imagen Qr
                /// 


                string QrSat = "https://verificacfdi.facturaelectronica.sat.gob.mx/Defaul.aspx?id=" + UUID + "&re=" + RfcEmi + "&rr=" + RfcRep + "&tt=" + Rtotal + "&fe=" + selloemi.Substring(selloemi.Length - 8, 8);
                QRCodeEncoder encoder = new QRCodeEncoder();
                Bitmap img = encoder.Encode(QrSat);
                System.Drawing.Image QR = (System.Drawing.Image)img;

                using (MemoryStream ms = new MemoryStream())
                {
                    QR.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] imageBytes = ms.ToArray();

                    iTextSharp.text.Image imgQr = iTextSharp.text.Image.GetInstance(ms.ToArray());
                    imgQr.BorderWidth = 0;
                    imgQr.SetAbsolutePosition(400, 150);
                    float porcentaje = 0.0f;
                    porcentaje = 100 / imgQr.Width;
                    imgQr.ScalePercent(porcentaje * 100);
                    documento.Add(imgQr);

                }


                documento.Close();
                FuncionesNomina Daos = new FuncionesNomina();
                Daos.sp_Tsellos_InsertUPdate_TSellosSat(1, 0, 0, NumEmpleado, anios, Tipodeperido, Perido, " ", selloemisor, UUID, SelloCF, RfcProv, Nomcer, fechatem);
            };
            string pathxm = path;
            path = path.Replace("\\XmlZip", "");
            path = path + "Pdfzio.zip";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            ZipFile.CreateFromDirectory(PathPDF, path);
            string sourceDirPdf = PathPDF;
            string SourceDirXml = pathxm;
            try
            {

                string[] pdfList = Directory.GetFiles(sourceDirPdf, "*.pdf");
                string[] xmlList = Directory.GetFiles(SourceDirXml, "*.xml");
                foreach (string f in pdfList)
                {
                    // Remove path from the file name.
                    string fName = f.Substring(sourceDirPdf.Length + 1);

                }
                foreach (string f in xmlList)
                {
                    // Remove path from the file name.
                    string fName = f.Substring(SourceDirXml.Length + 1);

                }
                foreach (string f in pdfList)
                {
                    System.IO.File.Delete(f);
                }
                foreach (string f in xmlList)
                {
                    System.IO.File.Delete(f);

                }
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);

            }

            List<EmisorReceptorBean> ListDatEmisor = new List<EmisorReceptorBean>();
            EmisorReceptorBean list = new EmisorReceptorBean();
            list.sMensaje = path;
            ListDatEmisor.Add(list);

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
        public JsonResult TotalesRecibo(int iIdEmpresa, int iIdEmpleado, int iPeriodo,int iespejo)
        {
            List<ReciboNominaBean> ListTotales = new List<ReciboNominaBean>();
            ListEmpleadosDao Dao = new ListEmpleadosDao();
            ListTotales = Dao.sp_SaldosTotales_Retrieve_TPlantillasCalculos(iIdEmpresa, iIdEmpleado, iPeriodo, iespejo);
            return Json(ListTotales);
        }

        [HttpPost]
        public JsonResult SearchDataEmpleado(int Empleado_id, int Empresa_id)
        {
            List<string> empleados = new List<string>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            if (Empresa_id == 0)
            {
                Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            }
            @Session["Empleado_id"] = Empleado_id;
            empleados = Dao.sp_Templeado_Retrieve_DatosEmpleado(Empresa_id, Empleado_id);
            return Json(empleados);
        }

        /// generar Pdf en emision de Recibos
        [HttpPost]
        public JsonResult GenPDF(int Anio, int TipoPeriodo, int Perido, String sIdEmpresas, int iRecibo, string sDEscripcion)
        {

            int Idusuario = Convert.ToInt32(Session["iIdUsuario"]), inactivo = 0,NoEjecuciones=0;


            List<EmpresasBean> NoEmple = new List<EmpresasBean>();
            List<EmpleadosBean> Empleados = new List<EmpleadosBean>();
            List<EmisorReceptorBean> ListDatEmisor = new List<EmisorReceptorBean>();
            List<EmisorReceptorBean> url = new List<EmisorReceptorBean>();
            List<ControlEjecucionBean> LisIdcontrol = new List<ControlEjecucionBean>();
            FuncionesNomina Dao = new FuncionesNomina();
            ListEmpleadosDao Dao2 = new ListEmpleadosDao();
            

            string CadeSat, UUID, RfcEmi, RfcRep, SelloCF, RfcProv, Nomcer, fechatem, selloemisor;
            int NumEmpleado, anios = Anio, Tipodeperido = TipoPeriodo, Version = 12, Folio = 0;
            Folio = Anio * 100000 + TipoPeriodo * 10000 + Perido * 10;
            var fileName = "";
            string PathPDF = "";
            string PathCarp = Server.MapPath("Archivos\\certificados\\PDF2\\");
            PathPDF = PathPDF.Replace("\\Empleados", "");
            PathCarp = PathCarp.Replace("\\Empleados", "");
           
            string path = Server.MapPath("Archivos\\certificados\\XmlZip\\");
            PathPDF = PathPDF.Replace("\\Empleados", "");
            string Nombrearc = PathPDF;
            int idEmpresa = 0, rows = 0;
            string[] valores = sIdEmpresas.Split(' ');
            rows = valores.Length - 1;
            int idempleado = 0;
            string urlpdf;
            int defi = 0;
            for (int i = 0; i < rows; i++)
            {
                idEmpresa = Convert.ToInt32(valores[i]);
                NoEmple = Dao.sp_NumeroEmple_Retrieve_TpCalculosLn(idEmpresa, TipoPeriodo, Perido, anios, 0);
                if (defi == 0) {
                    LisIdcontrol = Dao2.ps_ControlEje_Insert_CControlEjecEmpr(Idusuario, sDEscripcion, inactivo, idEmpresa, anios, Tipodeperido, Perido, iRecibo);
                    defi = 1;
                }
                Dao2.sp_CControlEjeLn_insert_CControlEjeLn(LisIdcontrol[0].iIdContro,idEmpresa,0, anios, Tipodeperido, Perido, iRecibo);
                Empleados = Dao.sp_EmpleadosEmpresa_periodo(idEmpresa, TipoPeriodo, Perido, anios, 1);
                NoEjecuciones = NoEjecuciones + NoEmple[0].iNoEmpleados;

                if (iRecibo == 1)
                {
                    PathCarp = PathCarp + "Simple\\Empresa" + idEmpresa + "\\Periodo"+ Perido+"\\";
                }
                if (iRecibo == 2)
                {
                    PathCarp = PathCarp + "Fiscal\\Empresa" + idEmpresa + "\\Periodo"+ Perido+"\\";
                }

                if (System.IO.File.Exists(PathCarp))
                {

                    PathPDF = PathCarp + "IPSNet";

                }
                else
                {
                   
                    DirectoryInfo di = Directory.CreateDirectory(PathCarp);
                    PathPDF = PathCarp + "IPSNet";
                }

                // con QR

                for (int a = 0; a < NoEmple[0].iNoEmpleados; a++) {
                    //nombre y unicacion del PDF
                    Nombrearc = PathPDF;
                    if (iRecibo == 1) {
                        Nombrearc = Nombrearc + "Recibo_E" + idEmpresa + "_N" + Empleados[a].iNumeroNomina + "_F" + Folio + ".pdf";
                    }
                    if (iRecibo == 2)
                    {
                        Nombrearc = Nombrearc + "ReciboFiscal_E" + idEmpresa + "_N" + Empleados[a].iNumeroNomina + "_F" + Folio + ".pdf";
                    }
                    int valido = 0;
                    idempleado = Empleados[a].iIdEmpleado;
                    ListDatEmisor = Dao2.sp_EmisorReceptor_Retrieve_EmisorReceptor(idEmpresa, Empleados[a].iIdEmpleado);
                    string sAntiguedad = "";
                    List<XMLBean> LisCer = new List<XMLBean>();

                    // con QR
                    List<SelloSatBean> LiTsat = new List<SelloSatBean>();
                    LiTsat = Dao2.sp_DatosSat_Retrieve_TSellosSat(idEmpresa, anios, Tipodeperido, Perido, Empleados[a].iIdEmpleado);
                    if (ListDatEmisor != null) {
                        if (iRecibo == 1 && ListDatEmisor[0].sRFC.Length > 3)
                        {
                            valido = 1;
                        };
                        if (iRecibo == 2 && LiTsat != null && ListDatEmisor[0].sRFC.Length > 3)
                        {
                            if (LiTsat[0].sUUID.Length > 3)
                            {
                                Dao2.sp_CCejecucionAndSen_update_TsellosSat(idEmpresa, idempleado, anios, Tipodeperido, Perido,0);
                                valido = 1;
                            };

                        };
                    };
                    if (valido == 1) {
                        ListDatEmisor[0].sUrl = PathPDF;
                        LisCer = Dao2.sp_FileCer_Retrieve_CCertificados(ListDatEmisor[0].sRFC);
                        string pathCert = Server.MapPath("Archivos\\certificados\\");
                        pathCert = pathCert.Replace("\\Empleados", "");
                        string s_certificadoKey = pathCert + LisCer[0].sfilekey;
                        string s_certificadoCer = pathCert + LisCer[0].sfilecer;
                        string s_transitorio = LisCer[0].stransitorio;
                        if (System.IO.File.Exists(s_certificadoKey))
                        {

                            System.Security.Cryptography.X509Certificates.X509Certificate CerSAT;
                            CerSAT = System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(s_certificadoCer);
                            byte[] bcert = CerSAT.GetSerialNumber();
                            string CerNo = LibreriasFacturas.StrReverse((string)Encoding.UTF8.GetString(bcert));
                            byte[] CERT_SIS = CerSAT.GetRawCertData();

                            // crecion del archivo PDF
                            FileStream Fs = new FileStream(Nombrearc, FileMode.Create);
                            Document documento = new Document(iTextSharp.text.PageSize.LETTER, 5, 10, 10, 5);
                            PdfWriter pw = PdfWriter.GetInstance(documento, Fs);
                            documento.Open();

                            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
                            iTextSharp.text.Font TTexNeg = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.BOLD);
                            iTextSharp.text.Font TexNom = new iTextSharp.text.Font(bf, 7, iTextSharp.text.Font.NORMAL);
                            iTextSharp.text.Font TexNeg = new iTextSharp.text.Font(bf, 7, iTextSharp.text.Font.BOLD);
                            iTextSharp.text.Font TTexNegCuerpo = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.BOLD);
                            iTextSharp.text.Font TexNegCuerpo = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);

                            //////Cabecera  
                          
                            string Palabra = ListDatEmisor[0].sNombreEmpresa;
                            Paragraph Empresa = new Paragraph(50, Palabra, TTexNeg);
                            Empresa.IndentationLeft = 90;


                            Paragraph Trfc = new Paragraph("R.F.C.:", TexNeg);
                            Trfc.IndentationLeft = 90;
                            Palabra = ListDatEmisor[0].sRFC;
                            RfcEmi = Palabra;
                            Paragraph Rfc = new Paragraph(-1, Palabra, TexNom);
                            Rfc.IndentationLeft = 112;
                            Paragraph Rfcpatron = new Paragraph(-1, Palabra, TexNom);
                            Rfcpatron.IndentationLeft = 132;


                            Paragraph TrfcPatron = new Paragraph("R.F.C. Patron:", TexNeg);
                            TrfcPatron.IndentationLeft = 90;
                            Palabra = ListDatEmisor[0].sAfiliacionIMSS;
                            Paragraph TRegPat = new Paragraph("Reg.Pat:", TexNeg);
                            TRegPat.IndentationLeft = 90;
                            Paragraph RegPat = new Paragraph(-1, Palabra, TexNom);
                            RegPat.IndentationLeft = 116;

                            /// Emprime Cabecera
                            documento.Add(Empresa);
                            documento.Add(Trfc);
                            documento.Add(Rfc);
                            documento.Add(TrfcPatron);
                            documento.Add(Rfcpatron);
                            documento.Add(TRegPat);
                            documento.Add(RegPat);


                            if (iRecibo == 2)
                            {

                                // con QR

                                Paragraph TfolioFis = new Paragraph(-50, "Folio Fiscal:", TexNeg);
                                TfolioFis.IndentationLeft = 412;
                                Palabra = LiTsat[0].sUUID;
                                CadeSat = "||" + Palabra + "|";
                                UUID = Palabra;

                                Paragraph folioFis = new Paragraph(-1, Palabra, TexNom);
                                folioFis.IndentationLeft = 450;
                                RegPat.IndentationLeft = 115;

                                Palabra = LiTsat[0].sNoCertificado;
                                Palabra = CerNo;
                                Paragraph TNumCertEmi = new Paragraph("No. de serie del Emisor:", TexNeg);
                                TNumCertEmi.IndentationLeft = 380;

                                Paragraph NumCertEmi = new Paragraph(-1, Palabra, TexNom);
                                NumCertEmi.IndentationLeft = 450;
                                Paragraph TFechaEmisior = new Paragraph("Fecha de inicio:", TexNeg);
                                TFechaEmisior.IndentationLeft = 377;
                                Palabra = LiTsat[0].Fecha;
                                Paragraph FechaEmisior = new Paragraph(-1, Palabra, TexNom);
                                FechaEmisior.IndentationLeft = 450;

                                Paragraph TFechaCertifi = new Paragraph("Fecha y hora de Certificación:", TexNeg);
                                TFechaCertifi.IndentationLeft = 363;
                                Palabra = LiTsat[0].Fechatimbrado;
                                CadeSat = CadeSat + Palabra + "|";

                                Paragraph FechaCertifi = new Paragraph(-1, Palabra, TexNom);
                                FechaCertifi.IndentationLeft = 450;
                                Paragraph TRegimenFis = new Paragraph("Regimen fiscal:", TexNeg);
                                TRegimenFis.IndentationLeft = 403;
                                Palabra = Convert.ToString(ListDatEmisor[0].iRegimenFiscal);
                                if (Palabra == "601") { Palabra = Palabra + "-General De Ley Personas Morales"; }
                                Paragraph RegimenFis = new Paragraph(-1, Palabra, TexNom);
                                RegimenFis.IndentationLeft = 450;

                                Paragraph TTipoCDFI = new Paragraph("Tipo de CDFI:", TexNeg);
                                TTipoCDFI.IndentationLeft = 406;
                                Paragraph TipoCDFI = new Paragraph(-1, "Recibo de Nomina", TexNom);
                                TipoCDFI.IndentationLeft = 450;
                                Paragraph TSerieFolio = new Paragraph("Serie y Folio:", TexNeg);
                                TSerieFolio.IndentationLeft = 409;

                                string folio = "";
                                List<XMLBean> LFolio = new List<XMLBean>();
                                LFolio = Dao2.sp_ObtenFolioCCertificados_RetrieveUpdate_Ccertificados(ListDatEmisor[0].sRFC);
                                if (LFolio != null) folio = LFolio[0].ifolio.ToString();
                                else ListDatEmisor[0].sMensaje = "Erro en Genera el folio Contacte a sistemas";
                                Palabra = folio;
                                Paragraph SerieFolio = new Paragraph(-1, Palabra, TexNom);
                                SerieFolio.FirstLineIndent = 450;

                                /// imprime cabecera de cello
                                documento.Add(TfolioFis);
                                documento.Add(folioFis);
                                documento.Add(TNumCertEmi);
                                documento.Add(NumCertEmi);
                                documento.Add(TFechaEmisior);
                                documento.Add(FechaEmisior);
                                documento.Add(TFechaCertifi);
                                documento.Add(FechaCertifi);
                                documento.Add(TRegimenFis);
                                documento.Add(RegimenFis);
                                documento.Add(TTipoCDFI);
                                documento.Add(TipoCDFI);
                                documento.Add(TSerieFolio);
                                documento.Add(SerieFolio);


                            }


                            ////////// Info Personal
                            Paragraph Espacio = new Paragraph(20, " ");
                            Paragraph table1 = new Paragraph();
                            table1.IndentationLeft = 50;
                            PdfPTable table = new PdfPTable(1);
                            table.HorizontalAlignment = 0;
                            table.PaddingTop = 10;
                            table.TotalWidth = 200;
                            table.LockedWidth = true;
                            BaseFont bf2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
                            iTextSharp.text.Font TexHatable = new iTextSharp.text.Font(bf2, 8, 1, BaseColor.WHITE);
                            // Esta es la primera fila
                            PdfPCell Cell = new PdfPCell();
                            Cell.BackgroundColor = BaseColor.BLACK;
                            Cell.AddElement(new Chunk("INFORMACION PERSONAL DEL TRABAJADOR", TexHatable));
                            table.AddCell(Cell);
                            table1.Add(table);


                            Palabra = Convert.ToString(ListDatEmisor[0].iIdNomina);
                            NumEmpleado = int.Parse(Palabra);
                            Paragraph TNoEmpleado = new Paragraph(10, "No.Empleado :", TTexNegCuerpo);
                            TNoEmpleado.IndentationLeft = 50;
                            Paragraph NoEmpleado = new Paragraph(-1, Palabra, TexNegCuerpo);
                            NoEmpleado.IndentationLeft = 108;
                            Paragraph TNommbre = new Paragraph("Nombre:", TTexNegCuerpo);
                            TNommbre.IndentationLeft = 50;

                            Palabra = Empleados[a].sNombreEmpleado;
                            Paragraph Nommbre = new Paragraph(-1, Palabra, TexNegCuerpo);
                            Nommbre.IndentationLeft = 85;



                            Paragraph TCurp = new Paragraph("Curp:", TTexNegCuerpo);
                            TCurp.IndentationLeft = 50;
                            Palabra = ListDatEmisor[0].sCURP;
                            Paragraph Curp = new Paragraph(-1, Palabra, TexNegCuerpo);
                            Curp.IndentationLeft = 73;


                            Paragraph TrfcEmp = new Paragraph("R.F.C.:", TTexNegCuerpo);
                            TrfcEmp.IndentationLeft = 50;
                            Palabra = ListDatEmisor[0].sRFCEmpleado;
                            RfcRep = Palabra;
                            Paragraph rfcEmp = new Paragraph(-1, Palabra, TexNegCuerpo);
                            rfcEmp.IndentationLeft = 78;


                            Paragraph TNSS = new Paragraph("NSS:", TTexNegCuerpo);
                            TNSS.IndentationLeft = 50;
                            Palabra = ListDatEmisor[0].sRegistroImss;
                            Paragraph NSS = new Paragraph(-1, Palabra, TexNegCuerpo);
                            NSS.IndentationLeft = 73;



                            Paragraph TRegimen = new Paragraph("Regimen:", TTexNegCuerpo);
                            TRegimen.IndentationLeft = 50;
                            Palabra = "02";
                            if (Palabra == "02") { Palabra = Palabra + "-Sueldos"; }
                            Paragraph Regimen = new Paragraph(-1, Palabra, TexNegCuerpo);
                            Regimen.IndentationLeft = 83;

                            Paragraph Espacio2 = new Paragraph(-80, " ");
                            Paragraph table3 = new Paragraph();
                            table3.IndentationLeft = 350;

                            PdfPTable table2 = new PdfPTable(1);
                            table2.HorizontalAlignment = 0;
                            table2.PaddingTop = 10;
                            table2.TotalWidth = 200;
                            table2.LockedWidth = true;


                            ///////// Info Laboral

                            PdfPCell Cell2 = new PdfPCell();
                            Cell2.BackgroundColor = BaseColor.BLACK;
                            Cell2.AddElement(new Chunk("INFORMACION LABORAL", TexHatable));
                            table2.AddCell(Cell2);
                            table3.Add(table2);

                            Paragraph TPuesto = new Paragraph(10, "Puesto:", TTexNegCuerpo);
                            TPuesto.IndentationLeft = 350;
                            Palabra = ListDatEmisor[0].sNombrePuesto;
                            if (Palabra.Length > 25)
                            {
                                Palabra = Palabra.Substring(0, 25);
                            }
                            Paragraph Puesto = new Paragraph(-1, Palabra, TexNegCuerpo);
                            Puesto.IndentationLeft = 375;



                            Paragraph TContrato = new Paragraph("Contrato:", TTexNegCuerpo);
                            TContrato.IndentationLeft = 350;
                            Palabra = ListDatEmisor[0].sTipoContrato;
                            Paragraph Contrato = new Paragraph(-1, Palabra, TexNegCuerpo);
                            Contrato.IndentationLeft = 383;


                            List<ReciboNominaBean> ListTotales = new List<ReciboNominaBean>();
                            ListTotales = Dao2.sp_SaldosTotales_Retrieve_TPlantillasCalculos(idEmpresa, Empleados[a].iIdEmpleado, Perido,0);

                            Paragraph TSalarioB = new Paragraph("Salario Base:", TTexNegCuerpo);
                            TSalarioB.IndentationLeft = 350;

                            if(ListTotales == null){
                                Palabra = "error contcte a sistemas";
                            };
                            if(ListTotales != null) {
                                Palabra = string.Format("{0:N2}", ListTotales[i].dSaldo);
                            }
                           
                            Paragraph SalarioB = new Paragraph(-1, Palabra, TexNegCuerpo);
                            SalarioB.IndentationLeft = 395;



                            var culture = System.Globalization.CultureInfo.CreateSpecificCulture("es-MX");
                            var styles = System.Globalization.DateTimeStyles.None;
                            DateTime dt1 = DateTime.Now;
                            DateTime dt2 = dt1;
                            DateTime dt3 = dt1;

                            List<CInicioFechasPeriodoBean> LFechaPerido = new List<CInicioFechasPeriodoBean>();
                            LFechaPerido = Dao2.sp_DatPeridoEmpresa(idEmpresa, TipoPeriodo, Anio, Perido);

                            bool fechaValida = DateTime.TryParse(LFechaPerido[0].sFechaInicio, culture, styles, out dt1);
                            fechaValida = DateTime.TryParse(LFechaPerido[0].sFechaFinal, culture, styles, out dt2);
                            fechaValida = DateTime.TryParse(LFechaPerido[0].sFechaPago, culture, styles, out dt3);

                            string sFechaInicialPago = String.Format("{0:yyyy-MM-dd}", dt1);
                            string sFechaFinalPago = String.Format("{0:yyyy-MM-dd}", dt2);
                            string sFechaPago = String.Format("{0:yyyy-MM-dd}", dt3);
                            string anoarchivo = String.Format("{0:yyyy}", dt2);
                            string sFechaInicioRelLaboral = String.Format("{0:yyyy-MM-dd}", dt3);

                            DateTime f1 = DateTime.Parse(sFechaInicioRelLaboral);
                            DateTime f2 = DateTime.Parse(sFechaFinalPago);
                            TimeSpan diferencia = f2.Subtract(f1);
                            sAntiguedad = "P" + ((int)(diferencia.Days / 7)).ToString() + "W";

                            Paragraph TAntiguedad = new Paragraph("Antigüedad:", TTexNegCuerpo);
                            TAntiguedad.IndentationLeft = 350;
                            Palabra = sAntiguedad;
                            Paragraph Antiguedad = new Paragraph(-1, Palabra + "(Semanas)", TexNegCuerpo);
                            Antiguedad.IndentationLeft = 390;


                            Paragraph TJornada = new Paragraph("Jornada:", TTexNegCuerpo);
                            TJornada.IndentationLeft = 350;
                            Palabra = "06";
                            Paragraph Jornada = new Paragraph(-1, Palabra, TexNegCuerpo);
                            Jornada.IndentationLeft = 379;

                            Paragraph TRiesgopu = new Paragraph("Riesgo Puesto:", TTexNegCuerpo);
                            TRiesgopu.IndentationLeft = 350;
                            Palabra = "1";
                            Paragraph Riesgopu = new Paragraph(-1, Palabra, TexNegCuerpo);
                            Riesgopu.IndentationLeft = 400;

                            //    /////////////// tipo de pago 

                            Paragraph Espacio3 = new Paragraph(20, " ");
                            Paragraph table6 = new Paragraph();
                            table6.IndentationLeft = 50;
                            PdfPTable table7 = new PdfPTable(1);
                            table7.HorizontalAlignment = 0;
                            table7.PaddingTop = 10;
                            table7.TotalWidth = 500;
                            table7.LockedWidth = true;

                            PdfPCell Cell3 = new PdfPCell();
                            Cell3.BackgroundColor = BaseColor.BLACK;
                            Cell3.AddElement(new Chunk("INFORMACION DE PAGO", TexHatable));
                            table7.AddCell(Cell3);
                            table6.Add(table7);



                            Paragraph TFecPago = new Paragraph("Fecha de Pago:", TTexNegCuerpo);
                            TFecPago.IndentationLeft = 50;
                            Palabra = sFechaPago;
                            Paragraph FecPago = new Paragraph(-1, Palabra, TexNegCuerpo);
                            FecPago.IndentationLeft = 100;


                            Paragraph TClaveb = new Paragraph("Clave:", TTexNegCuerpo);
                            TClaveb.IndentationLeft = 200;

                            string sBanco = ListDatEmisor[0].sDescripcion;
                            string sbanco;
                            string sCuentaBancaria = ListDatEmisor[0].sCtaCheques;

                            Palabra = ListDatEmisor[0].sCtaCheques;
                            Paragraph Claveb = new Paragraph(-1, Palabra, TexNegCuerpo);
                            Claveb.IndentationLeft = 223;
                            Paragraph TBanco = new Paragraph("Banco:", TTexNegCuerpo);
                            TBanco.IndentationLeft = 50;

                            if (Palabra.Length >= 7 && Palabra.Length < 18)
                            {
                                sbanco = sBanco;
                            }
                            else
                            {
                                sbanco = Palabra.Substring(0, 3);
                            }
                            List<EmisorReceptorBean> ListEmisor = new List<EmisorReceptorBean>();
                            //ListEmpleadosDao Dao = new ListEmpleadosDao();
                            ListEmisor = Dao2.sp_EmisorReceptor_Retrieve_EmisorReceptor(idEmpresa, ListDatEmisor[0].iIdEmpleado);
                            if (ListEmisor != null)
                            {

                                sbanco = sbanco + " " + ListEmisor[0].sDescripcion;
                            }

                            Paragraph Banco = new Paragraph(-1, sbanco, TexNegCuerpo);
                            Banco.IndentationLeft = 75;



                            Paragraph TPeriodo = new Paragraph(-22, "Periodo:", TTexNegCuerpo);
                            TPeriodo.IndentationLeft = 200;
                            Palabra = Convert.ToString(Perido);
                            Paragraph Periodo = new Paragraph(-1, Palabra, TexNegCuerpo);
                            Periodo.IndentationLeft = 227;

                            Paragraph TLugarExp = new Paragraph(-10, "Lugar de Expedicion:", TTexNegCuerpo);
                            TLugarExp.IndentationLeft = 380;
                            Palabra = "04600";
                            Paragraph LugarExp = new Paragraph(-1, " Cp: " + Palabra, TexNegCuerpo);
                            LugarExp.IndentationLeft = 453;

                            Paragraph TDiasPag = new Paragraph("Dias pagados:", TTexNegCuerpo);
                            TDiasPag.IndentationLeft = 50;

                            // dias Efectivos 

                            List<ReciboNominaBean> LisTRecibo = new List<ReciboNominaBean>();
                            LisTRecibo = Dao.sp_TpCalculoEmpleado_Retrieve_TpCalculoEmpleado(idEmpresa, ListDatEmisor[0].iIdEmpleado, Perido, TipoPeriodo, Anio,0);
                            decimal iTdias = LFechaPerido[0].iDiasEfectivos;
                            int TDias = 0;
                            string Dias = LisTRecibo[0].sNombre_Renglon;
                            string sDiasEfectivos = Convert.ToString(iTdias);

                            if (Dias.Length > 7)
                            {
                                if (LisTRecibo[0].iIdRenglon == 0)
                                {
                                    string[] dias = Dias.Split(':');
                                    Dias = dias[1].ToString();
                                    Dias = Dias.Replace("}", "");
                                }
                                else
                                {
                                    Dias = "0";
                                }
                                decimal DiasNo = Convert.ToDecimal(Dias);
                                iTdias = iTdias - DiasNo;
                                TDias = Convert.ToInt16(iTdias);
                                sDiasEfectivos = Convert.ToString(TDias);
                            }


                            Palabra = sDiasEfectivos;
                            Paragraph DiasPag = new Paragraph(-1, Palabra, TexNegCuerpo);
                            DiasPag.IndentationLeft = 98;
                            Paragraph espacio4 = new Paragraph(50, " ", TexNegCuerpo);


                            Paragraph table8 = new Paragraph();
                            table8.IndentationLeft = 50;

                            PdfPTable table9 = new PdfPTable(1);
                            table9.HorizontalAlignment = 0;
                            table9.PaddingTop = 10;
                            table9.TotalWidth = 350;
                            table9.LockedWidth = true;

                            PdfPCell Cell4 = new PdfPCell();
                            Cell4.BackgroundColor = BaseColor.BLACK;
                            Cell4.AddElement(new Chunk("LEYENDA", TexHatable));
                            table9.AddCell(Cell4);
                            table8.Add(table9);

                            Paragraph Espacio5 = new Paragraph(-16, " ");
                            Paragraph table10 = new Paragraph();
                            table10.IndentationLeft = 350;
                            PdfPTable table11 = new PdfPTable(1);
                            table11.HorizontalAlignment = 0;
                            table11.PaddingTop = 10;
                            table11.TotalWidth = 150;
                            table11.LockedWidth = true;

                            PdfPCell Cell5 = new PdfPCell();
                            Cell5.BackgroundColor = BaseColor.BLACK;
                            Cell5.AddElement(new Chunk("PERCEPCIONES", TexHatable));
                            table11.AddCell(Cell5);
                            table10.Add(table11);

                            Paragraph Espacio6 = new Paragraph(-16, " ");
                            Paragraph table12 = new Paragraph();
                            table12.IndentationLeft = 450;
                            PdfPTable table13 = new PdfPTable(1);
                            table13.HorizontalAlignment = 0;
                            table13.PaddingTop = 10;
                            table13.TotalWidth = 100;
                            table13.LockedWidth = true;

                            PdfPCell Cell6 = new PdfPCell();
                            Cell6.BackgroundColor = BaseColor.BLACK;
                            Cell6.AddElement(new Chunk("DEDUCIONES", TexHatable));
                            table13.AddCell(Cell6);
                            table12.Add(table13);

                            /// imprime en documento


                            documento.Add(Espacio);
                            documento.Add(table1);
                            documento.Add(TNoEmpleado);
                            documento.Add(NoEmpleado);
                            documento.Add(TNommbre);
                            documento.Add(Nommbre);
                            documento.Add(TCurp);
                            documento.Add(Curp);
                            documento.Add(TrfcEmp);
                            documento.Add(rfcEmp);
                            documento.Add(TNSS);
                            documento.Add(NSS);
                            documento.Add(TRegimen);
                            documento.Add(Regimen);
                            documento.Add(Espacio2);
                            documento.Add(table3);
                            documento.Add(TPuesto);
                            documento.Add(Puesto);
                            documento.Add(TContrato);
                            documento.Add(Contrato);
                            documento.Add(TSalarioB);
                            documento.Add(SalarioB);
                            documento.Add(TAntiguedad);
                            documento.Add(Antiguedad);
                            documento.Add(TJornada);
                            documento.Add(Jornada);
                            documento.Add(TRiesgopu);
                            documento.Add(Riesgopu);
                            documento.Add(Espacio3);
                            documento.Add(table6);
                            documento.Add(TFecPago);
                            documento.Add(FecPago);
                            documento.Add(TBanco);
                            documento.Add(Banco);
                            documento.Add(TDiasPag);
                            documento.Add(DiasPag);
                            documento.Add(TPeriodo);
                            documento.Add(Periodo);
                            documento.Add(TClaveb);
                            documento.Add(Claveb);
                            documento.Add(TLugarExp);
                            documento.Add(LugarExp);
                            documento.Add(espacio4);
                            documento.Add(table8);
                            documento.Add(Espacio5);
                            documento.Add(table10);
                            documento.Add(Espacio5);
                            documento.Add(table12);


                            //int a = 0;
                            string Palabra2;
                            decimal valor;
                            decimal per = 0;
                            decimal ded = 0;
                            decimal total;

                            if (LisTRecibo.Count > 0)
                            {
                                for (int x = 0; x < LisTRecibo.Count; x++)
                                {
                                    if (LisTRecibo[x].sValor == "Percepciones")
                                    {
                                        //string lengRenglon = "";
                                        string ImporGra = string.Format("{0:N2}", LisTRecibo[x].dSaldo);
                                        ImporGra = ImporGra.Replace(",", "");
                                        string IdRenglon = Convert.ToString(LisTRecibo[x].iIdRenglon);
                                        string concepto = LisTRecibo[x].sNombre_Renglon;
                                        if (IdRenglon == "1")
                                        {
                                            concepto = "Sueldo {" + sDiasEfectivos + " Dias}";
                                            //lengRenglon = "001";
                                        }
                                        //lengRenglon = "010";
                                        int idReglontama = IdRenglon.Length;
                                        //if (idReglontama == 1) { IdRenglon = "00" + IdRenglon; };
                                        //if (idReglontama == 2) { IdRenglon = "0" + IdRenglon; };


                                        Palabra = concepto;
                                        Palabra2 = ImporGra;
                                        valor = decimal.Parse(Palabra2);
                                        per = per + valor;
                                        Paragraph TLeyenda = new Paragraph(Palabra, TexNegCuerpo);
                                        TLeyenda.IndentationLeft = 75;
                                        Paragraph TPercep = new Paragraph(-1, Palabra2, TexNegCuerpo);
                                        TPercep.IndentationLeft = 375;
                                        documento.Add(TLeyenda);
                                        documento.Add(TPercep);

                                    }
                                    if (LisTRecibo[x].sValor == "Deducciones")
                                    {

                                        string lengRenglon = "";
                                        string ImporGra = string.Format("{0:N2}", LisTRecibo[x].dSaldo);
                                        ImporGra = ImporGra.Replace(",", "");
                                        string IdRenglon = Convert.ToString(LisTRecibo[x].iIdRenglon);
                                        string concepto = LisTRecibo[x].sNombre_Renglon;

                                        Palabra = concepto;
                                        Palabra2 = ImporGra;
                                        valor = decimal.Parse(Palabra2);
                                        ded = ded + valor;
                                        Paragraph TLeyenda = new Paragraph(Palabra, TexNegCuerpo);
                                        TLeyenda.IndentationLeft = 75;
                                        Paragraph TDedu = new Paragraph(-1, Palabra2, TexNegCuerpo);
                                        TDedu.IndentationLeft = 475;

                                        documento.Add(TLeyenda);
                                        documento.Add(TDedu);

                                    }
                                }

                            }

                            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0f, 100f, BaseColor.BLACK, Element.ALIGN_LEFT, 0.2f)));
                            p.IndentationLeft = 50;
                            p.IndentationRight = 50;
                            documento.Add(p);
                            Paragraph Ttotal = new Paragraph("Total:", TTexNegCuerpo);
                            Ttotal.IndentationLeft = 350;
                            string perp = per.ToString();
                            Paragraph Tper = new Paragraph(-1, perp, TTexNegCuerpo);
                            Tper.IndentationLeft = 375;


                            Paragraph Tper2 = new Paragraph(-1, perp, TTexNegCuerpo);
                            Tper2.IndentationLeft = 475;

                            string deduc = ded.ToString();
                            Paragraph Tdedu = new Paragraph(-1, deduc, TTexNegCuerpo);
                            Tdedu.IndentationLeft = 475;

                            Paragraph Tdedu2 = new Paragraph(-1, deduc, TTexNegCuerpo);
                            Tdedu2.IndentationLeft = 475;

                            total = per - ded;
                            string Rtotal = total.ToString();
                            Paragraph Ttotal2 = new Paragraph(-1, Rtotal, TTexNegCuerpo);
                            Ttotal2.IndentationLeft = 475;


                            Paragraph Espacio7 = new Paragraph(10, " ");
                            Paragraph TSbtotal = new Paragraph("Subtotal:", TTexNegCuerpo);
                            TSbtotal.IndentationLeft = 430;
                            Paragraph TDes = new Paragraph("Descuento:", TTexNegCuerpo);
                            TDes.IndentationLeft = 430;

                            Paragraph TTotal2 = new Paragraph("Total:", TTexNegCuerpo);
                            TTotal2.IndentationLeft = 430;

                            Paragraph Espacio8 = new Paragraph(5, " ");


                            documento.Add(Ttotal);
                            documento.Add(Tper);
                            documento.Add(Tdedu);
                            documento.Add(Espacio7);
                            documento.Add(TSbtotal);
                            documento.Add(Tper2);
                            documento.Add(TDes);
                            documento.Add(Tdedu2);
                            documento.Add(TTotal2);
                            documento.Add(Ttotal2);
                            documento.Add(Espacio8);

                            if (iRecibo == 2)
                            {



                                Paragraph table14 = new Paragraph();
                                table14.IndentationLeft = 50;

                                PdfPTable table15 = new PdfPTable(1);
                                table15.HorizontalAlignment = 0;
                                table15.PaddingTop = 10;
                                table15.TotalWidth = 250;
                                table15.LockedWidth = true;



                                PdfPCell Cell7 = new PdfPCell();
                                Cell7.BackgroundColor = BaseColor.BLACK;
                                Cell7.AddElement(new Chunk("SELLO DIGITAL EMISOR", TexHatable));
                                table15.AddCell(Cell7);


                                Palabra = LiTsat[0].sSelloSat; //// sello
                                string selloemi = Palabra;
                                selloemisor = Palabra;
                                Paragraph SelloEmi = new Paragraph(Palabra, TexNegCuerpo);

                                Paragraph TSello = new Paragraph();
                                table15.AddCell(SelloEmi);
                                table14.Add(table15);

                                Paragraph table16 = new Paragraph();
                                table16.IndentationLeft = 50;

                                PdfPTable table17 = new PdfPTable(1);
                                table17.HorizontalAlignment = 0;
                                table17.PaddingTop = 10;
                                table17.TotalWidth = 250;
                                table17.LockedWidth = true;

                                PdfPCell Cell8 = new PdfPCell();
                                Cell8.BackgroundColor = BaseColor.BLACK;
                                Cell8.AddElement(new Chunk("SELLO DIGITAL DEL SAT", TexHatable));
                                table17.AddCell(Cell8);


                                Palabra = LiTsat[0].sSelloCFD;
                                SelloCF = Palabra;
                                Palabra = LiTsat[0].sSelloSat;

                                string SelloSat2 = Palabra;
                                Paragraph SelloSAT = new Paragraph(Palabra, TexNegCuerpo);
                                table17.AddCell(SelloSAT);
                                table16.Add(table17);

                                //--
                                Paragraph table18 = new Paragraph();
                                table18.IndentationLeft = 50;

                                PdfPTable table19 = new PdfPTable(1);
                                table19.HorizontalAlignment = 0;
                                table19.PaddingTop = 10;
                                table19.TotalWidth = 250;
                                table19.LockedWidth = true;

                                PdfPCell Cell9 = new PdfPCell();
                                Cell9.BackgroundColor = BaseColor.BLACK;
                                Cell9.AddElement(new Chunk("Cadena Original del complemento de certificación digital del SAT", TexHatable));
                                table19.AddCell(Cell9);


                                RfcProv = LiTsat[0].Rfcprov;
                                Nomcer = LiTsat[0].sNoCertificado;
                                fechatem = LiTsat[0].Fechatimbrado;
                                string CadeSat2 = "||" + LiTsat[0].sUUID + "|" + LiTsat[0].Fechatimbrado + "|";
                                CadeSat = CadeSat2 + LiTsat[0].Rfcprov + "|" + selloemi + "|" + LiTsat[0].sNoCertificado + "||";
                                Paragraph CaSelloSAT = new Paragraph(CadeSat, TexNegCuerpo);
                                table19.AddCell(CaSelloSAT);
                                table18.Add(table19);



                                //    /// imagen Qr
                                //    /// 


                                string QrSat = "https://verificacfdi.facturaelectronica.sat.gob.mx/Defaul.aspx?id=" + LiTsat[0].sUUID + "&re=" + RfcEmi + "&rr=" + RfcRep + "&tt=" + Rtotal + "&fe=" + selloemi.Substring(selloemi.Length - 8, 8);
                                QRCodeEncoder encoder = new QRCodeEncoder();
                                Bitmap img = encoder.Encode(QrSat);
                                System.Drawing.Image QR = (System.Drawing.Image)img;

                                using (MemoryStream ms = new MemoryStream())
                                {
                                    QR.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    byte[] imageBytes = ms.ToArray();

                                    iTextSharp.text.Image imgQr = iTextSharp.text.Image.GetInstance(ms.ToArray());
                                    imgQr.BorderWidth = 0;
                                    imgQr.SetAbsolutePosition(400, 150);
                                    float porcentaje = 0.0f;
                                    porcentaje = 100 / imgQr.Width;
                                    imgQr.ScalePercent(porcentaje * 100);
                                    documento.Add(imgQr);

                                }


                                documento.Add(table14);
                                documento.Add(table16);
                                documento.Add(table18);           



                            }

                            documento.Close();
                        

                        }
                    }


                }


            }

            EmisorReceptorBean ls = new EmisorReceptorBean();
            {
                ls.iNoEjecutados = NoEjecuciones;
                ls.sUrl = PathCarp;
            }
            url.Add(ls);

            return Json(url);
        }

        /// generacion de pdf para impresion

        public JsonResult GenPDFmv(int Anio, int TipoPeriodo, int iIdperiodo, int iIdempresa) {

            List<EmisorReceptorBean> LisEmpresa = new List<EmisorReceptorBean>();
            return Json(LisEmpresa);

        }

        [HttpPost]
        
        /// Lista empleado Finiquito
        public JsonResult ListEmpleadoFin(int iIdEmpresa, int TipoPeriodo, int periodo, int Anio)
        {
            List<EmpleadosEmpresaBean> ListEmple = new List<EmpleadosEmpresaBean>();
            ListEmpleadosDao Dao = new ListEmpleadosDao();
            ListEmple = Dao.sp_EmpleadosFiniquito_Retrieve_Tfiniquito_hst(iIdEmpresa, periodo, Anio);
            return Json(ListEmple);
        }

        // Lsita de Calculo Finiquito 

        [HttpPost]

        public JsonResult ReciboFiniquito(int iIdEmpresa, int iIdEmpleado, int ianio, int iPeriodo,int idTipFiniquito)
        {
            
            List<ReciboNominaBean> LCRecibo = new List<ReciboNominaBean>();
            List<TablaNominaBean> LsTabla = new List<TablaNominaBean>();
            List<ReciboNominaBean> LsTotal = new List<ReciboNominaBean>();
            FuncionesNomina dao = new FuncionesNomina();
            LCRecibo = dao.sp_TpCalculoFiniEmpleado_Retrieve_TFiniquito(iIdEmpresa, iIdEmpleado, iPeriodo, ianio, idTipFiniquito);

            if (LCRecibo != null)
            {
                if (LCRecibo.Count > 0)
                {
                    for (int i = 0; i < LCRecibo.Count; i++)
                    {
                        if (LCRecibo[i].iIdRenglon != 990 && LCRecibo[i].iIdRenglon != 1990) {
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
                        if (LCRecibo[i].iIdRenglon == 990 || LCRecibo[i].iIdRenglon == 1990) {
                            ReciboNominaBean ls2 = new ReciboNominaBean();
                            ls2.iIdEmpleado = LCRecibo[i].iIdEmpleado;
                            ls2.iIdFiniquito = LCRecibo[i].iIdFiniquito;
                            ls2.iIdRenglon = LCRecibo[i].iIdRenglon;
                            ls2.dSaldo = LCRecibo[i].dSaldo;
                            ls2.sNombre_Renglon = LCRecibo[i].sNombre_Renglon;
                            LsTotal.Add(ls2);
                        }
                    }

                }


            }
            var result = new { Result = LsTabla, LsTotal };

            return Json(result);

        }

        /// Lista de tipo de Finiquito por empleado
        public JsonResult ListFiniquito(int iIdEmpresa, int iIdEmpleado,  int Anio,int  periodo)
        {
            List<TipoFiniquito> ListFini = new List<TipoFiniquito>();
            ListEmpleadosDao Dao = new ListEmpleadosDao();
            ListFini = Dao.sp_TpFiniquitosEmpleado_Retrieve_TFiniquito(iIdEmpresa, iIdEmpleado, Anio, periodo);
            return Json(ListFini);
        }


        /// Elimina archivo 

        public JsonResult deletArchivo( string path)
        {
            List<TipoFiniquito> archivo = new List<TipoFiniquito>();
            ListEmpleadosDao Dao = new ListEmpleadosDao();
            string PathArchivo = Server.MapPath(path);
            PathArchivo = PathArchivo.Replace("\\Empleados", "");
            if (System.IO.File.Exists(PathArchivo))
            {
                System.IO.File.Delete(PathArchivo);
                TipoFiniquito ls = new TipoFiniquito();
                ls.sMensaje = "archivoborrado";
                archivo.Add(ls);

            }
            return Json(archivo);
        }

        /// pdf masivos 

        [HttpPost]
        public JsonResult GPDFMasivos(int IdEmpresa, int Periodo, int anios, int Tipodeperido, int iRecibo)
        {

            int Idusuario = Convert.ToInt32(Session["iIdUsuario"]);
            int inactivo = 0;


            List<EmpresasBean> NoEmple = new List<EmpresasBean>();
            List<EmpleadosBean> Empleados = new List<EmpleadosBean>();
            List<EmisorReceptorBean> ListDatEmisor = new List<EmisorReceptorBean>();
            List<ReciboNominaBean> LisTRecibo = new List<ReciboNominaBean>();
            List<EmisorReceptorBean> url = new List<EmisorReceptorBean>();
            List<CInicioFechasPeriodoBean> LFechaPerido = new List<CInicioFechasPeriodoBean>();
            List<SelloSatBean> LiTsat = new List<SelloSatBean>();

            FuncionesNomina Dao = new FuncionesNomina();
            ListEmpleadosDao Dao2 = new ListEmpleadosDao();
          //  Dao2.ps_ControlEje_Insert_CControlEjecEmpr(Idusuario,sDEscripcion,inactivo);
            LFechaPerido = Dao2.sp_DatosPerido_Retrieve_DatosPerido(Periodo);

            string CadeSat, UUID, RfcEmi, RfcRep, SelloCF, RfcProv, Nomcer, fechatem, selloemisor;
            int NumEmpleado, Anio = anios, Tipoperido = Tipodeperido, Version = 12, Folio = 0;
            Folio = Anio * 100000 + Tipoperido * 10000 + Periodo * 10;
            var fileName = "";
            string Empre;
            string sDiasEfectivos;
            string PathPDF = Server.MapPath("Archivos\\certificados\\");
            string PathZip = Server.MapPath("Archivos\\certificados\\");
            PathPDF = PathPDF.Replace("\\Empleados", "");
            PathZip = PathZip.Replace("\\Empleados", "");
            PathPDF = PathPDF.Replace("\\Empleados", "");
            string Nombrearc = "RecibosNom";
            int idEmpresa = 0, rows = 0;
            Nombrearc = Nombrearc + "_"+ IdEmpresa+"_"+ LFechaPerido[0].iPeriodo+".pdf";
            rows = 1;
            int idempleado = 0;
            string urlpdf = Nombrearc;

            //nombre y ubicacion del PDF

            Nombrearc = PathPDF + Nombrearc;

            if (System.IO.File.Exists(Nombrearc))
            {
                //  System.IO.File.Delete(Nombrearc);
                EmisorReceptorBean ls = new EmisorReceptorBean();
                ls.sMensaje = "success";
                ListDatEmisor.Add(ls);



            }
            else
            {

                // crecion del archivo PDF
                FileStream Fs = new FileStream(Nombrearc, FileMode.Create);
                Document documento = new Document(iTextSharp.text.PageSize.LETTER, 2, 5, 5, 2);
                PdfWriter pw = PdfWriter.GetInstance(documento, Fs);
                documento.Open();

                for (int i = 0; i < rows; i++)
                {
                    idEmpresa = IdEmpresa;
                    NoEmple = Dao.sp_NoEmpleadosEmpresa_Retrieve_TempleadoNomina(idEmpresa, 0);
                    Empleados = Dao.sp_EmpleadosEmpresa_Retrieve_TempleadoNomina(idEmpresa, 1);


                    for (int a = 0; a < NoEmple[0].iNoEmpleados; a++)
                    {


                        int valido = 0;
                        idempleado = Empleados[a].iIdEmpleado;
                        ListDatEmisor = Dao2.sp_EmisorReceptor_Retrieve_EmisorReceptor(idEmpresa, Empleados[a].iIdEmpleado);
                        LisTRecibo = Dao.sp_TpCalculoEmpleado_Retrieve_TpCalculoEmpleado(idEmpresa, Empleados[a].iIdEmpleado, LFechaPerido[0].iPeriodo, Tipoperido, Anio, 0);
                        LiTsat = Dao2.sp_DatosSat_Retrieve_TSellosSat(idEmpresa, anios, Tipodeperido, LFechaPerido[0].iPeriodo, Empleados[a].iIdEmpleado);

                        if (LisTRecibo != null)
                        {


                            string sAntiguedad = "";
                            List<XMLBean> LisCer = new List<XMLBean>();

                            // con QR
                            LiTsat = Dao2.sp_DatosSat_Retrieve_TSellosSat(idEmpresa, anios, Tipodeperido, Periodo, Empleados[a].iIdEmpleado);

                            if (ListDatEmisor != null)
                            {
                                if (iRecibo == 1 && ListDatEmisor[0].sRFC.Length > 3)
                                {
                                    valido = 1;
                                };
                                if (iRecibo == 2 && LiTsat != null && ListDatEmisor[0].sRFC.Length > 3)
                                {
                                    if (LiTsat[0].sUUID.Length > 3)
                                    {
                                        valido = 1;
                                    };

                                };
                            };
                            if (valido == 1)
                            {
                                ListDatEmisor[0].sUrl = PathPDF;
                                LisCer = Dao2.sp_FileCer_Retrieve_CCertificados(ListDatEmisor[0].sRFC);
                                string pathCert = Server.MapPath("Archivos\\certificados\\");
                                pathCert = pathCert.Replace("\\Empleados", "");
                                string s_certificadoKey = pathCert + LisCer[0].sfilekey;
                                string s_certificadoCer = pathCert + LisCer[0].sfilecer;
                                string s_transitorio = LisCer[0].stransitorio;
                                if (System.IO.File.Exists(s_certificadoKey))
                                {

                                    System.Security.Cryptography.X509Certificates.X509Certificate CerSAT;
                                    CerSAT = System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(s_certificadoCer);
                                    byte[] bcert = CerSAT.GetSerialNumber();
                                    string CerNo = LibreriasFacturas.StrReverse((string)Encoding.UTF8.GetString(bcert));
                                    byte[] CERT_SIS = CerSAT.GetRawCertData();


                                    BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
                                    iTextSharp.text.Font TTexNeg = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.BOLD);
                                    iTextSharp.text.Font TexNom = new iTextSharp.text.Font(bf, 7, iTextSharp.text.Font.NORMAL);
                                    iTextSharp.text.Font Texchica = new iTextSharp.text.Font(bf, 4, iTextSharp.text.Font.NORMAL);
                                    iTextSharp.text.Font TexNeg = new iTextSharp.text.Font(bf, 7, iTextSharp.text.Font.BOLD);
                                    iTextSharp.text.Font TTexNegCuerpo = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.BOLD);
                                    iTextSharp.text.Font TexNegCuerpo = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);

                                    //////Cabecera  


                                    string Palabra = ListDatEmisor[0].sNombreEmpresa;
                                    Empre = Palabra;
                                    Paragraph Empresa = new Paragraph(25, Palabra, TTexNeg);
                                    Empresa.IndentationLeft = 40;


                                    Paragraph Trfc = new Paragraph("R.F.C.:", TexNeg);
                                    Trfc.IndentationLeft = 40;
                                    Palabra = ListDatEmisor[0].sRFC;
                                    RfcEmi = Palabra;
                                    Paragraph Rfc = new Paragraph(-1, Palabra, TexNom);
                                    Rfc.IndentationLeft = 64;
                                    Paragraph Rfcpatron = new Paragraph(-1, Palabra, TexNom);
                                    Rfcpatron.IndentationLeft = 86;


                                    Paragraph TrfcPatron = new Paragraph("R.F.C. Patron:", TexNeg);
                                    TrfcPatron.IndentationLeft = 40;
                                    Palabra = ListDatEmisor[0].sAfiliacionIMSS;
                                    Paragraph TRegPat = new Paragraph("Reg.Pat:", TexNeg);
                                    TRegPat.IndentationLeft = 40;
                                    Paragraph RegPat = new Paragraph(-1, Palabra, TexNom);
                                    RegPat.IndentationLeft = 68;



                                    /// Emprime Cabecera
                                    documento.Add(Empresa);
                                    documento.Add(Trfc);
                                    documento.Add(Rfc);
                                    documento.Add(TrfcPatron);
                                    documento.Add(Rfcpatron);
                                    documento.Add(TRegPat);
                                    documento.Add(RegPat);


                                    /// cabesera direccion

                                    Paragraph TDireccion = new Paragraph(-25, "Direccion:", TexNeg);
                                    TDireccion.IndentationLeft = 350;
                                    Palabra = ListDatEmisor[0].sCalle;
                                    Paragraph Direccion = new Paragraph(-1, Palabra, TexNom);
                                    Direccion.IndentationLeft = 382;


                                    Paragraph TCol = new Paragraph("Col.", TexNeg);
                                    TCol.IndentationLeft = 350;
                                    Palabra = ListDatEmisor[0].sColonia;
                                    Paragraph Col = new Paragraph(-1, Palabra, TexNom);
                                    Col.IndentationLeft = 370;

                                    Paragraph TCp = new Paragraph("CP:", TexNeg);
                                    TCp.IndentationLeft = 350;
                                    Palabra = Convert.ToString(ListDatEmisor[0].iCP);
                                    Paragraph cp = new Paragraph(-1, Palabra, TexNom);
                                    cp.IndentationLeft = 370;

                                    Palabra = " ";
                                    Paragraph espacio = new Paragraph(4, Palabra, TexNom);


                                    Paragraph TEmpleado = new Paragraph("Datos del Empleado", TexNeg);
                                    TEmpleado.IndentationLeft = 40;

                                    Paragraph TNoNomina = new Paragraph("No Empleado:", TexNeg);
                                    TNoNomina.IndentationLeft = 40;
                                    Palabra = Convert.ToString(ListDatEmisor[0].iIdEmpleado);
                                    Paragraph NoNomina = new Paragraph(-1, Palabra, TexNom);
                                    NoNomina.IndentationLeft = 88;



                                    Paragraph TRFCEmpleado = new Paragraph("RFC: ", TexNeg);
                                    TRFCEmpleado.IndentationLeft = 40;

                                    Palabra = Convert.ToString(ListDatEmisor[0].sRFCEmpleado);
                                    Paragraph RFCempleado = new Paragraph(-1, Palabra, TexNom);
                                    RFCempleado.IndentationLeft = 55;


                                    Paragraph TISSM = new Paragraph("AFIL. IMSS: ", TexNeg);
                                    TISSM.IndentationLeft = 40;

                                    Palabra = Convert.ToString(ListDatEmisor[0].sRegistroImss);
                                    Paragraph NoISSM = new Paragraph(-1, Palabra, TexNom);
                                    NoISSM.IndentationLeft = 80;


                                    Paragraph TDepto = new Paragraph("DEPTO: ", TexNeg);
                                    TDepto.IndentationLeft = 40;


                                    Palabra = Convert.ToString(ListDatEmisor[0].sDescripcionDepartamento);
                                    Paragraph Depto = new Paragraph(-1, Palabra, TexNom);
                                    Depto.IndentationLeft = 75;

                                    Paragraph TDiast = new Paragraph("Dias Trab.", TexNeg);
                                    TDiast.IndentationLeft = 40;


                                    // dias Efectivos 
                                    decimal iTdias = LFechaPerido[0].iDiasEfectivos;
                                    int TDias = 0;
                                    string Dias = LisTRecibo[0].sNombre_Renglon;
                                    sDiasEfectivos = Convert.ToString(iTdias);

                                    if (Dias.Length > 7)
                                    {
                                        if (LisTRecibo[0].iIdRenglon == 0)
                                        {
                                            string[] dias = Dias.Split(':');
                                            Dias = dias[1].ToString();
                                            Dias = Dias.Replace("}", "");
                                        }
                                        else
                                        {
                                            Dias = "0";
                                        }
                                        decimal DiasNo = Convert.ToDecimal(Dias);
                                        iTdias = iTdias - DiasNo;
                                        TDias = Convert.ToInt16(iTdias);
                                        sDiasEfectivos = Convert.ToString(TDias);

                                    }

                                    Palabra = Convert.ToString(sDiasEfectivos);
                                    Paragraph DiasT = new Paragraph(-1, Palabra, TexNom);
                                    DiasT.IndentationLeft = 75;

                                    Paragraph TPeriodo = new Paragraph(-35, "Periodo de pago: ", TexNeg);
                                    TPeriodo.IndentationLeft = 350;

                                    Palabra = Convert.ToString(LFechaPerido[0].sFechaInicio + " AL " + LFechaPerido[0].sFechaFinal);
                                    Paragraph Periodos = new Paragraph(-1, Palabra, TexNom);
                                    Periodos.IndentationLeft = 405;

                                    Paragraph Tpuesto = new Paragraph("Puesto: ", TexNeg);
                                    Tpuesto.IndentationLeft = 350;


                                    Palabra = Convert.ToString(ListDatEmisor[0].sNombrePuesto);
                                    Paragraph puesto = new Paragraph(-1, Palabra, TexNom);
                                    puesto.IndentationLeft = 380;



                                    Paragraph TSalariod = new Paragraph("Sala. Dirario: ", TexNeg);
                                    TSalariod.IndentationLeft = 350;

                                    int SD = Convert.ToInt32(ListDatEmisor[0].dSalarioMensual);
                                    SD = SD / 30;

                                    Palabra = Convert.ToString(SD);
                                    Paragraph Salariod = new Paragraph(-1, Palabra, TexNom);
                                    Salariod.IndentationLeft = 390;


                                    Paragraph TSalariodInt = new Paragraph("Sala. Dirario Int: ", TexNeg);
                                    TSalariodInt.IndentationLeft = 350;

                                    Palabra = Convert.ToString(ListDatEmisor[0].SDINT);
                                    Paragraph Salariodint = new Paragraph(-1, Palabra, TexNom);
                                    Salariodint.IndentationLeft = 405;

                                    Paragraph TCentroCost = new Paragraph("CEN.DE COSTOS:", TexNeg);
                                    TCentroCost.IndentationLeft = 350;

                                    Palabra = Convert.ToString(ListDatEmisor[0].sCentroCosto);
                                    Paragraph CentroCost = new Paragraph(-1, Palabra, TexNom);
                                    CentroCost.IndentationLeft = 410;

                                    documento.Add(TDireccion);
                                    documento.Add(Direccion);
                                    documento.Add(TCol);
                                    documento.Add(Col);
                                    documento.Add(TCp);
                                    documento.Add(cp);
                                    documento.Add(espacio);
                                    documento.Add(espacio);
                                    documento.Add(TEmpleado);
                                    documento.Add(espacio);
                                    documento.Add(TNoNomina);
                                    documento.Add(NoNomina);
                                    documento.Add(TRFCEmpleado);
                                    documento.Add(RFCempleado);
                                    documento.Add(TISSM);
                                    documento.Add(NoISSM);
                                    documento.Add(TDepto);
                                    documento.Add(Depto);
                                    documento.Add(TDiast);
                                    documento.Add(DiasT);
                                    documento.Add(TPeriodo);
                                    documento.Add(Periodos);
                                    documento.Add(Tpuesto);
                                    documento.Add(puesto);
                                    documento.Add(TSalariod);
                                    documento.Add(Salariod);
                                    documento.Add(TSalariodInt);
                                    documento.Add(Salariodint);
                                    documento.Add(TCentroCost);
                                    documento.Add(CentroCost);




                                    Paragraph Espacio2 = new Paragraph(-1, " ");
                                    Paragraph table1 = new Paragraph();
                                    table1.IndentationLeft = 100;
                                    PdfPTable table2 = new PdfPTable(2);
                                    table2.HorizontalAlignment = 0;
                                    table2.PaddingTop = 10;
                                    table2.TotalWidth = 500;
                                    table2.LockedWidth = true;

                                    Paragraph table3 = new Paragraph();
                                    table3.IndentationLeft = 50;


                                    PdfPTable table4 = new PdfPTable(2);
                                    table4.HorizontalAlignment = 0;
                                    table4.PaddingTop = 10;
                                    table4.TotalWidth = 250;
                                    table4.LockedWidth = true;

                                    Paragraph table5 = new Paragraph();
                                    table5.IndentationLeft = 310;

                                    PdfPTable table6 = new PdfPTable(2);
                                    table6.HorizontalAlignment = 0;
                                    table6.PaddingTop = 10;
                                    table6.TotalWidth = 250;
                                    table6.LockedWidth = true;




                                    PdfPCell Cell5 = new PdfPCell();
                                    Cell5.BackgroundColor = BaseColor.WHITE;
                                    Cell5.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                    Cell5.AddElement(new Chunk("PERCEPCIONES", TexNeg));


                                    PdfPCell Cell6 = new PdfPCell();
                                    Cell6.BackgroundColor = BaseColor.WHITE;
                                    Cell6.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                    Cell6.AddElement(new Chunk("DEDUCCIONES", TexNeg));
                                    table2.AddCell(Cell5);
                                    table2.AddCell(Cell6);
                                    table1.Add(table2);

                                    PdfPCell Cell7 = new PdfPCell();
                                    Cell7.BackgroundColor = BaseColor.WHITE;
                                    Cell7.MinimumHeight = 120f;
                                    //Cell7.HasMinimumHeight = 350f;
                                    Cell7.Border = iTextSharp.text.Rectangle.NO_BORDER;

                                    PdfPCell Cell8 = new PdfPCell();
                                    Cell8.BackgroundColor = BaseColor.WHITE;
                                    Cell8.MinimumHeight = 120f;
                                    Cell8.Border = iTextSharp.text.Rectangle.NO_BORDER;

                                    PdfPCell Cell9 = new PdfPCell();
                                    Cell9.BackgroundColor = BaseColor.WHITE;
                                    Cell9.MinimumHeight = 120f;
                                    Cell9.Border = iTextSharp.text.Rectangle.NO_BORDER;

                                    PdfPCell Cell10 = new PdfPCell();
                                    Cell10.MinimumHeight = 120f;
                                    Cell10.BackgroundColor = BaseColor.WHITE;
                                    Cell10.Border = iTextSharp.text.Rectangle.NO_BORDER;


                                    documento.Add(espacio);
                                    documento.Add(table1);

                                    int b = 0;
                                    string Palabra2;
                                    decimal valor;
                                    decimal per = 0;
                                    decimal ded = 0;
                                    decimal total;

                                    if (LisTRecibo.Count > 0)
                                    {
                                        for (int x = 0; x < LisTRecibo.Count; x++)
                                        {
                                            if (LisTRecibo[x].sValor == "Percepciones")
                                            {
                                                string lengRenglon = "";
                                                string ImporGra = string.Format("{0:N2}", LisTRecibo[x].dSaldo);
                                                ImporGra = ImporGra.Replace(",", "");
                                                string IdRenglon = Convert.ToString(LisTRecibo[x].iIdRenglon);
                                                string concepto = LisTRecibo[x].sNombre_Renglon;
                                                if (IdRenglon == "1")
                                                {
                                                    concepto = "Sueldo {" + sDiasEfectivos + " Dias}";
                                                    lengRenglon = "001";
                                                }
                                                lengRenglon = "010";
                                                int idReglontama = IdRenglon.Length;
                                                if (idReglontama == 1) { IdRenglon = "00" + IdRenglon; };
                                                if (idReglontama == 2) { IdRenglon = "0" + IdRenglon; };


                                                Palabra = concepto;
                                                Palabra2 = ImporGra;
                                                valor = decimal.Parse(Palabra2);
                                                per = per + valor;
                                                Paragraph TLeyenda = new Paragraph(Palabra, TexNegCuerpo);
                                                TLeyenda.IndentationLeft = 75;
                                                Paragraph TPercep = new Paragraph(-1, Palabra2, TexNegCuerpo);
                                                TPercep.IndentationLeft = 180;

                                                string Perp = concepto + "    " + ImporGra;

                                                Cell7.AddElement(new Chunk(Palabra, TexNeg));
                                                Cell8.AddElement(new Chunk(Palabra2, TexNeg));


                                            }
                                            if (LisTRecibo[x].sValor == "Deducciones")
                                            {

                                                string lengRenglon = "";
                                                string ImporGra = string.Format("{0:N2}", LisTRecibo[x].dSaldo);
                                                ImporGra = ImporGra.Replace(",", "");
                                                string IdRenglon = Convert.ToString(LisTRecibo[x].iIdRenglon);
                                                string concepto = LisTRecibo[x].sNombre_Renglon;

                                                Palabra = concepto;
                                                Palabra2 = ImporGra;
                                                valor = decimal.Parse(Palabra2);
                                                ded = ded + valor;
                                                Paragraph TLeyenda = new Paragraph(Palabra, TexNegCuerpo);
                                                TLeyenda.IndentationLeft = 300;
                                                Paragraph TDedu = new Paragraph(-1, Palabra2, TexNegCuerpo);
                                                TDedu.IndentationLeft = 450;
                                                Cell9.AddElement(new Chunk(Palabra, TexNeg));
                                                Cell10.AddElement(new Chunk(Palabra2, TexNeg));

                                            }
                                        }

                                    }


                                    table4.AddCell(Cell7);
                                    table4.AddCell(Cell8);
                                    table3.Add(table4);

                                    table6.AddCell(Cell9);
                                    table6.AddCell(Cell10);
                                    table5.Add(table6);

                                    Palabra = " ";
                                    Paragraph espacio3 = new Paragraph(-130, Palabra, TexNom);
                                    documento.Add(espacio);
                                    documento.Add(espacio);
                                    documento.Add(table3);
                                    documento.Add(espacio3);
                                    documento.Add(table5);
                                    documento.Add(espacio);
                                    documento.Add(espacio);
                                    Paragraph TTOtalPer = new Paragraph(" Total Percepciones: ", TexNeg);
                                    TTOtalPer.IndentationLeft = 100;

                                    Palabra = Convert.ToString(per);
                                    Paragraph Totaper = new Paragraph(-1, Palabra, TexNom);
                                    Totaper.IndentationLeft = 170;

                                    Paragraph TTotaldeduc = new Paragraph(-1, " Total deduccion: ", TexNeg);
                                    TTotaldeduc.IndentationLeft = 350;

                                    Palabra = Convert.ToString(ded);
                                    Paragraph Totadeduc = new Paragraph(-1, Palabra, TexNom);
                                    Totadeduc.IndentationLeft = 420;


                                    Paragraph TTipopago = new Paragraph("99:Otros   PAGO EN UNA SOLA EXHIBICIÓN ", TexNeg);
                                    TTipopago.IndentationLeft = 40;

                                    string cantidad = Convert.ToString(per - ded);
                                    cantidad = NumeroALetras(cantidad);

                                    Paragraph TTipogoEmpra = new Paragraph("RECIBI" + Empre + ", LA CANTIDAD DE: " + cantidad, TexNeg);
                                    TTipogoEmpra.IndentationLeft = 40;

                                    Palabra = " " /*Convert.ToString()*/;
                                    Paragraph CantidaLetr = new Paragraph(-1, Palabra, TexNom);
                                    CantidaLetr.IndentationLeft = 220;

                                    Paragraph TTotoal = new Paragraph(-8, "Total a Pagar: " + Convert.ToString(per - ded), TexNeg);
                                    TTotoal.IndentationLeft = 350;

                                    documento.Add(TTOtalPer);
                                    documento.Add(Totaper);
                                    documento.Add(TTotaldeduc);
                                    documento.Add(Totadeduc);
                                    documento.Add(espacio);
                                    documento.Add(TTipopago);
                                    documento.Add(TTipogoEmpra);
                                    documento.Add(CantidaLetr);
                                    documento.Add(TTotoal);

                                    PdfPTable tableQR = new PdfPTable(1);
                                    tableQR.HorizontalAlignment = 0;
                                    tableQR.PaddingTop = 10;
                                    tableQR.TotalWidth = 50;
                                    tableQR.LockedWidth = true;
                                    Paragraph tablqr = new Paragraph();
                                    tablqr.IndentationLeft = 40;

                                    PdfPTable tableSell = new PdfPTable(1);
                                    tableSell.HorizontalAlignment = 0;
                                    tableSell.PaddingTop = 10;
                                    tableSell.TotalWidth = 250;
                                    tableSell.LockedWidth = true;
                                    Paragraph tablSello = new Paragraph();
                                    tablSello.IndentationLeft = 100;



                                    PdfPCell Cellqr = new PdfPCell();
                                    Cellqr.MinimumHeight = 25f;
                                    Cellqr.FixedHeight = PageSize.LETTER.Height / 15;
                                    Cellqr.BackgroundColor = BaseColor.WHITE;
                                    Cellqr.Border = iTextSharp.text.Rectangle.NO_BORDER;

                                    PdfPCell CellSello = new PdfPCell();
                                    CellSello.MinimumHeight = 25f;
                                    CellSello.BackgroundColor = BaseColor.WHITE;
                                    CellSello.Border = iTextSharp.text.Rectangle.NO_BORDER;


                                    LiTsat = Dao2.sp_DatosSat_Retrieve_TSellosSat(idEmpresa, anios, Tipodeperido, LFechaPerido[0].iPeriodo, Empleados[a].iIdEmpleado);
                                    if (LiTsat != null)
                                    {

                                        Palabra = Convert.ToString(LiTsat[0].sUUID);
                                        Paragraph TFolio = new Paragraph("Folio Fiscal: " + Palabra, TexNeg);
                                        TFolio.IndentationLeft = 40;
                                        documento.Add(espacio);
                                        documento.Add(espacio);
                                        documento.Add(TFolio);


                                        Palabra = Convert.ToString(LiTsat[0].sSelloCFD);
                                        CellSello.AddElement(new Chunk(Palabra, Texchica));


                                        if (LiTsat[0].sUUID != "")
                                        {
                                            documento.Add(espacio);
                                            documento.Add(espacio);
                                            documento.Add(espacio);
                                            documento.Add(espacio);
                                            documento.Add(espacio);
                                            documento.Add(espacio);
                                            string selloemi = LiTsat[0].sSelloSat;
                                            string QrSat = "https://verificacfdi.facturaelectronica.sat.gob.mx/Defaul.aspx?id=" + LiTsat[0].sUUID + "&re=" + ListDatEmisor[0].sRFC + "&rr=" + ListDatEmisor[0].sRFCEmpleado + "&tt=" + (per - ded) + "&fe=" + selloemi.Substring(selloemi.Length - 8, 8);
                                            QRCodeEncoder encoder = new QRCodeEncoder();
                                            Bitmap img = encoder.Encode(QrSat);
                                            System.Drawing.Image QR = (System.Drawing.Image)img;
                                            using (MemoryStream ms = new MemoryStream())
                                            {
                                                QR.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                                byte[] imageBytes = ms.ToArray();

                                                iTextSharp.text.Image imgQr = iTextSharp.text.Image.GetInstance(ms.ToArray());
                                                imgQr.BorderWidth = 0;
                                                //imgQr.SetAbsolutePosition(450, 40);
                                                imgQr.IndentationLeft = 40;
                                                float porcentaje = 0.0f;
                                                porcentaje = 10 / imgQr.Width;
                                                imgQr.ScalePercent(porcentaje * 50);

                                                Cellqr.Image = iTextSharp.text.Image.GetInstance(imgQr);  //.AddElement(new Chunk(imgQr, Texchica));
                                                                                                          //documento.Add(imgQr);

                                            }


                                        };
                                        if (LiTsat[0].sUUID == "")
                                        {
                                            documento.Add(espacio);
                                            documento.Add(espacio);
                                            documento.Add(espacio);
                                            documento.Add(espacio);
                                            documento.Add(espacio);
                                            documento.Add(espacio);


                                        };


                                    }
                                    if (LiTsat == null)
                                    {

                                    }


                                    Paragraph espaciotablaSe = new Paragraph(-55, " ", TexNeg);
                                    Paragraph TFirmaEmple = new Paragraph(-25, "Firma Empleado", TexNeg);
                                    TFirmaEmple.IndentationLeft = 400;
                                    Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0f, 50f, BaseColor.BLACK, Element.ALIGN_LEFT, 0.2f)));
                                    p.IndentationLeft = 400;
                                    p.IndentationRight = 100;
                                    documento.Add(p);
                                    tableQR.AddCell(Cellqr);
                                    tablqr.Add(tableQR);
                                    tableSell.AddCell(CellSello);
                                    tablSello.Add(tableSell);
                                    documento.Add(tablqr);
                                    documento.Add(espaciotablaSe);
                                    documento.Add(tablSello);
                                    documento.Add(espacio);
                                    documento.Add(espacio);

                                    documento.Add(TFirmaEmple);
                                    documento.Add(espacio);
                                    documento.Add(espacio);
                                    documento.Add(espacio);
                                    documento.Add(espacio);
                                    documento.Add(espacio);
                                    documento.Add(espacio);
                                    documento.Add(espacio);

                                };

                            }
                        }


                    }
                }

                documento.Close();

                EmisorReceptorBean ls = new EmisorReceptorBean();
                {
                    ls.sUrl = urlpdf;
                    
                }



            };
    
            
            return Json(ListDatEmisor);
        }


          //// Cantidad en letra 
        public static string NumeroALetras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));

            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }

            res = NumeroALetras(Convert.ToDouble(entero)) + dec;
            return res;
        }

        // cambia la cantidad en letra 
        private static string NumeroALetras(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);

            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + NumeroALetras(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + NumeroALetras(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = NumeroALetras(Math.Truncate(value / 10) * 10) + " Y " + NumeroALetras(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + NumeroALetras(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = NumeroALetras(Math.Truncate(value / 100)) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = NumeroALetras(Math.Truncate(value / 100) * 100) + " " + NumeroALetras(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + NumeroALetras(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + NumeroALetras(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000) * 1000000);
            }
            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }

            return Num2Text;
        }

        // Carga la ultima ejecuion de la tabla Control de ejecucion 

        [HttpPost]
        public JsonResult TheLastEjecution()
        {
  
            List<EmpresasBean> NoEmple = new List<EmpresasBean>();         
            List<ControlEjecucionBean> LisIdcontrol = new List<ControlEjecucionBean>(); 
            ListEmpleadosDao Dao = new ListEmpleadosDao();
            LisIdcontrol=Dao.sp_UltimaEje_Retrieve_CControlejecEmpr();
            return Json(LisIdcontrol);
        }




    }
}