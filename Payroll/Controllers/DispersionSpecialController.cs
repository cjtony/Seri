using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using static iTextSharp.text.Font;
using System.IO;
using System.Text;
using System.IO.Compression;
using Payroll.Models.Daos;
using Payroll.Models.Beans;
using OfficeOpenXml;
using System.Data;
using OfficeOpenXml.Style;

namespace Payroll.Controllers
{
    public class DispersionSpecialController : Controller
    {
        // GET: DispersionSpecial
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public class DispersionSpecial
        {
            public Boolean bBandera      { get; set; }
            public String  sMensajeError { get; set; } 
            public string  sZip { get; set; }
            public string sEstadoZip { get; set; }
            public string sEstado { get; set; }
            public string sAnio { get; set; }
        }

        // Crea los folders necesarios
        public Boolean CreateFoldersToDeploy() {
            Boolean flag = false;
            string directoryTxt = Server.MapPath("/DispersionTXT").ToString();
            string directoryZip = Server.MapPath("/DispersionZIP").ToString();
            string nameFolderYear = DateTime.Now.Year.ToString();
            string nameFolderNom = "NOMINAS";
            string nameFolderInt = "INTERBANCARIOS";
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

        public DispersionSpecial DispersionPayroll(int group, int yearPeriod, int numberPeriod, int typePeriod, string dateDeposits, int mirror)
        {
            DispersionSpecial dispersionSpecials = new DispersionSpecial();
            Boolean flag            = false;
            Boolean flagMirror      = false;
            Boolean flagProsecutors = false;
            String messageError     = "none";
            DatosEmpresaBeanDispersion datosEmpresaBeanDispersion = new DatosEmpresaBeanDispersion();
            DataDispersionBusiness dataDispersionBusiness         = new DataDispersionBusiness();
            string nameFolder    = "";
            string nameFileError = "";
            DateTime dateGeneration     = DateTime.Now;
            string dateGenerationFormat = dateGeneration.ToString("MMddyyyy");
            string directoryZip   = Server.MapPath("/DispersionZIP").ToString();
            string directoryTxt   = Server.MapPath("/DispersionTXT").ToString();
            string nameFolderYear = DateTime.Now.Year.ToString();
            string msgEstatus     = "";
            string msgEstatusZip  = "";
            try {
                int keyBusiness  = int.Parse(Session["IdEmpresa"].ToString());
                int yearActually = DateTime.Now.Year;
                int typeReceipt  = (yearPeriod == yearActually) ? 1 : 0;
                int invoiceId    = yearPeriod * 100000 + typePeriod * 10000 + numberPeriod * 10;
                int invoiceIdMirror = yearPeriod * 100000 + typePeriod * 10000 + numberPeriod * 10 + 8;
                // Comprobar este StoredProcedure -> Adaptarlo al grupo que se selecciona
                datosEmpresaBeanDispersion = dataDispersionBusiness.sp_Datos_Empresa_Dispersion_Grupos(group);
                nameFolder = "DEPOSITOS_" + "E" + keyBusiness.ToString() + "P" + numberPeriod.ToString() + "A" + dateGeneration.ToString("yyyy-MM-dd");
                flagProsecutors = ProcessDepositsProsecutors(keyBusiness, invoiceId, typeReceipt, dateDeposits, yearPeriod, typePeriod, numberPeriod, datosEmpresaBeanDispersion.sNombreEmpresa, datosEmpresaBeanDispersion.sRfc, group);
                flagMirror = ProcessDepositsMirror(keyBusiness, invoiceId, typeReceipt, dateDeposits, yearPeriod, typePeriod, numberPeriod, datosEmpresaBeanDispersion.sNombreEmpresa, datosEmpresaBeanDispersion.sRfc, group);
                if (flagProsecutors == true || flagMirror == true) {
                    flag = true;
                }
                if (flag) {
                    // CREACCION DEL ZIP CON LOS ARCHIVOS
                    FileStream stream = new FileStream(directoryZip + @"\\" + nameFolderYear + @"\\" + "NOMINAS" + @"\" + nameFolder + ".zip", FileMode.OpenOrCreate);
                    ZipArchive fileZip = new ZipArchive(stream, ZipArchiveMode.Create);
                    System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(directoryTxt + @"\\" + nameFolderYear + @"\\" + "NOMINAS" + @"\\" + nameFolder);
                    FileInfo[] sourceFiles = directoryInfo.GetFiles();
                    foreach (FileInfo file in sourceFiles) {
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
                    try {
                        using (ZipArchive zipArchive = ZipFile.OpenRead(directoryZip + @"\\" + nameFolderYear + @"\\" + "NOMINAS" + @"\\" + nameFolder + ".zip")) {
                            foreach (ZipArchiveEntry archiveEntry in zipArchive.Entries) {
                                using (ZipArchive zipArchives = ZipFile.Open(directoryZip + @"\\" + nameFolderYear + @"\\" + "NOMINAS" + @"\\" + nameFolder + ".zip", ZipArchiveMode.Read)) {
                                    zEntrys = zipArchives.GetEntry(archiveEntry.ToString());
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
                flag         = false;
                messageError = exc.Message.ToString();
            }
            dispersionSpecials.bBandera      = flag;
            dispersionSpecials.sMensajeError = messageError;
            dispersionSpecials.sZip          = nameFolder;
            dispersionSpecials.sEstadoZip    = msgEstatusZip;
            dispersionSpecials.sEstado       = msgEstatus;
            dispersionSpecials.sAnio         = nameFolderYear;
            return dispersionSpecials;
        }

        public double Truncate(double value, int decimales)
        {
            double aux_value = Math.Pow(10, decimales);
            return (Math.Truncate(value * aux_value) / aux_value);
        }

        public Boolean ProcessDepositsProsecutors(int keyBusiness, int invoiceId, int typeReceipt, string dateDeposits, int yearPeriod, int typePeriod, int numberPeriod, string nameBusiness, string rfcBusiness, int group)
        {
            Boolean flag    = false;
            int k;
            int bankResult   = 0;
            string nameBankResult = "";
            string fileNamePDF    = "";
            string vFileName      = "";
            List<DataErrorAccountBank> dataErrors = new List<DataErrorAccountBank>();
            List<DatosDepositosBancariosBean> listDatosDepositosBancariosBeans    = new List<DatosDepositosBancariosBean>();
            DataDispersionBusiness dataDispersionBusiness                         = new DataDispersionBusiness();
            List<DatosProcesaChequesNominaBean> listDatosProcesaChequesNominaBean = new List<DatosProcesaChequesNominaBean>();
            DatosCuentaClienteBancoEmpresaBean datoCuentaClienteBancoEmpresaBean  = new DatosCuentaClienteBancoEmpresaBean();
            DispersionSpecialDao dispersionSpecialDao = new DispersionSpecialDao();
            List<BancosBean> bancosBeans              = new List<BancosBean>();
            List<EmpresasBean> empresas               = new List<EmpresasBean>();
            try {
                bancosBeans = dispersionSpecialDao.sp_Select_Config_Banks("NOMINA", 1, group);
                if (bancosBeans.Count == 0) {
                    return flag;
                }
                Boolean createFolders = CreateFoldersToDeploy();
                foreach (BancosBean bean in bancosBeans) {
                    bankResult     = bean.iIdBanco;
                    nameBankResult = bean.sNombreBanco;
                    listDatosDepositosBancariosBeans  = dispersionSpecialDao.sp_Procesa_Cheques_Total_Nomina_Special(bean.iConfiguracion, typePeriod, numberPeriod, yearPeriod, bean.iGrupoId, 0, bankResult);
                    listDatosProcesaChequesNominaBean = dispersionSpecialDao.sp_Procesa_Cheques_Nomina_Special(bean.iGrupoId, bean.iConfiguracion, bean.iIdBanco, 0, yearPeriod, typePeriod, numberPeriod);
                    if (listDatosProcesaChequesNominaBean.Count > 0) {
                        DateTime dateGeneration     = DateTime.Now;
                        string dateGenerationFormat = dateGeneration.ToString("MMddyyyy");
                        //-----------
                        string nameFolder = "DEPOSITOS_" + "E" + keyBusiness.ToString() + "P" + numberPeriod.ToString() + "A" + dateGeneration.ToString("yyyy-MM-dd");
                        //-----------
                        fileNamePDF = "CHQ_NOMINAS_E" + keyBusiness.ToString() + "A" + string.Format("{0:00}", (yearPeriod % 100)) + "P" + string.Format("{0:00}", numberPeriod) + "_B" + bankResult.ToString() + ".PDF";
                        string directoryTxt = Server.MapPath("/DispersionTXT/" + DateTime.Now.Year.ToString()).ToString() + "/NOMINAS/";
                        if (!System.IO.Directory.Exists(directoryTxt + @"\\" + nameFolder)) {
                            System.IO.Directory.CreateDirectory(directoryTxt + @"\\" + nameFolder);
                        }
                        if (bankResult == 72) {
                            vFileName = "NOMINAS_NI" + string.Format("{0:00000}", Convert.ToInt32(datoCuentaClienteBancoEmpresaBean.sNumeroCliente)) + "01";
                        } else {
                            vFileName = "E" + string.Format("{0:000}", keyBusiness.ToString()) + "A" + yearPeriod + yearPeriod.ToString() + "P" + string.Format("{0:000}", numberPeriod.ToString()) + "_B" + bankResult.ToString();
                        }

                        // BANAMEX -> NOMINA
                        if (bankResult == 2)
                        {
                            DateTime dateC = dateGeneration;
                            // ENCABEZADO  --> ARCHIVO OK
                            string tipoRegistroBanamexE = "1";
                            string numeroClienteBanamexE = "000" + datoCuentaClienteBancoEmpresaBean.sNumeroCliente;
                            string fechaBanamexE = dateC.ToString("ddMM") + dateC.ToString("yyyy").Substring(2, 2);
                            string valorFijoBanamex0 = "0001";
                            string nombreEmpresaBanamex = "";
                            if (nameBusiness.Length > 35)
                            {
                                nombreEmpresaBanamex = nameBusiness.Substring(0, 35);
                            }
                            else
                            {
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
                            foreach (DatosDepositosBancariosBean deposits in listDatosDepositosBancariosBeans)
                            {
                                if (deposits.iIdBanco == bankResult)
                                {
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
                            using (StreamWriter fileBanamex = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".txt", false, Encoding.UTF8))
                            {
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
                                foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean)
                                {
                                    if (payroll.iIdBanco == bankResult)
                                    {
                                        int longAcortAccount = payroll.sCuenta.Length;
                                        string accountUser = payroll.sCuenta;
                                        string formatAccount = "";
                                        if (longAcortAccount == 18)
                                        {
                                            string formatAccountSubstring = accountUser.Substring(0, longAcortAccount - 1);
                                            formatAccount = formatAccountSubstring.Substring(6, 11);
                                        }
                                        else
                                        {
                                            formatAccount = payroll.sCuenta;
                                        }
                                        string nameEmployee = payroll.sNombre + " " + payroll.sPaterno + " " + payroll.sMaterno;
                                        cantidadMovBanamexT += 1;
                                        sumaImpTotBanamexT += Convert.ToInt32(payroll.dImporte);
                                        string nombreCEmp = "";
                                        if (nameEmployee.Length > 57)
                                        {
                                            nombreCEmp = nameEmployee.Substring(0, 54);
                                        }
                                        else
                                        {
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
                        if (bankResult == 21)
                        {
                            // FOREACH DATOS TOTALES
                            double totalAmountHSBC = 0;
                            int hQuantityDeposits = 0;
                            foreach (DatosProcesaChequesNominaBean deposits in listDatosProcesaChequesNominaBean)
                            {
                                if (deposits.iIdBanco == bankResult)
                                {
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
                            string hSpaceWhite1 = "";
                            string hReferenceAlpa = "PAGONOM" + numberPeriod + "QFEB";
                            header = hValuePermanent1 + "," + hNivelAuthorization + "," + hReferenceNumber + "," + hTotalAmount + "," + hQuantityDeposits.ToString() + "," + hDateActually + "," + hSpaceWhite1 + "," + hReferenceAlpa;
                            stream.WriteLine(header);
                            foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean)
                            {
                                if (payroll.iIdBanco == bankResult)
                                {
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
                                    }
                                    else if (longAcortAccount == 9)
                                    {
                                        finallyAccount = "0" + payroll.sCuenta;
                                    }
                                    else
                                    {
                                        dataErrors.Add(
                                                new DataErrorAccountBank
                                                {
                                                    sBanco = "HSBC",
                                                    sCuenta = payroll.sCuenta,
                                                    sNomina = payroll.sNomina
                                                });
                                    }
                                    string amount = payroll.doImporte.ToString();
                                    string nameBen = payroll.sNombre.TrimEnd() + " " + payroll.sPaterno.TrimEnd();
                                    header = finallyAccount + "," + amount + "," + hReferenceAlpa + "," + nameBen;
                                    stream.WriteLine(header);
                                }
                            }
                            stream.Close();
                        }

                        // ARCHIVO TXT PARA SANTANDER -> NOMINA

                        if (bankResult == 14)
                        {
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
                            using (StreamWriter fileTxt = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".txt", false, Encoding.UTF8))
                            {
                                fileTxt.Write(headerLayout + "\n");
                                string spaceGenerate1 = "", spaceGenerate2 = "", spaceGenerate3 = "", numberCeroGene = "", consec1Generat = "", numberNomGener = "", totGenerate = "";
                                int longc = 5, long0 = 7, long1 = 30, long2 = 20, long3 = 30, long4 = 18, consecutiveInit = initConsecutiveNbOneN, resultSumTot = 0, longTot = 18;
                                int totalRecords = 0;
                                double totalAmount = 0;
                                foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean)
                                {
                                    if (payroll.iIdBanco == bankResult)
                                    {
                                        totalRecords += 1;
                                        totalAmount += payroll.doImporte;
                                        int longAcortAccount = payroll.sCuenta.Length;
                                        string finallyAccount = payroll.sCuenta;
                                        if (longAcortAccount == 18)
                                        {
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
                                        }
                                        else if (longAcortAccount == 9)
                                        {
                                            finallyAccount = "0" + payroll.sCuenta;
                                        }
                                        else
                                        {
                                            dataErrors.Add(
                                                    new DataErrorAccountBank
                                                    {
                                                        sBanco = "Santander",
                                                        sCuenta = payroll.sCuenta,
                                                        sNomina = payroll.sNomina
                                                    });
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
                                        fileTxt.Write(typeRegisterD + consec1Generat + consecutiveInit.ToString() + numberNomGener + payroll.sNomina + payroll.sPaterno + spaceGenerate1 + payroll.sMaterno + spaceGenerate2 + payroll.sNombre + spaceGenerate3 + finallyAccount + "     " + numberCeroGene + payroll.dImporte.ToString() + "\n");
                                        consec1Generat = ""; numberNomGener = "";
                                        spaceGenerate1 = ""; spaceGenerate2 = "";
                                        spaceGenerate3 = ""; numberCeroGene = "";
                                    }
                                }
                                if (bankResult == 14)
                                {
                                    int longTotGenerate = longTot - resultSumTot.ToString().Length;
                                    for (var j = 0; j < longTotGenerate; j++) { totGenerate += "0"; }
                                    int long1TotGenert = longc - (consecutiveInit + 1).ToString().Length;
                                    for (var h = 0; h < long1TotGenert; h++) { consec1Generat += "0"; }
                                    int longTotalR = 5;
                                    int resultLTR = longTotalR - totalRecords.ToString().Length;
                                    string cerosTotalRecords = "";
                                    for (var x = 0; x < resultLTR; x++)
                                    {
                                        cerosTotalRecords += "0";
                                    }
                                    string totLayout = "3" + consec1Generat + (consecutiveInit + 1).ToString() + cerosTotalRecords + totalRecords.ToString() + totGenerate + resultSumTot.ToString();
                                    fileTxt.Write(totLayout + "\n");
                                }
                                fileTxt.Close();
                            }
                        }


                        // ARCHIVO DISPERSION BANORTE -> NOMINA -> OK

                        if (bankResult == 72)
                        {

                            // INICIO CODIGO NUEVO

                            long[] TotIAbonos = new long[201];
                            for (k = 0; k <= 200; k++)
                            {
                                TotIAbonos[k] = 0;
                            }
                            long TotalNumAbonos = 0;
                            foreach (DatosProcesaChequesNominaBean bank in listDatosProcesaChequesNominaBean)
                            {
                                if (bankResult == bank.iIdBanco)
                                {
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
                            foreach (DatosDepositosBancariosBean deposits in listDatosDepositosBancariosBeans)
                            {
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
                            foreach (DatosProcesaChequesNominaBean bank in listDatosProcesaChequesNominaBean)
                            {
                                if (bankResult == bank.iIdBanco)
                                {
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
                            using (StreamWriter fileBanorte = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".txt", false, Encoding.UTF8))
                            {
                                fileBanorte.Write(headerLayoutBanorte + "\n");
                                string generaCNumEmpleadoB = "", generaCNumImporteB = "", generaCNumCuentaB = "";
                                int longNumEmpleado = 10, longNumImporte = 15, longNumCuenta = 18;
                                foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean)
                                {
                                    if (payroll.iIdBanco == bankResult)
                                    {
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
                                        int longNumCta = longNumCuenta - payroll.sCuenta.Length;
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
                        pr.Add("Fecha: " + datePdf.ToString("yyyy-MM-dd"));
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
                        PdfPTable tblPrueba = new PdfPTable(4);
                        tblPrueba.WidthPercentage = 100;
                        // Configuramos el título de las columnas de la tabla
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
                        tblPrueba.AddCell(clCtaCheques);
                        tblPrueba.AddCell(clBeneficiario);
                        tblPrueba.AddCell(clImporte);
                        tblPrueba.AddCell(clNomina);
                        double sumTotal = 0;
                        foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                            // Llenamos la tabla con información
                            sumTotal += payroll.doImporte;
                            clCtaCheques = new PdfPCell(new Phrase(payroll.sCuenta, _standardFont));
                            clCtaCheques.BorderWidth = 0;
                            clCtaCheques.Bottom = 80;
                            clBeneficiario = new PdfPCell(new Phrase(payroll.sNombre + " " + payroll.sPaterno + " " + payroll.sMaterno, _standardFont));
                            clBeneficiario.BorderWidth = 0;
                            clBeneficiario.Bottom = 80;
                            clImporte = new PdfPCell(new Phrase("$" + Convert.ToDecimal(payroll.doImporte).ToString("#, ##"), _standardFont));
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
                        doc.Add(tblPrueba);
                        doc.Add(Chunk.NEWLINE);
                        iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        // Creamos una tabla que contendrá los datos
                        PdfPTable tblTotal = new PdfPTable(2);
                        tblTotal.WidthPercentage = 100;
                        PdfPCell clTotal = new PdfPCell(new Phrase("Total: ", _standardFont2));
                        clTotal.BorderWidth = 0;
                        clTotal.Bottom = 80;
                        PdfPCell clImporteTotal = new PdfPCell(new Phrase("$" + sumTotal.ToString("#,##0.00"), _standardFont2));
                        clImporteTotal.BorderWidth = 0;
                        clImporteTotal.Bottom = 80;
                        tblTotal.AddCell(clTotal);
                        tblTotal.AddCell(clImporteTotal);
                        doc.Add(tblTotal);
                        doc.Close();
                        pw.Close();
                        flag = true;
                    }
                }
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
                flag = false;
            }
            return flag;
        }

        public Boolean ProcessDepositsMirror(int keyBusiness, int invoiceId, int typeReceipt, string dateDeposits, int yearPeriod, int typePeriod, int numberPeriod, string nameBusiness, string rfcBusiness, int group)
        {
            Boolean flag = false;
            int k;
            int bankResult = 0;
            string nameBankResult = "";
            string fileNamePDF = "";
            string vFileName = "";
            List<DataErrorAccountBank> dataErrors = new List<DataErrorAccountBank>();
            List<DatosDepositosBancariosBean> listDatosDepositosBancariosBeans    = new List<DatosDepositosBancariosBean>();
            DataDispersionBusiness dataDispersionBusiness                         = new DataDispersionBusiness();
            List<DatosProcesaChequesNominaBean> listDatosProcesaChequesNominaBean = new List<DatosProcesaChequesNominaBean>();
            DatosCuentaClienteBancoEmpresaBean datoCuentaClienteBancoEmpresaBean  = new DatosCuentaClienteBancoEmpresaBean();
            DispersionSpecialDao dispersionSpecialDao = new DispersionSpecialDao();
            List<BancosBean> bancosBeans = new List<BancosBean>();
            List<EmpresasBean> empresas  = new List<EmpresasBean>();
            try {
                bancosBeans = dispersionSpecialDao.sp_Select_Config_Banks("NOMINA", 1, group);
                if (bancosBeans.Count == 0) {
                    return flag;
                }
                Boolean createFolders = CreateFoldersToDeploy();
                foreach (BancosBean bean in bancosBeans) {
                    bankResult     = bean.iIdBanco;
                    nameBankResult = bean.sNombreBanco;
                    listDatosDepositosBancariosBeans = dispersionSpecialDao.sp_Procesa_Cheques_Total_Nomina_Special(bean.iConfiguracion, typePeriod, numberPeriod, yearPeriod, bean.iGrupoId, 1, bankResult);
                    listDatosProcesaChequesNominaBean = dispersionSpecialDao.sp_Procesa_Cheques_Nomina_Special(bean.iGrupoId, bean.iConfiguracion, bean.iIdBanco, 1, yearPeriod, typePeriod, numberPeriod);
                    if (listDatosProcesaChequesNominaBean.Count > 0) {
                        DateTime dateGeneration = DateTime.Now;
                        string dateGenerationFormat = dateGeneration.ToString("MMddyyyy");
                        //-----------
                        string nameFolder = "DEPOSITOS_" + "E" + keyBusiness.ToString() + "P" + numberPeriod.ToString() + "A" + dateGeneration.ToString("yyyy-MM-dd");
                        //-----------
                        fileNamePDF = "CHQ_NOMINAS_E" + keyBusiness.ToString() + "A" + string.Format("{0:00}", (yearPeriod % 100)) + "P" + string.Format("{0:00}", numberPeriod) + "_BE_" + bankResult.ToString() + ".PDF";
                        // -------------------------
                        string directoryTxt = Server.MapPath("/DispersionTXT/" + DateTime.Now.Year.ToString()).ToString() + "/NOMINAS/";
                        // -------------------------
                        if (!System.IO.Directory.Exists(directoryTxt + @"\\" + nameFolder))
                        {
                            System.IO.Directory.CreateDirectory(directoryTxt + @"\\" + nameFolder);
                        }
                        // -------------------------
                        if (bankResult == 72)  {
                            vFileName = "NOMINAS_NI" + string.Format("{0:00000}", Convert.ToInt32(datoCuentaClienteBancoEmpresaBean.sNumeroCliente)) + "01_ESP";
                        } else {
                            vFileName = "E" + string.Format("{0:000}", keyBusiness.ToString()) + "A" + yearPeriod + yearPeriod.ToString() + "P" + string.Format("{0:000}", numberPeriod.ToString()) + "_BE_" + bankResult.ToString();
                        }

                        // BANAMEX -> NOMINA
                        if (bankResult == 2)
                        {
                            DateTime dateC = dateGeneration;
                            // ENCABEZADO  --> ARCHIVO OK
                            string tipoRegistroBanamexE = "1";
                            string numeroClienteBanamexE = "000" + datoCuentaClienteBancoEmpresaBean.sNumeroCliente;
                            string fechaBanamexE = dateC.ToString("ddMM") + dateC.ToString("yyyy").Substring(2, 2);
                            string valorFijoBanamex0 = "0001";
                            string nombreEmpresaBanamex = "";
                            if (nameBusiness.Length > 35)
                            {
                                nombreEmpresaBanamex = nameBusiness.Substring(0, 35);
                            }
                            else
                            {
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
                            foreach (DatosDepositosBancariosBean deposits in listDatosDepositosBancariosBeans)
                            {
                                if (deposits.iIdBanco == bankResult)
                                {
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
                            using (StreamWriter fileBanamex = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".txt", false, Encoding.UTF8))
                            {
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
                                foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean)
                                {
                                    if (payroll.iIdBanco == bankResult)
                                    {
                                        int longAcortAccount = payroll.sCuenta.Length;
                                        string accountUser = payroll.sCuenta;
                                        string formatAccount = "";
                                        if (longAcortAccount == 18)
                                        {
                                            string formatAccountSubstring = accountUser.Substring(0, longAcortAccount - 1);
                                            formatAccount = formatAccountSubstring.Substring(6, 11);
                                        }
                                        else
                                        {
                                            formatAccount = payroll.sCuenta;
                                        }
                                        string nameEmployee = payroll.sNombre + " " + payroll.sPaterno + " " + payroll.sMaterno;
                                        cantidadMovBanamexT += 1;
                                        sumaImpTotBanamexT += Convert.ToInt32(payroll.dImporte);
                                        string nombreCEmp = "";
                                        if (nameEmployee.Length > 57)
                                        {
                                            nombreCEmp = nameEmployee.Substring(0, 54);
                                        }
                                        else
                                        {
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
                        if (bankResult == 21)
                        {
                            // FOREACH DATOS TOTALES
                            double totalAmountHSBC = 0;
                            int hQuantityDeposits = 0;
                            foreach (DatosProcesaChequesNominaBean deposits in listDatosProcesaChequesNominaBean)
                            {
                                if (deposits.iIdBanco == bankResult)
                                {
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
                            string hSpaceWhite1 = "";
                            string hReferenceAlpa = "PAGONOM" + numberPeriod + "QFEB";
                            header = hValuePermanent1 + "," + hNivelAuthorization + "," + hReferenceNumber + "," + hTotalAmount + "," + hQuantityDeposits.ToString() + "," + hDateActually + "," + hSpaceWhite1 + "," + hReferenceAlpa;
                            stream.WriteLine(header);
                            foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean)
                            {
                                if (payroll.iIdBanco == bankResult)
                                {
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
                                    }
                                    else if (longAcortAccount == 9)
                                    {
                                        finallyAccount = "0" + payroll.sCuenta;
                                    }
                                    else
                                    {
                                        dataErrors.Add(new DataErrorAccountBank { sBanco = "HSBC", sCuenta = payroll.sCuenta, sNomina = payroll.sNomina });
                                    }
                                    string amount = payroll.doImporte.ToString();
                                    string nameBen = payroll.sNombre.TrimEnd() + " " + payroll.sPaterno.TrimEnd();
                                    header = finallyAccount + "," + amount + "," + hReferenceAlpa + "," + nameBen;
                                    stream.WriteLine(header);
                                }
                            }
                            stream.Close();
                        }

                        // ARCHIVO TXT PARA SANTANDER -> NOMINA

                        if (bankResult == 14)
                        {
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
                            using (StreamWriter fileTxt = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".txt", false, Encoding.UTF8))
                            {
                                fileTxt.Write(headerLayout + "\n");
                                string spaceGenerate1 = "", spaceGenerate2 = "", spaceGenerate3 = "", numberCeroGene = "", consec1Generat = "", numberNomGener = "", totGenerate = "";
                                int longc = 5, long0 = 7, long1 = 30, long2 = 20, long3 = 30, long4 = 18, consecutiveInit = initConsecutiveNbOneN, resultSumTot = 0, longTot = 18;
                                int totalRecords = 0;
                                double totalAmount = 0;
                                foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean)
                                {
                                    if (payroll.iIdBanco == bankResult)
                                    {
                                        totalRecords += 1;
                                        totalAmount += payroll.doImporte;
                                        int longAcortAccount = payroll.sCuenta.Length;
                                        string finallyAccount = payroll.sCuenta;
                                        if (longAcortAccount == 18)
                                        {
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
                                        }
                                        else if (longAcortAccount == 9)
                                        {
                                            finallyAccount = "0" + payroll.sCuenta;
                                        }
                                        else
                                        {
                                            dataErrors.Add(new DataErrorAccountBank { sBanco = "Santander", sCuenta = payroll.sCuenta, sNomina = payroll.sNomina });
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
                                        fileTxt.Write(typeRegisterD + consec1Generat + consecutiveInit.ToString() + numberNomGener + payroll.sNomina + payroll.sPaterno + spaceGenerate1 + payroll.sMaterno + spaceGenerate2 + payroll.sNombre + spaceGenerate3 + finallyAccount + "     " + numberCeroGene + payroll.dImporte.ToString() + "\n");
                                        consec1Generat = ""; numberNomGener = "";
                                        spaceGenerate1 = ""; spaceGenerate2 = "";
                                        spaceGenerate3 = ""; numberCeroGene = "";
                                    }
                                }
                                if (bankResult == 14)
                                {
                                    int longTotGenerate = longTot - resultSumTot.ToString().Length;
                                    for (var j = 0; j < longTotGenerate; j++) { totGenerate += "0"; }
                                    int long1TotGenert = longc - (consecutiveInit + 1).ToString().Length;
                                    for (var h = 0; h < long1TotGenert; h++) { consec1Generat += "0"; }
                                    int longTotalR = 5;
                                    int resultLTR = longTotalR - totalRecords.ToString().Length;
                                    string cerosTotalRecords = "";
                                    for (var x = 0; x < resultLTR; x++)
                                    {
                                        cerosTotalRecords += "0";
                                    }
                                    string totLayout = "3" + consec1Generat + (consecutiveInit + 1).ToString() + cerosTotalRecords + totalRecords.ToString() + totGenerate + resultSumTot.ToString();
                                    fileTxt.Write(totLayout + "\n");
                                }
                                fileTxt.Close();
                            }
                        }

                        // ARCHIVO DISPERSION BANORTE -> NOMINA -> OK

                        if (bankResult == 72)
                        {

                            // INICIO CODIGO NUEVO

                            long[] TotIAbonos = new long[201];
                            for (k = 0; k <= 200; k++)
                            {
                                TotIAbonos[k] = 0;
                            }
                            long TotalNumAbonos = 0;
                            foreach (DatosProcesaChequesNominaBean bank in listDatosProcesaChequesNominaBean)
                            {
                                if (bankResult == bank.iIdBanco)
                                {
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
                            foreach (DatosDepositosBancariosBean deposits in listDatosDepositosBancariosBeans)
                            {
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
                            foreach (DatosProcesaChequesNominaBean bank in listDatosProcesaChequesNominaBean)
                            {
                                if (bankResult == bank.iIdBanco)
                                {
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
                            using (StreamWriter fileBanorte = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".txt", false, Encoding.UTF8))
                            {
                                fileBanorte.Write(headerLayoutBanorte + "\n");
                                string generaCNumEmpleadoB = "", generaCNumImporteB = "", generaCNumCuentaB = "";
                                int longNumEmpleado = 10, longNumImporte = 15, longNumCuenta = 18;
                                foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean)
                                {
                                    if (payroll.iIdBanco == bankResult)
                                    {
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
                                        int longNumCta = longNumCuenta - payroll.sCuenta.Length;
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

                        if (bankResult == 12)
                        {
                            // Inicio Codigo Nuevo
                            string pathSaveFile = Server.MapPath("~/Content/");
                            string routeTXTBancomer = pathSaveFile + "DISPERSION" + @"\\" + "BANCOMER" + @"\\" + "BANCOMER.txt";
                            string pathCompleteTXT = directoryTxt + @"\\" + nameFolder + @"\\" + vFileName + ".txt";
                            StringBuilder sb1;
                            //System.IO.File.Copy(routeTXTBancomer, pathCompleteTXT, true);
                            int totalRegistros = 0;
                            string V = "";
                            using (StreamWriter fileBancomer = new StreamWriter(pathCompleteTXT, false, new ASCIIEncoding()))
                            {
                                foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean)
                                {
                                    if (payroll.iIdBanco == bankResult)
                                    {
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
                        pr.Add("Fecha: " + datePdf.ToString("yyyy-MM-dd"));
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
                        PdfPTable tblPrueba = new PdfPTable(4);
                        tblPrueba.WidthPercentage = 100;
                        // Configuramos el título de las columnas de la tabla
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
                        tblPrueba.AddCell(clCtaCheques);
                        tblPrueba.AddCell(clBeneficiario);
                        tblPrueba.AddCell(clImporte);
                        tblPrueba.AddCell(clNomina);
                        double sumTotal = 0;
                        foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean) {
                            if (payroll.iIdBanco == bankResult) {
                                sumTotal += payroll.doImporte;
                                // Llenamos la tabla con información
                                clCtaCheques = new PdfPCell(new Phrase(payroll.sCuenta, _standardFont));
                                clCtaCheques.BorderWidth = 0;
                                clCtaCheques.Bottom = 80;
                                clBeneficiario = new PdfPCell(new Phrase(payroll.sNombre + " " + payroll.sPaterno + " " + payroll.sMaterno, _standardFont));
                                clBeneficiario.BorderWidth = 0;
                                clBeneficiario.Bottom = 80;
                                clImporte = new PdfPCell(new Phrase("$" + payroll.dImporte, _standardFont));
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
                        PdfPTable tblTotal = new PdfPTable(2);
                        tblTotal.WidthPercentage = 100;
                        PdfPCell clTotal = new PdfPCell(new Phrase("Total: ", _standardFont2));
                        clTotal.BorderWidth = 0;
                        clTotal.Bottom = 80;
                        PdfPCell clImporteTotal = new PdfPCell(new Phrase("$" + sumTotal.ToString("#,##0.00"), _standardFont2));
                        clImporteTotal.BorderWidth = 0;
                        clImporteTotal.Bottom = 80;
                        tblTotal.AddCell(clTotal);
                        tblTotal.AddCell(clImporteTotal);
                        doc.Add(tblTotal);
                        doc.Close();
                        pw.Close();
                        flag = true;

                    }
                }
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
                flag = false;
            }
            return flag;
        }

        public DispersionSpecial DispersionInterbank(int group, int yearPeriod, int numberPeriod, int typePeriod, string dateDeposits, int mirror)
        {
            DispersionSpecial dispersionSpecials = new DispersionSpecial();
            Boolean flag            = false;
            Boolean flagMirror      = false;
            Boolean flagProsecutors = false;
            String messageError     = "none";
            DatosEmpresaBeanDispersion datosEmpresaBeanDispersion                 = new DatosEmpresaBeanDispersion();
            DataDispersionBusiness dataDispersionBusiness                         = new DataDispersionBusiness();
            string nameFolder       = "";
            string nameFileError    = "";
            DateTime dateGeneration = DateTime.Now;
            string dateGenerationFormat = dateGeneration.ToString("MMddyyyy");
            string directoryZip   = Server.MapPath("/DispersionZIP").ToString();
            string directoryTxt   = Server.MapPath("/DispersionTXT").ToString() + "/" + DateTime.Now.Year.ToString() + "/INTERBANCARIOS/";
            string nameFolderYear = DateTime.Now.Year.ToString();
            string msgEstatus     = "";
            string msgEstatusZip  = "";
            Boolean createFolders = CreateFoldersToDeploy();
            try {
                int keyBusiness  = int.Parse(Session["IdEmpresa"].ToString());
                int yearActually = DateTime.Now.Year;
                int typeReceipt  = (yearPeriod == yearActually) ? 1 : 0;
                int invoiceId    = yearPeriod * 100000 + typePeriod * 10000 + numberPeriod * 10;
                int invoiceIdMirror = yearPeriod * 100000 + typePeriod * 10000 + numberPeriod * 10 + 8;
                datosEmpresaBeanDispersion = dataDispersionBusiness.sp_Datos_Empresa_Dispersion_Grupos(group);
                if (datosEmpresaBeanDispersion.iBanco_id.GetType().Name == "DBNull") {
                    // Retornar error
                }
                nameFolder = "DEPOSITOS_" + "E" + keyBusiness.ToString() + "P" + numberPeriod.ToString() + "A" + dateGeneration.ToString("yyyy-MM-dd");
                // -------------------------
                if (!System.IO.Directory.Exists(directoryTxt + @"\\" + nameFolder)) {
                    System.IO.Directory.CreateDirectory(directoryTxt + @"\\" + nameFolder);
                }
                flagProsecutors = processDepositsInterbank(keyBusiness, invoiceId, typeReceipt, dateDeposits, yearPeriod, typePeriod, numberPeriod, datosEmpresaBeanDispersion.sNombreEmpresa, datosEmpresaBeanDispersion.sRfc, group, 0);
                flagMirror = processDepositsInterbank(keyBusiness, invoiceId, typeReceipt, dateDeposits, yearPeriod, typePeriod, numberPeriod, datosEmpresaBeanDispersion.sNombreEmpresa, datosEmpresaBeanDispersion.sRfc, group, 1);
                if (flagProsecutors == true || flagMirror == true) {
                    flag = true;
                }
                if (flag) {
                    if (flag) {
                        // CREACCION DEL ZIP CON LOS ARCHIVOS
                        FileStream stream = new FileStream(directoryZip + @"\\" + nameFolderYear + @"\\" + "INTERBANCARIOS" + @"\" + nameFolder + ".zip", FileMode.OpenOrCreate);
                        ZipArchive fileZip = new ZipArchive(stream, ZipArchiveMode.Create);
                        System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(directoryTxt + @"\\" + nameFolder);
                        FileInfo[] sourceFiles = directoryInfo.GetFiles();
                        foreach (FileInfo file in sourceFiles) {
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
                        try {
                            using (ZipArchive zipArchive = ZipFile.OpenRead(directoryZip + @"\\" + nameFolderYear + @"\\" + "INTERBANCARIOS" + @"\\" + nameFolder + ".zip")) {
                                foreach (ZipArchiveEntry archiveEntry in zipArchive.Entries) {
                                    using (ZipArchive zipArchives = ZipFile.Open(directoryZip + @"\\" + nameFolderYear + @"\\" + "INTERBANCARIOS" + @"\\" + nameFolder + ".zip", ZipArchiveMode.Read)) {
                                        zEntrys = zipArchives.GetEntry(archiveEntry.ToString());
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
                        if (System.IO.File.Exists(directoryZip + @"\\" + nameFolderYear + @"\\" + "INTERBANCARIOS" + @"\\" + nameFolder + ".zip")) {
                            msgEstatus = "success";
                        } else {
                            msgEstatus = "failed";
                        }
                    }
                }
            } catch (Exception exc) {
                flag         = false;
                messageError = exc.Message.ToString();
            }
            dispersionSpecials.bBandera      = flag;
            dispersionSpecials.sMensajeError = messageError;
            dispersionSpecials.sZip          = nameFolder;
            dispersionSpecials.sEstadoZip    = msgEstatusZip;
            dispersionSpecials.sEstado       = msgEstatus;
            dispersionSpecials.sAnio         = nameFolderYear;
            return dispersionSpecials;
        }

        public Boolean processDepositsInterbank(int keyBusiness, int invoiceId, int typeReceipt, string dateDeposits, int yearPeriod, int typePeriod, int numberPeriod, string nameBusiness, string rfcBusiness, int group, int mirror)
        {
            Boolean flag = false;
            int k;
            int bankResult = 0;
            string nameBankResult = "";
            string fileNamePDF    = "";
            string vFileName      = "";
            List<DatosDepositosBancariosBean> listDatosDepositosBancariosBeans    = new List<DatosDepositosBancariosBean>();
            DataDispersionBusiness dataDispersionBusiness                         = new DataDispersionBusiness();
            List<DatosProcesaChequesNominaBean> listDatosProcesaChequesNominaBean = new List<DatosProcesaChequesNominaBean>();
            DatosCuentaClienteBancoEmpresaBean datoCuentaClienteBancoEmpresaBean  = new DatosCuentaClienteBancoEmpresaBean();
            DispersionSpecialDao dispersionSpecialDao = new DispersionSpecialDao();
            List<BancosBean> bancosBeans = new List<BancosBean>();
            List<EmpresasBean> empresas  = new List<EmpresasBean>();
            string directoryZip = Server.MapPath("/DispersionZIP").ToString();
            string directoryTxt = Server.MapPath("/DispersionTXT").ToString() + "/" + DateTime.Now.Year.ToString() + "/INTERBANCARIOS/";
            try {
                bancosBeans = dispersionSpecialDao.sp_Select_Config_Banks("INTERBANCARIO", 1, group);
                if (bancosBeans.Count == 0) {
                    return flag;
                }
                Boolean createFolders = CreateFoldersToDeploy();
                foreach (BancosBean bean in bancosBeans) {
                    bankResult    = bean.iIdBanco;
                    nameBankResult = bean.sNombreBanco;
                    listDatosDepositosBancariosBeans = dispersionSpecialDao.sp_Procesa_Cheques_Total_Nomina_Special(bean.iConfiguracion, typePeriod, numberPeriod, yearPeriod, bean.iGrupoId, mirror, bankResult);
                    if (mirror == 0) {
                        listDatosProcesaChequesNominaBean = dispersionSpecialDao.sp_Procesa_Cheques_Nomina_Special(bean.iGrupoId, bean.iConfiguracion, bean.iIdBanco, 0, yearPeriod, typePeriod, numberPeriod);
                    } else {
                        listDatosProcesaChequesNominaBean = dispersionSpecialDao.sp_Procesa_Cheques_Nomina_Special(bean.iGrupoId, bean.iConfiguracion, bean.iIdBanco, 1, yearPeriod, typePeriod, numberPeriod);
                    }
                    if (listDatosProcesaChequesNominaBean.Count > 0) {
                        DateTime dateGeneration     = DateTime.Now;
                        string dateGenerationFormat = dateGeneration.ToString("MMddyyyy"); 
                        string nameFolder = "DEPOSITOS_" + "E" + keyBusiness.ToString() + "P" + numberPeriod.ToString() + "A" + dateGeneration.ToString("yyyy-MM-dd");
                        fileNamePDF = "CHQ_NOMINAS_E" + keyBusiness.ToString() + "A" + string.Format("{0:00}", (yearPeriod % 100)) + "P" + string.Format("{0:00}", numberPeriod) + "_B" + bankResult.ToString() + "_INTERBANCOS.PDF";
                        if (!System.IO.Directory.Exists(directoryTxt + @"\\" + nameFolder)) {
                            System.IO.Directory.CreateDirectory(directoryTxt + @"\\" + nameFolder);
                        }
                        if (mirror == 0) {
                            fileNamePDF = "CHQ_NOMINAS_E" + keyBusiness.ToString() + "A" + string.Format("{0:00}", (yearPeriod % 100)) + "P" + string.Format("{0:00}", numberPeriod) + "_B" + bankResult.ToString() + "_INTERBANCOS.PDF";
                            if (bankResult == 72) {
                                vFileName = "NOMINAS_" + "PAG" + string.Format("{0:000000}", Convert.ToInt32(datoCuentaClienteBancoEmpresaBean.iPlaza)) + "01.txt";
                            } else {
                                vFileName = "NOMINAS_" + "E" + string.Format("{0:00}", keyBusiness.ToString()) + "A" + yearPeriod.ToString() + "P" + string.Format("{0:00}", Convert.ToInt16(numberPeriod)) + "B" + string.Format("{0:000}", bankResult) + "_INTERBANCOS.txt";
                            }
                        } else {
                            fileNamePDF = "CHQ_NOMINAS_E" + keyBusiness.ToString() + "A" + string.Format("{0:00}", (yearPeriod % 100)) + "P" + string.Format("{0:00}", numberPeriod) + "_B" + bankResult.ToString() + "_INTERBANCOSESP.PDF";
                            if (bankResult == 72) {
                                vFileName = "NOMINAS_" + "PAG" + string.Format("{0:000000}", Convert.ToInt32(datoCuentaClienteBancoEmpresaBean.iPlaza)) + "01_ESP.txt";
                            } else {
                                vFileName = "NOMINAS_" + "E" + string.Format("{0:00}", keyBusiness.ToString()) + "A" + yearPeriod.ToString() + "P" + string.Format("{0:00}", Convert.ToInt16(numberPeriod)) + "B" + string.Format("{0:000}", bankResult) + "_INTERBANCOSESP.txt";
                            }
                        }
                        // Creacion de archivos txt para dispersion interbancaria
                        if (bankResult == 2) {
                            // BANAMEX
                        }
                        if (bankResult == 14) {
                            // SANTANDER
                            // - DETALLE - \\
                            string numCuentaEmpresaSantanderD = datoCuentaClienteBancoEmpresaBean.sNumeroCuenta, fillerIntSantanderD1 = "     ", fillerIntSantanderD2 = "  ", sucursalIntSantanderD1 = "1001", plazaIntSantanderD1 = datoCuentaClienteBancoEmpresaBean.iPlaza.ToString(), campoFijoIntSantanderD1 = "DEPOSITO", fillerIntSantanderD3 = "                                                                                                                               ";
                            int consecutivoIntSantanderD1 = 0;
                            using (StreamWriter fileIntSantander = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName)) {
                                string espaciosNomEmpIntSantander = "", nombreEmpIntSantander = "", cerosImpIntSantander = "", cerosConIntSantander = "";
                                int longNomEmpIntSan = 40, longImpIntSan = 15, longConIntSan = 7;
                                foreach (DatosProcesaChequesNominaBean bank in listDatosProcesaChequesNominaBean) {
                                    consecutivoIntSantanderD1 += 1;
                                    string nameEmployee = bank.sNombre + " " + bank.sPaterno + " " + bank.sMaterno;
                                    if (nameEmployee.Length > 40) {
                                        nombreEmpIntSantander = nameEmployee.Substring(0, 39);
                                    } else {
                                        nombreEmpIntSantander = nameEmployee;
                                    }
                                    string clave = "";
                                    if (bank.sBanco.Length > 6) {
                                        clave = "B" + bank.sBanco.Substring(0, 4);
                                    }
                                    int longNomEmpIntSantanderResult = longNomEmpIntSan - nombreEmpIntSantander.Length;
                                    int longImpIntSantanderResult = longImpIntSan - bank.dImporte.ToString().Length;
                                    int longConIntSantanderResult = longConIntSan - consecutivoIntSantanderD1.ToString().Length;
                                    for (var b = 0; b < longNomEmpIntSantanderResult; b++) { espaciosNomEmpIntSantander += " "; }
                                    for (var t = 0; t < longImpIntSantanderResult; t++)    { cerosImpIntSantander += "0"; }
                                    for (var p = 0; p < longConIntSantanderResult; p++)    { cerosConIntSantander += "0"; }
                                    fileIntSantander.Write(numCuentaEmpresaSantanderD + fillerIntSantanderD1 + bank.sCuenta + fillerIntSantanderD2 + clave + nombreEmpIntSantander + espaciosNomEmpIntSantander + sucursalIntSantanderD1 + cerosImpIntSantander + bank.dImporte + plazaIntSantanderD1 + campoFijoIntSantanderD1 + fillerIntSantanderD3 + cerosConIntSantander + consecutivoIntSantanderD1.ToString() + "\n");
                                    espaciosNomEmpIntSantander = "";
                                    cerosImpIntSantander = "";
                                    cerosConIntSantander = "";
                                }
                                fileIntSantander.Close();
                            }
                            // # [ FIN -> CREACION DE DISPERSION DE SANTANDER (INTERBANCARIO) ] * \\
                        }
                        if (bankResult == 44) {
                            // SCOTIABANK
                            string tipoArchivoIntScotiabank = "EE",
                                tipoRegistroIntScotiabank = "HA",
                                numeroContratoIntScotiabank = "47848",
                                secuenciaIntScotiabank = "01",
                                fillerIntScotiabankHA1 = "                                                                                                                                                                                                                                                                                                                                                                       ";
                            string headerLayoutAIntScotiabank = tipoArchivoIntScotiabank + tipoRegistroIntScotiabank + numeroContratoIntScotiabank + secuenciaIntScotiabank + fillerIntScotiabankHA1;
                            // - ENCABEZADO BLOQUE - \\
                            string tipoRegistroBIntScotiabank = "HB",
                                monedaCuentaBIntScotiabank = "00",
                                usoFuturoIntScotiabank = "0000",
                                cuentaCargoIntScotiabank = datoCuentaClienteBancoEmpresaBean.sNumeroCuenta,
                                referenciaEmpresaIntScotiabank = "0000000001",
                                codigoStatusIntScotiabank = "000",
                                fillerIntScotiabankHB1 = "                                                                                                                                                                                                                                                                                                                                                ";
                            string headerLayoutBIntScotiabank = tipoArchivoIntScotiabank + tipoRegistroBIntScotiabank + monedaCuentaBIntScotiabank + usoFuturoIntScotiabank + cuentaCargoIntScotiabank + referenciaEmpresaIntScotiabank + codigoStatusIntScotiabank + fillerIntScotiabankHB1;
                            // - DETALLE - \\
                            string tipoRegistroCIntScotiabankD = "DA",
                                tipoPagoIntScotiabankD = "04",
                                claveMonedaIntScotiabank = "00",
                                fechaIntScotiabankD = dateGeneration.ToString("yyyyMMdd"),
                                servicioIntScotiabankD = "01", fillerIntScotiabankD1 = "                            ",
                                plazaIntScotiabankD = "00000",
                                sucursalIntScotiabankD = "00000",
                                paisIntScotiabankD = "00000",
                                fillerIntScotiabankD2 = "                                        ",
                                tipoCuentaIntScotiabankD1 = "9",
                                digitoIntScotiabankD1 = " ",
                                bancoEmisorIntScotiabankD1 = "044",
                                diasVigenciaIntScotiabankD = "001",
                                conceptoPagoIntScotiabankD = "PAGO NOMINA",
                                fillerIntScotiabankD3 = "                                       ",
                                fillerIntScotiabankD4 = "                                                            ",
                                fillerIntScotiabankD5 = "                      ";
                            int consecutivoIntScotiabankD1 = 0;
                            // - CREACION DE LISTA PARA LLENAR EL DETALLE - \\

                            using (StreamWriter fileIntScotiabank = new StreamWriter(directoryTxt + @"\\" + nameFolder + @"\\" + vFileName)) {
                                fileIntScotiabank.Write(headerLayoutAIntScotiabank + "\n");
                                fileIntScotiabank.Write(headerLayoutBIntScotiabank + "\n");
                                string cerosImpIntScotiabankD = "",
                                    cerosNumNomIntScotiabankD = "",
                                    espaciosNomEmpIntScotiabankD = "",
                                    nombreEmpIntScotiabankD = "",
                                    cerosConsecIntScotiabankD1 = "",
                                    cerosCtaCheIntScotiabankD1 = "",
                                    cerosCodStaIntScotiabankD1 = "",
                                    cerosTotMovIntScotiabank = "",
                                    cerosImpTotIntScotiabank = "";
                                int longImpIntScotiabankD = 15,
                                    longNumNomIntScotiabankD = 5,
                                    longNomEmpIntScotiabankD = 40,
                                    longConIntScotiabankD1 = 16,
                                    longCtaCheIntScotiabankD = 20,
                                    longCodStaIntScotiabankD = 25,
                                    totalMoviIntScotiabank = 0,
                                    longTotMovIntScotiabank = 7,
                                    importeTotalIntScotiabank = 0,
                                    longImpTotIntScotiabank = 17;
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
                                    string nameEmployee = bank.sNombre + " " + bank.sPaterno + " " + bank.sMaterno;
                                    if (nameEmployee.Length > 40) {
                                        nombreEmpIntScotiabankD = nameEmployee.Substring(0, 39);
                                    } else {
                                        nombreEmpIntScotiabankD = nameEmployee;
                                    }
                                    consecutivoIntScotiabankD1 += 1;
                                    totalMoviIntScotiabank += 1;
                                    importeTotalIntScotiabank += Convert.ToInt32(bank.dImporte);
                                    int longImpIntScotiabankDResult = longImpIntScotiabankD - bank.dImporte.ToString().Length;
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
                                    fileIntScotiabank.Write(tipoArchivoIntScotiabank + tipoRegistroCIntScotiabankD + tipoPagoIntScotiabankD + claveMonedaIntScotiabank + cerosImpIntScotiabankD + bank.dImporte.ToString() + fechaIntScotiabankD + servicioIntScotiabankD + cerosNumNomIntScotiabankD + bank.sNomina + fillerIntScotiabankD1 + nameEmployee + espaciosNomEmpIntScotiabankD + cerosConsecIntScotiabankD1 + consecutivoIntScotiabankD1.ToString() + plazaIntScotiabankD + sucursalIntScotiabankD + cerosCtaCheIntScotiabankD1 + bank.sCuenta + paisIntScotiabankD + fillerIntScotiabankD2 + tipoCuentaIntScotiabankD1 + digitoIntScotiabankD1 + plazaIntScotiabankD + bancoEmisorIntScotiabankD1 + sufBank + diasVigenciaIntScotiabankD + conceptoPagoIntScotiabankD + fillerIntScotiabankD3 + fillerIntScotiabankD4 + cerosCodStaIntScotiabankD1 + bank.sCuenta + fillerIntScotiabankD5 + "\n");
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
                        if (bankResult == 72) {
                            // BANORTE
                            string tipoOperacion = "04";
                            string cuentaOrigen = "";
                            string cuentaDestino = "";

                            // GRUPO DE EMPRESAS PARA DISPERSION
                            // INTERBANORTE
                            // DISPERSION POR GRUP
                            // ARCHIVO POR BANCO PARA TODAS LAS EMPRESAS
                            // UNA EMPRESA POR GRUPO 
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
                        pr.Add("Fecha: " + datePdf.ToString("yyyy-MM-dd"));
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
                        PdfPTable tblPrueba = new PdfPTable(4);
                        tblPrueba.WidthPercentage = 100;
                        // Configuramos el título de las columnas de la tabla
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
                        tblPrueba.AddCell(clCtaCheques);
                        tblPrueba.AddCell(clBeneficiario);
                        tblPrueba.AddCell(clImporte);
                        tblPrueba.AddCell(clNomina);
                        foreach (DatosProcesaChequesNominaBean payroll in listDatosProcesaChequesNominaBean)
                        {
                            // Llenamos la tabla con información
                            clCtaCheques = new PdfPCell(new Phrase(payroll.sCuenta, _standardFont));
                            clCtaCheques.BorderWidth = 0;
                            clCtaCheques.Bottom = 80;
                            clBeneficiario = new PdfPCell(new Phrase(payroll.sNombre + " " + payroll.sPaterno + " " + payroll.sMaterno, _standardFont));
                            clBeneficiario.BorderWidth = 0;
                            clBeneficiario.Bottom = 80;
                            clImporte = new PdfPCell(new Phrase("$" + payroll.dImporte, _standardFont));
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
                        doc.Add(tblPrueba);
                        doc.Close();
                        pw.Close();
                        flag = true;
                    }
                }
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
                flag = false;
            }
            return flag;
        }

        [HttpPost]
        public JsonResult DispersionSpecialInit (int group, string type, int yearPeriod, int numberPeriod, int typePeriod, string dataDeposits, int mirror)
        {
            Boolean flag         = false;
            String  messageError = "none";
            DispersionSpecial dispersions = new DispersionSpecial();
            string nameFolder = "";
            string labelShow  = "";
            try {
                if (type.Trim() == "NOM") {
                    dispersions = DispersionPayroll(group, yearPeriod, numberPeriod, typePeriod, dataDeposits, mirror);
                    nameFolder  = "NOMINAS";
                    labelShow   = "de nomina";
                } else if (type.Trim() == "INT") {
                    dispersions = DispersionInterbank(group, yearPeriod, numberPeriod, typePeriod, dataDeposits, mirror);
                    nameFolder  = "INTERBANCARIOS";
                    labelShow   = "interbancarios";
                } else {
                    return Json(new { Bandera = false, MensajeError = "NOTVALID" });
                }
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            } 
            return Json(new { Bandera = dispersions.bBandera, MensajeError = dispersions.sMensajeError,  Zip = dispersions.sZip, EstadoZip = dispersions.sEstadoZip, Estado = dispersions.sEstado, Anio = dispersions.sAnio, Carpeta = nameFolder, Label = labelShow });
        }

        [HttpPost]
        public JsonResult RestartToDeploySpecial(string paramNameFile, int paramYear, string paramCode)
        {
            Boolean flag    = false;
            Boolean flagZIP = false;
            Boolean flagTXT = false;
            String messageError = "none";
            string nameFileFZ   = (paramCode == "NOM") ? "NOMINAS" : "INTERBANCARIOS";
            string directoryZip = Server.MapPath("/DispersionZIP/" + paramYear.ToString() + "/" + nameFileFZ + "/" + paramNameFile + ".zip");
            string directoryTxt = Server.MapPath("/DispersionTXT/" + paramYear.ToString() + "/" + nameFileFZ + "/" + paramNameFile);
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
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, MensajeError = messageError, TXT = flagTXT, ZIP = flagZIP });

        }

        public Boolean GenerateFoldersReports(string folderFather, string folderChild, string nameFile) {
            Boolean flag = false;
            string pathSaveFile = Server.MapPath("~/Content/");
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

        [HttpPost]
        public JsonResult ReporteDs(int group, string type, int yearPeriod, int numberPeriod, int typePeriod, string dataDeposits, int mirror)
        {
            Boolean flag         = false;
            String  messageError = "none";
            DispersionSpecialDao dispersion = new DispersionSpecialDao();
            DispersionBean dispersionBean   = new DispersionBean();
            string pathSaveFile   = Server.MapPath("~/Content/");
            string nameFolder     = "DISPERSION";
            string nameFolderRe   = "ESPECIAL";
            string nameFileRepr   = "DISPERSIONSP" + mirror.ToString() + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".xlsx";
            ReportesDao reportDao = new ReportesDao();
            string pathComplete = pathSaveFile + nameFolder + @"\\" + nameFolderRe + @"\\";
            int rowsDataTable = 1, columnsDataTable = 0;
            try {
                Boolean createFolders = GenerateFoldersReports(nameFolder, nameFolderRe, nameFileRepr);
                if (createFolders) {
                    DataTable dataTable = new DataTable();
                    dataTable.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    dataTable        = dispersion.sp_Reporte_Dispersion_Especial(group, 0, mirror, yearPeriod, typePeriod, numberPeriod);
                    columnsDataTable = dataTable.Columns.Count + 1;
                    rowsDataTable    = dataTable.Rows.Count;
                    if (rowsDataTable > 0) {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (ExcelPackage excel = new ExcelPackage()) {
                            excel.Workbook.Worksheets.Add(Path.GetFileNameWithoutExtension(nameFileRepr));
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

        [HttpPost]
        public JsonResult RestartReportFile(string folder, string file)
        {
            Boolean flag = false;
            Boolean flagDeleted = false;
            String  messageError = "none";
            string directoryZip = Server.MapPath("~/Content/DISPERSION/"+folder+"/"+file);
            try {
                if (System.IO.File.Exists(directoryZip)) {
                    System.IO.File.Delete(directoryZip);
                    flagDeleted = true;
                }
                flag = true;
            } catch (Exception exc) {
                flag = false;
                messageError = exc.Message.ToString();
            }
            return Json(new { Bandera = flag, Eliminado = flagDeleted, MensajeError = messageError });
        }

    }
}