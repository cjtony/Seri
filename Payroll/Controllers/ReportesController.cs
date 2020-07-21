using System;
using System.Collections.Generic;
using System.Data;
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
        public JsonResult ReportPayroll(string typeOption, int keyOptionSel, int typePeriod, int numberPeriod, int yearPeriod)
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
            int rowsDataTable = 0, columnsDataTable = 0;
            try {
                int keyUser  = Convert.ToInt32(Session["iIdUsuario"].ToString());
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
                                    worksheet.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                    worksheet.Cells[1, i].Style.Font.Bold = true;
                                    worksheet.Cells[1, i].Style.WrapText = true;
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
            int rowsDataTable = 0, columnsDataTable = 0;
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
            int rowsDataTable = 0, columnsDataTable = 0;
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
            int rowsDataTable = 0, columnsDataTable = 0;
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
            int rowsDataTable = 0, columnsDataTable = 0;
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
            int rowsDataTable = 0, columnsDataTable = 0;
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