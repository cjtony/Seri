﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Payroll.Models.Beans;
using Payroll.Models.Daos;

namespace Payroll.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public PartialViewResult GenerarReporte()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult LoadGroupBusiness(int stateGrpBusiness)
        {
            Boolean flag         = false;
            String  messageError = "none";
            List<GruposEmpresasBean> listGrpBusinessBean = new List<GruposEmpresasBean>();
            GruposEmpresasDao        groupBusinessDaoD   = new GruposEmpresasDao();
            try {
                listGrpBusinessBean = groupBusinessDaoD.sp_Datos_GruposEmpresas(stateGrpBusiness);
                if (listGrpBusinessBean.Count > 0) {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = listGrpBusinessBean });
        }

        [HttpPost]
        public JsonResult BusinessGroup(int keyBusinessGroup)
        {
            Boolean flag         = false;
            String  messageError = "none";
            List<GruposEmpresasBean> listBusinessGroupBean = new List<GruposEmpresasBean>();
            GruposEmpresasDao        businessGroupDaoD     = new GruposEmpresasDao();
            try {
                listBusinessGroupBean = businessGroupDaoD.sp_Datos_EmpresasGrupo(keyBusinessGroup);
                if (listBusinessGroupBean.Count > 0) {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = listBusinessGroupBean });
        }

        public Boolean GenerateFoldersReports(string folderFather, string folderChild, string nameFile)
        {
            Boolean flag         = false;
            string  pathSaveFile = Server.MapPath("~/Content/");
            try {
                if (!Directory.Exists(pathSaveFile + folderFather)) {
                    Directory.CreateDirectory(pathSaveFile + folderFather);
                }
                if (!Directory.Exists(pathSaveFile + folderFather + @"\\" + folderChild)) {
                    Directory.CreateDirectory(pathSaveFile + folderFather + @"\\" + folderChild);
                }
                string pathComplete = pathSaveFile + folderFather + @"\\" + folderChild + @"\\" + nameFile;
                if (System.IO.File.Exists(pathComplete)) {
                    System.IO.File.Delete(pathComplete);
                }
                flag = true;
            } catch (Exception exc) {
                flag = false;
            }
            return flag;
        }

        /* GENERACION DE REPORTES */
        [HttpPost]
        public JsonResult ReportPayrollOrg(string typeOption, int keyOptionSel, int typePeriod, int numberPeriod, int yearPeriod, int refreshData)
        {
            Stopwatch stopwatch = new Stopwatch();
            Stopwatch stopwatch1 = new Stopwatch();
            double tiempo = 0;
            Boolean flag        = false;
            Boolean ValidDataN  = false;
            Boolean checkDataN  = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder   = "REPORTES";
            string nameFolderRe = "NOMINA";
            string fileName     = "HCalculo_E" + keyOptionSel.ToString() + "_A" + yearPeriod.ToString() + "_NP" + numberPeriod.ToString() + "_TP" + typePeriod.ToString() + "_A.xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete   = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable     = 0;
            int columnsDataTable  = 0;
            int i, ii, Renglon, a, m, m1, m2, m3;
            int cant1 = 0;
            int cant2 = 0;
            int cant3 = 0;
            int cant4 = 0;
            int[] RenglonesNom            = new int[10];
            int[] RenglonesNomDeduc       = new int[10];
            int[] RenglonesNomEspejo      = new int[10];
            int[] RenglonesNomDeducEspejo = new int[10];
            decimal Total_Percepciones_Fiscal;
            decimal Total_Deducciones_Fiscal;
            decimal Total_Percepciones_Espejo;
            decimal Total_Deducciones_Espejo;
            List<RenglonesHCBean> listRenglones     = new List<RenglonesHCBean>();
            List<RenglonesHCBean> listRenglones1    = new List<RenglonesHCBean>();
            List<RenglonesHCBean> listRenglonesEsp  = new List<RenglonesHCBean>();
            List<RenglonesHCBean> listRenglonesEsp1 = new List<RenglonesHCBean>();
            List< DatosGeneralesHC> datosGenerales  = new List<DatosGeneralesHC>();
            DetallesRenglon detallesRenglon = new DetallesRenglon();
            try {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excel = new ExcelPackage()) {
                    excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(fileName));
                    var worksheet = excel.Workbook.Worksheets[Path.GetFileNameWithoutExtension(fileName)];
                    
                    worksheet.Cells[1, 1].Value = "AÑO";
                    worksheet.Cells[1, 2].Value = "PERIODO";
                    worksheet.Cells[1, 3].Value = "EMPRESA";
                    worksheet.Cells[1, 4].Value = "NOMBRE EMPRESA";
                    worksheet.Cells[1, 5].Value = "NOMINA";
                    worksheet.Cells[1, 6].Value = "APELLIDO PATERNO";
                    worksheet.Cells[1, 7].Value = "APELLIDO MATERNO";
                    worksheet.Cells[1, 8].Value = "NOMBRE";
                    worksheet.Cells[1, 9].Value = "REG.IMSS";
                    worksheet.Cells[1, 10].Value = "RFC";
                    worksheet.Cells[1, 11].Value = "CURP";
                    worksheet.Cells[1, 12].Value = "PUESTO";
                    worksheet.Cells[1, 13].Value = "NOMBRE PUESTO";
                    worksheet.Cells[1, 14].Value = "NIVEL JERARQUICO";
                    worksheet.Cells[1, 15].Value = "DEPTO.";
                    worksheet.Cells[1, 16].Value = "NOMBRE DEPTO.";
                    worksheet.Cells[1, 17].Value = "CENTRO COSTO";
                    worksheet.Cells[1, 18].Value = "DESCR.CENTRO COSTO";
                    worksheet.Cells[1, 19].Value = "REGIONAL";
                    worksheet.Cells[1, 20].Value = "CVE.REGIONAL";
                    worksheet.Cells[1, 21].Value = "DESCR.REGIONAL";
                    worksheet.Cells[1, 22].Value = "SUCURSAL";
                    worksheet.Cells[1, 23].Value = "CVE.SUCURSAL";
                    worksheet.Cells[1, 24].Value = "DESCR.SUCURSAL";
                    worksheet.Cells[1, 25].Value = "FECHA ANTIGUEDAD";
                    worksheet.Cells[1, 26].Value = "FECHA INGRESO";
                    worksheet.Cells[1, 27].Value = "SUELDO";
                    worksheet.Cells[1, 28].Value = "VACANTE CUBIERTA";
                    worksheet.Cells[1, 29].Value = "ULTIMA POSICION";
                    worksheet.Cells[1, 30].Value = "ULT. SDI";

                    for (i = 1; i < 31; i++) {
                        worksheet.Cells[1, i].Style.Fill.SetBackground(System.Drawing.Color.LightSkyBlue);
                    }

                    ii = 30;

                    // Obtenemos los renglones calculados
                    listRenglones = reportDao.sp_Renglones_Hoja_Calculo(keyOptionSel, typePeriod, numberPeriod, yearPeriod, 0, 0, 1000);
                    if (listRenglones.Count > 0) {
                        RenglonesNom = new int[listRenglones.Count];
                        int r = 0;
                        foreach (RenglonesHCBean renglon in listRenglones) {
                            ii += 1;
                            Renglon         = Convert.ToInt32(renglon.iIdRenglon);
                            RenglonesNom[r] = Convert.ToInt32(renglon.iIdRenglon);
                            worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightGreen);
                            worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                            r += 1;
                        }
                    }

                    a = ii;
                    ii += 1;
                    worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightGreen);
                    worksheet.Cells[1, ii + 1].Value = "TOTAL PERCEPCIONES FISCAL";

                    listRenglones1 = reportDao.sp_Renglones_Hoja_Calculo(keyOptionSel, typePeriod, numberPeriod, yearPeriod, 0, 1000, 2000);
                    if (listRenglones1.Count > 0) {
                        RenglonesNomDeduc = new int[listRenglones1.Count];
                        int j = 0;
                        foreach (RenglonesHCBean renglon in listRenglones1) {
                            ii += 1;
                            Renglon              = Convert.ToInt32(renglon.iIdRenglon);
                            RenglonesNomDeduc[j] = Renglon;
                            worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                            worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                            j += 1;
                        }
                    }

                    ii += 1;
                    worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                    worksheet.Cells[1, ii + 1].Value = "TOTAL DEDUCCIONES FISCAL";
                    ii += 1;
                    worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightSteelBlue);
                    worksheet.Cells[1, ii + 1].Value = "NETO A PAGAR FISCAL";

                    listRenglonesEsp = reportDao.sp_Renglones_Hoja_Calculo(keyOptionSel, typePeriod, numberPeriod, yearPeriod, 1, 0, 1000);
                    if (listRenglonesEsp.Count > 0) {
                        RenglonesNomEspejo = new int[listRenglonesEsp.Count];
                        int g = 0;
                        foreach (RenglonesHCBean renglon in listRenglonesEsp) {
                            ii += 1;
                            Renglon               = Convert.ToInt32(renglon.iIdRenglon);
                            RenglonesNomEspejo[g] = Renglon;
                            worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightGreen);
                            worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                            g += 1;
                        }
                    }

                    ii += 1;
                    worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightGreen);
                    worksheet.Cells[1, ii + 1].Value = "TOTAL PERCEPCIONES GASTOS";

                    listRenglonesEsp1 = reportDao.sp_Renglones_Hoja_Calculo(keyOptionSel, typePeriod, numberPeriod, yearPeriod, 1, 1000, 2000);
                    if (listRenglonesEsp1.Count > 0) {
                        RenglonesNomDeducEspejo = new int[listRenglonesEsp1.Count];
                        int y = 0;
                        foreach (RenglonesHCBean renglon in listRenglonesEsp1) {
                            ii += 1;
                            Renglon = Convert.ToInt32(renglon.iIdRenglon);
                            RenglonesNomDeducEspejo[y] = Renglon;
                            worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                            worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                            y += 1;
                        }
                    }

                    ii += 1;
                    worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                    worksheet.Cells[1, ii + 1].Value = "TOTAL DEDUCCIONES GASTO";
                    ii += 1;
                    worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightSteelBlue);
                    worksheet.Cells[1, ii + 1].Value = "NETO A PAGAR GASTO";
                    ii += 1;
                    worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightCyan);
                    worksheet.Cells[1, ii + 1].Value = "NETO A PAGAR TOTAL";
                    stopwatch1.Start();
                    string inicio = DateTime.Now.ToString("hh:mm:ss");
                    DateTime comienzo = Convert.ToDateTime(inicio);
                    datosGenerales = reportDao.sp_Datos_Generales_HC(keyOptionSel, typePeriod, numberPeriod, yearPeriod);
                    if (datosGenerales.Count > 0) {
                        int business = 0, payroll = 0;
                        int p = 1;
                        foreach (DatosGeneralesHC dato in datosGenerales) {

                            business = dato.iEmpresa;
                            payroll  = dato.iNomina;

                            worksheet.Cells[p + 1, 1].Style.Numberformat.Format = "0";
                            worksheet.Cells[p + 1, 1].Value = dato.iAnio;
                            worksheet.Cells[p + 1, 2].Style.Numberformat.Format = "0";
                            worksheet.Cells[p + 1, 2].Value = dato.iPeriodo;
                            worksheet.Cells[p + 1, 3].Style.Numberformat.Format = "0";
                            worksheet.Cells[p + 1, 3].Value = dato.iEmpresa;
                            worksheet.Cells[p + 1, 4].Value = dato.sEmpresa;
                            worksheet.Cells[p + 1, 5].Style.Numberformat.Format = "0";
                            worksheet.Cells[p + 1, 5].Value = dato.iNomina;
                            worksheet.Cells[p + 1, 6].Value = dato.sPaterno;
                            worksheet.Cells[p + 1, 7].Value = dato.sMaterno;
                            worksheet.Cells[p + 1, 8].Value = dato.sNombreE;
                            worksheet.Cells[p + 1, 9].Value = dato.sRegImss;
                            worksheet.Cells[p + 1, 10].Value = dato.sRfc;
                            worksheet.Cells[p + 1, 11].Value = dato.sCurp;
                            worksheet.Cells[p + 1, 12].Value = dato.sPuesto;
                            worksheet.Cells[p + 1, 13].Value = dato.sNombrePuesto;
                            worksheet.Cells[p + 1, 14].Value = dato.sNivelJerarquico;
                            worksheet.Cells[p + 1, 15].Value = dato.sDepto;
                            worksheet.Cells[p + 1, 16].Value = dato.sNombreDepto;
                            worksheet.Cells[p + 1, 17].Value = dato.sCentrCosto;
                            worksheet.Cells[p + 1, 18].Value = dato.sDescCentrCosto;
                            worksheet.Cells[p + 1, 19].Style.Numberformat.Format = "0";
                            worksheet.Cells[p + 1, 19].Value = dato.iRegional;
                            worksheet.Cells[p + 1, 20].Value = dato.sClvRegional;
                            worksheet.Cells[p + 1, 21].Value = dato.sDescRegional;
                            worksheet.Cells[p + 1, 22].Style.Numberformat.Format = "0";
                            worksheet.Cells[p + 1, 22].Value = dato.iSucursal;
                            worksheet.Cells[p + 1, 23].Value = dato.sClvSucursal;
                            worksheet.Cells[p + 1, 24].Value = dato.sDescSucursal;
                            worksheet.Cells[p + 1, 25].Value = dato.sFechaAnt;
                            worksheet.Cells[p + 1, 26].Value = dato.sFechaIng;
                            worksheet.Cells[p + 1, 27].Style.Numberformat.Format = "0.00";
                            worksheet.Cells[p + 1, 27].Value = dato.dSueldo;
                            worksheet.Cells[p + 1, 28].Style.Numberformat.Format = "0";
                            worksheet.Cells[p + 1, 28].Value = dato.iVacanteC;
                            worksheet.Cells[p + 1, 29].Style.Numberformat.Format = "0";
                            worksheet.Cells[p + 1, 29].Value = dato.iUltimaPos;
                            worksheet.Cells[p + 1, 30].Style.Numberformat.Format = "0.00";
                            worksheet.Cells[p + 1, 30].Value = dato.dUltSdi;

                            Total_Percepciones_Fiscal = 0;

                            for (m = 0; m < RenglonesNom.Count(); m++) {
                                detallesRenglon = reportDao.sp_Detalle_Renglones(business, payroll, numberPeriod, typePeriod, yearPeriod, RenglonesNom[m], 0);
                                if (detallesRenglon.iRenglon == 0) {
                                    worksheet.Cells[p + 1, m + 2 + 30].Value = "";
                                } else {
                                    worksheet.Cells[p + 1, m + 2 + 30].Style.Numberformat.Format = "0.00";
                                    Total_Percepciones_Fiscal += Convert.ToDecimal(detallesRenglon.dSaldo);
                                    worksheet.Cells[p + 1, m + 2 + 30].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                }
                            }

                            cant1 = RenglonesNom.Length;

                            worksheet.Cells[p + 1, cant1 + 2 + 30].Style.Numberformat.Format = "0.00";
                            worksheet.Cells[p + 1, cant1 + 2 + 30].Style.Fill.SetBackground(System.Drawing.Color.LightGreen);
                            worksheet.Cells[p + 1, cant1 + 2 + 30].Value = Total_Percepciones_Fiscal;
                            m1 = m;

                            int rest1 = 0;
                            if (RenglonesNomDeduc.Length > cant1) {
                                rest1 = RenglonesNomDeduc.Length - cant1;
                            } else {
                                rest1 = cant1 - RenglonesNomDeduc.Length;
                            }

                            Total_Deducciones_Fiscal = 0;

                            for (m = 0; m < RenglonesNomDeduc.Count(); m++) {
                                detallesRenglon = reportDao.sp_Detalle_Renglones(business, payroll, numberPeriod, typePeriod, yearPeriod, RenglonesNomDeduc[m], 0);
                                if (detallesRenglon.iRenglon == 0) {
                                    worksheet.Cells[p + 1, cant1 + m + rest1 + 30].Value = "";
                                } else {
                                    worksheet.Cells[p + 1, cant1 + m + rest1 + 30].Style.Numberformat.Format = "0.00";
                                    Total_Deducciones_Fiscal += Convert.ToDecimal(detallesRenglon.dSaldo);
                                    worksheet.Cells[p + 1, cant1 + m + rest1 + 30].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                }
                            }

                            worksheet.Cells[p + 1, cant1 + m + rest1 + 30].Style.Numberformat.Format = "0.00";
                            worksheet.Cells[p + 1, cant1 + m + rest1 + 30].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                            worksheet.Cells[p + 1, cant1 + m + rest1 + 30].Value = Total_Deducciones_Fiscal;
                            m2 = m + m1;

                            // Monto neto a pagar (FISCAL)
                            worksheet.Cells[p + 1, cant1 + m + rest1 + 1 + 30].Style.Numberformat.Format = "0.00";
                            worksheet.Cells[p + 1, cant1 + m + rest1 + 1 + 30].Style.Fill.SetBackground(System.Drawing.Color.LightSteelBlue);
                            worksheet.Cells[p + 1, cant1 + m + rest1 + 1 + 30].Value = Total_Percepciones_Fiscal - Total_Deducciones_Fiscal;

                            cant2 = RenglonesNomDeduc.Length;

                            int rest2 = 0;

                            if (RenglonesNomEspejo.Length > cant2) {
                                rest2 = RenglonesNomEspejo.Length - cant2;
                            } else { 
                                rest2 = cant2 - RenglonesNomEspejo.Length;
                            }
                            Total_Percepciones_Espejo = 0;
                            int rest3 = rest1 - 1;
                            for (m = 0; m < RenglonesNomEspejo.Count(); m++) { 
                                detallesRenglon = reportDao.sp_Detalle_Renglones(business, payroll, numberPeriod, typePeriod, yearPeriod, RenglonesNomEspejo[m], 1);
                                if (detallesRenglon.iRenglon == 0) {
                                    int re0 = cant2 + cant1 + m + rest2 + rest3 + 30;
                                    worksheet.Cells[p + 1, cant2 + cant1 + m + rest2 + rest3 + 30].Value = "";
                                } else {
                                    int re2 = cant2 + cant1 + m + rest2 + rest3 + 30;
                                    worksheet.Cells[p + 1, cant2 + cant1 + m + rest2 + rest3 + 30].Style.Numberformat.Format = "0.00";
                                    Total_Percepciones_Espejo += Convert.ToDecimal(detallesRenglon.dSaldo);
                                    worksheet.Cells[p + 1, cant2 + cant1 + m + rest2 + rest3 + 30].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                }
                            }
                            int re1 = cant2 + cant1 + m + rest2 + rest3 + 30 + 1;
                            worksheet.Cells[p + 1, cant2 + cant1 + m + rest2 + rest3 + 30].Style.Numberformat.Format = "0.00";
                            worksheet.Cells[p + 1, cant2 + cant1 + m + rest2 + rest3 + 30].Style.Fill.SetBackground(System.Drawing.Color.LightGreen);
                            worksheet.Cells[p + 1, cant2 + cant1 + m + rest2 + rest3 + 30].Value = Total_Percepciones_Espejo;
                            //// m1 += 43;
                            // m3 = m2 + m;

                            cant3 = RenglonesNomEspejo.Length;

                            int rest4 = 0;

                            if (RenglonesNomDeducEspejo.Length > cant3) {
                                rest4 = RenglonesNomDeducEspejo.Length - cant3;
                            } else {
                                rest4 = cant3 - RenglonesNomDeducEspejo.Length;
                            }

                            Total_Deducciones_Espejo = 0;

                            for (m = 0; m < RenglonesNomDeducEspejo.Count(); m++) {
                                detallesRenglon = reportDao.sp_Detalle_Renglones(business, payroll, numberPeriod, typePeriod, yearPeriod, RenglonesNomDeducEspejo[m], 1);
                                m1++;
                                int tp = cant1 + cant2 + cant3 + m + rest2 + rest3 + 30 + 1;
                                int t = 0;
                                if (detallesRenglon.iRenglon == 0) {
                                    worksheet.Cells[p + 1, cant1 + cant2 + cant3 + m + rest2 + rest3 + 30 + 1].Value = "";
                                } else {
                                    worksheet.Cells[p + 1, cant1 + cant2 + cant3 + m + rest2 + rest3 + 30 + 1].Style.Numberformat.Format = "0.00";
                                    Total_Deducciones_Espejo += Convert.ToDecimal(detallesRenglon.dSaldo);
                                    worksheet.Cells[p + 1, cant1 + cant2 + cant3 + m + rest2 + rest3 + 30 + 1].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                }
                            }

                            worksheet.Cells[p + 1, cant1 + cant2 + cant3 + m + rest2 + rest3 + 30 + 1].Style.Numberformat.Format = "0.00";
                            worksheet.Cells[p + 1, cant1 + cant2 + cant3 + m + rest2 + rest3 + 30 + 1].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                            worksheet.Cells[p + 1, cant1 + cant2 + cant3 + m + rest2 + rest3 + 30 + 1].Value = Total_Deducciones_Espejo;

                            //// Neto a Pagar Espejo

                            worksheet.Cells[p + 1, cant1 + cant2 + cant3 + m + rest2 + rest3 + 30 + 2].Style.Numberformat.Format = "0.00";
                            worksheet.Cells[p + 1, cant1 + cant2 + cant3 + m + rest2 + rest3 + 30 + 2].Style.Fill.SetBackground(System.Drawing.Color.LightSteelBlue);
                            worksheet.Cells[p + 1, cant1 + cant2 + cant3 + m + rest2 + rest3 + 30 + 2].Value = Total_Percepciones_Espejo - Total_Deducciones_Espejo;

                            worksheet.Cells[p + 1, cant1 + cant2 + cant3 + m + rest2 + rest3 + 30 + 3].Style.Numberformat.Format = "0.00";
                            worksheet.Cells[p + 1, cant1 + cant2 + cant3 + m + rest2 + rest3 + 30 + 3].Style.Fill.SetBackground(System.Drawing.Color.LightCyan);
                            worksheet.Cells[p + 1, cant1 + cant2 + cant3 + m + rest2 + rest3 + 30 + 3].Value = Total_Percepciones_Fiscal - Total_Deducciones_Fiscal + Total_Percepciones_Espejo - Total_Deducciones_Espejo;

                            p += 1;
                        }
                    }

                    //DataTable dataTable = new DataTable("DATOS_NOMINA");
                    //dataTable = reportDao.sp_Datos_Generales_HC_DataTable(keyOptionSel, typePeriod, numberPeriod, yearPeriod);
                    //DataSet dataSet = new DataSet();
                    
                    FileInfo excelFile = new FileInfo(pathComplete + fileName);
                    excel.SaveAs(excelFile);
                    stopwatch1.Stop();
                    string final = DateTime.Now.ToString("hh:mm:ss");
                    DateTime termino = Convert.ToDateTime(final);
                    tiempo = termino.Subtract(comienzo).TotalMinutes;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Archivo = fileName, Folder = nameFolderRe, Rows = rowsDataTable, Columns = columnsDataTable, Validacion = ValidDataN, Tiempo = tiempo });
        }

        [HttpPost]
        public JsonResult ReportPayroll(string typeOption, int keyOptionSel, int typePeriod, int numberPeriod, int yearPeriod, int refreshData)
        {
            Boolean flag         = false;
            Boolean ValidDataN   = false;
            Boolean checkDataN   = false;
            String  messageError = "none";
            string  pathSaveFile = Server.MapPath("~/Content/");
            string  nameFolder   = "REPORTES";
            string  nameFolderRe = "NOMINA";
            string  nameFilePrototype = "HCalculo_E" + keyOptionSel.ToString() + "_A" + yearPeriod.ToString() + "_NP" + numberPeriod.ToString() +  "_TP" + typePeriod.ToString() + "_A.xlsx" ;
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                int keyUser  = Convert.ToInt32(Session["iIdUsuario"].ToString());
                if (refreshData == 1) {
                    Boolean refresh = reportDao.sp_Refresca_Datos_Reporte_Nomina(typeOption, keyOptionSel, typePeriod, numberPeriod, yearPeriod, keyUser);
                }
                // VALIDAR QUE EXISTAN CALCULOS
                ValidDataN = reportDao.sp_Comprueba_Existe_Calculos_Nomina(typeOption, keyOptionSel, typePeriod, numberPeriod, yearPeriod);
                if (ValidDataN) {
                    // COMPROBAR DATOS PARA EL REPORTE
                    checkDataN = reportDao.sp_Consulta_Existe_Reporte_Nomina(typeOption, keyOptionSel, typePeriod, numberPeriod, yearPeriod, keyUser);
                    if (!checkDataN) {
                        // EJECUTAR CURSOR
                        Boolean resultCursor = false;
                        if (typeOption == "BUSINESS") {
                            // CURSOR POR EMPRESA
                            resultCursor = reportDao.sp_Cursor_Genera_Datos_Reporte_Nomina(typeOption, keyOptionSel, typePeriod, numberPeriod, yearPeriod, keyUser);
                        } else {
                            // CURSOR POR GRUPO DE EMPRESAS
                            resultCursor = reportDao.sp_Cursor_Genera_Datos_Reporte_Nomina_Grupo_Empresas(keyOptionSel, typePeriod, numberPeriod, yearPeriod, keyUser);
                        }
                    } 
                    Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFilePrototype);
                    if (createFolders) {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        DataTable dataTable = new DataTable("DATOS_NOMINA");
                        dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                        if (typeOption == "BUSINESS") {
                            dataTable = reportDao.sp_Datos_Reporte_Nomina(typeOption, keyOptionSel, typePeriod, numberPeriod, yearPeriod, keyUser);
                        } else {
                            dataTable = reportDao.sp_Datos_Reporte_Nomina_Grupo_Empresas(typeOption, keyOptionSel, typePeriod, numberPeriod, yearPeriod, keyUser);
                        }
                        using (ExcelPackage excel = new ExcelPackage()) {
                            excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFilePrototype));
                            columnsDataTable = dataTable.Columns.Count + 1;
                            rowsDataTable = dataTable.Rows.Count;
                            if (rowsDataTable > 0) {
                                var worksheet = excel.Workbook.Worksheets[Path.GetFileNameWithoutExtension(nameFilePrototype)];
                                for (var i = 1; i < columnsDataTable; i++) {
                                    //worksheet.Cells[1, i].Style.Numberformat.Format = "#,##0.00%";
                                    worksheet.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                    worksheet.Cells[1, i].Style.Font.Bold = true;
                                    worksheet.Cells[1, i].Style.WrapText  = true;
                                    worksheet.Cells[1, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                    worksheet.Cells[1, i].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                }
                                worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                                FileInfo excelFile = new FileInfo(pathComplete + nameFilePrototype);
                                excel.SaveAs(excelFile);
                            }
                            excel.Dispose();
                            flag = true;
                        }
                    }
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Archivo = nameFilePrototype, Folder = nameFolderRe, Rows = rowsDataTable, Columns = columnsDataTable, Validacion = ValidDataN });
        }


        [HttpPost]
        public JsonResult ReportEmployeesUp(string typeOption, int keyOptionSel, string dateS, string dateE)
        {
            Boolean flag          = false;
            String  messageError  = "none";
            string pathSaveFile   = Server.MapPath("~/Content/");
            string nameFolder     = "REPORTES";
            string nameFolderRe   = "ALTAS_EMPLEADOS";
            string nameFileRepr   = "ALTAS_EMPLEADOS.xlsx";
            ReportesDao reportDao = new ReportesDao(); 
            string pathComplete   = pathSaveFile + nameFolder +  @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable         = new DataTable();
                    dataTable.Locale            = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable                   = reportDao.sp_Datos_Reporte_Altas_Empleado_Fechas(typeOption, keyOptionSel, dateS, dateE);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable  = dataTable.Columns.Count + 1;
                        rowsDataTable     = dataTable.Rows.Count;
                        if (rowsDataTable > 0) {
                            var worksheet = excel.Workbook.Worksheets[Path.GetFileNameWithoutExtension(nameFileRepr)];
                            for (var i = 1; i < columnsDataTable; i++) {
                                worksheet.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                                worksheet.Cells[1, i].Style.WrapText = true;
                                worksheet.Cells[1, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                worksheet.Cells[1, i].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                            }
                            worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                            FileInfo excelFile = new FileInfo(pathComplete + nameFileRepr);
                            excel.SaveAs(excelFile);
                        }
                        excel.Dispose();
                        flag = true;
                    } 
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Archivo = nameFileRepr, Folder = nameFolderRe, Rows = rowsDataTable, Columns = columnsDataTable });
        }

        [HttpPost]
        public JsonResult ReportEmployeesDown(string typeOption, int keyOptionSel, string dateS, string dateE)
        {
            Boolean flag          = false;
            String  messageError  = "none";
            string pathSaveFile   = Server.MapPath("~/Content/"); 
            string nameFolder     = "REPORTES";
            string nameFolderRe   = "BAJAS_EMPLEADOS";
            string nameFileRepr   = "BAJA_FEC_F"+ dateS.Replace("-","") +"_F" + dateE.Replace("-","")+ ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete   = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable         = new DataTable();
                    dataTable.Locale            = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable                   = reportDao.sp_Datos_Reporte_Bajas_Empleados_Fechas(typeOption, keyOptionSel, dateS, dateE);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable    = dataTable.Rows.Count;
                        if (rowsDataTable > 0) {
                            var worksheet = excel.Workbook.Worksheets[Path.GetFileNameWithoutExtension(nameFileRepr)];
                            for (var i = 1; i < columnsDataTable; i++) {
                                worksheet.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                                worksheet.Cells[1, i].Style.WrapText = true;
                                worksheet.Cells[1, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                worksheet.Cells[1, i].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                            }
                            worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                            FileInfo excelFile = new FileInfo(pathComplete + nameFileRepr);
                            excel.SaveAs(excelFile);
                        }
                        excel.Dispose();
                        flag = true;
                    }
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Archivo = nameFileRepr, Folder = nameFolderRe, Rows = rowsDataTable, Columns = columnsDataTable });
        }

        [HttpPost]
        public JsonResult ReportEmployeesActiveWithSalary(string typeOption, int keyOptionSel, string dateActive)
        {
            Boolean flag = false;
            String  messageError  = "none";
            string pathSaveFile   = Server.MapPath("~/Content/");
            string nameFolder     = "REPORTES";
            string nameFolderRe   = "EMPLEADOS_ACTIVOS_CON_SUELDO";
            string nameFileRepr   = "CAT_EMP_AC_F" + dateActive.Replace("-","") + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete   = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable         = new DataTable();
                    dataTable.Locale            = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable                   = reportDao.sp_Datos_Reporte_Empleados_Activos_Con_Sueldo(typeOption, keyOptionSel, dateActive.Trim());
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable     = dataTable.Rows.Count;
                        if (rowsDataTable > 0) {
                            var worksheet = excel.Workbook.Worksheets[Path.GetFileNameWithoutExtension(nameFileRepr)];
                            for (var i = 1; i < columnsDataTable; i++) {
                                worksheet.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                                worksheet.Cells[1, i].Style.WrapText = true;
                                worksheet.Cells[1, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                worksheet.Cells[1, i].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                            }
                            worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                            FileInfo excelFile = new FileInfo(pathComplete + nameFileRepr);
                            excel.SaveAs(excelFile);
                        }
                        excel.Dispose();
                        flag = true;
                    }
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new {  Bandera = flag, MensajeError = messageError, Archivo = nameFileRepr, Folder = nameFolderRe, Rows = rowsDataTable, Columns = columnsDataTable });
        }

        [HttpPost]
        public JsonResult ReportEmployeesActiveWithoutSalary(string typeOption, int keyOptionSel, string dateActive)
        {
            Boolean flag = false;
            String  messageError  = "none";
            string pathSaveFile   = Server.MapPath("~/Content/");
            string nameFolder     = "REPORTES";
            string nameFolderRe   = "EMPLEADOS_ACTIVOS_SIN_SUELDO";
            string nameFileRepr   = "CATEMPACSSF" + dateActive.Replace("-", "") + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete   = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable         = new DataTable();
                    dataTable.Locale            = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Empleados_Activos_Sin_Sueldo(typeOption, keyOptionSel, dateActive.Trim());
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable    = dataTable.Rows.Count;
                        if (rowsDataTable > 0) {
                            var worksheet = excel.Workbook.Worksheets[Path.GetFileNameWithoutExtension(nameFileRepr)];
                            for (var i = 1; i < columnsDataTable; i++) {
                                worksheet.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                                worksheet.Cells[1, i].Style.WrapText = true;
                                worksheet.Cells[1, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                worksheet.Cells[1, i].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                            }
                            worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                            FileInfo excelFile = new FileInfo(pathComplete + nameFileRepr);
                            excel.SaveAs(excelFile);
                        }
                        excel.Dispose();
                        flag = true;
                    }
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Archivo = nameFileRepr, Folder = nameFolderRe, Rows = rowsDataTable, Columns = columnsDataTable });
        }

        [HttpPost]
        public JsonResult ReportBillsChecksDetailsTotals(string typeOption, int keyOptionSel, string typeReport, int yearSelect, int periodSelect, int typePSelect)
        {
            Boolean flag          = false;
            String  messageError  = "none";
            string pathSaveFile   = Server.MapPath("~/Content/");
            string nameFolder     = "REPORTES";
            string nameFolderRe   = (typeReport == "ABONO") ? "ABONO_DETALLE" : "ABONO_TOTAL";
            string nameFileValid  = (typeReport == "ABONO") ? "ABONO" : "ABOTOTAL";
            string nameFileRepr   = nameFileValid + "_E" + keyOptionSel.ToString() + "_A" + yearSelect.ToString() + "_P" + periodSelect.ToString() + "_T" + typePSelect.ToString() + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete   = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale    = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable           = (typeReport == "ABONO") ? reportDao.sp_Datos_Reporte_Cuenta_Cheques_Detalle(typeOption, keyOptionSel, yearSelect, periodSelect, typePSelect) : reportDao.sp_Datos_Reporte_Cuenta_Cheques_Totales(typeOption, keyOptionSel, yearSelect, periodSelect, typePSelect); 
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable    = dataTable.Rows.Count;
                        if (rowsDataTable > 0) {
                            var worksheet = excel.Workbook.Worksheets[Path.GetFileNameWithoutExtension(nameFileRepr)];
                            for (var i = 1; i < columnsDataTable; i++) {
                                worksheet.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                                worksheet.Cells[1, i].Style.WrapText = true;
                                worksheet.Cells[1, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                worksheet.Cells[1, i].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                            }
                            worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                            FileInfo excelFile = new FileInfo(pathComplete + nameFileRepr);
                            excel.SaveAs(excelFile);
                        }
                        excel.Dispose();
                        flag = true;
                    }
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Archivo = nameFileRepr, Folder = nameFolderRe, Rows = rowsDataTable, Columns = columnsDataTable });
        }

    }
}