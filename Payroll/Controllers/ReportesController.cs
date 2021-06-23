using System;
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

        /* REPORTE DE BAJAS */
        [HttpPost]
        public JsonResult ReportPayrollOrgBajas(string typeOption, int keyOptionSel, int typePeriod, int numberPeriod, int yearPeriod, int refreshData, int typeSend)
        {
            Stopwatch stopwatch = new Stopwatch();
            Stopwatch stopwatch1 = new Stopwatch();
            string tiempo = "";
            Boolean flag = false;
            Boolean ValidDataN  = false;
            Boolean checkDataN  = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "NOMINA";
            string fileName = "HCalculo_E" + keyOptionSel.ToString() + "_A" + yearPeriod.ToString() + "_NP" + numberPeriod.ToString() + "_TP" + typePeriod.ToString() + "_A_BAJAS.xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 0;
            int columnsDataTable = 0;
            int i, ii, Renglon, a, m, m1, m2, m3;
            int cant1 = 0;
            int cant2 = 0;
            int cant3 = 0;
            int cant4 = 0;
            int[] RenglonesNom = new int[10];
            int[] RenglonesNomDeduc = new int[10];
            int[] RenglonesNomEspejo = new int[10];
            int[] RenglonesNomDeducEspejo = new int[10];
            decimal Total_Percepciones_Fiscal;
            decimal Total_Deducciones_Fiscal;
            decimal Total_Percepciones_Espejo;
            decimal Total_Deducciones_Espejo;
            List<RenglonesHCBean> listRenglones     = new List<RenglonesHCBean>();
            List<RenglonesHCBean> listRenglones1    = new List<RenglonesHCBean>();
            List<RenglonesHCBean> listRenglonesEsp  = new List<RenglonesHCBean>();
            List<RenglonesHCBean> listRenglonesEsp1 = new List<RenglonesHCBean>();
            List<DatosGeneralesHC> datosGenerales   = new List<DatosGeneralesHC>();
            DetallesRenglon renglonNoEspejo = new DetallesRenglon();
            DetallesRenglon renglonSiEspejo = new DetallesRenglon();
            DetallesRenglon detallesRenglon = new DetallesRenglon();
            try {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelB = new ExcelPackage()) {
                    excelB.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(fileName));
                    var worksheetB = excelB.Workbook.Worksheets[Path.GetFileNameWithoutExtension(fileName)];

                    worksheetB.Cells[1, 1].Value = "AÑO";
                    worksheetB.Cells[1, 2].Value = "PERIODO";
                    worksheetB.Cells[1, 3].Value = "EMPRESA";
                    //worksheet.Cells[1, 4].Value = "NOMBRE EMPRESA";
                    //worksheet.Cells[1, 5].Value = "NOMINA";
                    //worksheet.Cells[1, 6].Value = "APELLIDO PATERNO";
                    //worksheet.Cells[1, 7].Value = "APELLIDO MATERNO";
                    //worksheet.Cells[1, 8].Value = "NOMBRE";
                    //worksheet.Cells[1, 9].Value = "REG.IMSS";
                    //worksheet.Cells[1, 10].Value = "RFC";
                    //worksheet.Cells[1, 11].Value = "CURP";
                    //worksheet.Cells[1, 12].Value = "PUESTO";
                    //worksheet.Cells[1, 13].Value = "NOMBRE PUESTO";
                    //worksheet.Cells[1, 14].Value = "NIVEL JERARQUICO";
                    //worksheet.Cells[1, 15].Value = "DEPTO.";
                    //worksheet.Cells[1, 16].Value = "NOMBRE DEPTO.";
                    //worksheet.Cells[1, 17].Value = "CENTRO COSTO";
                    //worksheet.Cells[1, 18].Value = "DESCR.CENTRO COSTO";
                    //worksheet.Cells[1, 19].Value = "REGIONAL";
                    //worksheet.Cells[1, 20].Value = "CVE.REGIONAL";
                    //worksheet.Cells[1, 21].Value = "DESCR.REGIONAL";
                    //worksheet.Cells[1, 22].Value = "SUCURSAL";
                    //worksheet.Cells[1, 23].Value = "CVE.SUCURSAL";
                    //worksheet.Cells[1, 24].Value = "DESCR.SUCURSAL";
                    //worksheet.Cells[1, 25].Value = "FECHA ANTIGUEDAD";
                    //worksheet.Cells[1, 26].Value = "FECHA INGRESO";
                    //worksheet.Cells[1, 27].Value = "SUELDO";
                    //worksheet.Cells[1, 28].Value = "VACANTE CUBIERTA";
                    //worksheet.Cells[1, 29].Value = "ULTIMA POSICION";
                    //worksheet.Cells[1, 30].Value = "ULT. SDI";

                    //for (i = 1; i < 31; i++) {
                    //    worksheet.Cells[1, i].Style.Fill.SetBackground(System.Drawing.Color.LightSkyBlue);
                    //}

                    //ii = 30;

                    //listRenglones = reportDao.sp_Renglones_Hoja_Calculo_BAJAS(keyOptionSel, typePeriod, numberPeriod, yearPeriod, 0, 0, 1000);
                    //if (listRenglones.Count > 0) {
                    //    RenglonesNom = new int[listRenglones.Count];
                    //    int r = 0;
                    //    foreach (RenglonesHCBean renglon in listRenglones) {
                    //        ii += 1;
                    //        Renglon = Convert.ToInt32(renglon.iIdRenglon);
                    //        RenglonesNom[r] = Convert.ToInt32(renglon.iIdRenglon);
                    //        worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightGreen);
                    //        worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                    //        r += 1;
                    //    }
                    //}

                    //a = ii;

                    FileInfo excelFile = new FileInfo(pathComplete + fileName);
                    excelB.SaveAs(excelFile);
                    stopwatch1.Stop();
                    string final = DateTime.Now.ToString("hh:mm:ss");
                    DateTime termino = Convert.ToDateTime(final);
                    tiempo = stopwatch1.Elapsed.Hours + " h " + stopwatch1.Elapsed.Minutes + " m " + stopwatch1.Elapsed.Seconds + " s ";
                    flag = true;

                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Archivo = fileName, Folder = nameFolderRe, Rows = datosGenerales.Count, Validacion = ValidDataN, Tiempo = tiempo });
        }

        /* GENERACION DE REPORTES */
        [HttpPost]
        public JsonResult ReportPayrollOrg(string typeOption, int keyOptionSel, int typePeriod, int numberPeriod, int yearPeriod, int refreshData, int typeSend)
        {
            Stopwatch stopwatch  = new Stopwatch();
            Stopwatch stopwatch1 = new Stopwatch();
            string tiempo       = "";
            Boolean flag        = false;
            Boolean ValidDataN  = false;
            Boolean checkDataN  = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder   = "REPORTES";
            string nameFolderRe = "NOMINA";
            string initName     = "";
            if (typeSend == 1) {
                initName = "HCalculo_E";
            } else {
                initName = "HCBajas_E";
            }
            string fileName = initName + keyOptionSel.ToString() + "_A" + yearPeriod.ToString() + "_NP" + numberPeriod.ToString() + "_TP" + typePeriod.ToString() + "_A.xlsx";
            Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, fileName);
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
            DetallesRenglon renglonNoEspejo = new DetallesRenglon();
            DetallesRenglon renglonSiEspejo = new DetallesRenglon();
            DetallesRenglon detallesRenglon = new DetallesRenglon();
            // * Inicio Codigo Nuevo * \\
            List<RenglonesHCBean> listRenglonesC1 = new List<RenglonesHCBean>();
            List<RenglonesHCBean> listRenglonesC2 = new List<RenglonesHCBean>();
            int[] renglonesC1 = new int[10];
            int[] renglonesC2 = new int[10];
            // * Fin Codigo Nuevo * \\
            try {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excel = new ExcelPackage()) {
                    excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(fileName));
                    var worksheet = excel.Workbook.Worksheets[Path.GetFileNameWithoutExtension(fileName)];
                    
                    worksheet.Cells[1, 1].Value  = "AÑO";
                    worksheet.Cells[1, 2].Value  = "PERIODO";
                    worksheet.Cells[1, 3].Value  = "EMPRESA";
                    worksheet.Cells[1, 4].Value  = "NOMBRE EMPRESA";
                    worksheet.Cells[1, 5].Value  = "NOMINA";
                    worksheet.Cells[1, 6].Value  = "APELLIDO PATERNO";
                    worksheet.Cells[1, 7].Value  = "APELLIDO MATERNO";
                    worksheet.Cells[1, 8].Value  = "NOMBRE";
                    worksheet.Cells[1, 9].Value  = "REG.IMSS";
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
                    worksheet.Cells[1, 31].Value = "Registro Patronal";

                    for (i = 1; i < 32; i++) {
                        worksheet.Cells[1, i].Style.Fill.SetBackground(System.Drawing.Color.LightSkyBlue);
                    }

                    ii = 31;

                    if (typeSend == 1) {
                        // Obtenemos los renglones calculados
                        listRenglones = reportDao.sp_Renglones_Hoja_Calculo(keyOptionSel, typePeriod, numberPeriod, yearPeriod, 0, 0, 1000, typeOption);
                        if (listRenglones.Count > 0)
                        {
                            RenglonesNom = new int[listRenglones.Count];
                            int r = 0;
                            foreach (RenglonesHCBean renglon in listRenglones)
                            {
                                ii += 1;
                                Renglon = Convert.ToInt32(renglon.iIdRenglon);
                                RenglonesNom[r] = Convert.ToInt32(renglon.iIdRenglon);
                                worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightGreen);
                                worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                                r += 1;
                            }
                        } else {
                            RenglonesNom = new int[0];
                        }

                        a = ii;

                        listRenglones1 = reportDao.sp_Renglones_Hoja_Calculo(keyOptionSel, typePeriod, numberPeriod, yearPeriod, 0, 1000, 2000, typeOption);
                        if (listRenglones1.Count > 0) {
                            
                            RenglonesNomDeduc = new int[listRenglones1.Count];
                            int j = 0;
                            foreach (RenglonesHCBean renglon in listRenglones1) {
                                ii += 1;
                                Renglon = Convert.ToInt32(renglon.iIdRenglon);
                                RenglonesNomDeduc[j] = Renglon;
                                worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                                worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                                j += 1;
                            }
                        } else {
                            RenglonesNomDeduc = new int[0];
                        }

                        ii += 1;
                        worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightSteelBlue);
                        worksheet.Cells[1, ii + 1].Value = "NETO A PAGAR FISCAL";

                        listRenglonesEsp = reportDao.sp_Renglones_Hoja_Calculo(keyOptionSel, typePeriod, numberPeriod, yearPeriod, 1, 0, 1000, typeOption);
                        if (listRenglonesEsp.Count > 0)
                        {
                            RenglonesNomEspejo = new int[listRenglonesEsp.Count];
                            int g = 0;
                            foreach (RenglonesHCBean renglon in listRenglonesEsp)
                            {
                                ii += 1;
                                Renglon = Convert.ToInt32(renglon.iIdRenglon);
                                RenglonesNomEspejo[g] = Renglon;
                                worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightGreen);
                                worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                                g += 1;
                            }
                        }
                        else
                        {
                            RenglonesNomEspejo = new int[0];
                        }

                        listRenglonesEsp1 = reportDao.sp_Renglones_Hoja_Calculo(keyOptionSel, typePeriod, numberPeriod, yearPeriod, 1, 1000, 2000, typeOption);
                        if (listRenglonesEsp1.Count > 0)
                        {
                            RenglonesNomDeducEspejo = new int[listRenglonesEsp1.Count];
                            int y = 0;
                            foreach (RenglonesHCBean renglon in listRenglonesEsp1)
                            {
                                ii += 1;
                                Renglon = Convert.ToInt32(renglon.iIdRenglon);
                                RenglonesNomDeducEspejo[y] = Renglon;
                                worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                                worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                                y += 1;
                            }
                        }
                        else
                        {
                            RenglonesNomDeducEspejo = new int[0];
                        }

                        ii += 1;
                        worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                        worksheet.Cells[1, ii + 1].Value = "NETO A PAGAR GASTO";

                        ii += 1;
                        worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.Aquamarine);
                        worksheet.Cells[1, ii + 1].Value = "TOTAL GENERAL";


                    } else {

                        listRenglones = reportDao.sp_Renglones_Hoja_Calculo_BAJAS(keyOptionSel, typePeriod, numberPeriod, yearPeriod, 0, 0, 1000);
                        if (listRenglones.Count > 0) {
                            RenglonesNom = new int[listRenglones.Count];
                            int r = 0;
                            foreach (RenglonesHCBean renglon in listRenglones) {
                                ii += 1;
                                Renglon = Convert.ToInt32(renglon.iIdRenglon);
                                RenglonesNom[r] = Convert.ToInt32(renglon.iIdRenglon);
                                worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightGreen);
                                worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                                r += 1;
                            }
                        } else {
                            RenglonesNom = new int[0];
                        }

                        a = ii;

                        listRenglones1 = reportDao.sp_Renglones_Hoja_Calculo_BAJAS(keyOptionSel, typePeriod, numberPeriod, yearPeriod, 0, 1000, 2000);
                        if (listRenglones1.Count > 0) {
                            RenglonesNomDeduc = new int[listRenglones1.Count];
                            int j = 0;
                            foreach (RenglonesHCBean renglon in listRenglones1) {
                                ii += 1;
                                Renglon = Convert.ToInt32(renglon.iIdRenglon);
                                RenglonesNomDeduc[j] = Renglon;
                                worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                                worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                                j += 1;
                            }
                        } else {
                            RenglonesNomDeduc = new int[0];
                        }

                        ii += 1;
                        worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightSteelBlue);
                        worksheet.Cells[1, ii + 1].Value = "NETO A PAGAR FISCAL";

                        // ------------------------------------ \\ 

                        listRenglonesEsp = reportDao.sp_Renglones_Hoja_Calculo_BAJAS(keyOptionSel, typePeriod, numberPeriod, yearPeriod, 1, 0, 1000);
                        if (listRenglonesEsp.Count > 0) {
                            RenglonesNomEspejo = new int[listRenglonesEsp.Count];
                            int g = 0;
                            foreach (RenglonesHCBean renglon in listRenglonesEsp)
                            {
                                ii += 1;
                                Renglon = Convert.ToInt32(renglon.iIdRenglon);
                                RenglonesNomEspejo[g] = Renglon;
                                worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightGreen);
                                worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                                g += 1;
                            }
                        } else {
                            RenglonesNomEspejo = new int[0];
                        }

                        listRenglonesEsp1 = reportDao.sp_Renglones_Hoja_Calculo_BAJAS(keyOptionSel, typePeriod, numberPeriod, yearPeriod, 1, 1000, 2000);
                        if (listRenglonesEsp1.Count > 0) {
                            RenglonesNomDeducEspejo = new int[listRenglonesEsp1.Count];
                            int y = 0;
                            foreach (RenglonesHCBean renglon in listRenglonesEsp1)
                            {
                                ii += 1;
                                Renglon = Convert.ToInt32(renglon.iIdRenglon);
                                RenglonesNomDeducEspejo[y] = Renglon;
                                worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                                worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                                y += 1;
                            }
                        } else {
                            RenglonesNomDeducEspejo = new int[0];
                        }

                        ii += 1;
                        worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                        worksheet.Cells[1, ii + 1].Value = "NETO A PAGAR GASTO";

                    }

                    stopwatch1.Start();
                    string inicio = DateTime.Now.ToString("hh:mm:ss");
                    DateTime comienzo = Convert.ToDateTime(inicio);
                    if (typeSend == 1) {
                        datosGenerales = reportDao.sp_Datos_Generales_HC(keyOptionSel, typePeriod, numberPeriod, yearPeriod, typeSend, typeOption);
                        ValidDataN = (datosGenerales.Count > 0) ? true : false;
                        if (datosGenerales.Count > 0) {
                            int business = 0, payroll = 0;
                            int p = 1;
                            foreach (DatosGeneralesHC dato in datosGenerales) {

                                business = dato.iEmpresa;
                                payroll = dato.iNomina;
                                 
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
                                worksheet.Cells[p + 1, 31].Value = dato.sRegistroPatronal;
                                Total_Percepciones_Fiscal = 0;
                                double renglon481 = 0;
                                decimal importe = 0;

                                if (dato.iGrupoEmpresaId == 9 || dato.iGrupoEmpresaId == 12 || dato.iGrupoEmpresaId == 15) {
                                    for (m = 0; m < RenglonesNom.Count(); m++) {
                                        detallesRenglon = reportDao.sp_Detalle_Renglones(business, payroll, numberPeriod, typePeriod, yearPeriod, RenglonesNom[m], 0);
                                        if (detallesRenglon.dSaldo <= 0) {
                                            worksheet.Cells[p + 1, m + 2 + 31].Value = "";
                                        } else {
                                            if (RenglonesNom[m] == 481) {
                                                renglon481 = Convert.ToDouble(detallesRenglon.dSaldo);
                                            }
                                            importe = detallesRenglon.dSaldo;
                                            if (RenglonesNom[m] == 990) {
                                                importe = detallesRenglon.dSaldo - Convert.ToDecimal(renglon481);
                                            }
                                            worksheet.Cells[p + 1, m + 2 + 31].Style.Numberformat.Format = "0.00";
                                            Total_Percepciones_Fiscal += Convert.ToDecimal(importe);
                                            worksheet.Cells[p + 1, m + 2 + 31].Value = Convert.ToDecimal(importe);
                                        }
                                    }
                                } else {
                                    for (m = 0; m < RenglonesNom.Count(); m++) {
                                        detallesRenglon = reportDao.sp_Detalle_Renglones(business, payroll, numberPeriod, typePeriod, yearPeriod, RenglonesNom[m], 0);
                                        if (detallesRenglon.dSaldo <= 0) {
                                            worksheet.Cells[p + 1, m + 2 + 31].Value = "";
                                        } else {
                                            worksheet.Cells[p + 1, m + 2 + 31].Style.Numberformat.Format = "0.00";
                                            Total_Percepciones_Fiscal += Convert.ToDecimal(detallesRenglon.dSaldo);
                                            worksheet.Cells[p + 1, m + 2 + 31].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                        }
                                    }
                                }

                                cant1 = RenglonesNom.Length;
                                cant2 = cant1 + RenglonesNomDeduc.Length;
                                cant3 = cant2 + RenglonesNomEspejo.Length; 

                                Total_Deducciones_Fiscal = 0;

                                for (m = 0; m < RenglonesNomDeduc.Count(); m++)
                                {
                                    detallesRenglon = reportDao.sp_Detalle_Renglones(business, payroll, numberPeriod, typePeriod, yearPeriod, RenglonesNomDeduc[m], 0);
                                    if (detallesRenglon.dSaldo <= 0)
                                    {
                                        worksheet.Cells[p + 1, cant1 + m + 2 + 31].Value = "";
                                    }
                                    else
                                    {
                                        worksheet.Cells[p + 1, cant1 + m + 2 + 31].Style.Numberformat.Format = "0.00";
                                        Total_Deducciones_Fiscal += Convert.ToDecimal(detallesRenglon.dSaldo);
                                        worksheet.Cells[p + 1, cant1 + m + 2 + 31].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                    }
                                }


                                ////// Monto neto a pagar (FISCAL)
                                renglonNoEspejo = reportDao.sp_Liquidos_Espejo_No_Espejo(business, typePeriod, numberPeriod, yearPeriod, 0, payroll);
                                if (dato.iGrupoEmpresaId == 9 || dato.iGrupoEmpresaId == 12 || dato.iGrupoEmpresaId == 15) {
                                    decimal total = renglonNoEspejo.dSaldo - Convert.ToDecimal(renglon481);
                                    worksheet.Cells[p + 1, cant1 + m + 2 + 31].Style.Numberformat.Format = "0.00";
                                    worksheet.Cells[p + 1, cant1 + m + 2 + 31].Style.Fill.SetBackground(System.Drawing.Color.LightSteelBlue);
                                    worksheet.Cells[p + 1, cant1 + m + 2 + 31].Value = total;
                                } else {
                                    worksheet.Cells[p + 1, cant1 + m + 2 + 31].Style.Numberformat.Format = "0.00";
                                    worksheet.Cells[p + 1, cant1 + m + 2 + 31].Style.Fill.SetBackground(System.Drawing.Color.LightSteelBlue);
                                    worksheet.Cells[p + 1, cant1 + m + 2 + 31].Value = renglonNoEspejo.dSaldo;
                                }

                                Total_Percepciones_Espejo = 0;
                                for (m = 0; m < RenglonesNomEspejo.Count(); m++)
                                {
                                    detallesRenglon = reportDao.sp_Detalle_Renglones(business, payroll, numberPeriod, typePeriod, yearPeriod, RenglonesNomEspejo[m], 1);
                                    if (detallesRenglon.dSaldo <= 0)
                                    {
                                        worksheet.Cells[p + 1, cant2 + m + 3 + 31].Value = "";
                                    }
                                    else
                                    {
                                        worksheet.Cells[p + 1, cant2 + m + 3 + 31].Style.Numberformat.Format = "0.00";
                                        Total_Percepciones_Espejo += Convert.ToDecimal(detallesRenglon.dSaldo);
                                        worksheet.Cells[p + 1, cant2 + m + 3 + 31].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                    }
                                }

                                Total_Deducciones_Espejo = 0;

                                for (m = 0; m < RenglonesNomDeducEspejo.Count(); m++)
                                {
                                    detallesRenglon = reportDao.sp_Detalle_Renglones(business, payroll, numberPeriod, typePeriod, yearPeriod, RenglonesNomDeducEspejo[m], 1);
                                    if (detallesRenglon.dSaldo <= 0)
                                    {
                                        worksheet.Cells[p + 1, cant3 + m + 3 + 31].Value = "";
                                    }
                                    else
                                    {
                                        worksheet.Cells[p + 1, cant3 + m + 3 + 31].Style.Numberformat.Format = "0.00";
                                        Total_Deducciones_Espejo += Convert.ToDecimal(detallesRenglon.dSaldo);
                                        worksheet.Cells[p + 1, cant3 + m + 3 + 31].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                    }
                                }

                                renglonSiEspejo = reportDao.sp_Liquidos_Espejo_No_Espejo(business, typePeriod, numberPeriod, yearPeriod, 1, payroll);
                                worksheet.Cells[p + 1, cant3 + m + 3 + 31].Style.Numberformat.Format = "0.00";
                                worksheet.Cells[p + 1, cant3 + m + 3 + 31].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                                worksheet.Cells[p + 1, cant3 + m + 3 + 31].Value = renglonSiEspejo.dSaldo;

                                worksheet.Cells[p + 1, cant3 + m + 4 + 31].Style.Numberformat.Format = "0.00";
                                worksheet.Cells[p + 1, cant3 + m + 4 + 31].Style.Fill.SetBackground(System.Drawing.Color.Aquamarine);
                                if (dato.iGrupoEmpresaId == 9 || dato.iGrupoEmpresaId == 12 || dato.iGrupoEmpresaId == 15) {
                                    decimal resultadoF = renglonNoEspejo.dSaldo - Convert.ToDecimal(renglon481);
                                    worksheet.Cells[p + 1, cant3 + m + 4 + 31].Value = resultadoF + renglonSiEspejo.dSaldo;
                                } else {
                                    worksheet.Cells[p + 1, cant3 + m + 4 + 31].Value = renglonNoEspejo.dSaldo + renglonSiEspejo.dSaldo;
                                }

                                p += 1;
                            }
                        }
                    } else {
                        datosGenerales = reportDao.sp_Datos_Generales_HC_BAJAS(keyOptionSel, typePeriod, numberPeriod, yearPeriod, typeSend, typeOption);
                        ValidDataN = (datosGenerales.Count > 0) ? true : false;
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
                                    detallesRenglon = reportDao.sp_Detalle_Renglones_BAJAS(business, payroll, numberPeriod, typePeriod, yearPeriod, RenglonesNom[m], 0);
                                    if (detallesRenglon.dSaldo < 0) {
                                        worksheet.Cells[p + 1, m + 2 + 30].Value = "";
                                    } else {
                                        worksheet.Cells[p + 1, m + 2 + 30].Style.Numberformat.Format = "0.00";
                                        Total_Percepciones_Fiscal += Convert.ToDecimal(detallesRenglon.dSaldo);
                                        worksheet.Cells[p + 1, m + 2 + 30].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                    }
                                }

                                cant1 = RenglonesNom.Length;
                                cant2 = cant1 + RenglonesNomDeduc.Length;
                                cant3 = cant2 + RenglonesNomEspejo.Length;

                                Total_Deducciones_Fiscal = 0;

                                for (m = 0; m < RenglonesNomDeduc.Count(); m++) {
                                    detallesRenglon = reportDao.sp_Detalle_Renglones_BAJAS(business, payroll, numberPeriod, typePeriod, yearPeriod, RenglonesNomDeduc[m], 0);
                                    if (detallesRenglon.dSaldo < 0) {
                                        worksheet.Cells[p + 1, cant1 + m + 2 + 30].Value = "";
                                    } else {
                                        worksheet.Cells[p + 1, cant1 + m + 2 + 30].Style.Numberformat.Format = "0.00";
                                        Total_Deducciones_Fiscal += Convert.ToDecimal(detallesRenglon.dSaldo);
                                        worksheet.Cells[p + 1, cant1 + m + 2 + 30].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                    }
                                }

                                renglonNoEspejo = reportDao.sp_Liquidos_Espejo_No_Espejo_BAJAS(business, typePeriod, numberPeriod, yearPeriod, 0, payroll);
                                worksheet.Cells[p + 1, cant1 + m + 2 + 30].Style.Numberformat.Format = "0.00";
                                worksheet.Cells[p + 1, cant1 + m + 2 + 30].Style.Fill.SetBackground(System.Drawing.Color.LightSteelBlue);
                                worksheet.Cells[p + 1, cant1 + m + 2 + 30].Value = renglonNoEspejo.dSaldo;

                                Total_Percepciones_Espejo = 0;
                                for (m = 0; m < RenglonesNomEspejo.Count(); m++) {
                                    detallesRenglon = reportDao.sp_Detalle_Renglones_BAJAS(business, payroll, numberPeriod, typePeriod, yearPeriod, RenglonesNomEspejo[m], 1);
                                    if (detallesRenglon.dSaldo < 0) {
                                        worksheet.Cells[p + 1, cant2 + m + 3 + 30].Value = "";
                                    } else {
                                        worksheet.Cells[p + 1, cant2 + m + 3 + 30].Style.Numberformat.Format = "0.00";
                                        Total_Percepciones_Espejo += Convert.ToDecimal(detallesRenglon.dSaldo);
                                        worksheet.Cells[p + 1, cant2 + m + 3 + 30].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                    }
                                }

                                Total_Deducciones_Espejo = 0;

                                for (m = 0; m < RenglonesNomDeducEspejo.Count(); m++) {
                                    detallesRenglon = reportDao.sp_Detalle_Renglones_BAJAS(business, payroll, numberPeriod, typePeriod, yearPeriod, RenglonesNomDeducEspejo[m], 1);
                                    if (detallesRenglon.dSaldo < 0) {
                                        worksheet.Cells[p + 1, cant3 + m + 3 + 30].Value = "";
                                    } else {
                                        worksheet.Cells[p + 1, cant3 + m + 3 + 30].Style.Numberformat.Format = "0.00";
                                        Total_Deducciones_Espejo += Convert.ToDecimal(detallesRenglon.dSaldo);
                                        worksheet.Cells[p + 1, cant3 + m + 3 + 30].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                    }
                                }

                                renglonSiEspejo = reportDao.sp_Liquidos_Espejo_No_Espejo_BAJAS(business, typePeriod, numberPeriod, yearPeriod, 1, payroll);
                                worksheet.Cells[p + 1, cant3 + m + 3 + 30].Style.Numberformat.Format = "0.00";
                                worksheet.Cells[p + 1, cant3 + m + 3 + 30].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                                worksheet.Cells[p + 1, cant3 + m + 3 + 30].Value = Total_Deducciones_Espejo;

                                p += 1;
                            }
                        }
                    }
                    
                    FileInfo excelFile = new FileInfo(pathComplete + fileName);
                    excel.SaveAs(excelFile);
                    stopwatch1.Stop();
                    string final = DateTime.Now.ToString("hh:mm:ss");
                    DateTime termino = Convert.ToDateTime(final);
                    tiempo = stopwatch1.Elapsed.Hours + " h " + stopwatch1.Elapsed.Minutes + " m " + stopwatch1.Elapsed.Seconds + " s ";
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Archivo = fileName, Folder = nameFolderRe, Rows = datosGenerales.Count, Validacion = ValidDataN, Tiempo = tiempo });
        }

        [HttpPost]
        public JsonResult ReportPayroll(string typeOption, int keyOptionSel, int typePeriod, int numberPeriod, int yearPeriod, int refreshData, int typeSend)
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
        public JsonResult ReportGeneralEmployees(string typeOption, int keyOptionSel)
        {

            Boolean flag        = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder   = "REPORTES";
            string nameFolderRe = "GENERAL";
            string extensionFle = (typeOption != "BUSINESS") ? ".csv" : ".xlsx";
            string nameFileRepr = "GENERAL" + typeOption + extensionFle;
            ReportesDao reportDao = new ReportesDao();
            string pathComplete   = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable     = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable         = new DataTable();
                    dataTable.Locale            = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable        = reportDao.sp_Datos_Generales_Empleados(typeOption, keyOptionSel);
                    columnsDataTable = dataTable.Columns.Count + 1;
                    rowsDataTable    = dataTable.Rows.Count;
                    if (rowsDataTable > 0) {
                        using (ExcelPackage excel = new ExcelPackage()) {
                            excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
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
                                worksheet.Cells[1, i].Style.WrapText  = true;
                                worksheet.Cells[1, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                worksheet.Cells[1, i].Style.VerticalAlignment   = ExcelVerticalAlignment.Top;
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
        public JsonResult ReportMovements(string typeOption, int keyOptionSel, int yearSelect, int periodSelect, int typePSelect)
        {
            Boolean flag = false;
            String messageError  = "none";
            string pathSaveFile  = Server.MapPath("~/Content/");
            string nameFolder    = "REPORTES";
            string nameFolderRe  = "MOVIMIENTOS";
            string nameFileValid = nameFolderRe;
            string nameFileRepr  = nameFileValid + "_E" + keyOptionSel.ToString() + "_A" + yearSelect.ToString() + "_P" + periodSelect.ToString() + "_T" + typePSelect.ToString() + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable         = new DataTable();
                    dataTable.Locale            = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Movimientos_Empleados(typeOption, keyOptionSel, yearSelect, periodSelect, typePSelect);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
                        if (rowsDataTable > 0)
                        {
                            var worksheet = excel.Workbook.Worksheets[Path.GetFileNameWithoutExtension(nameFileRepr)];
                            for (var i = 1; i < columnsDataTable; i++)
                            {
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
        public JsonResult SearchOptionsNPHC(int year, int key, int type, int option, int period)
        {
            Boolean flag         = false;
            String  messageError = "none";
            List<PeriodoBean> periodos         = new List<PeriodoBean>();
            List<TipoPeriodoBean> tipoPeriodos = new List<TipoPeriodoBean>();
            ReportesDao reportesDao            = new ReportesDao();                                                                                                                     
            try { 
                if (option == 1) {
                    periodos     = reportesDao.sp_Available_Periods_Business(year, key, type);
                    if (periodos.Count > 0) {
                        flag = true;
                    }
                    return Json(new { Bandera = flag, MensajeError = messageError, Datos = periodos});
                } else {
                    tipoPeriodos = reportesDao.sp_Available_Type_Periods_Business(year, key, type, period);
                    if (tipoPeriodos.Count > 0) {
                        flag = true;
                    }
                    return Json(new { Bandera = flag, MensajeError = messageError, Datos = tipoPeriodos });
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Periodos = periodos, Tipos = tipoPeriodos });
        }

        [HttpPost]
        public JsonResult GenerateReportCatalogs (string key)
        {
            Boolean flag = false;
            String messageError  = "none";
            string pathSaveFile  = Server.MapPath("~/Content/");
            string nameFolder    = "REPORTES";
            string nameFolderRe  = "CATALOGOS";
            string nameFileValid = nameFolderRe;
            int keyBusiness = Convert.ToInt32(Session["IdEmpresa"]);
            string nameFileRepr  = key.Trim() + keyBusiness.ToString() + ".xlsx";
            ReportesCatalogos reportDao = new ReportesCatalogos();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    if (key == "POSICIONES") {
                        dataTable = reportDao.sp_Reporte_Posiciones(keyBusiness);
                    } else if (key == "PUESTOS") {
                        dataTable = reportDao.sp_Reporte_Puestos(keyBusiness);
                    } else if (key == "DEPARTAMENTOS") {
                        dataTable = reportDao.sp_Reporte_Departamentos(keyBusiness);
                    } else if (key == "LOCALIDADES") {
                        dataTable = reportDao.sp_Reporte_Localidades(keyBusiness);
                    } else if (key == "CENTROSCOSTO") {
                        dataTable = reportDao.sp_Reporte_CentrosCosto(keyBusiness);
                    }
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Archivo = nameFileRepr, Folder = nameFolderRe, Rows = rowsDataTable, Columns = columnsDataTable });
        }

        [HttpPost]
        public JsonResult DeleteReportCatalogs(string file, string folder)
        {
            Boolean flag = false;
            String messageError = "none";
            try {
                string path = Server.MapPath("~/Content/");
                if (System.IO.File.Exists(path + "REPORTES" + "//" + folder + "//" + file)) {
                    System.IO.File.Delete(path + "REPORTES" + "//" + folder + "//" + file);
                }
                if (!System.IO.File.Exists(path + "REPORTES" + "//" + folder + "//" + file)) {
                    flag = true;
                }
            } catch (Exception exc) {
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        [HttpPost]
        public JsonResult ReportAccumulatedForPeriodAndPayroll(int year, string periods, int payroll, int typePeriod, int business)
        {
            Boolean flag         = false;
            String messageError  = "none";
            string pathSaveFile  = Server.MapPath("~/Content/");
            string nameFolder    = "REPORTES";
            string nameFolderRe  = "ACUMULADOSPERIODOSNOMINA";
            string nameFileValid = nameFolderRe;
            string nameFileRepr  = "ACUM" + "_E" + business.ToString() + "_A" + year.ToString() + "_P" + periods.Replace(',','_').ToString() + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Acumulados_Por_Periodo_Y_Nomina(year, periods, payroll, typePeriod, business);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult ReportAccumulatedCrusaders(int year, int periodStart, int periodEnd, int typePeriod, string option, int keyOption)
        {
            Boolean flag = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "ACUMULADOSPERIODOSNOMINA";
            string nameFileValid = nameFolderRe;
            string nameFileRepr = "ACUMMATRIXPI" + periodStart.ToString() + "PF" + periodEnd.ToString() + option.ToString() + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Acumulados_Matrix(year, periodStart, periodEnd, typePeriod, option, keyOption);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult ReportReceiptPayroll(int year, int periodStart, int periodEnd, int typePeriod, string option, int keyOption)
        {
            Boolean flag = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "RECIBOSNOMINA";
            string nameFileValid = nameFolderRe;
            string nameFileRepr = "RECINOMIA" + year.ToString() + "T" + periodStart.ToString() + "P" + periodEnd.ToString() + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Recibos_Nomina(year, periodStart, periodEnd, typePeriod, option, keyOption);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult CatalogsGeneral(string report)
        {
            Boolean flag         = false;
            String  messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "CATALOGOSGENERALES";
            string nameFileValid = nameFolderRe;
            int keyBusiness = Convert.ToInt32(Session["IdEmpresa"]);
            string nameFileRepr = report + ".csv";
            ReportesCatalogos reportDao = new ReportesCatalogos();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    if (report != "") {
                        dataTable = reportDao.sp_Reporte_Catalogos_General(report);
                    }
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult GenerateReportEstructure(string option, int keyOption, string date)
        {
            Boolean flag = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "ESTRUCTURA";
            string nameFileValid = nameFolderRe;
            string nameFileRepr = "ESTRUCTURAE_" + keyOption.ToString() + "F_" + date.ToString().Replace("-","") + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Estructura(date, option, keyOption);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult GenerateReportEmployeesTypePaidCash(string option, int keyOption)
        {
            Boolean flag = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "ESTRUCTURA";
            string nameFileValid = nameFolderRe;
            string nameFileRepr = "REPORTEEMPLEADOSPAGO1E" + keyOption.ToString() + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Empleados_Pago_Efectivo(option, keyOption);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult GenerateReportAdeudos(string option, int keyOption)
        {
            Boolean flag = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "ADEUDOS";
            string nameFileValid = nameFolderRe;
            string nameFileRepr = "REPORTEADEUDOSE" + keyOption.ToString() + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Adeudos(option, keyOption);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult GenerateReportHolidaysEmployees(string option, int keyOption)
        {
            Boolean flag = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "VACACIONES";
            string nameFileValid = nameFolderRe;
            string nameFileRepr = "REPORTEVACACIONESE" + keyOption.ToString() + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Vacaciones(option, keyOption);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult GenerateReportCreditsInfonavitHistory(string option, int keyOption)
        {
            Boolean flag = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "CREDITOS";
            string nameFileValid = nameFolderRe;
            string nameFileRepr = "CREDITOSINFONAVITHISTORICOE" + keyOption.ToString() + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Creditos_Infonavit_Historico(option, keyOption);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult GenerateReportCreditsInfonavitAssets(string option, int keyOption)
        {
            Boolean flag = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "CREDITOS";
            string nameFileValid = nameFolderRe;
            string nameFileRepr = "CREDITOSINFONAVITACTIVOSE" + keyOption.ToString() + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Creditos_Infonavit_Activos(option, keyOption);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult GenerateReportIncapacidadesTCR (string option, int keyOption)
        {
            Boolean flag = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "INCAPACIDADESTCR";
            string nameFileValid = nameFolderRe;
            string nameFileRepr = "INCAPACIDADES" + keyOption.ToString() + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Incapacidades_TCR(option, keyOption);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult GenerateReportPensionsFood(string option, int keyOption)
        {
            Boolean flag = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "PENSIONES";
            string nameFileValid = nameFolderRe;
            string nameFileRepr = "PENSIONESALIMENTICASE" + keyOption.ToString() + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Pensiones_Alimenticias(option, keyOption);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult GenerateReportLowCreditInfonavit(string dateStart, string dateEnd, string option, int keyOption)
        {
            Boolean flag = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "BAJASCREDITOSINFONAVIT";
            string nameFileValid = nameFolderRe;
            string nameFileRepr = "BAJAINFO_F" + dateStart.ToString().Replace("-","") + "_F" + dateEnd.ToString().Replace("-", "") + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Bajas_Credito_Infonavit(dateStart, dateEnd, option, keyOption);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult GenerateReportPayRiseDates(string dateStart, string dateEnd, string option, int keyOption)
        {
            Boolean flag = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "AUMENTOSSUELDO";
            string nameFileValid = nameFolderRe;
            string nameFileRepr = "AUMENFEC_E" + keyOption.ToString() + "_F" + dateStart.ToString().Replace("-", "") + "_F" + dateEnd.ToString().Replace("-", "") + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Aumentos_Sueldos(dateStart, dateEnd, option, keyOption);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult GenerateReportDetailOfPayrollLinesYearActually(string periodStart, string periodEnd, string lines, int typePeriod, string option, int keyOption)
        {
            Boolean flag = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "DETALLERENGLONES";
            string nameFileValid = nameFolderRe;
            string nameFileRepr = "RECIRENGA_" + DateTime.Now.Year.ToString() + "_P_" + periodStart.ToString().Replace("-", "") + "_P_  " + periodEnd.ToString().Replace("-", "") + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Detalle_Renglones_Nomina(periodStart, periodEnd, lines, typePeriod, option, keyOption);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult GenerateReportAcum(int year, int periodStart, int periodEnd, int typePeriod, string option, int keyOption)
        {
            Stopwatch stopwatch = new Stopwatch();
            Stopwatch stopwatch1 = new Stopwatch();
            string tiempo = "";
            Boolean flag = false;
            Boolean ValidDataN = false;
            Boolean checkDataN = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "ACUMULADOSSICOSS";
            string initName = "";
            string fileName = "ACUMULADOSE"+ keyOption.ToString() +"A" + year.ToString() + "PI" + periodStart.ToString() + "PF" + periodEnd.ToString() + ".xlsx";
            Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, fileName);
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 0;
            int columnsDataTable = 0;
            int i, ii, Renglon, a, m, m1, m2, m3;
            int cant1 = 0;
            int cant2 = 0;
            int cant3 = 0;
            int cant4 = 0;
            int[] RenglonesNom = new int[10];
            int[] RenglonesNomDeduc = new int[10];
            int[] RenglonesNomEspejo = new int[10];
            int[] RenglonesNomDeducEspejo = new int[10];
            decimal Total_Percepciones_Fiscal;
            decimal Total_Deducciones_Fiscal;
            decimal Total_Percepciones_Espejo;
            decimal Total_Deducciones_Espejo;
            List<RenglonesHCBean> listRenglones = new List<RenglonesHCBean>();
            List<RenglonesHCBean> listRenglones1 = new List<RenglonesHCBean>();
            List<RenglonesHCBean> listRenglonesEsp = new List<RenglonesHCBean>();
            List<RenglonesHCBean> listRenglonesEsp1 = new List<RenglonesHCBean>();
            List<DatosGeneralesHC> datosGenerales = new List<DatosGeneralesHC>();
            DetallesRenglon renglonNoEspejo = new DetallesRenglon();
            DetallesRenglon renglonSiEspejo = new DetallesRenglon();
            DetallesRenglon detallesRenglon = new DetallesRenglon();
            // * Inicio Codigo Nuevo * \\
            List<RenglonesHCBean> listRenglonesC1 = new List<RenglonesHCBean>();
            List<RenglonesHCBean> listRenglonesC2 = new List<RenglonesHCBean>();
            int[] renglonesC1 = new int[10];
            int[] renglonesC2 = new int[10];
            // * Fin Codigo Nuevo * \\
            try {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excel = new ExcelPackage()) {
                    excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(fileName));
                    var worksheet = excel.Workbook.Worksheets[Path.GetFileNameWithoutExtension(fileName)];
                    worksheet.Cells[1, 1].Value = "AÑO";
                    worksheet.Cells[1, 2].Value = "PERIODO_INICIO_PERIODO_FINAL";
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
                    worksheet.Cells[1, 31].Value = "Registro Patronal";

                    for (i = 1; i < 32; i++) {
                        worksheet.Cells[1, i].Style.Fill.SetBackground(System.Drawing.Color.LightSkyBlue);
                    }
                    ii = 31;

                    // Obtenemos los renglones calculados
                    listRenglones = reportDao.sp_Renglones_Hoja_Calculo_Acumulados(keyOption, typePeriod, periodStart, periodEnd, year, 0, 0, 1000, option);
                    if (listRenglones.Count > 0) {
                        RenglonesNom = new int[listRenglones.Count];
                        int r = 0;
                        foreach (RenglonesHCBean renglon in listRenglones) {
                            ii += 1;
                            Renglon = Convert.ToInt32(renglon.iIdRenglon);
                            RenglonesNom[r] = Convert.ToInt32(renglon.iIdRenglon);
                            worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightGreen);
                            worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                            r += 1;
                        }
                    } else {
                        RenglonesNom = new int[0];
                    }

                    a = ii;

                    listRenglones1 = reportDao.sp_Renglones_Hoja_Calculo_Acumulados(keyOption, typePeriod, periodStart, periodEnd, year, 0, 1000, 2000, option);
                    if (listRenglones1.Count > 0) {
                        RenglonesNomDeduc = new int[listRenglones1.Count];
                        int j = 0;
                        foreach (RenglonesHCBean renglon in listRenglones1) {
                            ii += 1;
                            Renglon = Convert.ToInt32(renglon.iIdRenglon);
                            RenglonesNomDeduc[j] = Renglon;
                            worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                            worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                            j += 1;
                        }
                    } else {
                        RenglonesNomDeduc = new int[0];
                    }

                    ii += 1;
                    worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightSteelBlue);
                    worksheet.Cells[1, ii + 1].Value = "NETO A PAGAR FISCAL";

                    listRenglonesEsp = reportDao.sp_Renglones_Hoja_Calculo_Acumulados(keyOption, typePeriod, periodStart, periodEnd, year, 1, 0, 1000, option);
                    if (listRenglonesEsp.Count > 0) {
                        RenglonesNomEspejo = new int[listRenglonesEsp.Count];
                        int g = 0;
                        foreach (RenglonesHCBean renglon in listRenglonesEsp) {
                            ii += 1;
                            Renglon = Convert.ToInt32(renglon.iIdRenglon);
                            RenglonesNomEspejo[g] = Renglon;
                            worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightGreen);
                            worksheet.Cells[1, ii + 1].Value = "(" + Renglon.ToString() + ")" + renglon.sNombreRenglon;
                            g += 1;
                        }
                    } else {
                        RenglonesNomEspejo = new int[0];
                    }

                    listRenglonesEsp1 = reportDao.sp_Renglones_Hoja_Calculo_Acumulados(keyOption, typePeriod, periodStart, periodEnd, year, 1, 1000, 2000, option);
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
                    } else {
                        RenglonesNomDeducEspejo = new int[0];
                    }

                    ii += 1;
                    worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                    worksheet.Cells[1, ii + 1].Value = "NETO A PAGAR GASTO";

                    ii += 1;
                    worksheet.Cells[1, ii + 1].Style.Fill.SetBackground(System.Drawing.Color.Aquamarine);
                    worksheet.Cells[1, ii + 1].Value = "TOTAL GENERAL";

                    stopwatch1.Start();
                    string inicio = DateTime.Now.ToString("hh:mm:ss");
                    DateTime comienzo = Convert.ToDateTime(inicio);

                    datosGenerales = reportDao.sp_Datos_Generales_HC_Acumulados(keyOption, typePeriod, periodStart, periodEnd, year, 1, option);
                    ValidDataN = (datosGenerales.Count > 0) ? true : false;

                    if (datosGenerales.Count > 0)
                    {
                        int business = 0, payroll = 0;
                        int p = 1;
                        foreach (DatosGeneralesHC dato in datosGenerales)
                        {

                            business = dato.iEmpresa;
                            payroll = dato.iNomina;

                            worksheet.Cells[p + 1, 1].Style.Numberformat.Format = "0";
                            worksheet.Cells[p + 1, 1].Value = dato.iAnio;
                            worksheet.Cells[p + 1, 2].Value = dato.sPeriodo;
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
                            worksheet.Cells[p + 1, 31].Value = dato.sRegistroPatronal;
                            Total_Percepciones_Fiscal = 0;

                            for (m = 0; m < RenglonesNom.Count(); m++)
                            {
                                detallesRenglon = reportDao.sp_Detalle_Renglones_Acumulados(business, payroll, periodStart, periodEnd, typePeriod, year, RenglonesNom[m], 0);
                                if (detallesRenglon.dSaldo <= 0)
                                {
                                    worksheet.Cells[p + 1, m + 2 + 31].Value = "";
                                }
                                else
                                {
                                    worksheet.Cells[p + 1, m + 2 + 31].Style.Numberformat.Format = "0.00";
                                    Total_Percepciones_Fiscal += Convert.ToDecimal(detallesRenglon.dSaldo);
                                    worksheet.Cells[p + 1, m + 2 + 31].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                }
                            }

                            cant1 = RenglonesNom.Length;
                            cant2 = cant1 + RenglonesNomDeduc.Length;
                            cant3 = cant2 + RenglonesNomEspejo.Length;

                            Total_Deducciones_Fiscal = 0;

                            for (m = 0; m < RenglonesNomDeduc.Count(); m++)
                            {
                                detallesRenglon = reportDao.sp_Detalle_Renglones_Acumulados(business, payroll, periodStart, periodEnd, typePeriod, year, RenglonesNomDeduc[m], 0);
                                if (detallesRenglon.dSaldo <= 0)
                                {
                                    worksheet.Cells[p + 1, cant1 + m + 2 + 31].Value = "";
                                }
                                else
                                {
                                    worksheet.Cells[p + 1, cant1 + m + 2 + 31].Style.Numberformat.Format = "0.00";
                                    Total_Deducciones_Fiscal += Convert.ToDecimal(detallesRenglon.dSaldo);
                                    worksheet.Cells[p + 1, cant1 + m + 2 + 31].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                }
                            }


                            ////// Monto neto a pagar (FISCAL)
                            renglonNoEspejo = reportDao.sp_Liquidos_Espejo_No_Espejo_Acumulados(business, typePeriod, periodStart, periodEnd, year, 0, payroll);
                            worksheet.Cells[p + 1, cant1 + m + 2 + 31].Style.Numberformat.Format = "0.00";
                            worksheet.Cells[p + 1, cant1 + m + 2 + 31].Style.Fill.SetBackground(System.Drawing.Color.LightSteelBlue);
                            worksheet.Cells[p + 1, cant1 + m + 2 + 31].Value = renglonNoEspejo.dSaldo;

                            Total_Percepciones_Espejo = 0;
                            for (m = 0; m < RenglonesNomEspejo.Count(); m++)
                            {
                                detallesRenglon = reportDao.sp_Detalle_Renglones_Acumulados(business, payroll, periodStart, periodEnd, typePeriod, year, RenglonesNomEspejo[m], 1);
                                if (detallesRenglon.dSaldo <= 0)
                                {
                                    worksheet.Cells[p + 1, cant2 + m + 3 + 31].Value = "";
                                }
                                else
                                {
                                    worksheet.Cells[p + 1, cant2 + m + 3 + 31].Style.Numberformat.Format = "0.00";
                                    Total_Percepciones_Espejo += Convert.ToDecimal(detallesRenglon.dSaldo);
                                    worksheet.Cells[p + 1, cant2 + m + 3 + 31].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                }
                            }

                            Total_Deducciones_Espejo = 0;

                            for (m = 0; m < RenglonesNomDeducEspejo.Count(); m++)
                            {
                                detallesRenglon = reportDao.sp_Detalle_Renglones_Acumulados(business, payroll, periodStart, periodEnd, typePeriod, year, RenglonesNomDeducEspejo[m], 1);
                                if (detallesRenglon.dSaldo <= 0)
                                {
                                    worksheet.Cells[p + 1, cant3 + m + 3 + 31].Value = "";
                                }
                                else
                                {
                                    worksheet.Cells[p + 1, cant3 + m + 3 + 31].Style.Numberformat.Format = "0.00";
                                    Total_Deducciones_Espejo += Convert.ToDecimal(detallesRenglon.dSaldo);
                                    worksheet.Cells[p + 1, cant3 + m + 3 + 31].Value = Convert.ToDecimal(detallesRenglon.dSaldo);
                                }
                            }

                            renglonSiEspejo = reportDao.sp_Liquidos_Espejo_No_Espejo_Acumulados(business, typePeriod, periodStart, periodEnd, year, 1, payroll);
                            worksheet.Cells[p + 1, cant3 + m + 3 + 31].Style.Numberformat.Format = "0.00";
                            worksheet.Cells[p + 1, cant3 + m + 3 + 31].Style.Fill.SetBackground(System.Drawing.Color.LightPink);
                            worksheet.Cells[p + 1, cant3 + m + 3 + 31].Value = renglonSiEspejo.dSaldo;

                            worksheet.Cells[p + 1, cant3 + m + 4 + 31].Style.Numberformat.Format = "0.00";
                            worksheet.Cells[p + 1, cant3 + m + 4 + 31].Style.Fill.SetBackground(System.Drawing.Color.Aquamarine);
                            worksheet.Cells[p + 1, cant3 + m + 4 + 31].Value = renglonNoEspejo.dSaldo + renglonSiEspejo.dSaldo;

                            p += 1;
                        }
                    }
                    FileInfo excelFile = new FileInfo(pathComplete + fileName);
                    excel.SaveAs(excelFile);
                    stopwatch1.Stop();
                    string final = DateTime.Now.ToString("hh:mm:ss");
                    DateTime termino = Convert.ToDateTime(final);
                    tiempo = stopwatch1.Elapsed.Hours + " h " + stopwatch1.Elapsed.Minutes + " m " + stopwatch1.Elapsed.Seconds + " s ";
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Archivo = fileName, Folder = nameFolderRe, Rows = datosGenerales.Count, Validacion = ValidDataN, Tiempo = tiempo });
        }

        [HttpPost]
        public JsonResult GenerateReportFaltasSic(string typeOption, int keyOptionSel, string dateS, string dateE)
        {
            Boolean flag = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "BAJAS_EMPLEADOS";
            string nameFileRepr = "FALTASFI" + dateS.Replace("-", "") + "_FF" + dateE.Replace("-", "") + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Faltas_SIC(typeOption, keyOptionSel, dateS, dateE);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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
        public JsonResult GenerateReportIncapacidadesSic(string typeOption, int keyOptionSel, string dateS, string dateE)
        {
            Boolean flag = false;
            String messageError = "none";
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "REPORTES";
            string nameFolderRe = "BAJAS_EMPLEADOS";
            string nameFileRepr = "INCAPACIDADESFI" + dateS.Replace("-", "") + "_FF" + dateE.Replace("-", "") + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable = reportDao.sp_Datos_Reporte_Incapacidades_SIC(typeOption, keyOptionSel, dateS, dateE);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        columnsDataTable = dataTable.Columns.Count + 1;
                        rowsDataTable = dataTable.Rows.Count;
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