using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using static iTextSharp.text.Font;
using System.IO;
using System.Text;
using System.IO.Compression;
using System.Globalization;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SpreadsheetLight;

namespace Payroll.Controllers
{
    public class DispersionController : Controller
    {
        // GET: Dispersion

        // Muestra la informacion del periodo actual de la nomina
        [HttpPost]
        public JsonResult LoadInfoPeriodPayroll(string yearAct)
        {
            Boolean flag = false;
            String messageError = "none";
            LoadTypePeriodPayrollBean periodBean = new LoadTypePeriodPayrollBean();
            LoadTypePeriodPayrollDaoD periodDaoD = new LoadTypePeriodPayrollDaoD();
            try
            {
                int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                periodBean = periodDaoD.sp_Load_Info_Periodo_Empr(keyBusiness, Convert.ToInt32(yearAct.ToString().Trim()));
                flag = (periodBean.sMensaje == "success") ? true : false;
            }
            catch (Exception exc)
            {
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, InfoPeriodo = periodBean, MensajeError = messageError });
        }

        // Muestra los datos de los empleados con nomina retenida
        [HttpPost]
        public JsonResult PayrollRetainedEmployees()
        {
            Boolean flag = false;
            String messageError = "none";
            List<PayrollRetainedEmployeesBean> payRetainedBean = new List<PayrollRetainedEmployeesBean>();
            PayrollRetainedEmployeesDaoD payRetainedDaoD = new PayrollRetainedEmployeesDaoD();
            try
            {
                int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                payRetainedBean = payRetainedDaoD.sp_Retrieve_NominasRetenidas(keyBusiness);
            }
            catch (Exception exc)
            {
                messageError = exc.Message.ToString();
            }
            var data = new { data = payRetainedBean };
            return Json(data);
        }

        // Muestra los empleados de la empresa a retener nomina
        [HttpPost]
        public JsonResult SearchEmployeesRetainedPayroll(string searchEmployee, string filter)
        {
            Boolean flag = false;
            String messageError = "none";
            Char[] charactersClear = { ' ', '*', '.', '<', '>', '=', '?', '|', '(', ')', '!', '%', '#', '@', '$', '/', '^' };
            string searchClear = searchEmployee.ToString().Trim(charactersClear);
            List<SearchEmployeePayRetainedBean> employeePayRetBean = new List<SearchEmployeePayRetainedBean>();
            SearchEmployeePayRetainedDaoD employeePayRetDaoD = new SearchEmployeePayRetainedDaoD();
            try {
                int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                employeePayRetBean = employeePayRetDaoD.sp_SearchEmploye_Ret_Nomina(keyBusiness, searchClear, filter);
                flag = (employeePayRetBean.Count > 0) ? true : false;
            } catch (Exception exc) {
                messageError = exc.Message.ToString();
            }
            return Json(employeePayRetBean);
        }

        // Carga el periodo actual
        [HttpPost]
        public JsonResult LoadTypePeriod(int year, int typePeriod)
        {
            Boolean flag = false;
            String messageError = "none";
            LoadTypePeriodBean loadTypePerBean = new LoadTypePeriodBean();
            LoadTypePeriodDaoD loadTypePerDaoD = new LoadTypePeriodDaoD();
            try {
                int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                loadTypePerBean = loadTypePerDaoD.sp_Load_Type_Period_Empresa(keyBusiness, year, typePeriod);
                flag = (loadTypePerBean.sMensaje == "success") ? true : false;
            } catch (Exception exc) {
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = loadTypePerBean });
        }

        // Guarda la retencion de nomina del empleado
        [HttpPost]
        public JsonResult RetainedPayrollEmployee(int keyEmployee, int typePeriod, int periodPayroll, int yearRetained, string descriptionRetained)
        {
            Boolean flag = false;
            String messageError = "none";
            PayrollRetainedEmployeesBean retPayEmployeeBean = new PayrollRetainedEmployeesBean();
            PayrollRetainedEmployeesDaoD retPayEmployeeDaoD = new PayrollRetainedEmployeesDaoD();
            try {
                int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                int keyUser = Convert.ToInt32(Session["iIdUsuario"].ToString());
                retPayEmployeeBean = retPayEmployeeDaoD.sp_Insert_Empleado_Retenida_Nomina(keyBusiness, keyEmployee, typePeriod, periodPayroll, yearRetained, descriptionRetained, keyUser);
                flag = (retPayEmployeeBean.sMensaje == "success") ? true : false;
            } catch (Exception exc) {
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        // Remueve la nomina retenida al empleado
        [HttpPost]
        public JsonResult RemovePayrollRetainedEmployee(int keyPayrollRetained)
        {
            Boolean flag = false;
            String messageError = "none";
            PayrollRetainedEmployeesBean retPayEmployeeBean = new PayrollRetainedEmployeesBean();
            PayrollRetainedEmployeesDaoD retPayEmployeeDaoD = new PayrollRetainedEmployeesDaoD();
            try {
                retPayEmployeeBean = retPayEmployeeDaoD.sp_Update_Remove_Nomina_Retenida(keyPayrollRetained);
                flag = (retPayEmployeeBean.sMensaje == "success") ? true : false;
            } catch (Exception exc) {
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        // Crea los folders necesarios
        public Boolean CreateFoldersToDeploy()
        {
            Boolean flag = false;
            string directoryTxt   = Server.MapPath("/DispersionTXT").ToString();
            string directoryZip   = Server.MapPath("/DispersionZIP").ToString();
            string nameFolderYear = DateTime.Now.Year.ToString();
            string nameFolderNom  = "NOMINAS";
            string nameFolderInt  = "INTERBANCARIOS";
            try {
                if (!System.IO.Directory.Exists(directoryTxt + @"\\" + nameFolderYear)) {
                    System.IO.Directory.CreateDirectory(directoryTxt + @"\\" + nameFolderYear);
                }
                if (!System.IO.Directory.Exists(directoryZip + @"\\" + nameFolderYear)) {
                    System.IO.Directory.CreateDirectory(directoryZip + @"\\" + nameFolderYear);
                }
                if (!System.IO.Directory.Exists(directoryTxt + @"\\" + nameFolderYear + @"\\" + nameFolderNom)) {
                    System.IO.Directory.CreateDirectory(directoryTxt + @"\\" + nameFolderYear + @"\\" + nameFolderNom);
                }
                if (!System.IO.Directory.Exists(directoryTxt + @"\\" + nameFolderYear + @"\\" + nameFolderInt)) {
                    System.IO.Directory.CreateDirectory(directoryTxt + @"\\" + nameFolderYear + @"\\" + nameFolderInt);
                }
                if (!System.IO.Directory.Exists(directoryZip + @"\\" + nameFolderYear + @"\\" + nameFolderNom)) {
                    System.IO.Directory.CreateDirectory(directoryZip + @"\\" + nameFolderYear + @"\\" + nameFolderNom);
                }
                if (!System.IO.Directory.Exists(directoryZip + @"\\" + nameFolderYear + @"\\" + nameFolderInt)) {
                    System.IO.Directory.CreateDirectory(directoryZip + @"\\" + nameFolderYear + @"\\" + nameFolderInt);
                }
                flag = true;
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            }
            return flag;
        }

        // Muestra informacion al desplegar la dispersion
        [HttpPost]
        public JsonResult ToDeployDispersion(int yearDispersion, int typePeriodDisp, int periodDispersion, string dateDispersion, string type)
        {
            Boolean flag1 = false;
            Boolean flag2 = false;
            String messageError = "none";
            List<DataDepositsBankingBean> daDepBankingBean = new List<DataDepositsBankingBean>();
            DataDispersionBusiness daDiBusinessDaoD        = new DataDispersionBusiness();
            List<BankDetailsBean> bankDetailsBean          = new List<BankDetailsBean>();
            try {
                int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                daDepBankingBean = daDiBusinessDaoD.sp_Obtiene_Depositos_Bancarios(keyBusiness, yearDispersion, typePeriodDisp, periodDispersion, type);
                if (daDepBankingBean.Count > 0) {
                    flag1 = true;
                    bankDetailsBean = daDiBusinessDaoD.sp_Datos_Banco(daDepBankingBean);
                    flag2 = (bankDetailsBean.Count > 0) ? true : false;
                }
            } catch (Exception exc) {
                messageError = exc.Message.ToString();
            }
            return Json(new { BanderaDispersion = flag1, BanderaBancos = flag2, MensajeError = messageError, DatosDepositos = daDepBankingBean, DatosBancos = bankDetailsBean });
        }

        [HttpPost]
        public JsonResult ToDeployDispersionSpecial(int keyGroup, int year, int typePeriod, int period, string date, string type)
        {
            Boolean flag1 = false;
            Boolean flag2 = false;
            String  messageError = "none";
            List<DataDepositsBankingBean> dataDeposits = new List<DataDepositsBankingBean>();
            DataDispersionBusiness dataDispersion      = new DataDispersionBusiness();
            List<BankDetailsBean> bankDetails          = new List<BankDetailsBean>();
            try {
                dataDeposits = dataDispersion.sp_Obtiene_Depositos_Bancarios_Especial(keyGroup, year, typePeriod, period, "");
                if (dataDeposits.Count > 0) {
                    flag1       = true;
                    bankDetails = dataDispersion.sp_Datos_Banco(dataDeposits);
                    flag2       = (bankDetails.Count > 0) ? true : false;
                }
            } catch (Exception exc) {
                flag1        = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { BanderaDispersion = flag1, BanderaBancos = flag2, MensajeError = messageError, DatosDepositos = dataDeposits, DatosBancos = bankDetails });
        }

        // Valida existencia de banco interbancario
        [HttpPost]
        public JsonResult ValidateBankInterbank(int type)
        {
            Boolean flag = false;
            String  messageError = "none";
            LoadDataTableBean loadDataTable    = new LoadDataTableBean();
            LoadDataTableDaoD loadDataTableDao = new LoadDataTableDaoD();
            try {
                int keyBusiness = int.Parse(Session["IdEmpresa"].ToString());
                loadDataTable = loadDataTableDao.sp_Valida_Existencia_Banco_Interbancario(keyBusiness, type);
                if (loadDataTable.sMensaje == "SUCCESS") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        // Procesa los depositos de nomina
        [HttpPost]
        public JsonResult ProcessDepositsPayroll(int yearPeriod, int numberPeriod, int typePeriod, string dateDeposits, int mirror, int type, string dateDisC)
        {
            DateTime test;
            if (dateDisC != "") {
                test = Convert.ToDateTime(dateDisC.ToString());
            }
            Boolean flag            = false;
            Boolean flagMirror      = false;
            Boolean flagProsecutors = false;
            String  messageError = "none";
            DatosEmpresaBeanDispersion datosEmpresaBeanDispersion = new DatosEmpresaBeanDispersion();
            DataDispersionBusiness dataDispersionBusiness         = new DataDispersionBusiness();
            string nameFolder           = "";
            string nameFileError        = "";
            DateTime dateGeneration     = DateTime.Now;
            string dateGenerationFormat = dateGeneration.ToString("MMddyyyy");
            string directoryZip   = Server.MapPath("/DispersionZIP").ToString();
            string directoryTxt   = Server.MapPath("/DispersionTXT").ToString();
            string nameFolderYear = DateTime.Now.Year.ToString();
            string msgEstatus     = ""; 
            string msgEstatusZip  = "";
            try {
                int keyBusiness   = int.Parse(Session["IdEmpresa"].ToString());
                int yearActually  = DateTime.Now.Year;
                int typeReceipt   = (yearPeriod == yearActually) ? 1 : 0;
                int invoiceId     = yearPeriod * 100000 + typePeriod * 10000 + numberPeriod * 10;
                int invoiceIdMirror        = yearPeriod * 100000 + typePeriod * 10000 + numberPeriod * 10 + 8;
                datosEmpresaBeanDispersion = dataDispersionBusiness.sp_Datos_Empresa_Dispersion(keyBusiness, type);
                nameFolder = "DEPOSITOS_" + "E" + keyBusiness.ToString() + "P" + numberPeriod.ToString() + "A" + dateGeneration.ToString("yyyy").Substring(2, 2);
                if (System.IO.File.Exists(directoryZip + @"\\" + nameFolderYear + @"\\" + "NOMINAS" + @"\" + nameFolder  + ".zip")) {
                    System.IO.File.Delete(directoryZip + @"\\" + nameFolderYear + @"\\" + "NOMINAS" + @"\" + nameFolder + ".zip");
                }
                if (Directory.Exists(directoryTxt + @"\\" + nameFolderYear + @"\\" + "NOMINAS" + @"\" + nameFolder)) {
                    Directory.Delete(directoryTxt + @"\\" + nameFolderYear + @"\\" + "NOMINAS" + @"\" + nameFolder, recursive: true);
                }
                flagProsecutors = ProcessDepositsProsecutors(keyBusiness, invoiceId, typeReceipt, dateDeposits, yearPeriod, typePeriod, numberPeriod, datosEmpresaBeanDispersion.sNombreEmpresa, datosEmpresaBeanDispersion.sRfc, dateDisC);
                if (mirror == 1) {
                    flagMirror = ProcessDepositsMirror(keyBusiness, invoiceId, typeReceipt, dateDeposits, yearPeriod, typePeriod, numberPeriod, datosEmpresaBeanDispersion.sNombreEmpresa, datosEmpresaBeanDispersion.sRfc, dateDisC);
                }
                if (flagProsecutors == true || flagMirror == true) {
                    flag = true;
                }
                if (flag) {
                    // CREACCION DEL ZIP CON LOS ARCHIVOS
                    FileStream stream  = new FileStream(directoryZip + @"\\" + nameFolderYear + @"\\" + "NOMINAS" + @"\" + nameFolder + ".zip", FileMode.OpenOrCreate);
                    ZipArchive fileZip = new ZipArchive(stream, ZipArchiveMode.Create);
                    System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(directoryTxt + @"\\" + nameFolderYear + @"\\" + "NOMINAS" + @"\\" + nameFolder);
                    FileInfo[] sourceFiles = directoryInfo.GetFiles();
                    foreach (FileInfo file in sourceFiles) {
                        Stream sourceStream   = file.OpenRead();
                        ZipArchiveEntry entry = fileZip.CreateEntry(file.Name);
                        Stream zipStream      = entry.Open();
                        sourceStream.CopyTo(zipStream);
                        zipStream.Close();
                        sourceStream.Close();
                    }
                    ZipArchiveEntry zEntrys;
                    fileZip.Dispose();
                    stream.Close();
                    try {
                        using (ZipArchive zipArchive = ZipFile.OpenRead(directoryZip + @"\\" + nameFolderYear + @"\\" + "NOMINAS" + @"\\" + nameFolder + ".zip")) {
                            foreach (ZipArchiveEntry archiveEntry in zipArchive.Entries) {
                                using (ZipArchive zipArchives = ZipFile.Open(directoryZip + @"\\" + nameFolderYear + @"\\" + "NOMINAS" + @"\\" + nameFolder + ".zip", ZipArchiveMode.Read)) {
                                    zEntrys       = zipArchives.GetEntry(archiveEntry.ToString());
                                    nameFileError = zEntrys.Name;
                                    using (StreamReader read = new StreamReader(zEntrys.Open())) {
                                        if (read.ReadLine().Length > 0) {
                                            msgEstatusZip = "filesuccess";
                                        } else {
                                            msgEstatusZip = "fileerror";
                                        }
                                    }
                                }
                            }
                        }
                    } catch (InvalidDataException ide) {
                        Console.WriteLine(ide.Message.ToString() + " En el archivo : " + nameFileError);
                        msgEstatus = "fileError";
                    } catch (Exception exc) {
                        msgEstatus = exc.Message.ToString();
                    }
                    if (System.IO.File.Exists(directoryZip + @"\\" + nameFolderYear + @"\\" + "NOMINAS" + @"\\" + nameFolder + ".zip")) {
                        msgEstatus = "success";
                    } else {
                        msgEstatus = "failed";
                    }
                }
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Zip = nameFolder, EstadoZip = msgEstatusZip, Estado = msgEstatus, Anio = nameFolderYear });
        }

        public double Truncate(double value, int decimales)
        {
            double aux_value = Math.Pow(10, decimales);
            return (Math.Truncate(value * aux_value) / aux_value);
        }

        public Boolean ProcessDepositsProsecutors(int keyBusiness, int invoiceId, int typeReceipt, string dateDeposits, int yearPeriod, int typePeriod, int numberPeriod, string nameBusiness, string rfcBusiness, string dateDisC)
        {
            Boolean flag    = false; 
            Boolean notData = true;
            int k; 
            int bankResult   = 0;  
            string nameBankResult = "";
            string fileNamePDF    = "";
            string vFileName      = ""; 
            List<DatosDepositosBancariosBean>   listDatosDepositosBancariosBeans  = new List<DatosDepositosBancariosBean>();
            DataDispersionBusiness              dataDispersionBusiness            = new DataDispersionBusiness();
            List<DatosProcesaChequesNominaBean> listDatosProcesaChequesNominaBean = new List<DatosProcesaChequesNominaBean>();
            DatosCuentaClienteBancoEmpresaBean  datoCuentaClienteBancoEmpresaBean = new DatosCuentaClienteBancoEmpresaBean();
            DatosDispersionArchivosBanamex datosDispersionArchivosBanamex = new DatosDispersionArchivosBanamex();
            List<DataErrorAccountBank> dataErrors = new List<DataErrorAccountBank>();
            double renglon1481 = 0; 
            try {
                listDatosDepositosBancariosBeans = dataDispersionBusiness.sp_Procesa_Cheques_Total_Nomina(keyBusiness, typePeriod, numberPeriod, yearPeriod);
                if (listDatosDepositosBancariosBeans.Count > 0) {
                    notData = false;
                }
                if (notData) {
                    return flag;
                }
                listDatosProcesaChequesNominaBean = dataDispersionBusiness.sp_Procesa_Cheques_Nomina(keyBusiness, typePeriod, numberPeriod, yearPeriod);
                if (listDatosProcesaChequesNominaBean.Count == 0) {
                    return flag;
                }
                Boolean createFolders = CreateFoldersToDeploy();
                foreach (DatosProcesaChequesNominaBean data in listDatosProcesaChequesNominaBean) {
                    if (data.dImporte != 0) {
                        if (data.iIdBanco != bankResult) {
                            if (bankResult != 0) {
                                // Por ejecutar
                            }
                            bankResult     = data.iIdBanco;
                            nameBankResult = data.sBanco;
                            // Ejecutar un sp
                            datoCuentaClienteBancoEmpresaBean = dataDispersionBusiness.sp_Cuenta_Cliente_Banco_Empresa(keyBusiness, bankResult);
                            if (datoCuentaClienteBancoEmpresaBean.sMensaje == "SUCCESS") {
                                DateTime dateGeneration     = DateTime.Now;
                                string dateGenerationFormat = dateGeneration.ToString("MMddyyyy");
                                if (dateDisC != "") {
                                    dateGenerationFormat = Convert.ToDateTime(dateDisC.ToString()).ToString("MMddyyyy");
                                }
                                //-----------
                                string nameFolder = "DEPOSITOS_" + "E" + keyBusiness.ToString() + "P" + numberPeriod.ToString() + "A" + dateGeneration.ToString("yyyy").Substring(2, 2);
                                //-----------
                                fileNamePDF  = "CHQ_NOMINAS_E" + keyBusiness.ToString() + "A" + string.Format("{0:00}", (yearPeriod % 100)) + "P" + string.Format("{0:00}", numberPeriod) + "_B" + bankResult.ToString() + ".PDF";
                                string directoryTxt = Server.MapPath("/DispersionTXT/" + DateTime.Now.Year.ToString()).ToString() + "/NOMINAS/";
                                if (!System.IO.Directory.Exists(directoryTxt + @"\\" + nameFolder)) {
                                    System.IO.Directory.CreateDirectory(directoryTxt + @"\\" + nameFolder);
                                }
                                // -------------------------
                                if (bankResult == 72) {
                                    vFileName = "NOMINAS_NI" + string.Format("{0:00000}", Convert.ToInt32(datoCuentaClienteBancoEmpresaBean.sNumeroCliente)) + "01";
                                } else {
                                    vFileName = "E" + string.Format("{0:000}", keyBusiness.ToString()) + "A" + yearPeriod + yearPeriod.ToString() + "P" + string.Format("{0:000}", numberPeriod.ToString()) + "_B" + bankResult.ToString();
                                }

                                // BANAMEX -> NOMINA
                                if (bankResult == 2) {
                                    DateTime dateC = dateGeneration;
                                    if (dateDisC != "") {
                                        dateC = Convert.ToDateTime(dateDisC.ToString());
                                    } 
                                    // ENCABEZADO  --> ARCHIVO OK
                                    string tipoRegistroBanamexE  = "1";
                                    string numeroClienteBanamexE = "000" + datoCuentaClienteBancoEmpresaBean.sNumeroCliente;
                                    string fechaBanamexE         = dateC.ToString("ddMM") + dateC.ToString("yyyy").Substring(2, 2);
                                    string valorFijoBanamex0     = "0001";
                                    string nombreEmpresaBanamex  = "";
                                    if (nameBusiness.Length > 35) {
                                        nombreEmpresaBanamex = nameBusiness.Substring(0, 35);
                                    } else {
                                        nombreEmpresaBanamex = nameBusiness;
                                    }
                                    string valorFijoBanamex1 = "CNOMINA";
                                    string fillerBanamexE1 = " ";
                                    string fechaBanamexE1 = dateC.ToString("ddMMyyyy") + "     ";
                                    string valorFijoBanamex2 = "05";
                                    string fillerBanamexE2 = "                                        "; 
                                    string valorFijoBanamex3 = "C00";
                                    //HEADER
                                    string headerLayoutBanamex = tipoRegistroBanamexE + numeroClienteBanamexE + fechaBanamexE + valorFijoBanamex0 + nombreEmpresaBanamex + valorFijoBanamex1 + fillerBanamexE1 + fechaBanamexE1 + valorFijoBanamex2 + fillerBanamexE2 + valorFijoBanamex3;
                                    // FOREACH DATOS TOTALES
                                    string importeTotalBanamexG = "";
                                    // INICIO CODIGO NUEVO RESTA RENGLON 1481 
                                    double sumaImporte = 0;
                                    foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                                       if (bankResult == payroll.iIdBanco) {
                                            double restaImporte = 0;
                                            renglon1481 = dataDispersionBusiness.sp_Comprueba_Existencia_Renglon_Vales(keyBusiness,
                                                    Convert.ToInt32(payroll.sNomina), numberPeriod, typePeriod, yearPeriod);
                                            if (renglon1481 > 0) {
                                                restaImporte = payroll.doImporte - renglon1481;
                                                sumaImporte += restaImporte;
                                            } else {
                                                sumaImporte += payroll.doImporte;
                                            }
                                        }
                                    }
                                    importeTotalBanamexG = sumaImporte.ToString().Replace(".", "");
                                    // FIN CODIGO NUEVO RESTA RENGLON 1481
                                    //foreach (DatosDepositosBancariosBean deposits in listDatosDepositosBancariosBeans) {
                                    //    if (deposits.iIdBanco == bankResult) {
                                    //        importeTotalBanamexG = deposits.sImporte;
                                    //        break;
                                    //    }
                                    //}
                                    // - GLOBAL - \\
                                    string tipoRegistroBanamexG = "2";
                                    string cargoBanamexG        = "1";
                                    string monedaBanamexG       = "001"; 
                                    string tipoCuentaBanamexG   = "01";
                                    // PENDIENTE SUCURSAL
                                    string sucursalBanamexG     = "7009";
                                    string valorFijoBanamexG1   = "0000000000000"; 
                                    string numeroCuentaBanamex  = datoCuentaClienteBancoEmpresaBean.sNumeroCuenta;
                                    string generaCImporteTBG    = "";
                                    int longImporteTotalBG      = 18;
                                    int longITBG = longImporteTotalBG - importeTotalBanamexG.Length;
                                    for (var u = 0; u < longITBG; u++) { generaCImporteTBG += "0"; }
                                    string globalLayoutBanamex = tipoRegistroBanamexG + cargoBanamexG + monedaBanamexG + generaCImporteTBG + importeTotalBanamexG + tipoCuentaBanamexG + sucursalBanamexG + valorFijoBanamexG1 + numeroCuentaBanamex;
                                    // - DETALLE - \\
                                    string tipoRegistroBanamexD = "3";
                                    string abonoBanamexD        = "0";
                                    string metodoPagoBanamexD   = "001";
                                    string tipoCuentaBanamexD   = "01";
                                    string fillerBanamexD1      = "                              ";
                                    string valorFijoBanamexD1   = "NOMINA";
                                    string fillerBanamexD2      = "                                                          ";
                                    string valorFijoBanamexD2   = "0000";
                                    string fillerBanamexD3      = "       ";
                                    string valorFijoBanamexD3   = "00";
                                    using (StreamWriter fileBanamex = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".txt", false, Encoding.UTF8)) {
                                        fileBanamex.Write(headerLayoutBanamex + "\n");
                                        fileBanamex.Write(globalLayoutBanamex + "\n");
                                        string cerosImpTotBnxD    = "";
                                        string cerosNumCueBnxD    = "";
                                        string cerosNumNomBnxD    = "";
                                        string espaciosNomEmpBnxD = "";
                                        int longImpTotBnxD = 18;
                                        int longNumCueBnxD = 20;
                                        int longNumNomBnxD = 10;
                                        int cantidadMovBanamexT = 0;
                                        int sumaImpTotBanamexT  = 0; 
                                        int longNomEmpBnxD      = 55;
                                        foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                                            if (payroll.iIdBanco == bankResult) {
                                                int longAcortAccount = payroll.sCuenta.Length;
                                                string accountUser = payroll.sCuenta;
                                                string formatAccount = "";
                                                if (longAcortAccount == 18) {
                                                    string formatAccountSubstring = accountUser.Substring(0, longAcortAccount - 1);
                                                    formatAccount = formatAccountSubstring.Substring(6, 11);
                                                } else {
                                                    formatAccount = payroll.sCuenta;
                                                }
                                                string nameEmployee = payroll.sNombre + " " + payroll.sPaterno + " " + payroll.sMaterno;
                                                cantidadMovBanamexT += 1;

                                                // INICIO CODIGO NUEVO (RESTA RENGLON 1481)
                                                double restaImporte = 0;
                                                string importeFinal = "";
                                                renglon1481 = dataDispersionBusiness.sp_Comprueba_Existencia_Renglon_Vales(keyBusiness,
                                                        Convert.ToInt32(payroll.sNomina), numberPeriod, typePeriod, yearPeriod);
                                                if (renglon1481 > 0) {
                                                    restaImporte = payroll.doImporte - renglon1481;
                                                    importeFinal = restaImporte.ToString().Replace(".", "");
                                                } else {
                                                    importeFinal = payroll.dImporte.ToString();
                                                }
                                                // FIN CODIGO NUEVO

                                                //sumaImpTotBanamexT += Convert.ToInt32(payroll.dImporte);
                                                sumaImpTotBanamexT += Convert.ToInt32(importeFinal);
                                                string nombreCEmp = "";
                                                if (nameEmployee.Length > 57 ) {
                                                    nombreCEmp = nameEmployee.Substring(0, 54);
                                                } else {
                                                    nombreCEmp = nameEmployee;
                                                }
                                                //int longImpTotBnxDResult = longImpTotBnxD - payroll.dImporte.ToString().Length;
                                                int longImpTotBnxDResult = longImpTotBnxD - importeFinal.ToString().Length;
                                                int longNumCueBnxDResult = longNumCueBnxD - payroll.sCuenta.Length;
                                                int longNumNomBnxDResult = longNumNomBnxD - payroll.sNomina.Length;
                                                int longNomEmpBnxDResult = longNomEmpBnxD - nombreCEmp.Length;
                                                for (var f = 0; f < longImpTotBnxDResult; f++) { cerosImpTotBnxD += "0"; }
                                                for (var r = 0; r < 9; r++) { cerosNumCueBnxD += "0"; }
                                                for (var c = 0; c < longNumNomBnxDResult; c++) { cerosNumNomBnxD += "0"; }
                                                for (var s = 0; s < longNomEmpBnxDResult; s++) { espaciosNomEmpBnxD += " "; }
                                                // payroll.dImporte.ToString()
                                                fileBanamex.Write(tipoRegistroBanamexD + abonoBanamexD + metodoPagoBanamexD + cerosImpTotBnxD + importeFinal + tipoCuentaBanamexD + cerosNumCueBnxD + formatAccount + fillerBanamexD1 + cerosNumNomBnxD + payroll.sNomina + nombreCEmp +          espaciosNomEmpBnxD + valorFijoBanamexD1 + fillerBanamexD2 + valorFijoBanamexD2 + fillerBanamexD3 + valorFijoBanamexD3 + "\n");
                                                cerosImpTotBnxD = "";
                                                cerosNumCueBnxD = "";
                                                cerosNumNomBnxD = "";
                                                espaciosNomEmpBnxD = "";
                                            }
                                        }
                                        // - TOTALES - \\
                                        string tipoRegistroBanamexT = "4";
                                        string claveMonedaBanamexT  = "001";
                                        string valorFijoBanamexT1   = "000001";
                                        string cerosCanMovBnxT      = "";
                                        string cerosSumImpTotBnxT   = "";
                                        int longSumMovBnxT          = 6;
                                        int longSumImpTotBnxT       = 18;
                                        int longSumMovBnxtResult    = longSumMovBnxT - cantidadMovBanamexT.ToString().Length;
                                        int longSumImpTotBnxTResult = longSumImpTotBnxT - sumaImpTotBanamexT.ToString().Length;
                                        for (var s = 0; s < longSumMovBnxtResult; s++) { cerosCanMovBnxT += "0"; }
                                        for (var w = 0; w < longSumImpTotBnxTResult; w++) { cerosSumImpTotBnxT += "0"; }
                                        string totalesLayoutBanamex = tipoRegistroBanamexT + claveMonedaBanamexT + cerosCanMovBnxT + cantidadMovBanamexT.ToString() + cerosSumImpTotBnxT + sumaImpTotBanamexT.ToString() + valorFijoBanamexT1 + cerosSumImpTotBnxT + sumaImpTotBanamexT.ToString();
                                        fileBanamex.Write(totalesLayoutBanamex + "\n");
                                        cerosCanMovBnxT    = "";
                                        cerosSumImpTotBnxT = "";
                                        fileBanamex.Close();
                                    }
                                }

                                // ARCHIVO CSV PARA HSBC -> NOMINA
                                if (bankResult == 21) {

                                    // FOREACH DATOS TOTALES
                                    double totalAmountHSBC = 0;
                                    int hQuantityDeposits = 0;
                                    foreach (DatosProcesaChequesNominaBean deposits in listDatosProcesaChequesNominaBean) {
                                        if (deposits.iIdBanco == bankResult) {
                                            totalAmountHSBC += deposits.doImporte;
                                            hQuantityDeposits += 1;
                                        }
                                    }
                                    string nameBank   = "HSBC";
                                    string outCsvFile = string.Format(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".csv", nameBank + DateTime.Now.ToString("_yyyyMMdd HHmms"));
                                    String header  = "";
                                    var stream = System.IO.File.CreateText(outCsvFile);
                                    // HEADER
                                    string hValuePermanent1    = "MXPRLF";
                                    string hNivelAuthorization = "F";
                                    string hReferenceNumber = datoCuentaClienteBancoEmpresaBean.sNumeroCuenta;
                                    string hTotalAmount        = Truncate(totalAmountHSBC, 2).ToString();
                                    string hDateActually = DateTime.Now.ToString("ddMMyyyy");
                                    if (dateDisC != "") {
                                        hDateActually = Convert.ToDateTime(dateDisC.ToString()).ToString("ddMMyyyy");
                                    }
                                    string hSpaceWhite1        = " ";
                                    string hReferenceAlpa      = "PAGONOM" + numberPeriod + "QFEB";
                                    header = hValuePermanent1 + "," + hNivelAuthorization + "," + hReferenceNumber + "," + hTotalAmount + "," + hQuantityDeposits.ToString() + "," + hDateActually + "," + hSpaceWhite1 + "," + hReferenceAlpa;
                                    stream.WriteLine(header);

                                    // FIN CODIGO NUEVO

                                    string pathFile = string.Format(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + "test.xlsx", nameBank + DateTime.Now.ToString("_yyyyMMdd HHmms"));
                                    SLDocument sLDocument = new SLDocument();
                                    System.Data.DataTable dt = new System.Data.DataTable();
                                    System.Data.DataTable dt2 = new System.Data.DataTable();

                                    dt.Columns.Add(hValuePermanent1, typeof(string));
                                    dt.Columns.Add(hNivelAuthorization, typeof(string));
                                    dt.Columns.Add(hReferenceNumber, typeof(int));
                                    dt.Columns.Add(hTotalAmount, typeof(string));
                                    dt.Columns.Add(hQuantityDeposits.ToString(), typeof(string));
                                    dt.Columns.Add(hDateActually, typeof(string));
                                    dt.Columns.Add(hSpaceWhite1, typeof(string));
                                    dt.Columns.Add(hReferenceAlpa, typeof(string));
                                    sLDocument.ImportDataTable(1, 1, dt, true);

                                    dt2.Columns.Add("", typeof(string));
                                    dt2.Columns.Add("0.00", typeof(decimal));
                                    dt2.Columns.Add("", typeof(string));
                                    dt2.Columns.Add("", typeof(string));

                                    // FIN CODIGO NUEVO

                                    foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                                        if (payroll.iIdBanco == bankResult) {
                                            int longAcortAccount = payroll.sCuenta.Length;
                                            string finallyAccount = payroll.sCuenta;
                                            if (longAcortAccount == 18) {
                                                string accountUser = payroll.sCuenta;
                                                string formatAccountSubstring = accountUser.Substring(0, longAcortAccount - 1);
                                                string formatAccount = "";
                                                if (longAcortAccount == 18) {
                                                    formatAccount = formatAccountSubstring.Substring(0, 7);
                                                }
                                                string cerosAccount = "";
                                                for (var t = 0; t < formatAccount.Length + 1; t++) {
                                                    cerosAccount += "0";
                                                }
                                                finallyAccount = formatAccountSubstring.Substring(7, 10);
                                            } else if (longAcortAccount == 9) {
                                                finallyAccount = "0" + payroll.sCuenta;
                                            } else {
                                                dataErrors.Add(
                                                        new DataErrorAccountBank {sBanco = "HSBC", sCuenta = payroll.sCuenta, sNomina = payroll.sNomina });
                                            }
                                            if (finallyAccount == "6373333668") {
                                                int jd = 0;
                                            }
                                            var test = payroll.dImporte.ToString().Insert(payroll.dImporte.ToString().Length - 2, ".");
                                            string amountt = Truncate(Convert.ToDouble(payroll.sImporte), 2).ToString();
                                            string amount = payroll.doImporte.ToString();
                                            string nameEmployee = payroll.sNombre.TrimEnd() + " " + payroll.sPaterno.TrimEnd() + " " + payroll.sMaterno.TrimEnd();
                                            if (nameEmployee.Length > 35) {
                                                nameEmployee = nameEmployee.Substring(0, 35);
                                            }
                                            header = finallyAccount + "," + test.ToString() + "," + hReferenceAlpa + "," + nameEmployee;

                                            dt2.Rows.Add(finallyAccount.ToString(), test.ToString(), hReferenceAlpa, nameEmployee);

                                            stream.WriteLine(header);
                                        }
                                    }
                                    sLDocument.ImportDataTable(2, 1, dt2, false);
                                    sLDocument.SaveAs(pathFile);
                                    stream.Close();
                                }

                                // ARCHIVO TXT PARA SANTANDER -> NOMINA

                                if (bankResult == 14) {
                                    // - ENCABEZADO - \\ ARCHIVO OK
                                    int initConsecutiveNbOneN    = 1;
                                    string  typeRegisterN        = "1";
                                    string consecutiveNumberOneN = "0000";
                                    string senseA                = "E";
                                    string numCtaBusiness        = datoCuentaClienteBancoEmpresaBean.sNumeroCuenta;
                                    string fillerLayout          = "     ";
                                    string headerLayout          = typeRegisterN + consecutiveNumberOneN + initConsecutiveNbOneN.ToString() + senseA + dateGenerationFormat + numCtaBusiness + fillerLayout + dateGenerationFormat;
                                    // - DETALLE - \\                                                                          
                                    string typeRegisterD = "2";
                                    using (StreamWriter fileTxt = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".txt", false, new ASCIIEncoding()))
                                    {
                                        fileTxt.Write(headerLayout + "\n");
                                        string spaceGenerate1 = "", spaceGenerate2 = "", spaceGenerate3 = "", numberCeroGene = "", consec1Generat = "", numberNomGener = "", totGenerate = "";
                                        int longc = 5, long0 = 7, long1 = 30, long2 = 20, long3 = 30, long4 = 18, consecutiveInit = initConsecutiveNbOneN, resultSumTot = 0, longTot = 18;
                                        int totalRecords = 0;
                                        double totalAmount = 0;
                                        foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                                            if (payroll.iIdBanco == bankResult) {
                                                totalRecords += 1;
                                                int longAcortAccount = payroll.sCuenta.Length;
                                                string finallyAccount = payroll.sCuenta;
                                                if (longAcortAccount == 18) {
                                                    string accountUser = payroll.sCuenta;
                                                    string formatAccountSubstring = accountUser.Substring(0, longAcortAccount - 1);
                                                    string formatAccount = "";
                                                    if (longAcortAccount == 18)
                                                    {
                                                        formatAccount = formatAccountSubstring.Substring(0, 6);
                                                    }
                                                    string cerosAccount = "";
                                                    for (var t = 0; t < formatAccount.Length + 1; t++)
                                                    {
                                                        cerosAccount += "0";
                                                    }
                                                    finallyAccount = formatAccountSubstring.Substring(6, 11);
                                                } else if (longAcortAccount == 9) {
                                                    finallyAccount = "0" + payroll.sCuenta;
                                                } else if (longAcortAccount == 10) { 

                                                } else {
                                                    dataErrors.Add(
                                                            new DataErrorAccountBank
                                                            {
                                                                sBanco = "Santander",
                                                                sCuenta = payroll.sCuenta,
                                                                sNomina = payroll.sNomina
                                                            });
                                                }
                                                consecutiveInit += 1;
                                                // INICIO CODIGO NUEVO (RESTA RENGLON 1481)
                                                double restaImporte = 0;
                                                string importeFinal = "";
                                                renglon1481 = dataDispersionBusiness.sp_Comprueba_Existencia_Renglon_Vales(keyBusiness,
                                                        Convert.ToInt32(payroll.sNomina), numberPeriod, typePeriod, yearPeriod);
                                                if (renglon1481 > 0) {
                                                    restaImporte = payroll.doImporte - renglon1481;
                                                    importeFinal = restaImporte.ToString().Replace(".", "");
                                                } else {
                                                    importeFinal = payroll.dImporte.ToString();
                                                }
                                                // FIN CODIGO NUEVO
                                                totalAmount += payroll.doImporte;
                                                int longConsec = longc - consecutiveInit.ToString().Length;
                                                int longNumNom = long0 - payroll.sNomina.Length;
                                                int longApepat = long1 - payroll.sPaterno.Length;
                                                int longApemat = long2 - payroll.sMaterno.Length;
                                                int longNomEmp = long3 - payroll.sNombre.Length;
                                                //int longImport = long4 - payroll.dImporte.ToString().Length;
                                                int longImport = long4 - importeFinal.ToString().Length;
                                                for (var y = 0; y < longConsec; y++) { consec1Generat += "0"; }
                                                for (var g = 0; g < longNumNom; g++) { numberNomGener += "0"; }
                                                for (var i = 0; i < longApepat; i++) { spaceGenerate1 += " "; }
                                                for (var t = 0; t < longApemat; t++) { spaceGenerate2 += " "; }
                                                for (var z = 0; z < longNomEmp; z++) { spaceGenerate3 += " "; }
                                                for (var x = 0; x < longImport; x++) { numberCeroGene += "0"; }
                                                //resultSumTot += Convert.ToInt32(payroll.dImporte);
                                                resultSumTot += Convert.ToInt32(importeFinal);
                                                fileTxt.WriteLine(typeRegisterD + consec1Generat + consecutiveInit.ToString() + numberNomGener + payroll.sNomina + payroll.sPaterno.Replace("Ñ", "N") + spaceGenerate1 + payroll.sMaterno.Replace("Ñ", "N") + spaceGenerate2 + payroll.sNombre.Replace("Ñ", "N") + spaceGenerate3 + finallyAccount + "     " + numberCeroGene + importeFinal.ToString());
                                                //payroll.dImporte.ToString()
                                                consec1Generat = ""; numberNomGener = "";
                                                spaceGenerate1 = ""; spaceGenerate2 = "";
                                                spaceGenerate3 = ""; numberCeroGene = "";
                                            }
                                        }
                                        if (bankResult == 14) {
                                            int longTotGenerate = longTot - resultSumTot.ToString().Length;
                                            for (var j = 0; j < longTotGenerate; j++) { totGenerate += "0"; }
                                            int long1TotGenert = longc - (consecutiveInit + 1).ToString().Length;
                                            for (var h = 0; h < long1TotGenert; h++) { consec1Generat += "0"; }
                                            int longTotalR = 5;
                                            int resultLTR = longTotalR - totalRecords.ToString().Length;
                                            string cerosTotalRecords = "";
                                            for (var x = 0; x < resultLTR; x++) {
                                                cerosTotalRecords += "0";
                                            }
                                            string totLayout = "3" + consec1Generat + (consecutiveInit + 1).ToString() + cerosTotalRecords + totalRecords.ToString() + totGenerate + resultSumTot.ToString();
                                            fileTxt.WriteLine(totLayout);
                                        }
                                        fileTxt.Close();
                                    }
                                }

                                // ARCHIVO DISPERSION BANORTE -> NOMINA -> OK

                                if (bankResult == 72) {

                                    // INICIO CODIGO NUEVO

                                    long[] TotIAbonos = new long[201];
                                    for (k = 0; k <= 200; k++)
                                    {
                                        TotIAbonos[k] = 0;
                                    }
                                    long TotalNumAbonos = 0;
                                    foreach (DatosProcesaChequesNominaBean bank in listDatosProcesaChequesNominaBean) {
                                        if (bankResult == bank.iIdBanco) {
                                            TotalNumAbonos += 1;
                                        }
                                    }
                                    StringBuilder sb1;
                                    sb1 = new StringBuilder("");
                                    sb1.Append("H");
                                    sb1.Append("NE");
                                    sb1.Append(string.Format("{0:00000}", Convert.ToInt32(datoCuentaClienteBancoEmpresaBean.sNumeroCliente)));
                                    sb1.Append(dateGeneration.ToString("yyyyMMdd"));
                                    sb1.Append("01");
                                    sb1.Append(string.Format("{0:000000}", TotalNumAbonos));
                                    sb1.Append(string.Format("{0:000000000000000}", TotIAbonos[72]));
                                    sb1.Append("0".PadRight(49, '0'));
                                    sb1.Append(" ".PadLeft(77, ' '));
                                    string ts = sb1.ToString();

                                    // FIN CODIGO NUEVO
                                    string importeTotalBanorte = "";
                                    // INICIO CODIGO NUEVO RESTA RENGLON 1481 
                                    double sumaImporte = 0;
                                    foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean)
                                    {
                                        if (bankResult == payroll.iIdBanco) {
                                            double restaImporte = 0;
                                            renglon1481 = dataDispersionBusiness.sp_Comprueba_Existencia_Renglon_Vales(keyBusiness,
                                                    Convert.ToInt32(payroll.sNomina), numberPeriod, typePeriod, yearPeriod);
                                            if (renglon1481 > 0) {
                                                restaImporte = payroll.doImporte - renglon1481; 
                                                sumaImporte += restaImporte;
                                            } else { 
                                                sumaImporte += payroll.doImporte;
                                            }
                                        }
                                    }
                                    importeTotalBanorte = sumaImporte.ToString().Replace(".", "");
                                    // FIN CODIGO NUEVO RESTA RENGLON 1481
                                    //foreach (DatosDepositosBancariosBean deposits in listDatosDepositosBancariosBeans) {
                                    //    if (deposits.iIdBanco == bankResult) { importeTotalBanorte = deposits.sImporte; break; }
                                    //}
                                    // - ENCABEZADO - \\ 
                                    string cerosImporteTotal      = "";
                                    string tipoRegistroBanorteE   = "H";
                                    string claveServicioBanorte   = "NE";
                                    string promotorBanorte        = datoCuentaClienteBancoEmpresaBean.sNumeroCliente;
                                    string consecutivoBanorte     = "01";
                                    string importeTotalAYBBanorte = "0000000000000000000000000000000000000000000000000";
                                    string fillerBanorte          = "                                                                             ";
                                    string generaCNumEmpresa      = "";
                                    string generaCNumRegistros    = "";
                                    int longNumEmpresa   = 5; 
                                    int longNumRegistros = 6;
                                    int longAmoutTotal   = 15;
                                    int resultLongAmount = longAmoutTotal - importeTotalBanorte.Length;
                                    string cerosImp2 = "";
                                    string quantityRegistersLong = "";
                                    int quantityRegisters = 0;
                                    foreach (DatosProcesaChequesNominaBean bank in listDatosProcesaChequesNominaBean) {
                                        if (bankResult == bank.iIdBanco) {
                                            quantityRegisters += 1;
                                        }
                                    }
                                    int longQuantityRegisters = 6;
                                    int resultLongQuantityRegisters = longQuantityRegisters - quantityRegisters.ToString().Length;
                                    for (var f = 0; f < resultLongQuantityRegisters; f++)
                                    {
                                        quantityRegistersLong += "0";
                                    }
                                    quantityRegistersLong += quantityRegisters.ToString();
                                    string dateGenerationDisp = dateGeneration.ToString("yyyyMMdd");
                                    if (dateDisC != "") {
                                        dateGenerationDisp = Convert.ToDateTime(dateDisC.ToString()).ToString("yyyyMMdd");
                                    }
                                    for (var j = 0; j < resultLongAmount; j++) { cerosImporteTotal += "0"; }
                                    string headerLayoutBanorte    = tipoRegistroBanorteE + claveServicioBanorte + promotorBanorte + dateGenerationDisp +  consecutivoBanorte + quantityRegistersLong + cerosImp2 + cerosImporteTotal + importeTotalBanorte + importeTotalAYBBanorte + fillerBanorte;
                                    // - DETALLE - \\
                                    string tipoRegistroBanorteD     = "D";
                                    string fechaAplicacionBanorte   = dateGenerationDisp;
                                    string numBancoReceptorBanorteD = "072";
                                    string tipoCuentaBanorteD       = "01";
                                    string tipoMovimientoBanorteD   = "0"; 
                                    string fillerBanorteD0          = " ";
                                    string importeIvaBanorteD       = "00000000";
                                    string fillerBanorteD           = "                                                                                ";
                                    string fillerBanorteD1          = "                  ";
                                    double sumTest = 0;
                                    decimal sumtests = 0;
                                    using (StreamWriter fileBanorte = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".txt", false, Encoding.UTF8)) {
                                        fileBanorte.Write(headerLayoutBanorte + "\n");
                                        string generaCNumEmpleadoB = "", generaCNumImporteB = "", generaCNumCuentaB = "";
                                        int longNumEmpleado = 10, longNumImporte = 15, longNumCuenta = 18;
                                        foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                                            if (payroll.iIdBanco == bankResult) {
                                                int longAcortAccount = payroll.sCuenta.Length;
                                                string finallyAccount = payroll.sCuenta;
                                                if (longAcortAccount == 18)
                                                {
                                                    string accountUser = payroll.sCuenta;
                                                    string formatAccountSubstring = accountUser.Substring(0, longAcortAccount - 1);
                                                    string formatAccount = "";
                                                    if (longAcortAccount == 18)
                                                    {
                                                        formatAccount = formatAccountSubstring.Substring(0, 7);
                                                    }
                                                    string cerosAccount = "";
                                                    for (var t = 0; t < formatAccount.Length + 1; t++)
                                                    {
                                                        cerosAccount += "0";
                                                    }
                                                    finallyAccount = formatAccountSubstring.Substring(7, 10);
                                                    // finallyAccount = cerosAccount + formatAccountSubstring.Substring(7, 10);
                                                }
                                                else if (longAcortAccount == 9)
                                                {
                                                    finallyAccount = "0" + payroll.sCuenta;
                                                }
                                                else if (longAcortAccount == 10)
                                                {
                                                    finallyAccount = payroll.sCuenta;
                                                } else if (longAcortAccount == 11) {
                                                    finallyAccount = payroll.sCuenta.Remove(0, 1);
                                                } else {
                                                    dataErrors.Add(
                                                            new DataErrorAccountBank
                                                            {
                                                                sBanco = "BANORTE",
                                                                sCuenta = payroll.sCuenta,
                                                                sNomina = payroll.sNomina
                                                            });
                                                }
                                                sumTest += payroll.doImporte;
                                                sumtests += payroll.dImporte;
                                                // INICIO CODIGO NUEVO (RESTA RENGLON 1481)
                                                double restaImporte = 0;
                                                string importeFinal = "";
                                                renglon1481 = dataDispersionBusiness.sp_Comprueba_Existencia_Renglon_Vales(keyBusiness,
                                                        Convert.ToInt32(payroll.sNomina), numberPeriod, typePeriod, yearPeriod);
                                                if (renglon1481 > 0) {
                                                    restaImporte = payroll.doImporte - renglon1481;
                                                    importeFinal = restaImporte.ToString().Replace(".", "");
                                                } else {
                                                    importeFinal = payroll.dImporte.ToString();
                                                }
                                                // FIN CODIGO NUEVO
                                                int longNumEmp = longNumEmpleado - payroll.sNomina.Length;
                                                //int longNumImp = longNumImporte - payroll.dImporte.ToString().Length;
                                                int longNumImp = longNumImporte - importeFinal.ToString().Length;
                                                int longNumCta = 10 - finallyAccount.Length;
                                                string cerosAccountDefault = "00000000";
                                                for (var b = 0; b < longNumEmp; b++) { generaCNumEmpleadoB += "0"; }
                                                for (var v = 0; v < longNumImp; v++) { generaCNumImporteB += "0"; }
                                                for (var p = 0; p < longNumCta; p++) { generaCNumCuentaB += "0"; }
                                                // payroll.dImporte.ToString()
                                                fileBanorte.Write(tipoRegistroBanorteD + fechaAplicacionBanorte + generaCNumEmpleadoB + payroll.sNomina + fillerBanorteD + generaCNumImporteB + importeFinal + numBancoReceptorBanorteD + tipoCuentaBanorteD + generaCNumCuentaB + cerosAccountDefault + finallyAccount + tipoMovimientoBanorteD + fillerBanorteD0 + importeIvaBanorteD + fillerBanorteD1 + "\n");
                                                generaCNumEmpleadoB = "";
                                                generaCNumImporteB = "";
                                                generaCNumCuentaB = "";
                                            }
                                        }
                                        string test = sumTest.ToString();
                                        string test2 = sumtests.ToString();
                                        //fileBanorte.WriteLine(sumtests.ToString() + " test  " + test2);
                                        fileBanorte.Close();
                                    }
                                }

                                // ARCHIVO DISPERSION BANCOMER -> NOMINA

                                if (bankResult == 12) {

                                    // Inicio Codigo Nuevo
                                    string pathSaveFile = Server.MapPath("~/Content/");
                                    string routeTXTBancomer = pathSaveFile + "DISPERSION" + @"\\" + "BANCOMER" + @"\\" + "BANCOMER.txt";
                                    string pathCompleteTXT = directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".txt";
                                    StringBuilder sb1;
                                    //System.IO.File.Copy(routeTXTBancomer, pathCompleteTXT, true);
                                    int totalRegistros = 0;
                                    string V = "";
                                    using (StreamWriter fileBancomer = new StreamWriter(pathCompleteTXT, false, new ASCIIEncoding())) {
                                        foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                                            if (payroll.iIdBanco == bankResult) {
                                                // INICIO CODIGO NUEVO (RESTA RENGLON 1481)
                                                double restaImporte = 0;
                                                decimal importeFinal = 0;
                                                renglon1481 = dataDispersionBusiness.sp_Comprueba_Existencia_Renglon_Vales(keyBusiness,
                                                        Convert.ToInt32(payroll.sNomina), numberPeriod, typePeriod, yearPeriod);
                                                if (renglon1481 > 0) {
                                                    restaImporte = payroll.doImporte - renglon1481;
                                                    importeFinal = Convert.ToDecimal(restaImporte.ToString().Replace(".", ""));
                                                } else {
                                                    importeFinal = payroll.dImporte;
                                                }
                                                // FIN CODIGO NUEVO
                                                totalRegistros += 1;
                                                sb1 = new StringBuilder("");
                                                sb1.Append(string.Format("{0:000000000}", totalRegistros));
                                                sb1.Append(" ".PadLeft(16, ' '));
                                                sb1.Append("99");
                                                V = payroll.sCuenta.Trim();
                                                if (V.Length <= 10)
                                                    V = V.PadLeft(10, '0');
                                                else                            // FCH
                                                    V = V.Substring(7, 10);         // FCH
                                                sb1.Append(V);
                                                sb1.Append(" ".PadLeft(10, ' '));
                                                sb1.Append(string.Format("{0:000000000000000}", importeFinal));
                                                V = payroll.sNombre + " " + payroll.sPaterno + " " + payroll.sMaterno.Trim();
                                                if (V.Length < 38)
                                                    V = V.PadRight(38, ' ');
                                                else
                                                    V = V.Substring(0, 38);
                                                sb1.Append(V);
                                                sb1.Append("  001001");
                                                fileBancomer.WriteLine(sb1.ToString());
                                            }
                                        }
                                        fileBancomer.Close();
                                    }
                                }

                                FileStream fs = new FileStream(directoryTxt + @"\\" + nameFolder + @"\\" + fileNamePDF, FileMode.Create);
                                Document doc = new Document(iTextSharp.text.PageSize.LETTER, 20, 40, 20, 40);
                                PdfWriter pw = PdfWriter.GetInstance(doc, fs);
                                doc.AddTitle("Reporte de Dispersion");
                                doc.AddAuthor("");
                                doc.Open();

                                // Creamos el tipo de Font que vamos utilizar
                                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                                // Escribimos el encabezamiento en el documento
                                Font fontDefault = new Font(FontFamily.HELVETICA, 10);
                                Paragraph pr = new Paragraph();
                                DateTime datePdf = DateTime.Now;
                                pr.Font = fontDefault;
                                pr.Add("Fecha: " + datePdf.ToString("yyyy-MM-dd") + " Periodo: " + numberPeriod.ToString());
                                pr.Alignment = Element.ALIGN_LEFT;
                                doc.Add(pr);
                                pr.Clear();
                                pr.Add("IPSNet \n Dépositos " + nameBankResult);
                                pr.Alignment = Element.ALIGN_CENTER;
                                doc.Add(pr);
                                doc.Add(Chunk.NEWLINE);
                                pr.Clear();
                                pr.Add(nameBusiness + "\n" + rfcBusiness);
                                pr.Alignment = Element.ALIGN_CENTER;
                                doc.Add(pr);
                                doc.Add(Chunk.NEWLINE);
                                
                                PdfPTable tblPrueba = new PdfPTable(4);
                                tblPrueba.WidthPercentage = 100;
                                PdfPCell clCtaCheques = new PdfPCell(new Phrase("Cta. Cheques", _standardFont));
                                clCtaCheques.BorderWidth = 0;
                                clCtaCheques.BorderWidthBottom = 0.75f;
                                clCtaCheques.Bottom = 80;
                                PdfPCell clBeneficiario = new PdfPCell(new Phrase("Beneficiario", _standardFont));
                                clBeneficiario.BorderWidth = 0;
                                clBeneficiario.BorderWidthBottom = 0.75f;
                                clBeneficiario.Bottom = 60;
                                PdfPCell clImporte = new PdfPCell(new Phrase("Importe", _standardFont));
                                clImporte.BorderWidth = 0;
                                clImporte.BorderWidthBottom = 0.75f;
                                clImporte.Bottom = 40;
                                PdfPCell clNomina = new PdfPCell(new Phrase("Nomina", _standardFont));
                                clNomina.BorderWidth = 0;
                                clNomina.BorderWidthBottom = 0.75f;
                                clNomina.Bottom = 20; 
                                tblPrueba.AddCell(clCtaCheques);
                                tblPrueba.AddCell(clBeneficiario);
                                tblPrueba.AddCell(clImporte);
                                tblPrueba.AddCell(clNomina);
                                int d = 0;
                                Decimal sumTotal = 0;
                                int cantidadEmpleados = 0;
                                foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                                    if (payroll.iIdBanco == bankResult) {
                                        // INICIO CODIGO NUEVO (RESTA RENGLON 1481)
                                        double restaImporte = 0;
                                        double importeFinal = 0;
                                        renglon1481 = dataDispersionBusiness.sp_Comprueba_Existencia_Renglon_Vales(keyBusiness,
                                                Convert.ToInt32(payroll.sNomina), numberPeriod, typePeriod, yearPeriod);
                                        if (renglon1481 > 0) {
                                            restaImporte = payroll.doImporte - renglon1481;
                                            importeFinal = restaImporte;
                                        } else {
                                            importeFinal = payroll.doImporte;
                                        }
                                        // FIN CODIGO NUEVO
                                        cantidadEmpleados += 1;
                                        //sumTotal += Convert.ToDecimal(payroll.doImporte);
                                        sumTotal += Convert.ToDecimal(importeFinal);
                                        d += 1; 
                                        clCtaCheques = new PdfPCell(new Phrase(payroll.sCuenta, _standardFont));
                                        clCtaCheques.BorderWidth = 0;
                                        clCtaCheques.Bottom = 80;
                                        clBeneficiario = new PdfPCell(new Phrase(payroll.sNombre + " " + payroll.sPaterno + " " + payroll.sMaterno, _standardFont));
                                        clBeneficiario.BorderWidth = 0;
                                        clBeneficiario.Bottom = 80;
                                        //clImporte = new PdfPCell(new Phrase("$ " + Convert.ToDecimal(payroll.doImporte).ToString("#,##0.00"), _standardFont));
                                        clImporte = new PdfPCell(new Phrase("$ " + Convert.ToDecimal(importeFinal).ToString("#,##0.00"), _standardFont));
                                        clImporte.BorderWidth = 0;
                                        clImporte.Bottom = 80;
                                        clNomina = new PdfPCell(new Phrase(payroll.sNomina, _standardFont));
                                        clNomina.BorderWidth = 0;
                                        clNomina.Bottom = 80; 
                                        tblPrueba.AddCell(clCtaCheques);
                                        tblPrueba.AddCell(clBeneficiario);
                                        tblPrueba.AddCell(clImporte);
                                        tblPrueba.AddCell(clNomina);
                                    }
                                }
                                doc.Add(tblPrueba);
                                doc.Add(Chunk.NEWLINE);
                                iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                                // Creamos una tabla que contendrá los datos
                                PdfPTable tblTotal = new PdfPTable(1);
                                tblTotal.WidthPercentage = 100;
                                PdfPCell clTotal = new PdfPCell(new Phrase("Total: " + "$ " + sumTotal.ToString("#,##0.00"), _standardFont2));
                                clTotal.BorderWidth = 0; 
                                clTotal.Bottom = 80;
                                tblTotal.AddCell(clTotal);
                                doc.Add(tblTotal);
                                // Creamos la tabla del total de empleados
                                PdfPTable tblTotalEmpleados = new PdfPTable(1);
                                tblTotalEmpleados.WidthPercentage = 100;
                                PdfPCell clTotalEmpleados = new PdfPCell(new Phrase("Empleados: " + cantidadEmpleados.ToString() + " registros.", _standardFont2));
                                clTotalEmpleados.BorderWidth = 0;
                                clTotalEmpleados.Bottom = 80;
                                tblTotalEmpleados.AddCell(clTotalEmpleados);
                                doc.Add(tblTotalEmpleados);
                                doc.Close();
                                pw.Close();
                                flag = true;
                                if (dataErrors.Count > 0) {
                                    using (StreamWriter fileLog = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + "LOG_ACCOUNTS" + ".txt", false, Encoding.UTF8))
                                    {
                                        fileLog.Write("Fecha: " + DateTime.Now.ToString() + "\n");
                                        fileLog.Write("\n");
                                        fileLog.Write("CUENTAS CON LONGITUD MENOR A 18 CARACTERES");
                                        fileLog.Write("\n");
                                        foreach (DataErrorAccountBank err in dataErrors) {
                                            fileLog.Write("Nomina: " + err.sNomina + ". Banco: " + err.sBanco + ". Cuenta: " + err.sCuenta);
                                            fileLog.Write("\n");
                                        }
                                        fileLog.Close();
                                    }
                                }
                            }
                        }
                    }
                }
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
                flag = false;
            }
            return flag;
        }

        public Boolean ProcessDepositsMirror(int keyBusiness, int invoiceId, int typeReceipt, string dateDeposits, int yearPeriod, int typePeriod, int numberPeriod, string nameBusiness, string rfcBusiness, string dateDisC)
        {
            Boolean flag    = false;
            Boolean notData = true;
            int k;
            int bankResult   = 0;
            string nameBankResult = "";
            string fileNamePDF    = "";
            string vFileName      = "";
            List<DataErrorAccountBank> dataErrors = new List<DataErrorAccountBank>();
            List<DatosDepositosBancariosBean> listDatosDepositosBancariosBeans = new List<DatosDepositosBancariosBean>();
            DataDispersionBusiness dataDispersionBusiness = new DataDispersionBusiness();
            List<DatosProcesaChequesNominaBean> listDatosProcesaChequesNominaBean = new List<DatosProcesaChequesNominaBean>();
            DatosCuentaClienteBancoEmpresaBean datoCuentaClienteBancoEmpresaBean = new DatosCuentaClienteBancoEmpresaBean();
            DatosDispersionArchivosBanamex datosDispersionArchivosBanamex = new DatosDispersionArchivosBanamex();
            try {
                listDatosDepositosBancariosBeans = dataDispersionBusiness.sp_Procesa_Cheques_Total_Nomina_Espejo(keyBusiness, typePeriod, numberPeriod, yearPeriod);
                if (listDatosDepositosBancariosBeans.Count > 0) {
                    notData = false;
                }
                if (notData) {
                    return flag;
                }
                listDatosProcesaChequesNominaBean = dataDispersionBusiness.sp_Procesa_Cheques_Nomina_Espejo(keyBusiness, typePeriod, numberPeriod, yearPeriod);
                if (listDatosProcesaChequesNominaBean.Count == 0) {
                    return flag;
                }
                Boolean createFolders = CreateFoldersToDeploy();
                foreach (DatosProcesaChequesNominaBean data in listDatosProcesaChequesNominaBean) {
                    if (data.dImporte != 0) {
                        if (data.iIdBanco != bankResult) {
                            if (bankResult != 0)  {
                                // Por ejecutar
                            }
                            bankResult = data.iIdBanco;
                            nameBankResult = data.sBanco;
                            // Ejecutar un sp
                            datoCuentaClienteBancoEmpresaBean = dataDispersionBusiness.sp_Cuenta_Cliente_Banco_Empresa(keyBusiness, bankResult);
                            if (datoCuentaClienteBancoEmpresaBean.sMensaje == "SUCCESS") {
                                DateTime dateGeneration = DateTime.Now;
                                string dateGenerationFormat = dateGeneration.ToString("MMddyyyy");
                                //-----------
                                string nameFolder = "DEPOSITOS_" + "E" + keyBusiness.ToString() + "P" + numberPeriod.ToString() + "A" + dateGeneration.ToString("yyyy").Substring(2, 2);
                                //-----------
                                fileNamePDF = "CHQ_NOMINAS_E" + keyBusiness.ToString() + "A" + string.Format("{0:00}", (yearPeriod % 100)) + "P" + string.Format("{0:00}", numberPeriod) + "_BE_" + bankResult.ToString() + ".PDF";
                                // -------------------------
                                string directoryTxt = Server.MapPath("/DispersionTXT/" + DateTime.Now.Year.ToString()).ToString() + "/NOMINAS/";
                                // -------------------------
                                if (!System.IO.Directory.Exists(directoryTxt + @"\\" + nameFolder)) {
                                    System.IO.Directory.CreateDirectory(directoryTxt + @"\\" + nameFolder);
                                }
                                // -------------------------
                                if (bankResult == 72) {
                                    vFileName = "NOMINAS_NI" + string.Format("{0:00000}", Convert.ToInt32(datoCuentaClienteBancoEmpresaBean.sNumeroCliente)) + "01_ESP";
                                } else {
                                    vFileName = "E" + string.Format("{0:000}", keyBusiness.ToString()) + "A" + yearPeriod + yearPeriod.ToString() + "P" + string.Format("{0:000}", numberPeriod.ToString()) + "_BE_" + bankResult.ToString();
                                }

                                // BANAMEX -> NOMINA
                                if (bankResult == 2) {
                                    DateTime dateC = dateGeneration;
                                    if (dateDisC != "") {
                                        dateC = Convert.ToDateTime(dateDisC.ToString());
                                    }
                                    // ENCABEZADO  --> ARCHIVO OK
                                    string tipoRegistroBanamexE = "1";
                                    string numeroClienteBanamexE = "000" + datoCuentaClienteBancoEmpresaBean.sNumeroCliente;
                                    string fechaBanamexE = dateC.ToString("ddMM") + dateC.ToString("yyyy").Substring(2, 2);
                                    string valorFijoBanamex0 = "0001";
                                    string nombreEmpresaBanamex = "";
                                    if (nameBusiness.Length > 35) {
                                        nombreEmpresaBanamex = nameBusiness.Substring(0, 35);
                                    } else {
                                        nombreEmpresaBanamex = nameBusiness;
                                    }
                                    string valorFijoBanamex1 = "CNOMINA";
                                    string fillerBanamexE1 = " ";
                                    string fechaBanamexE1 = dateC.ToString("ddMMyyyy") + "     ";
                                    string valorFijoBanamex2 = "05";
                                    string fillerBanamexE2 = "                                        ";
                                    string valorFijoBanamex3 = "C00";
                                    //HEADER
                                    string headerLayoutBanamex = tipoRegistroBanamexE + numeroClienteBanamexE + fechaBanamexE + valorFijoBanamex0 + nombreEmpresaBanamex + valorFijoBanamex1 + fillerBanamexE1 + fechaBanamexE1 + valorFijoBanamex2 + fillerBanamexE2 + valorFijoBanamex3;
                                    // FOREACH DATOS TOTALES
                                    string importeTotalBanamexG = "";
                                    foreach (DatosDepositosBancariosBean deposits in listDatosDepositosBancariosBeans) {
                                        if (deposits.iIdBanco == bankResult) {
                                            importeTotalBanamexG = deposits.sImporte;
                                            break;
                                        }
                                    }
                                    // - GLOBAL - \\
                                    string tipoRegistroBanamexG = "2";
                                    string cargoBanamexG = "1";
                                    string monedaBanamexG = "001";
                                    string tipoCuentaBanamexG = "01";
                                    // PENDIENTE SUCURSAL
                                    string sucursalBanamexG = "7009";
                                    string valorFijoBanamexG1 = "0000000000000";
                                    string numeroCuentaBanamex = datoCuentaClienteBancoEmpresaBean.sNumeroCuenta;
                                    string generaCImporteTBG = "";
                                    int longImporteTotalBG = 18;
                                    int longITBG = longImporteTotalBG - importeTotalBanamexG.Length;
                                    for (var u = 0; u < longITBG; u++) { generaCImporteTBG += "0"; }
                                    string globalLayoutBanamex = tipoRegistroBanamexG + cargoBanamexG + monedaBanamexG + generaCImporteTBG + importeTotalBanamexG + tipoCuentaBanamexG + sucursalBanamexG + valorFijoBanamexG1 + numeroCuentaBanamex;
                                    // - DETALLE - \\
                                    string tipoRegistroBanamexD = "3";
                                    string abonoBanamexD = "0";
                                    string metodoPagoBanamexD = "001";
                                    string tipoCuentaBanamexD = "01";
                                    string fillerBanamexD1 = "                              ";
                                    string valorFijoBanamexD1 = "NOMINA";
                                    string fillerBanamexD2 = "                                                          ";
                                    string valorFijoBanamexD2 = "0000";
                                    string fillerBanamexD3 = "       ";
                                    string valorFijoBanamexD3 = "00";
                                    using (StreamWriter fileBanamex = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".txt", false, Encoding.UTF8)) {
                                        fileBanamex.Write(headerLayoutBanamex + "\n");
                                        fileBanamex.Write(globalLayoutBanamex + "\n");
                                        string cerosImpTotBnxD = "";
                                        string cerosNumCueBnxD = "";
                                        string cerosNumNomBnxD = "";
                                        string espaciosNomEmpBnxD = "";
                                        int longImpTotBnxD = 18;
                                        int longNumCueBnxD = 20;
                                        int longNumNomBnxD = 10;
                                        int cantidadMovBanamexT = 0;
                                        int sumaImpTotBanamexT = 0;
                                        int longNomEmpBnxD = 55;
                                        foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                                            if (payroll.iIdBanco == bankResult) {
                                                int longAcortAccount = payroll.sCuenta.Length;
                                                string accountUser = payroll.sCuenta;
                                                string formatAccount = "";
                                                if (longAcortAccount == 18) {
                                                    string formatAccountSubstring = accountUser.Substring(0, longAcortAccount - 1);
                                                    formatAccount = formatAccountSubstring.Substring(6, 11);
                                                } else {
                                                    formatAccount = payroll.sCuenta;
                                                }
                                                string nameEmployee = payroll.sNombre + " " + payroll.sPaterno + " " + payroll.sMaterno;
                                                cantidadMovBanamexT += 1;
                                                sumaImpTotBanamexT += Convert.ToInt32(payroll.dImporte);
                                                string nombreCEmp = "";
                                                if (nameEmployee.Length > 57) {
                                                    nombreCEmp = nameEmployee.Substring(0, 54);
                                                } else {
                                                    nombreCEmp = nameEmployee;
                                                }
                                                int longImpTotBnxDResult = longImpTotBnxD - payroll.dImporte.ToString().Length;
                                                int longNumCueBnxDResult = longNumCueBnxD - payroll.sCuenta.Length;
                                                int longNumNomBnxDResult = longNumNomBnxD - payroll.sNomina.Length;
                                                int longNomEmpBnxDResult = longNomEmpBnxD - nombreCEmp.Length;
                                                for (var f = 0; f < longImpTotBnxDResult; f++) { cerosImpTotBnxD += "0"; }
                                                for (var r = 0; r < 9; r++) { cerosNumCueBnxD += "0"; }
                                                for (var c = 0; c < longNumNomBnxDResult; c++) { cerosNumNomBnxD += "0"; }
                                                for (var s = 0; s < longNomEmpBnxDResult; s++) { espaciosNomEmpBnxD += " "; }
                                                fileBanamex.Write(tipoRegistroBanamexD + abonoBanamexD + metodoPagoBanamexD + cerosImpTotBnxD + payroll.dImporte.ToString() + tipoCuentaBanamexD + cerosNumCueBnxD + formatAccount + fillerBanamexD1 + cerosNumNomBnxD + payroll.sNomina + nombreCEmp + espaciosNomEmpBnxD + valorFijoBanamexD1 + fillerBanamexD2 + valorFijoBanamexD2 + fillerBanamexD3 + valorFijoBanamexD3 + "\n");
                                                cerosImpTotBnxD = "";
                                                cerosNumCueBnxD = "";
                                                cerosNumNomBnxD = "";
                                                espaciosNomEmpBnxD = "";
                                            }
                                        }
                                        // - TOTALES - \\
                                        string tipoRegistroBanamexT = "4";
                                        string claveMonedaBanamexT = "001";
                                        string valorFijoBanamexT1 = "000001";
                                        string cerosCanMovBnxT = "";
                                        string cerosSumImpTotBnxT = "";
                                        int longSumMovBnxT = 6;
                                        int longSumImpTotBnxT = 18;
                                        int longSumMovBnxtResult = longSumMovBnxT - cantidadMovBanamexT.ToString().Length;
                                        int longSumImpTotBnxTResult = longSumImpTotBnxT - sumaImpTotBanamexT.ToString().Length;
                                        for (var s = 0; s < longSumMovBnxtResult; s++) { cerosCanMovBnxT += "0"; }
                                        for (var w = 0; w < longSumImpTotBnxTResult; w++) { cerosSumImpTotBnxT += "0"; }
                                        string totalesLayoutBanamex = tipoRegistroBanamexT + claveMonedaBanamexT + cerosCanMovBnxT + cantidadMovBanamexT.ToString() + cerosSumImpTotBnxT + sumaImpTotBanamexT.ToString() + valorFijoBanamexT1 + cerosSumImpTotBnxT + sumaImpTotBanamexT.ToString();
                                        fileBanamex.Write(totalesLayoutBanamex + "\n");
                                        cerosCanMovBnxT = "";
                                        cerosSumImpTotBnxT = "";
                                        fileBanamex.Close();
                                    }
                                }

                                // ARCHIVO CSV PARA HSBC -> NOMINA
                                if (bankResult == 21) {
                                    // FOREACH DATOS TOTALES
                                    double totalAmountHSBC = 0;
                                    int hQuantityDeposits = 0;
                                    foreach (DatosProcesaChequesNominaBean deposits in listDatosProcesaChequesNominaBean) {
                                        if (deposits.iIdBanco == bankResult) {
                                            totalAmountHSBC += deposits.doImporte;
                                            hQuantityDeposits += 1;
                                        }
                                    }
                                    string nameBank = "HSBC";
                                    string outCsvFile = string.Format(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".csv", nameBank + DateTime.Now.ToString("_yyyyMMdd HHmms"));
                                    String header = "";
                                    var stream = System.IO.File.CreateText(outCsvFile);
                                    // HEADER
                                    string hValuePermanent1 = "MXPRLF";
                                    string hNivelAuthorization = "F";
                                    string hReferenceNumber = datoCuentaClienteBancoEmpresaBean.sNumeroCuenta;
                                    string hTotalAmount = Truncate(totalAmountHSBC, 2).ToString();
                                    string hDateActually = DateTime.Now.ToString("ddMMyyyy");
                                    if (dateDisC != "") {
                                        hDateActually = Convert.ToDateTime(dateDisC.ToString()).ToString("ddMMyyyy");
                                    }
                                    string hSpaceWhite1 = "";
                                    string hReferenceAlpa = "PAGONOM" + numberPeriod + "QFEB";
                                    header = hValuePermanent1 + "," + hNivelAuthorization + "," + hReferenceNumber + "," + hTotalAmount + "," + hQuantityDeposits.ToString() + "," + hDateActually + "," + hSpaceWhite1 + "," + hReferenceAlpa;
                                    stream.WriteLine(header);
                                    foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                                        if (payroll.iIdBanco == bankResult) {
                                            int longAcortAccount = payroll.sCuenta.Length;
                                            string finallyAccount = payroll.sCuenta;
                                            if (longAcortAccount == 18) {
                                                string accountUser = payroll.sCuenta;
                                                string formatAccountSubstring = accountUser.Substring(0, longAcortAccount - 1);
                                                string formatAccount = "";
                                                if (longAcortAccount == 18) {
                                                    formatAccount = formatAccountSubstring.Substring(0, 7);
                                                }
                                                string cerosAccount = "";
                                                for (var t = 0; t < formatAccount.Length + 1; t++) {
                                                    cerosAccount += "0";
                                                }
                                                finallyAccount = formatAccountSubstring.Substring(7, 10);
                                            } else if (longAcortAccount == 9) {
                                                finallyAccount = "0" + payroll.sCuenta;
                                            } else {
                                                dataErrors.Add( new DataErrorAccountBank { sBanco = "HSBC", sCuenta = payroll.sCuenta, sNomina = payroll.sNomina });
                                            }
                                            string amount = payroll.doImporte.ToString();
                                            string nameBen = payroll.sNombre.TrimEnd() + " " + payroll.sPaterno.TrimEnd() + " " + payroll.sMaterno.TrimEnd();
                                            header = finallyAccount + "," + amount + "," + hReferenceAlpa + "," + nameBen;
                                            stream.WriteLine(header);
                                        }
                                    }
                                    stream.Close();
                                }

                                // ARCHIVO TXT PARA SANTANDER -> NOMINA

                                if (bankResult == 14) {
                                    // - ENCABEZADO - \\ ARCHIVO OK
                                    int initConsecutiveNbOneN = 1;
                                    string typeRegisterN = "1";
                                    string consecutiveNumberOneN = "0000";
                                    string senseA = "E";
                                    string numCtaBusiness = datoCuentaClienteBancoEmpresaBean.sNumeroCuenta;
                                    string fillerLayout = "     ";
                                    string headerLayout = typeRegisterN + consecutiveNumberOneN + initConsecutiveNbOneN.ToString() + senseA + dateGenerationFormat + numCtaBusiness + fillerLayout + dateGenerationFormat;
                                    // - DETALLE - \\                                                                          
                                    string typeRegisterD = "2";
                                    using (StreamWriter fileTxt = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".txt", false, Encoding.UTF8)) {
                                        fileTxt.Write(headerLayout + "\n");
                                        string spaceGenerate1 = "", spaceGenerate2 = "", spaceGenerate3 = "", numberCeroGene = "", consec1Generat = "", numberNomGener = "", totGenerate = "";
                                        int longc = 5, long0 = 7, long1 = 30, long2 = 20, long3 = 30, long4 = 18, consecutiveInit = initConsecutiveNbOneN, resultSumTot = 0, longTot = 18;
                                        int totalRecords = 0;
                                        double totalAmount = 0;
                                        foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                                            if (payroll.iIdBanco == bankResult) {
                                                totalRecords += 1;
                                                totalAmount += payroll.doImporte;
                                                int longAcortAccount = payroll.sCuenta.Length;
                                                string finallyAccount = payroll.sCuenta;
                                                if (longAcortAccount == 18) {
                                                    string accountUser = payroll.sCuenta;
                                                    string formatAccountSubstring = accountUser.Substring(0, longAcortAccount - 1);
                                                    string formatAccount = "";
                                                    if (longAcortAccount == 18) {
                                                        formatAccount = formatAccountSubstring.Substring(0, 6);
                                                    }
                                                    string cerosAccount = "";
                                                    for (var t = 0; t < formatAccount.Length + 1; t++) {
                                                        cerosAccount += "0";
                                                    }
                                                    finallyAccount = formatAccountSubstring.Substring(6, 11);
                                                } else if (longAcortAccount == 9) {
                                                    finallyAccount = "0" + payroll.sCuenta;
                                                } else {
                                                    dataErrors.Add( new DataErrorAccountBank { sBanco = "Santander", sCuenta = payroll.sCuenta, sNomina = payroll.sNomina });
                                                }
                                                consecutiveInit += 1;
                                                int longConsec = longc - consecutiveInit.ToString().Length;
                                                int longNumNom = long0 - payroll.sNomina.Length;
                                                int longApepat = long1 - payroll.sPaterno.Length;
                                                int longApemat = long2 - payroll.sMaterno.Length;
                                                int longNomEmp = long3 - payroll.sNombre.Length;
                                                int longImport = long4 - payroll.dImporte.ToString().Length;
                                                for (var y = 0; y < longConsec; y++) { consec1Generat += "0"; }
                                                for (var g = 0; g < longNumNom; g++) { numberNomGener += "0"; }
                                                for (var i = 0; i < longApepat; i++) { spaceGenerate1 += " "; }
                                                for (var t = 0; t < longApemat; t++) { spaceGenerate2 += " "; }
                                                for (var z = 0; z < longNomEmp; z++) { spaceGenerate3 += " "; }
                                                for (var x = 0; x < longImport; x++) { numberCeroGene += "0"; }
                                                resultSumTot += Convert.ToInt32(payroll.dImporte);
                                                fileTxt.Write(typeRegisterD + consec1Generat + consecutiveInit.ToString() + numberNomGener + payroll.sNomina + payroll.sPaterno.Replace("Ñ", "N") + spaceGenerate1 + payroll.sMaterno.Replace("Ñ", "N") + spaceGenerate2 + payroll.sNombre.Replace("Ñ", "N") + spaceGenerate3 + finallyAccount + "     " + numberCeroGene + payroll.dImporte.ToString() + "\n");
                                                consec1Generat = ""; numberNomGener = "";
                                                spaceGenerate1 = ""; spaceGenerate2 = "";
                                                spaceGenerate3 = ""; numberCeroGene = "";
                                            }
                                        }
                                        if (bankResult == 14) {
                                            int longTotGenerate = longTot - resultSumTot.ToString().Length;
                                            for (var j = 0; j < longTotGenerate; j++) { totGenerate += "0"; }
                                            int long1TotGenert = longc - (consecutiveInit + 1).ToString().Length;
                                            for (var h = 0; h < long1TotGenert; h++) { consec1Generat += "0"; }
                                            int longTotalR = 5;
                                            int resultLTR = longTotalR - totalRecords.ToString().Length;
                                            string cerosTotalRecords = "";
                                            for (var x = 0; x < resultLTR; x++) {
                                                cerosTotalRecords += "0";
                                            }
                                            string totLayout = "3" + consec1Generat + (consecutiveInit + 1).ToString() + cerosTotalRecords + totalRecords.ToString() + totGenerate + resultSumTot.ToString();
                                            fileTxt.Write(totLayout + "\n");
                                        }
                                        fileTxt.Close();
                                    }
                                }

                                // ARCHIVO DISPERSION BANORTE -> NOMINA -> OK

                                if (bankResult == 72) {

                                    // INICIO CODIGO NUEVO

                                    long[] TotIAbonos = new long[201];
                                    for (k = 0; k <= 200; k++) {
                                        TotIAbonos[k] = 0;
                                    }
                                    long TotalNumAbonos = 0;
                                    foreach (DatosProcesaChequesNominaBean bank in listDatosProcesaChequesNominaBean) {
                                        if (bankResult == bank.iIdBanco) {
                                            TotalNumAbonos += 1;
                                        }
                                    }
                                    StringBuilder sb1;
                                    sb1 = new StringBuilder("");
                                    sb1.Append("H");
                                    sb1.Append("NE");
                                    sb1.Append(string.Format("{0:00000}", Convert.ToInt32(datoCuentaClienteBancoEmpresaBean.sNumeroCliente)));
                                    sb1.Append(dateGeneration.ToString("yyyyMMdd"));
                                    sb1.Append("01");
                                    sb1.Append(string.Format("{0:000000}", TotalNumAbonos));
                                    sb1.Append(string.Format("{0:000000000000000}", TotIAbonos[72]));
                                    sb1.Append("0".PadRight(49, '0'));
                                    sb1.Append(" ".PadLeft(77, ' '));
                                    string ts = sb1.ToString();

                                    // FIN CODIGO NUEVO

                                    string importeTotalBanorte = "";
                                    foreach (DatosDepositosBancariosBean deposits in listDatosDepositosBancariosBeans) {
                                        if (deposits.iIdBanco == bankResult) { importeTotalBanorte = deposits.sImporte; break; }
                                    }
                                    // - ENCABEZADO - \\ 
                                    string cerosImporteTotal = "";
                                    string tipoRegistroBanorteE = "H";
                                    string claveServicioBanorte = "NE";
                                    string promotorBanorte = datoCuentaClienteBancoEmpresaBean.sNumeroCliente;
                                    string consecutivoBanorte = "01";
                                    string importeTotalAYBBanorte = "0000000000000000000000000000000000000000000000000";
                                    string fillerBanorte = "                                                                             ";
                                    string generaCNumEmpresa = "";
                                    string generaCNumRegistros = "";
                                    int longNumEmpresa = 5;
                                    int longNumRegistros = 6;
                                    int longAmoutTotal = 15;
                                    int resultLongAmount = longAmoutTotal - importeTotalBanorte.Length;
                                    string cerosImp2 = "";
                                    string quantityRegistersLong = "";
                                    int quantityRegisters = 0;
                                    foreach (DatosProcesaChequesNominaBean bank in listDatosProcesaChequesNominaBean) {
                                        if (bankResult == bank.iIdBanco) {
                                            quantityRegisters += 1;
                                        }
                                    }
                                    int longQuantityRegisters = 6;
                                    int resultLongQuantityRegisters = longQuantityRegisters - quantityRegisters.ToString().Length;
                                    for (var f = 0; f < resultLongQuantityRegisters; f++) {
                                        quantityRegistersLong += "0";
                                    }
                                    quantityRegistersLong += quantityRegisters.ToString();
                                    string dateGenerationDisp = dateGeneration.ToString("yyyyMMdd");
                                    if (dateDisC != "")  {
                                        dateGenerationDisp = Convert.ToDateTime(dateDisC.ToString()).ToString("yyyyMMdd");
                                    }
                                    for (var j = 0; j < resultLongAmount; j++) { cerosImporteTotal += "0"; }
                                    string headerLayoutBanorte = tipoRegistroBanorteE + claveServicioBanorte + promotorBanorte + dateGenerationDisp + consecutivoBanorte + quantityRegistersLong + cerosImp2 + cerosImporteTotal + importeTotalBanorte + importeTotalAYBBanorte + fillerBanorte;
                                    // - DETALLE - \\
                                    string tipoRegistroBanorteD = "D";
                                    string fechaAplicacionBanorte = dateGenerationDisp;
                                    string numBancoReceptorBanorteD = "072";
                                    string tipoCuentaBanorteD = "01";
                                    string tipoMovimientoBanorteD = "0";
                                    string fillerBanorteD0 = " ";
                                    string importeIvaBanorteD = "00000000";
                                    string fillerBanorteD = "                                                                                ";
                                    string fillerBanorteD1 = "                  ";
                                    double sumTest = 0;
                                    decimal sumtests = 0;
                                    using (StreamWriter fileBanorte = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".txt", false, Encoding.UTF8)) {
                                        fileBanorte.Write(headerLayoutBanorte + "\n");
                                        string generaCNumEmpleadoB = "", generaCNumImporteB = "", generaCNumCuentaB = "";
                                        int longNumEmpleado = 10, longNumImporte = 15, longNumCuenta = 18;
                                        foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                                            if (payroll.iIdBanco == bankResult) {
                                                int longAcortAccount = payroll.sCuenta.Length;
                                                string finallyAccount = payroll.sCuenta;
                                                if (longAcortAccount == 18) {
                                                    string accountUser = payroll.sCuenta;
                                                    string formatAccountSubstring = accountUser.Substring(0, longAcortAccount - 1);
                                                    string formatAccount = "";
                                                    if (longAcortAccount == 18) {
                                                        formatAccount = formatAccountSubstring.Substring(0, 7);
                                                    }
                                                    string cerosAccount = "";
                                                    for (var t = 0; t < formatAccount.Length + 1; t++) {
                                                        cerosAccount += "0";
                                                    }
                                                    finallyAccount = formatAccountSubstring.Substring(7, 10);
                                                    // finallyAccount = cerosAccount + formatAccountSubstring.Substring(7, 10);
                                                } else if (longAcortAccount == 9) {
                                                    finallyAccount = "0" + payroll.sCuenta;
                                                } else if (longAcortAccount == 10) {
                                                    finallyAccount = payroll.sCuenta;
                                                }
                                                else if (longAcortAccount == 11)
                                                {
                                                    finallyAccount = payroll.sCuenta.Remove(0, 1);
                                                }
                                                else
                                                {
                                                    dataErrors.Add(
                                                            new DataErrorAccountBank
                                                            {
                                                                sBanco = "BANORTE",
                                                                sCuenta = payroll.sCuenta,
                                                                sNomina = payroll.sNomina
                                                            });
                                                }
                                                sumTest += payroll.doImporte;
                                                sumtests += payroll.dImporte;
                                                int longNumEmp = longNumEmpleado - payroll.sNomina.Length;
                                                int longNumImp = longNumImporte - payroll.dImporte.ToString().Length;
                                                int longNumCta = 10 - finallyAccount.Length;
                                                string cerosAccountDefault = "00000000";
                                                for (var b = 0; b < longNumEmp; b++) { generaCNumEmpleadoB += "0"; }
                                                for (var v = 0; v < longNumImp; v++) { generaCNumImporteB += "0"; }
                                                for (var p = 0; p < longNumCta; p++) { generaCNumCuentaB += "0"; }
                                                fileBanorte.Write(tipoRegistroBanorteD + fechaAplicacionBanorte + generaCNumEmpleadoB + payroll.sNomina + fillerBanorteD + generaCNumImporteB + payroll.dImporte.ToString() + numBancoReceptorBanorteD + tipoCuentaBanorteD + generaCNumCuentaB + cerosAccountDefault + finallyAccount + tipoMovimientoBanorteD + fillerBanorteD0 + importeIvaBanorteD + fillerBanorteD1 + "\n");
                                                generaCNumEmpleadoB = "";
                                                generaCNumImporteB = "";
                                                generaCNumCuentaB = "";
                                            }
                                        }
                                        string test = sumTest.ToString();
                                        string test2 = sumtests.ToString();
                                        //fileBanorte.WriteLine(sumtests.ToString() + " test  " + test2);
                                        fileBanorte.Close();
                                    }
                                }

                                // ARCHIVO DISPERSION BANCOMER -> NOMINA

                                if (bankResult == 12) {
                                    // Inicio Codigo Nuevo
                                    string pathSaveFile = Server.MapPath("~/Content/");
                                    string routeTXTBancomer = pathSaveFile + "DISPERSION" + @"\\" + "BANCOMER" + @"\\" + "BANCOMER.txt";
                                    string pathCompleteTXT = directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".txt";
                                    StringBuilder sb1;
                                    //System.IO.File.Copy(routeTXTBancomer, pathCompleteTXT, true);
                                    int totalRegistros = 0;
                                    string V = "";
                                    using (StreamWriter fileBancomer = new StreamWriter(pathCompleteTXT, false, new ASCIIEncoding())) {
                                        foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                                            if (payroll.iIdBanco == bankResult) {
                                                totalRegistros += 1;
                                                sb1 = new StringBuilder("");
                                                sb1.Append(string.Format("{0:000000000}", totalRegistros));
                                                sb1.Append(" ".PadLeft(16, ' '));
                                                sb1.Append("99");
                                                V = payroll.sCuenta.Trim();
                                                if (V.Length <= 10)
                                                    V = V.PadLeft(10, '0');
                                                else                            // FCH
                                                    V = V.Substring(7, 10);         // FCH
                                                sb1.Append(V);
                                                sb1.Append(" ".PadLeft(10, ' '));
                                                sb1.Append(string.Format("{0:000000000000000}", payroll.dImporte));
                                                V = payroll.sNombre + " " + payroll.sPaterno + " " + payroll.sMaterno.Trim();
                                                if (V.Length < 38)
                                                    V = V.PadRight(38, ' ');
                                                else
                                                    V = V.Substring(0, 38);
                                                sb1.Append(V);
                                                sb1.Append("  001001");
                                                fileBancomer.WriteLine(sb1.ToString());
                                            }
                                        }
                                        fileBancomer.Close();
                                    }
                                }


                                FileStream fs = new FileStream(directoryTxt + @"\\" + nameFolder + @"\\" + fileNamePDF, FileMode.Create);
                                Document doc = new Document(iTextSharp.text.PageSize.LETTER, 20, 40, 20, 40);
                                PdfWriter pw = PdfWriter.GetInstance(doc, fs);
                                doc.AddTitle("Reporte de Dispersion");
                                doc.AddAuthor("");
                                doc.Open();

                                // Creamos el tipo de Font que vamos utilizar
                                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                                // Escribimos el encabezamiento en el documento
                                Font fontDefault = new Font(FontFamily.HELVETICA, 10);
                                Paragraph pr = new Paragraph();
                                DateTime datePdf = DateTime.Now;
                                pr.Font = fontDefault;
                                pr.Add("Fecha: " + datePdf.ToString("yyyy-MM-dd") + " Periodo: " + numberPeriod.ToString());
                                pr.Alignment = Element.ALIGN_LEFT;
                                doc.Add(pr);
                                pr.Clear();
                                pr.Add("IPSNet \n Dépositos " + nameBankResult);
                                pr.Alignment = Element.ALIGN_CENTER;
                                doc.Add(pr);
                                doc.Add(Chunk.NEWLINE);
                                pr.Clear();
                                pr.Add(nameBusiness + "\n" + rfcBusiness);
                                pr.Alignment = Element.ALIGN_CENTER;
                                doc.Add(pr);
                                doc.Add(Chunk.NEWLINE);
                                // Creamos una tabla que contendrá los datos
                                PdfPTable tblPrueba       = new PdfPTable(4);
                                tblPrueba.WidthPercentage = 100;
                                // Configuramos el título de las columnas de la tabla
                                PdfPCell clCtaCheques          = new PdfPCell(new Phrase("Cta. Cheques", _standardFont));
                                clCtaCheques.BorderWidth       = 0;
                                clCtaCheques.BorderWidthBottom = 0.75f;
                                clCtaCheques.Bottom            = 80;
                                PdfPCell clBeneficiario          = new PdfPCell(new Phrase("Beneficiario", _standardFont));
                                clBeneficiario.BorderWidth       = 0;
                                clBeneficiario.BorderWidthBottom = 0.75f;
                                clBeneficiario.Bottom            = 60;
                                PdfPCell clImporte          = new PdfPCell(new Phrase("Importe", _standardFont));
                                clImporte.BorderWidth       = 0;
                                clImporte.BorderWidthBottom = 0.75f;
                                clImporte.Bottom            = 40;
                                PdfPCell clNomina           = new PdfPCell(new Phrase("Nomina", _standardFont));
                                clNomina.BorderWidth        = 0;
                                clNomina.BorderWidthBottom  = 0.75f;
                                clNomina.Bottom             = 20;
                                // Añadimos las celdas a la tabla
                                tblPrueba.AddCell(clCtaCheques);
                                tblPrueba.AddCell(clBeneficiario);
                                tblPrueba.AddCell(clImporte);
                                tblPrueba.AddCell(clNomina);
                                double sumTotal = 0;
                                int cantidadEmpleados = 0;
                                foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                                    if (payroll.iIdBanco == bankResult) {
                                        cantidadEmpleados += 1;
                                        sumTotal += payroll.doImporte;
                                        // Llenamos la tabla con información
                                        clCtaCheques = new PdfPCell(new Phrase(payroll.sCuenta, _standardFont));
                                        clCtaCheques.BorderWidth = 0;
                                        clCtaCheques.Bottom = 80;
                                        clBeneficiario = new PdfPCell(new Phrase(payroll.sNombre + " " + payroll.sPaterno + " " + payroll.sMaterno, _standardFont));
                                        clBeneficiario.BorderWidth = 0;
                                        clBeneficiario.Bottom = 80;
                                        clImporte = new PdfPCell(new Phrase("$ " + Convert.ToDecimal(payroll.doImporte).ToString("#,##0.00"), _standardFont));
                                        clImporte.BorderWidth = 0;
                                        clImporte.Bottom = 80;
                                        clNomina = new PdfPCell(new Phrase(payroll.sNomina, _standardFont));
                                        clNomina.BorderWidth = 0;
                                        clNomina.Bottom = 80;
                                        // Añadimos las celdas a la tabla
                                        tblPrueba.AddCell(clCtaCheques);
                                        tblPrueba.AddCell(clBeneficiario);
                                        tblPrueba.AddCell(clImporte);
                                        tblPrueba.AddCell(clNomina);
                                    }
                                }
                                doc.Add(tblPrueba);
                                doc.Add(Chunk.NEWLINE);
                                iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                                // Creamos una tabla que contendrá los datos
                                PdfPTable tblTotal = new PdfPTable(1);
                                tblTotal.WidthPercentage = 100;
                                PdfPCell clTotal = new PdfPCell(new Phrase("Total: " + "$ " + sumTotal.ToString("#,##0.00"), _standardFont2));
                                clTotal.BorderWidth = 0;
                                clTotal.Bottom = 80;
                                tblTotal.AddCell(clTotal);
                                doc.Add(tblTotal);
                                // Creamos la tabla del total de empleados
                                PdfPTable tblTotalEmpleados = new PdfPTable(1);
                                tblTotalEmpleados.WidthPercentage = 100;
                                PdfPCell clTotalEmpleados = new PdfPCell(new Phrase("Empleados: " + cantidadEmpleados.ToString() + " registros.", _standardFont2));
                                clTotalEmpleados.BorderWidth = 0;
                                clTotalEmpleados.Bottom = 80;
                                tblTotalEmpleados.AddCell(clTotalEmpleados);
                                doc.Add(tblTotalEmpleados);
                                doc.Close();
                                pw.Close();
                                flag = true;
                            }
                        }
                    }
                }
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
                flag = false;
            }
            return flag;
        }

        [HttpPost]
        public JsonResult RestartToDeploy(string paramNameFile, int paramYear, string paramCode)
        {
            Boolean flag         = false;
            Boolean flagZIP      = false;
            Boolean flagTXT      = false;
            String  messageError = "none";
            string  nameFileFZ   = (paramCode == "NOM") ? "NOMINAS" : "INTERBANCARIOS";
            string  directoryZip = Server.MapPath("/DispersionZIP/" + paramYear.ToString() + "/" + nameFileFZ + "/" + paramNameFile + ".zip");
            string  directoryTxt = Server.MapPath("/DispersionTXT/" + paramYear.ToString() + "/" + nameFileFZ + "/" + paramNameFile);
            try {
                if (System.IO.File.Exists(directoryZip)) {
                    System.IO.File.Delete(directoryZip);
                    flagZIP = true;
                }
                if (System.IO.Directory.Exists(directoryTxt)) {
                    System.IO.Directory.Delete(directoryTxt, recursive: true);
                    flagTXT = true;
                }
                flag = true;
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, TXT = flagTXT, ZIP = flagZIP });
        }

        [HttpPost]
        public JsonResult ProcessDepositsInterbank(int yearPeriod, int numberPeriod, int typePeriod, string dateDeposits, int mirror, int type, string dateDisC, int tipPago)
        {
            Boolean flag            = false;
            Boolean flagMirror      = false;
            Boolean flagProsecutors = false;
            Boolean first           = false;
            String messageError     = "none";
            DatosEmpresaBeanDispersion datosEmpresaBeanDispersion = new DatosEmpresaBeanDispersion();
            DataDispersionBusiness     dataDispersionBusiness     = new DataDispersionBusiness();
            List<DatosDepositosBancariosBean> listDatosDepositosBancariosBeans = new List<DatosDepositosBancariosBean>();
            DatosCuentaClienteBancoEmpresaBean datoCuentaClienteBancoEmpresaBean = new DatosCuentaClienteBancoEmpresaBean();
            List<DatosProcesaChequesNominaBean> listDatosProcesaChequesNominaBean = new List<DatosProcesaChequesNominaBean>();
            string nameFolder    = "";
            string nameFileError = "";
            DateTime dateGeneration     = DateTime.Now;
            string dateGenerationFormat = dateGeneration.ToString("MMddyyyy");
            string directoryZip   = Server.MapPath("/DispersionZIP").ToString();
            string directoryTxt   = Server.MapPath("/DispersionTXT").ToString() + "/" + DateTime.Now.Year.ToString() + "/INTERBANCARIOS/";
            string nameFolderYear = DateTime.Now.Year.ToString();
            string msgEstatus     = "";
            string msgEstatusZip  = "";
            int bankInterbank     = 2;
            int turns             = 0;
            int totalRecords      = 0;
            string fileNamePdfPM  = "";
            string fileNameTxtPM  = "";
            Boolean createFolders = CreateFoldersToDeploy();
            try {
                int keyBusiness  = int.Parse(Session["IdEmpresa"].ToString());
                int yearActually = DateTime.Now.Year;
                int typeReceipt  = (yearPeriod == yearActually) ? 1 : 0;
                int invoiceId       = yearPeriod * 100000 + typePeriod * 10000 + numberPeriod * 10;
                int invoiceIdMirror = yearPeriod * 100000 + typePeriod * 10000 + numberPeriod * 10 + 8;
                int invoiceSendSP;
                datosEmpresaBeanDispersion = dataDispersionBusiness.sp_Datos_Empresa_Dispersion(keyBusiness, type);
                if (datosEmpresaBeanDispersion.iBanco_id.GetType().Name == "DBNull") {
                    // Retornar error
                }
                nameFolder = "DEPOSITOS_" + "E" + keyBusiness.ToString() + "P" + numberPeriod.ToString() + "A" + dateGeneration.ToString("yyyy").Substring(2, 2);
                // -------------------------
                if (System.IO.File.Exists(directoryZip + @"\\" + nameFolderYear + @"\\" + "INTERBANCARIOS" + @"\" + nameFolder + ".zip")) {
                    System.IO.File.Delete(directoryZip + @"\\" + nameFolderYear + @"\\" + "INTERBANCARIOS" + @"\" + nameFolder + ".zip");
                }
                if (Directory.Exists(directoryTxt + @"\\" + nameFolder)) {
                    Directory.Delete(directoryTxt + @"\\" + nameFolder, recursive: true);
                }
                if (!System.IO.Directory.Exists(directoryTxt + @"\\" + nameFolder)) {
                    System.IO.Directory.CreateDirectory(directoryTxt + @"\\" + nameFolder);
                }
                while (turns < 2) {
                    first = true;
                    turns += 1;
                    totalRecords = 0;
                    invoiceId = (turns == 1) ? invoiceId : invoiceIdMirror ;
                    if (turns == 2 && mirror == 0) {
                        break;
                    }
                    if (turns == 1) {
                        listDatosDepositosBancariosBeans = dataDispersionBusiness.sp_Procesa_Cheques_Total_Interbancarios(keyBusiness, typePeriod, numberPeriod, yearPeriod);
                    } else {
                        listDatosDepositosBancariosBeans = dataDispersionBusiness.sp_Procesa_Cheques_Total_Interbancarios_Espejo(keyBusiness, typePeriod, numberPeriod, yearPeriod);
                    }
                    if (listDatosDepositosBancariosBeans.Count == 0) {
                        flagProsecutors = false;
                        string msjInterbanks = "SIN DEPOSITOS";
                    }
                    bankInterbank = datosEmpresaBeanDispersion.iBanco_id;
                    datoCuentaClienteBancoEmpresaBean = dataDispersionBusiness.sp_Cuenta_Cliente_Banco_Empresa(keyBusiness, bankInterbank);
                    // Genera nombre del pdf
                    if (turns == 1) {
                        fileNamePdfPM = "CHQ_NOMINAS_E" + keyBusiness.ToString() + "A" + string.Format("{0:00}", (yearPeriod % 100)) + "P" + string.Format("{0:000}", Convert.ToInt16(numberPeriod)) + "B" + string.Format("{0:000}", bankInterbank) + "_INTERBANCOS.PDF";
                    } else {
                        fileNamePdfPM = "CHQ_NOMINAS_E" + keyBusiness.ToString() + "A" + string.Format("{0:00}", (yearPeriod % 100)) + "P" + string.Format("{0:000}", Convert.ToInt16(numberPeriod)) + "B" + string.Format("{0:000}", bankInterbank) + "_INTERBANCOSESP.PDF";
                    }
                    // Genera el nombre de los archivos txt
                    if (turns == 1) {
                        if (bankInterbank == 72) {
                            fileNameTxtPM = "NOMINAS_" + "PAG" + string.Format("{0:000000}", Convert.ToInt32(datoCuentaClienteBancoEmpresaBean.iPlaza)) + "01.txt";
                        } else {
                            fileNameTxtPM = "NOMINAS_" + "E" + string.Format("{0:00}", keyBusiness.ToString()) + "A" + yearPeriod.ToString() + "P" + string.Format("{0:00}", Convert.ToInt16(numberPeriod)) + "B" + string.Format("{0:000}", bankInterbank) + "_INTERBANCOS.txt";
                        }
                    } else {
                        if (bankInterbank == 72) {
                            fileNameTxtPM = "NOMINAS_" + "PAG" + string.Format("{0:000000}", Convert.ToInt32(datoCuentaClienteBancoEmpresaBean.iPlaza)) + "01_ESP.txt";
                        } else {
                            fileNameTxtPM = "NOMINAS_" + "E" + string.Format("{0:00}", keyBusiness.ToString()) + "A" + yearPeriod.ToString() + "P" + string.Format("{0:00}", Convert.ToInt16(numberPeriod)) + "B" + string.Format("{0:000}", bankInterbank) + "_INTERBANCOSESP.txt";
                        }
                    }
                    // Ejecuta el sp que obtiene los datos de los depositos
                    if (turns == 1) {
                        listDatosProcesaChequesNominaBean = dataDispersionBusiness.sp_Procesa_Cheques_Interbancarios(keyBusiness, typePeriod, numberPeriod, yearPeriod);
                    } else {
                        listDatosProcesaChequesNominaBean = dataDispersionBusiness.sp_Procesa_Cheques_Interbancarios_Espejo(keyBusiness, typePeriod, numberPeriod, yearPeriod);
                    }
                    if (listDatosProcesaChequesNominaBean.Count > 0) {
                        FileStream fs = new FileStream(directoryTxt + @"\\" + nameFolder + @"\\" + fileNamePdfPM, FileMode.Create);
                        Document doc = new Document(iTextSharp.text.PageSize.LETTER, 20, 40, 20, 40);
                        PdfWriter pw = PdfWriter.GetInstance(doc, fs);
                        doc.AddTitle("Reporte de Dispersion");
                        doc.AddAuthor("");
                        doc.Open();

                        // Creamos el tipo de Font que vamos utilizar
                        iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                        // Escribimos el encabezamiento en el documento
                        Font fontDefault = new Font(FontFamily.HELVETICA, 10);
                        Paragraph pr = new Paragraph();
                        DateTime datePdf = DateTime.Now;
                        pr.Font = fontDefault;
                        pr.Add("Fecha: " + datePdf.ToString("yyyy-MM-dd") + " Periodo: " + numberPeriod.ToString());
                        pr.Alignment = Element.ALIGN_LEFT;
                        doc.Add(pr);
                        pr.Clear();
                        pr.Add("IPSNet \n Dépositos " + datosEmpresaBeanDispersion.sDescripcion);
                        pr.Alignment = Element.ALIGN_CENTER;
                        doc.Add(pr);
                        doc.Add(Chunk.NEWLINE);
                        pr.Clear();
                        pr.Add(datosEmpresaBeanDispersion.sNombreEmpresa + "\n" + datosEmpresaBeanDispersion.sRfc);
                        pr.Alignment = Element.ALIGN_CENTER;
                        doc.Add(pr);
                        doc.Add(Chunk.NEWLINE);
                        // Creamos una tabla que contendrá los datos
                        PdfPTable tblPrueba = new PdfPTable(4);
                        tblPrueba.WidthPercentage = 100;
                        PdfPCell clCtaCheques = new PdfPCell(new Phrase("Cta. Cheques", _standardFont));
                        clCtaCheques.BorderWidth = 0;
                        clCtaCheques.BorderWidthBottom = 0.75f;
                        clCtaCheques.Bottom = 80;
                        PdfPCell clBeneficiario = new PdfPCell(new Phrase("Beneficiario", _standardFont));
                        clBeneficiario.BorderWidth = 0;
                        clBeneficiario.BorderWidthBottom = 0.75f;
                        clBeneficiario.Bottom = 60;
                        PdfPCell clImporte = new PdfPCell(new Phrase("Importe", _standardFont));
                        clImporte.BorderWidth = 0;
                        clImporte.BorderWidthBottom = 0.75f;
                        clImporte.Bottom = 40;
                        PdfPCell clNomina = new PdfPCell(new Phrase("Nomina", _standardFont));
                        clNomina.BorderWidth = 0;
                        clNomina.BorderWidthBottom = 0.75f;
                        clNomina.Bottom = 20;
                        // Añadimos las celdas a la tabla
                        //tblPrueba.AddCell(consecutive);
                        tblPrueba.AddCell(clCtaCheques);
                        tblPrueba.AddCell(clBeneficiario);
                        tblPrueba.AddCell(clImporte);
                        tblPrueba.AddCell(clNomina);
                        int dc = 0;
                        double sumTotal = 0;
                        int cantidadEmpleados = 0;
                        double renglon1481 = 0;
                        foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                            dc += 1;
                            // INICIO CODIGO NUEVO (RESTA RENGLON 1481)
                            double restaImporte = 0;
                            double importeFinal = 0;
                            renglon1481 = dataDispersionBusiness.sp_Comprueba_Existencia_Renglon_Vales(keyBusiness,
                                    Convert.ToInt32(payroll.sNomina), numberPeriod, typePeriod, yearPeriod);
                            if (renglon1481 > 0) {
                                restaImporte = payroll.doImporte - renglon1481;
                                importeFinal = restaImporte;
                            } else {
                                importeFinal = payroll.doImporte;
                            }
                            // FIN CODIGO NUEVO
                            //sumTotal += payroll.doImporte;
                            sumTotal += importeFinal;
                            cantidadEmpleados += 1;
                            clCtaCheques = new PdfPCell(new Phrase(payroll.sCuenta, _standardFont));
                            clCtaCheques.BorderWidth = 0;
                            clCtaCheques.Bottom = 80;
                            clBeneficiario = new PdfPCell(new Phrase(payroll.sNombre + " " + payroll.sPaterno + " " + payroll.sMaterno, _standardFont));
                            clBeneficiario.BorderWidth = 0;
                            clBeneficiario.Bottom = 80;
                            //clImporte = new PdfPCell(new Phrase("$ " + Convert.ToDecimal(payroll.doImporte).ToString("#,##0.00"), _standardFont));
                            clImporte = new PdfPCell(new Phrase("$ " + Convert.ToDecimal(importeFinal).ToString("#,##0.00"), _standardFont));
                            clImporte.BorderWidth = 0;
                            clImporte.Bottom = 80;
                            clNomina = new PdfPCell(new Phrase(payroll.sNomina, _standardFont));
                            clNomina.BorderWidth = 0;
                            clNomina.Bottom = 80;
                            tblPrueba.AddCell(clCtaCheques);
                            tblPrueba.AddCell(clBeneficiario);
                            tblPrueba.AddCell(clImporte);
                            tblPrueba.AddCell(clNomina);
                        }
                        doc.Add(tblPrueba);
                        doc.Add(Chunk.NEWLINE);
                        iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        // Creamos una tabla que contendrá los datos
                        PdfPTable tblTotal = new PdfPTable(1);
                        tblTotal.WidthPercentage = 100;
                        PdfPCell clTotal = new PdfPCell(new Phrase("Total: " + "$ " + sumTotal.ToString("#,##0.00"), _standardFont2));
                        clTotal.BorderWidth = 0;
                        clTotal.Bottom = 80;
                        tblTotal.AddCell(clTotal);
                        doc.Add(tblTotal);
                        // Creamos la tabla del total de empleados
                        PdfPTable tblTotalEmpleados = new PdfPTable(1);
                        tblTotalEmpleados.WidthPercentage = 100;
                        PdfPCell clTotalEmpleados = new PdfPCell(new Phrase("Empleados: " + cantidadEmpleados.ToString() + " registros.", _standardFont2));
                        clTotalEmpleados.BorderWidth = 0;
                        clTotalEmpleados.Bottom = 80;
                        tblTotalEmpleados.AddCell(clTotalEmpleados);
                        doc.Add(tblTotalEmpleados);
                        doc.Close();
                        pw.Close();
                        if (bankInterbank == 14) {
                            // SANTANDER - ARCHIVO OK (INTERBANCARIO)
                            // - DETALLE - \\
                            string campoFijoIntSantanderD1 = "NOMINA";
                            string fillerIntSantanderD3 = "                                                                                                                            ";
                            if (tipPago == 2) {
                                campoFijoIntSantanderD1 = "HON";
                                fillerIntSantanderD3 = "                                                                                                                               ";
                            }
                            string numCuentaEmpresaSantanderD = datoCuentaClienteBancoEmpresaBean.sNumeroCuenta, fillerIntSantanderD1 = "     ", fillerIntSantanderD2 = "  ", sucursalIntSantanderD1 = "1001", plazaIntSantanderD1 = "01001";
                            int consecutivoIntSantanderD1 = 0;
                            using (StreamWriter fileIntSantander = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + fileNameTxtPM))
                            {
                                string espaciosNomEmpIntSantander = "", nombreEmpIntSantander = "", cerosImpIntSantander = "", cerosConIntSantander = "";
                                int longNomEmpIntSan = 40, longImpIntSan = 15, longConIntSan = 7;
                                foreach (DatosProcesaChequesNominaBean bank in listDatosProcesaChequesNominaBean) {
                                    consecutivoIntSantanderD1 += 1;
                                    string nameEmployee = bank.sNombre.Replace("Ñ","N") + " " + bank.sPaterno.Replace("Ñ", "N") + " " + bank.sMaterno.Replace("Ñ","N");
                                    if (nameEmployee.Length > 40) {
                                        nombreEmpIntSantander = nameEmployee.Substring(0, 39);
                                    } else {
                                        nombreEmpIntSantander = nameEmployee;
                                    }
                                    // INICIO CODIGO NUEVO (RESTA RENGLON 1481)
                                    double restaImporte = 0;
                                    string importeFinal = "";
                                    renglon1481 = dataDispersionBusiness.sp_Comprueba_Existencia_Renglon_Vales(keyBusiness,
                                            Convert.ToInt32(bank.sNomina), numberPeriod, typePeriod, yearPeriod);
                                    if (renglon1481 > 0) {
                                        restaImporte = bank.doImporte - renglon1481;
                                        importeFinal = restaImporte.ToString().Replace(".", "");
                                    } else {
                                        importeFinal = bank.dImporte.ToString();
                                    }
                                    // FIN CODIGO NUEVO
                                    string clave = bank.sCodigo;
                                    int longNomEmpIntSantanderResult = longNomEmpIntSan - nombreEmpIntSantander.Length;
                                    //int longImpIntSantanderResult = longImpIntSan - bank.dImporte.ToString().Length;
                                    int longImpIntSantanderResult = longImpIntSan - importeFinal.ToString().Length;
                                    int longConIntSantanderResult = longConIntSan - consecutivoIntSantanderD1.ToString().Length;
                                    for (var b = 0; b < longNomEmpIntSantanderResult; b++) { espaciosNomEmpIntSantander += " "; }
                                    for (var t = 0; t < longImpIntSantanderResult; t++) { cerosImpIntSantander += "0"; }
                                    for (var p = 0; p < longConIntSantanderResult; p++) { cerosConIntSantander += "0"; }
                                    // bank.dImporte
                                    fileIntSantander.Write(numCuentaEmpresaSantanderD + fillerIntSantanderD1 + bank.sCuenta + fillerIntSantanderD2 + clave + nombreEmpIntSantander + espaciosNomEmpIntSantander + sucursalIntSantanderD1 + cerosImpIntSantander + importeFinal + plazaIntSantanderD1 + campoFijoIntSantanderD1 + fillerIntSantanderD3 + cerosConIntSantander + consecutivoIntSantanderD1.ToString() + "\n");
                                    espaciosNomEmpIntSantander = "";
                                    cerosImpIntSantander = "";
                                    cerosConIntSantander = "";
                                }
                                fileIntSantander.Close();
                            }
                            // # [ FIN -> CREACION DE DISPERSION DE SANTANDER (INTERBANCARIO) ] * \\
                        }
                        
                        
                        
                        if (bankInterbank == 44) {
                            // SCOTIABANK -- ARCHIVO OK (INTERBANCARIO)
                            string tipoArchivoIntScotiabank = "EE", 
                                tipoRegistroIntScotiabank   = "HA", 
                                numeroContratoIntScotiabank = "", 
                                secuenciaIntScotiabank = "01",
                                fillerIntScotiabankHA1 = "                                                                                                                                                                                                                                                                                                                                                                       ";

                            if (keyBusiness == 8) {
                                numeroContratoIntScotiabank = "88178";
                            } else if (keyBusiness == 10) {
                                numeroContratoIntScotiabank = "47848";
                            } else if (keyBusiness == 7) {
                                numeroContratoIntScotiabank = "48426";
                            } else if (keyBusiness == 15) {
                                numeroContratoIntScotiabank = "85301";
                            } else {
                                numeroContratoIntScotiabank = "00000";
                            }

                            string headerLayoutAIntScotiabank = tipoArchivoIntScotiabank + tipoRegistroIntScotiabank + numeroContratoIntScotiabank + secuenciaIntScotiabank + fillerIntScotiabankHA1;
                            // - ENCABEZADO BLOQUE - \\
                            string tipoRegistroBIntScotiabank = "HB", 
                                monedaCuentaBIntScotiabank = "00", 
                                usoFuturoIntScotiabank = "00000", 
                                cuentaCargoIntScotiabank = datoCuentaClienteBancoEmpresaBean.sNumeroCuenta, 
                                referenciaEmpresaIntScotiabank = "0000000001", 
                                codigoStatusIntScotiabank = "000", 
                                fillerIntScotiabankHB1 = "                                                                                                                                                                                                                                                                                                                                                ";
                            string headerLayoutBIntScotiabank = tipoArchivoIntScotiabank + tipoRegistroBIntScotiabank + monedaCuentaBIntScotiabank + usoFuturoIntScotiabank + cuentaCargoIntScotiabank + referenciaEmpresaIntScotiabank + codigoStatusIntScotiabank + fillerIntScotiabankHB1;
                            // - DETALLE - \\
                            string fechaIntScotiabankD = dateGeneration.ToString("yyyyMMdd");
                            if (dateDisC != "") {
                                fechaIntScotiabankD = Convert.ToDateTime(dateDisC.ToString()).ToString("yyyyMMdd");
                            }
                            string conceptoPagoIntScotiabankD = "PAGO NOMINA";
                            if (tipPago == 2) {
                                conceptoPagoIntScotiabankD = "HONORARIOS ";
                            }
                            string tipoRegistroCIntScotiabankD = "DA", 
                                tipoPagoIntScotiabankD = "04", 
                                claveMonedaIntScotiabank = "00",  
                                servicioIntScotiabankD = "01", 
                                fillerIntScotiabankD1 = "                            ", 
                                plazaIntScotiabankD = "00000", 
                                sucursalIntScotiabankD = "00000", 
                                paisIntScotiabankD = "00000", 
                                fillerIntScotiabankD2 = "                                        ", 
                                tipoCuentaIntScotiabankD1 = "9", 
                                digitoIntScotiabankD1 = " ", 
                                bancoEmisorIntScotiabankD1 = "044", 
                                diasVigenciaIntScotiabankD = "001",  
                                fillerIntScotiabankD3 = "                                       ", 
                                fillerIntScotiabankD4 = "                                                            ", 
                                fillerIntScotiabankD5 = "                      ";
                            int consecutivoIntScotiabankD1 = 0;
                            // - CREACION DE LISTA PARA LLENAR EL DETALLE - \\
                            using (StreamWriter fileIntScotiabank = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + fileNameTxtPM)) {
                                fileIntScotiabank.Write(headerLayoutAIntScotiabank + "\n");
                                fileIntScotiabank.Write(headerLayoutBIntScotiabank + "\n");
                                string cerosImpIntScotiabankD    = "", 
                                    cerosNumNomIntScotiabankD    = "", 
                                    espaciosNomEmpIntScotiabankD = "",
                                    nombreEmpIntScotiabankD      = "",
                                    cerosConsecIntScotiabankD1   = "",
                                    cerosCtaCheIntScotiabankD1   = "", 
                                    cerosCodStaIntScotiabankD1   = "", 
                                    cerosTotMovIntScotiabank     = "",
                                    cerosImpTotIntScotiabank     = "";
                                int longImpIntScotiabankD     = 15,
                                    longNumNomIntScotiabankD  = 5, 
                                    longNomEmpIntScotiabankD  = 40,
                                    longConIntScotiabankD1    = 16, 
                                    longCtaCheIntScotiabankD  = 20, 
                                    longCodStaIntScotiabankD  = 25, 
                                    totalMoviIntScotiabank    = 0, 
                                    longTotMovIntScotiabank   = 7,
                                    importeTotalIntScotiabank = 0,
                                    longImpTotIntScotiabank   = 17;
                                foreach (DatosProcesaChequesNominaBean bank in listDatosProcesaChequesNominaBean) {
                                    int clvBank = bank.iIdBanco;
                                    string sufBank = "";
                                    if (clvBank.ToString().Length == 1) {
                                        sufBank = "00" + clvBank.ToString();
                                    } else if (clvBank.ToString().Length == 2) {
                                        sufBank = "0" + clvBank.ToString();
                                    } else {
                                        sufBank = clvBank.ToString();
                                    }
                                    string nameEmployee = bank.sPaterno + " " + bank.sMaterno + " " + bank.sNombre;
                                    if (nameEmployee.Length > 40) {
                                        nombreEmpIntScotiabankD = nameEmployee.Substring(0, 39);
                                    } else {
                                        nombreEmpIntScotiabankD = nameEmployee;
                                    }
                                    int filler28 = 28;
                                    string filler28F = "";
                                    int payrollEmp = bank.sNomina.ToString().Length;
                                    int longPayroll = 5;
                                    int accortPayroll = payrollEmp - 5;
                                    if (accortPayroll != 0) {
                                        int length28 = filler28 - accortPayroll;
                                        for (var v = 0; v < length28; v++) {
                                            filler28F += " ";
                                        }
                                    } else {
                                        filler28F = fillerIntScotiabankD1;
                                    }
                                    consecutivoIntScotiabankD1 += 1;
                                    totalMoviIntScotiabank += 1;
                                    // INICIO CODIGO NUEVO (RESTA RENGLON 1481)
                                    double restaImporte = 0;
                                    string importeFinal = "";
                                    renglon1481 = dataDispersionBusiness.sp_Comprueba_Existencia_Renglon_Vales(keyBusiness,
                                            Convert.ToInt32(bank.sNomina), numberPeriod, typePeriod, yearPeriod);
                                    if (renglon1481 > 0) {
                                        restaImporte = bank.doImporte - renglon1481;
                                        importeFinal = restaImporte.ToString().Replace(".", "");
                                    } else {
                                        importeFinal = bank.dImporte.ToString();
                                    }
                                    // FIN CODIGO NUEVO
                                    //importeTotalIntScotiabank += Convert.ToInt32(bank.dImporte);
                                    importeTotalIntScotiabank += Convert.ToInt32(importeFinal);
                                    //int longImpIntScotiabankDResult = longImpIntScotiabankD - bank.dImporte.ToString().Length;
                                    int longImpIntScotiabankDResult = longImpIntScotiabankD - importeFinal.ToString().Length;
                                    int longNumNomIntScotiabankDResult = longNumNomIntScotiabankD - bank.sNomina.Length;
                                    int longNomEmpIntScotiabankDResult = longNomEmpIntScotiabankD - nombreEmpIntScotiabankD.Length;
                                    int longConsecIntScotiabankDResult = longConIntScotiabankD1 - consecutivoIntScotiabankD1.ToString().Length;
                                    int longCtaCheIntScotiabankDResult = longCtaCheIntScotiabankD - bank.sCuenta.Length;
                                    int longCodStaIntScotiabankDResult = longCodStaIntScotiabankD - bank.sCuenta.Length;
                                    for (var q = 0; q < longImpIntScotiabankDResult; q++) { cerosImpIntScotiabankD += "0"; }
                                    for (var a = 0; a < longNumNomIntScotiabankDResult; a++) { cerosNumNomIntScotiabankD += "0"; }
                                    for (var u = 0; u < longNomEmpIntScotiabankDResult; u++) { espaciosNomEmpIntScotiabankD += " "; }
                                    for (var v = 0; v < longConsecIntScotiabankDResult; v++) { cerosConsecIntScotiabankD1 += "0"; }
                                    for (var r = 0; r < longCtaCheIntScotiabankDResult; r++) { cerosCtaCheIntScotiabankD1 += "0"; }
                                    for (var e = 0; e < longCodStaIntScotiabankDResult; e++) { cerosCodStaIntScotiabankD1 += "0"; }
                                    // bank.dImporte.ToString()
                                    fileIntScotiabank.Write(tipoArchivoIntScotiabank + tipoRegistroCIntScotiabankD + tipoPagoIntScotiabankD + claveMonedaIntScotiabank + cerosImpIntScotiabankD + importeFinal + fechaIntScotiabankD + servicioIntScotiabankD + cerosNumNomIntScotiabankD + bank.sNomina + filler28F + nameEmployee + espaciosNomEmpIntScotiabankD + cerosConsecIntScotiabankD1 + consecutivoIntScotiabankD1.ToString() + plazaIntScotiabankD + sucursalIntScotiabankD + cerosCtaCheIntScotiabankD1 + bank.sCuenta + paisIntScotiabankD + fillerIntScotiabankD2 + tipoCuentaIntScotiabankD1 + digitoIntScotiabankD1 + plazaIntScotiabankD + bancoEmisorIntScotiabankD1 + sufBank + diasVigenciaIntScotiabankD + conceptoPagoIntScotiabankD + fillerIntScotiabankD3 + fillerIntScotiabankD4 + cerosCodStaIntScotiabankD1 + bank.sCuenta + fillerIntScotiabankD5 + "\n");
                                    cerosImpIntScotiabankD = "";
                                    cerosNumNomIntScotiabankD = "";
                                    espaciosNomEmpIntScotiabankD = "";
                                    cerosConsecIntScotiabankD1 = "";
                                    cerosCtaCheIntScotiabankD1 = "";
                                    cerosCodStaIntScotiabankD1 = "";
                                }
                                // - TRAILER BLOQUE - \\
                                int longTotMovIntScotiabankResult = longTotMovIntScotiabank - totalMoviIntScotiabank.ToString().Length;
                                int longImpTotIntScotiabankResult = longImpTotIntScotiabank - importeTotalIntScotiabank.ToString().Length;
                                string tipoRegistroDIntScotiabank = "TB", tipoRegistroEIntScotiabank = "TA", cantidadMovIntScotiabank = "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", fillerIntScotiabankTB = "                                                                                                                           ";
                                for (var d = 0; d < longTotMovIntScotiabankResult; d++) { cerosTotMovIntScotiabank += "0"; }
                                for (var w = 0; w < longImpTotIntScotiabankResult; w++) { cerosImpTotIntScotiabank += "0"; }
                                string trailerBloqueIntScotiabank = tipoArchivoIntScotiabank + tipoRegistroDIntScotiabank + cerosTotMovIntScotiabank + totalMoviIntScotiabank.ToString() + cerosImpTotIntScotiabank + importeTotalIntScotiabank.ToString() + cantidadMovIntScotiabank + fillerIntScotiabankTB;
                                fileIntScotiabank.Write(trailerBloqueIntScotiabank + "\n");
                                string trailerArchivoIntScotiabank = tipoArchivoIntScotiabank + tipoRegistroEIntScotiabank + cerosTotMovIntScotiabank + totalMoviIntScotiabank.ToString() + cerosImpTotIntScotiabank + importeTotalIntScotiabank.ToString() + cantidadMovIntScotiabank + fillerIntScotiabankTB;
                                fileIntScotiabank.Write(trailerArchivoIntScotiabank + "\n");
                                // - TRAILER ARCHIVO - \\
                                fileIntScotiabank.Close();
                            }
                        }
                        
                        
                        
                        
                        // BANORTE -> INTERBANCARIO -> OK
                        if (bankInterbank == 72) {
                            
                            string tipoOperacion = "04";
                            string cuentaOrigen  = "";
                            string cuentaDestino = "";
                            string apartCeros1   = "00000000000";
                            string apartCeros2   = "0";
                            string apartCeros3   = "00";
                            string referenceDate = DateTime.Now.ToString("ddMMyyyy");
                            if (dateDisC != "") {
                                referenceDate = Convert.ToDateTime(dateDisC.ToString()).ToString("ddMMyyyy");
                            }
                            string descriptionPd = "PAGO NOMINA                   ";
                            if (tipPago == 2) {
                                descriptionPd = "HONORARIOS                    ";
                            }
                            string coinOrigin    = "1";
                            string coingDestiny  = "1";
                            string ivaBanorte    = "00000000000000";
                            string emailBusiness = "";
                            if (keyBusiness == 2074) {
                                emailBusiness = "asesoresimpasrh@gmail.com              ";
                            } else {
                                emailBusiness = "dgarcia@gruposeri.com                  ";
                            }
                            // Longitudes campos
                            int longNumberPayroll  = 13;
                            int longNumberADestiny = 10;
                            int longNumberImport   = 14;
                            int longNumberBRfc     = 13;
                            int longNumberEmail    = 70;
                            int longDetailsTotal   = 255;
                            using (StreamWriter fileIntBanorte = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + fileNameTxtPM, false, new ASCIIEncoding()))
                            {
                                foreach (DatosProcesaChequesNominaBean data in listDatosProcesaChequesNominaBean) {
                                    string nameEmployee = data.sNombre.TrimEnd() + " " + data.sPaterno.TrimEnd() + " " + data.sMaterno.TrimEnd();
                                    if (nameEmployee.Length > 70) {
                                        nameEmployee.Substring(0, 69);
                                    }
                                    string payroll = data.sNomina;
                                    int longPayroll = longNumberPayroll - payroll.Length;
                                    string accountOrigin = datoCuentaClienteBancoEmpresaBean.sNumeroCuenta;
                                    int longAcountOrigin = longNumberADestiny - accountOrigin.Length;
                                    string accountDestiny = data.sCuenta;
                                    if (accountDestiny.Length == 16) {
                                        accountDestiny = "00" + accountDestiny;
                                    }
                                    string importPaid = "";
                                    // INICIO CODIGO NUEVO (RESTA RENGLON 1481)
                                    double restaImporte = 0;
                                    string importeFinal = "";
                                    renglon1481 = dataDispersionBusiness.sp_Comprueba_Existencia_Renglon_Vales(keyBusiness,
                                            Convert.ToInt32(data.sNomina), numberPeriod, typePeriod, yearPeriod);
                                    if (renglon1481 > 0) {
                                        restaImporte = data.doImporte - renglon1481;
                                        importeFinal = restaImporte.ToString().Replace(".", "");
                                    } else {
                                        importeFinal = data.dImporte.ToString();
                                    }
                                    // FIN CODIGO NUEVO
                                    //int longImportPaid = longNumberImport - data.dImporte.ToString().Length;
                                    int longImportPaid = longNumberImport - importeFinal.ToString().Length;
                                    string rfcBusiness = datosEmpresaBeanDispersion.sRfc;
                                    int longRfcBusiness = longNumberBRfc - rfcBusiness.Length;
                                    int longEmailBusiness = longNumberEmail - emailBusiness.Length;
                                    for (var i = 0; i < longPayroll; i++) {
                                        payroll += " ";
                                    }
                                    for (var j = 0; j < longAcountOrigin; j++) {
                                        accountOrigin += "0";
                                    }
                                    for (var k = 0; k < longImportPaid; k++) {
                                        importPaid += "0";
                                    }
                                    for (var p = 0; p < longRfcBusiness; p++) {
                                        rfcBusiness += " ";
                                    }
                                    //importPaid += data.dImporte.ToString();
                                    importPaid += importeFinal.ToString();
                                    string fillerFinal = "";
                                    string cadenaFinal = "";
                                    // QUINCENALES CRISTINA 158 , 159
                                    if (keyBusiness == 2074 || keyBusiness == 2073 || keyBusiness == 2067 || keyBusiness == 158 || keyBusiness == 159) {
                                        cadenaFinal = tipoOperacion + payroll + "0000000000" + accountOrigin + apartCeros3 + accountDestiny + importPaid + apartCeros3 + referenceDate + descriptionPd + coinOrigin + coingDestiny + rfcBusiness + ivaBanorte + emailBusiness + referenceDate;
                                    } else {
                                        cadenaFinal = tipoOperacion + payroll + apartCeros1 + accountOrigin + apartCeros3 + accountDestiny + importPaid + apartCeros3 + referenceDate + descriptionPd + coinOrigin + coingDestiny + rfcBusiness + ivaBanorte + emailBusiness + referenceDate + nameEmployee;
                                    }
                                    int longFinalFiller = longDetailsTotal - cadenaFinal.Length;
                                    for (var x = 0; x < longFinalFiller; x++)
                                    {
                                        fillerFinal += " ";
                                    }
                                    if (keyBusiness == 2074 || keyBusiness == 2073 || keyBusiness == 2067 || keyBusiness == 158 || keyBusiness == 159) {
                                        fileIntBanorte.Write(tipoOperacion + payroll + "0000000000" + accountOrigin + apartCeros3 + accountDestiny + importPaid + apartCeros3 + referenceDate + descriptionPd + coinOrigin + coingDestiny + rfcBusiness + ivaBanorte + emailBusiness + referenceDate + fillerFinal + "\n");
                                    } else {
                                        fileIntBanorte.Write(tipoOperacion + payroll + apartCeros1 + accountOrigin + apartCeros3 + accountDestiny + importPaid + apartCeros3 + referenceDate + descriptionPd + coinOrigin + coingDestiny + rfcBusiness + ivaBanorte + emailBusiness + referenceDate + nameEmployee + fillerFinal + "\n");
                                    }
                                }
                                fileIntBanorte.Close();
                            }
                        }
                       
                        flag = true;
                    } 
                }
                if (flag) {
                    // CREACCION DEL ZIP CON LOS ARCHIVOS
                    FileStream stream = new FileStream(directoryZip + @"\\" + nameFolderYear + @"\\" + "INTERBANCARIOS" + @"\" + nameFolder + ".zip", FileMode.OpenOrCreate);
                    ZipArchive fileZip = new ZipArchive(stream, ZipArchiveMode.Create);
                    System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(directoryTxt + @"\\" + nameFolder);
                    FileInfo[] sourceFiles = directoryInfo.GetFiles();
                    foreach (FileInfo file in sourceFiles)
                    {
                        Stream sourceStream = file.OpenRead();
                        ZipArchiveEntry entry = fileZip.CreateEntry(file.Name);
                        Stream zipStream = entry.Open();
                        sourceStream.CopyTo(zipStream);
                        zipStream.Close();
                        sourceStream.Close();
                    }
                    ZipArchiveEntry zEntrys;
                    fileZip.Dispose();
                    stream.Close();
                    try
                    {
                        using (ZipArchive zipArchive = ZipFile.OpenRead(directoryZip + @"\\" + nameFolderYear + @"\\" + "INTERBANCARIOS" + @"\\" + nameFolder + ".zip"))
                        {
                            foreach (ZipArchiveEntry archiveEntry in zipArchive.Entries)
                            {
                                using (ZipArchive zipArchives = ZipFile.Open(directoryZip + @"\\" + nameFolderYear + @"\\" + "INTERBANCARIOS" + @"\\" + nameFolder + ".zip", ZipArchiveMode.Read))
                                {
                                    zEntrys = zipArchives.GetEntry(archiveEntry.ToString());
                                    nameFileError = zEntrys.Name;
                                    using (StreamReader read = new StreamReader(zEntrys.Open()))
                                    {
                                        if (read.ReadLine().Length > 0)
                                        {
                                            msgEstatusZip = "filesuccess";
                                        }
                                        else
                                        {
                                            msgEstatusZip = "fileerror";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (InvalidDataException ide)
                    {
                        Console.WriteLine(ide.Message.ToString() + " En el archivo : " + nameFileError);
                        msgEstatus = "fileError";
                    }
                    catch (Exception exc)
                    {
                        msgEstatus = exc.Message.ToString();
                    }
                    if (System.IO.File.Exists(directoryZip + @"\\" + nameFolderYear + @"\\" + "NOMINAS" + @"\\" + nameFolder + ".zip"))
                    {
                        msgEstatus = "success";
                    }
                    else
                    {
                        msgEstatus = "failed";
                    }
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Zip = nameFolder, EstadoZip = msgEstatusZip, Estado = msgEstatus, Anio = nameFolderYear });
        }

        [HttpPost]
        public JsonResult LoadGroupBusiness()
        {
            Boolean flag = false;
            String  messageError = "none";
            StringBuilder htmlTableBody = new StringBuilder();
            List<GroupBusinessDispersionBean> groupBusinesses = new List<GroupBusinessDispersionBean>();
            DataDispersionBusiness dataDispersion = new DataDispersionBusiness();
            try {
                groupBusinesses = dataDispersion.sp_Load_Group_Business_Dispersion();
                if (groupBusinesses.Count > 0) {
                    flag = true;
                    foreach (GroupBusinessDispersionBean data in groupBusinesses) {
                        htmlTableBody.Append(
                            "<tr><td> " + data.sNombreGrupo + " </td>" +
                            "<td> <button onclick='fViewBusinessGroup("+ data.iIdGrupoEmpresa +", \"" + data.sNombreGrupo + "\")' type='button' class='btn btn-success btn-sm btn-icon-split shadow'> <span class='icon text-white-50'><i class='fas fa-eye'></i></span> <span class='text'>Empresas</span> </button> " +
                            "<button onclick='fViewBanks(" + data.iIdGrupoEmpresa + ", \"" + data.sNombreGrupo + "\")' type='button' class='btn btn-success btn-sm btn-icon-split shadow'> <span class='icon text-white-50'><i class='fas fa-piggy-bank'></i></span> <span class='text'>Bancos</span> </button> " +
                            "</td></tr>");
                    }
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Html = htmlTableBody.ToString(), Datos = groupBusinesses });
        }

        /*
         *  hola este es un comentario bro 
         */

        [HttpPost]
        public JsonResult SaveNewGroupBusiness(string name)
        {
            Boolean flag = false;
            String  messageError = "none";
            GroupBusinessDispersionBean groupBusiness = new GroupBusinessDispersionBean();
            DataDispersionBusiness dataDispersion     = new DataDispersionBusiness();
            try {
                int keyUser = Convert.ToInt32(Session["iIdUsuario"].ToString());
                groupBusiness = dataDispersion.sp_Save_New_Group_Business_Dispersion(name.Trim(), keyUser);
                if (groupBusiness.sMensaje == "SUCCESS") {
                    flag = true;
                } else if (groupBusiness.sMensaje == "EXISTS") {
                    return Json(new { Bandera = false, Mensaje = "EXISTS" });
                } else {
                    return Json(new { Bandera = false, Mensaje = "ERROR" });
                }
            } catch (Exception exc) {
                messageError = exc.Message.ToString();
                flag = false;
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Mensaje = "SUCCESS" });
        } 

        [HttpPost]
        public JsonResult LoadBusinessNotGroup()
        {
            Boolean flag = false;
            String  messageError = "none";
            List<EmpresasBean> empresasBean = new List<EmpresasBean>();
            DataDispersionBusiness dataDispersion = new DataDispersionBusiness();
            try {
                empresasBean = dataDispersion.sp_Load_Business_Not_In_Groups_Dispersion();
                if (empresasBean.Count > 0) {
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = empresasBean });
        }

        [HttpPost]
        public JsonResult SaveAsignGroupBusiness(int group, int business)
        {
            Boolean flag = false;
            String  messageError = "none";
            GroupBusinessDispersionBean groupBusiness = new GroupBusinessDispersionBean();
            DataDispersionBusiness dataDispersion = new DataDispersionBusiness();
            try {
                groupBusiness = dataDispersion.sp_Save_Asign_Group_Business(group, business);
                if (groupBusiness.sMensaje == "INSERT") {
                    flag = true;
                } else if (groupBusiness.sMensaje == "NOTINSERT") {
                    return Json(new { Bandera = false, Mensaje = "NOTINSERT" });
                } else {
                    return Json(new { Bandera = false, Mensaje = "ERROR" });
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        [HttpPost]
        public JsonResult ViewBusinessGroup(int keyGroup)
        {
            Boolean flag = false;
            String messageError = "none";
            List<EmpresasBean> empresas = new List<EmpresasBean>();
            DataDispersionBusiness dataDispersion = new DataDispersionBusiness();
            try {
                empresas = dataDispersion.sp_View_Business_Group_Dispersion(keyGroup);
                if (empresas.Count > 0) {
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = empresas });
        }

        [HttpPost]
        public JsonResult RemoveBusinessGroup(int keyBusinessGroup)
        {
            Boolean flag          = false;
            String  messageError  = "none";
            EmpresasBean empresas = new EmpresasBean();
            DataDispersionBusiness dataDispersion = new DataDispersionBusiness();
            try {
                empresas = dataDispersion.sp_Remove_Business_Group(keyBusinessGroup);
                if (empresas.sMensaje == "SUCCESS") {
                    flag = true;
                } else if (empresas.sMensaje == "NOTDELETE") {
                    return Json(new { Bandera = false, MensajeError = "NOTDELETE" });
                } else {
                    return Json(new { Bandera = false, MensajeError = "ERROR" });
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        /*
         * Bancos dispersion especial
         */

        [HttpPost]
        public JsonResult ViewBanks(string key, int keyGroup)
        {
            Boolean flag         = false;
            String  messageError = "none";
            DataDispersionBusiness dataDispersion = new DataDispersionBusiness();
            List<BancosBean> bancosBean           = new List<BancosBean>();
            try {
                bancosBean = dataDispersion.sp_View_Banks_Group_Business_Dispersion(keyGroup);
                if (bancosBean.Count > 0) {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = bancosBean });
        }

        [HttpPost]
        public JsonResult ShowConfigBanks(int keyGroup, string type, string option)
        {
            Boolean flag = false;
            String  messageError = "none";
            List<BankInt> bankInts = new List<BankInt>();
            List<BancosBean> bancosBean = new List<BancosBean>();
            DataDispersionBusiness dataDispersion = new DataDispersionBusiness();
            try {
                bankInts = dataDispersion.sp_View_Config_Banks(keyGroup, type, option);
                bancosBean = dataDispersion.sp_View_Banks_Available_Dispersion(keyGroup, type, option, 0);
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = bankInts, Bancos = bancosBean });
        }

        [HttpPost]
        public JsonResult SaveBanksDSBank (int keyGroup, string type, int banorte, int santander, int scotiabank, int banamex, int bancomer)
        {
            Boolean flag         = false;
            String  messageError = "none";
            BancosBean bancosBean = new BancosBean();
            List<BankInt> bankInt = new List<BankInt>();
            DataDispersionBusiness dataDispersion = new DataDispersionBusiness();
            string typeSend = (type == "INT") ? "INTERBANCARIO" : "NOMINA";
            try { 
                bankInt.Add(new BankInt { sNombre = "BANORTE",    iBanco = 72, iActivo = banorte    });
                bankInt.Add(new BankInt { sNombre = "SANTANDER",  iBanco = 14, iActivo = santander  });
                bankInt.Add(new BankInt { sNombre = "SCOTIABANK", iBanco = 44, iActivo = scotiabank });
                if (typeSend == "NOMINA") {
                    bankInt.Add(new BankInt { sNombre = "BANAMEX",  iBanco = 2,  iActivo = banamex  });
                    bankInt.Add(new BankInt { sNombre = "BANCOMER", iBanco = 12, iActivo = bancomer });
                } 
                int keyUser = Convert.ToInt32(Session["iIdUsuario"].ToString());
                bancosBean = dataDispersion.sp_Save_Banks_Group_Interbank(keyGroup, keyUser, bankInt, typeSend);
                if (bancosBean.sMensaje == "SUCCESS") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        [HttpPost]
        public JsonResult ShowBanksConfigDetails(int keyGroup, string type, string option, int configuration)
        {
            Boolean flag = false;
            String  messageError = "none";
            List<BancosBean> bancosBean = new List<BancosBean>();
            DataDispersionBusiness dataDispersion = new DataDispersionBusiness();
            try {
                bancosBean = dataDispersion.sp_View_Banks_Available_Dispersion(keyGroup, type, option, configuration);
                if (bancosBean.Count > 0) {
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = bancosBean });
        }

        [HttpPost]
        public JsonResult SaveConfigDetails(int keyGroup, string type, int configurationId, int bank)
        {
            Boolean flag          = false;
            String  messageError  = "none";
            BancosBean bancosBean = new BancosBean();
            DataDispersionBusiness dataDispersion = new DataDispersionBusiness();
            try {
                int keyUser = Convert.ToInt32(Session["iIdUsuario"].ToString());
                bancosBean  = dataDispersion.sp_Save_Config_Details_Banks(keyGroup, type, configurationId, bank, keyUser);
                if (bancosBean.sMensaje == "success") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        [HttpPost]
        public JsonResult RemoveBankDetail (int keyConfigBank, int keyDetailConfig)
        {
            Boolean flag = false;
            String  messageError  = "none";
            BancosBean bancosBean = new BancosBean();
            DataDispersionBusiness dataDispersion = new DataDispersionBusiness();
            try {
                bancosBean = dataDispersion.sp_Remove_Bank_Details(keyDetailConfig, keyConfigBank);
                if (bancosBean.sMensaje == "success") {
                    flag = true;
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        [HttpPost]
        public JsonResult ConfigDataBank(int keyConfig)
        {
            Boolean flag = false;
            String  messageError = "none";
            DataDispersionBusiness dataDispersion          = new DataDispersionBusiness();
            DatosCuentaClienteBancoEmpresaBean datosCuenta = new DatosCuentaClienteBancoEmpresaBean();
            try {
                datosCuenta = dataDispersion.sp_View_Config_Data_Account_Bank(keyConfig);
                if (datosCuenta.sMensaje == "SUCCESS") {
                    flag = true;
                } else if (datosCuenta.sMensaje == "NOTFOUND") {
                    return Json(new { Bandera = flag, MensajeError = datosCuenta.sMensaje });
                } else {
                    return Json(new { Bandera = flag, MensajeError = datosCuenta.sMensaje });
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Datos = datosCuenta });
        }

        [HttpPost]
        public JsonResult SaveConfigDataBank(string nClient, string nAccount, string nClabe, string nSquare, int keyConfig)
        {
            Boolean flag         = false;
            String  messageError = "none";
            DataDispersionBusiness dataDispersion          = new DataDispersionBusiness();
            DatosCuentaClienteBancoEmpresaBean datosCuenta = new DatosCuentaClienteBancoEmpresaBean();
            try {
                datosCuenta = dataDispersion.sp_Save_Config_Data_Account_Bank(nClient, nAccount, nClabe, nSquare, keyConfig);
                if (datosCuenta.sMensaje == "SUCCESS") {
                    flag = true;
                } else {
                    return Json(new { Bandera = flag, MensajeError = datosCuenta.sMensaje });
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError });
        }

        public Boolean GenerateFoldersReports(string folderFather, string folderChild, string nameFile)
        {
            Boolean flag = false;
            string pathSaveFile = Server.MapPath("~/Content/");
            try
            {
                if (!Directory.Exists(pathSaveFile + folderFather))
                {
                    Directory.CreateDirectory(pathSaveFile + folderFather);
                }
                if (!Directory.Exists(pathSaveFile + folderFather + @"\\" + folderChild))
                {
                    Directory.CreateDirectory(pathSaveFile + folderFather + @"\\" + folderChild);
                }
                string pathComplete = pathSaveFile + folderFather + @"\\" + folderChild + @"\\" + nameFile;
                if (System.IO.File.Exists(pathComplete))
                {
                    System.IO.File.Delete(pathComplete);
                }
                flag = true;
            }
            catch (Exception exc)
            {
                flag = false;
            }
            return flag;
        }

        [HttpPost]
        public JsonResult GenerateReportDispersion(int yearPeriod, int numberPeriod, int typePeriod)
        {
            Boolean flag = false;
            String messageError = "none";
            DispersionSpecialDao dispersion = new DispersionSpecialDao();
            DispersionBean dispersionBean = new DispersionBean();
            string pathSaveFile = Server.MapPath("~/Content/");
            string nameFolder = "DISPERSION";
            string nameFolderRe = "NORMAL";
            string nameFileRepr = "DISPERSIONNM" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    int keyBusiness = Convert.ToInt32(Session["IdEmpresa"]);
                    dataTable = dispersion.sp_Reporte_Dispersion(keyBusiness, yearPeriod, typePeriod, numberPeriod);
                    columnsDataTable = dataTable.Columns.Count + 1;
                    rowsDataTable = dataTable.Rows.Count;
                    if (rowsDataTable > 0) {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (ExcelPackage excel = new ExcelPackage()) {
                            excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
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
                            flag = true;
                            excel.Dispose();
                        }
                    }
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, Archivo = nameFileRepr, Folder = nameFolderRe, Rows = rowsDataTable, Columns = columnsDataTable });
        }

    }
}