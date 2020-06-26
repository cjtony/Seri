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
        public JsonResult ReportPayroll(string typeOption, int keyOptionSel, int periodActually)
        {
            Boolean flag         = false;
            String  messageError = "none";
            string  pathSaveFile = Server.MapPath("~/Content/");
            string  nameFolder   = "REPORTES";
            string  nameFolderRe = "NOMINA";
            string  nameFilePrototype = "HCalculo_E" + keyOptionSel.ToString() + "_A" + DateTime.Now.Year + "_P" + periodActually.ToString() + "_A.xlsx" ;
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFilePrototype);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable = new DataTable("DATOS_NOMINA");
                    dataTable.Locale    = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable           = reportDao.sp_Datos_Reporte_Nomina(keyOptionSel, periodActually);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFilePrototype));
                        int columnsDataTable = dataTable.Columns.Count + 1;
                        var worksheet = excel.Workbook.Worksheets[Path.GetFileNameWithoutExtension(nameFilePrototype)];
                        for (var i = 1; i < columnsDataTable; i++) {
                            worksheet.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            worksheet.Cells[1, i].Style.Font.Bold = true;
                            worksheet.Cells[1, i].Style.WrapText  = true;
                            worksheet.Cells[1, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            worksheet.Cells[1, i].Style.VerticalAlignment   = ExcelVerticalAlignment.Top;
                        }
                        worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                        FileInfo excelFile = new FileInfo(pathComplete + nameFilePrototype);
                        excel.SaveAs(excelFile);
                        flag = true;
                    }
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Archivo = nameFilePrototype, Folder = nameFolderRe });
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
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable         = new DataTable();
                    dataTable.Locale            = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable                   = reportDao.sp_Datos_Reporte_Altas_Empleado_Fechas(typeOption, keyOptionSel, dateS, dateE);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        int columnsDataTable  = dataTable.Columns.Count + 1;
                        var worksheet         = excel.Workbook.Worksheets[Path.GetFileNameWithoutExtension(nameFileRepr)];
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
                        flag = true;
                    } 
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Archivo = nameFileRepr, Folder = nameFolderRe });
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
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    DataTable dataTable         = new DataTable();
                    dataTable.Locale            = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable                   = reportDao.sp_Datos_Reporte_Bajas_Empleados_Fechas(typeOption, keyOptionSel, dateS, dateE);
                    using (ExcelPackage excel = new ExcelPackage()) {
                        excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
                        int columnsDataTable = dataTable.Columns.Count + 1;
                        var worksheet        = excel.Workbook.Worksheets[Path.GetFileNameWithoutExtension(nameFileRepr)];
                        for (var i = 1; i < columnsDataTable; i++){
                            worksheet.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            worksheet.Cells[1, i].Style.Font.Bold = true;
                            worksheet.Cells[1, i].Style.WrapText  = true;
                            worksheet.Cells[1, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            worksheet.Cells[1, i].Style.VerticalAlignment   = ExcelVerticalAlignment.Top;
                        }
                        worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                        FileInfo excelFile = new FileInfo(pathComplete + nameFileRepr);
                        excel.SaveAs(excelFile);
                        flag = true;
                    }
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Archivo = nameFileRepr, Folder = nameFolderRe });
        }

    }
}