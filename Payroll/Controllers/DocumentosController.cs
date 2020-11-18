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
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using word = Microsoft.Office.Interop.Word;
using Org.BouncyCastle.Asn1.Misc;
using Microsoft.Office.Interop.Word;
using Microsoft.Ajax.Utilities;
using System.Web.Helpers;
using System.Web.UI.WebControls;
using System.IO.Compression;

namespace Payroll.Controllers
{
    public class DocumentosController : Controller
    {
        // GET: Documentos
        public PartialViewResult KitContratacion()
        {
            return PartialView();
        }

        // llena  listado de tipo de empleados
        [HttpPost]
        public JsonResult LisTipodeEmpleado()
        {
            List<TipoDeEmpleadoBean> LisTipoEmpleado = new List<TipoDeEmpleadoBean>();
            BajasEmpleadosDaoD Dao = new BajasEmpleadosDaoD();
            LisTipoEmpleado = Dao.sp_TipoEmpleado_Retrieve_Cgeneral();
            return Json(LisTipoEmpleado);
        }

        /// carga datos de empleados con kit de contratacion
        [HttpPost]
        public JsonResult KitEmpleados(int IdTipoempleado ,int opBaja) {
            List<EmisorReceptorBean> LisTipoEmpleado = new List<EmisorReceptorBean>();
            BajasEmpleadosDaoD Dao = new BajasEmpleadosDaoD();
            LisTipoEmpleado = Dao.sp_EmpladosKitDoc_Retrieve_Cgeneral(IdTipoempleado, opBaja);
             return Json(LisTipoEmpleado);
        }

        // LLenado de correspondencia en word bajas
        [HttpPost]
        public JsonResult KitDocbaja(int iIdempresa, int iNomina, int iCategoria)
        {
            string path = " ";
            string NomArchios = "";
            if (iCategoria == 1)
            {
                path = Server.MapPath("Archivos\\KitContratacion\\Kitcontra.DOCX");
            }
            if (iCategoria == 2)
            {
                path = Server.MapPath("Archivos\\KitContratacion\\CONVBAJA2.DOCX");
                NomArchios = "CONVBAJA2.DOCX)";
            }
            path = path.Replace("\\Documentos", "");
            int idmepres = 0;
                idmepres= iIdempresa;
            int inomina = 0;
                 inomina=iNomina;
            List<EmisorReceptorBean> LisTipoEmpleado = new List<EmisorReceptorBean>();
            BajasEmpleadosDaoD Dao = new BajasEmpleadosDaoD();
            ListEmpleadosDao Dao2 = new ListEmpleadosDao();
              LisTipoEmpleado = Dao2.sp_EmisorReceptor_Retrieve_EmisorReceptor(iIdempresa, iNomina);
           // LisTipoEmpleado = Dao2.sp_EmisorReceptor_Retrieve_EmisorReceptor(idmepres, inomina);
            if (LisTipoEmpleado != null)
            {
                object Parametros = path;
                object missing = Type.Missing;

                DateTime sFechaantiguedad = Convert.ToDateTime(LisTipoEmpleado[0].sFechaAntiguedad);
                string DiaAntiguedad = sFechaantiguedad.Day.ToString();
                string MesAntiguedad = Mes(sFechaantiguedad.Month.ToString());
                string anioAntiguedad = sFechaantiguedad.Year.ToString();

                DateTime sFechaingreso = Convert.ToDateTime(LisTipoEmpleado[0].sFechaIngreso);
                string DiaIngreso = sFechaingreso.Day.ToString();
                string MesIngreso = Mes(sFechaingreso.Month.ToString());
                string anioIngreso = sFechaingreso.Year.ToString();
                string DiaBaja = " ";
                string MesBaja = " ";
                string AnioBaja = " ";

                if (iCategoria == 2)
                {
                    if (LisTipoEmpleado[0].sFechaBajaEmple != " " && LisTipoEmpleado[0].sFechaBajaEmple != "")
                    {
                        DateTime sFechaBAjaEmple = Convert.ToDateTime(LisTipoEmpleado[0].sFechaBajaEmple);
                        DiaBaja = sFechaBAjaEmple.Day.ToString();
                        MesBaja = Mes(sFechaBAjaEmple.Month.ToString());
                        AnioBaja = sFechaBAjaEmple.Year.ToString();
                    }
                    else
                    {
                        DiaBaja = " ";
                        MesBaja = " ";
                        AnioBaja = " ";

                    }

                };


                Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
                //carga documento 
                Microsoft.Office.Interop.Word.Document doc = null;
                doc = app.Documents.Open(Parametros, missing, missing);
                app.Selection.Find.ClearFormatting();
                app.Selection.Find.Replacement.ClearFormatting();
                app.Selection.Find.Execute("<NombreEmpleado>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sApellPatemple + " " + LisTipoEmpleado[0].sApellMatemple + " " + LisTipoEmpleado[0].sNombreemple, 2);
                app.Selection.Find.Execute("<fechaingreso>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sFechaIngreso, 2);
                app.Selection.Find.Execute("<DiaIngreso>", missing, missing, missing, missing, missing, missing, missing, missing, DiaIngreso, 2);
                app.Selection.Find.Execute("<MesIngreso>", missing, missing, missing, missing, missing, missing, missing, missing, MesIngreso, 2);
                app.Selection.Find.Execute("<AnioIngreso>", missing, missing, missing, missing, missing, missing, missing, missing, anioIngreso, 2);
                app.Selection.Find.Execute("<DiaAntiguedad>", missing, missing, missing, missing, missing, missing, missing, missing, DiaAntiguedad, 2);
                app.Selection.Find.Execute("<MesAntiguedad>", missing, missing, missing, missing, missing, missing, missing, missing, MesAntiguedad, 2);
                app.Selection.Find.Execute("<AnioAntiguedad>", missing, missing, missing, missing, missing, missing, missing, missing, anioAntiguedad, 2);
                if (iCategoria == 2)
                {
                    app.Selection.Find.Execute("<DiaBaja>", missing, missing, missing, missing, missing, missing, missing, missing, DiaBaja, 2);
                    app.Selection.Find.Execute("<MesBaja>", missing, missing, missing, missing, missing, missing, missing, missing, MesBaja, 2);
                    app.Selection.Find.Execute("<AnioBaja>", missing, missing, missing, missing, missing, missing, missing, missing, AnioBaja, 2);

                };

                app.Selection.Find.Execute("<Puesto>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sNombrePuesto, 2);
                //app.Selection.Find.Execute("<Fechaingreso2>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sFechaIngreso,2);
                app.Selection.Find.Execute("<NombreEmpresa>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sNombreEmpresa, 2);
                app.Selection.Find.Execute("<NombEmpleado>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sNombreemple, 2);
                app.Selection.Find.Execute("<ApellidoPaternoEmpleado>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sApellPatemple, 2);
                app.Selection.Find.Execute("<ApellidoMaterEmp>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sApellMatemple, 2);
                app.Selection.Find.Execute("<FechaNacimiento>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sFechaNacimiento, 2);
                app.Selection.Find.Execute("<LugarFecha>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sCiudadEmple + "," + LisTipoEmpleado[0].sFechaIngreso, 2);
                app.Selection.Find.Execute("<DirecEmpresa>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sCiudadEmple + "," + LisTipoEmpleado[0].sCalle + " Col." + LisTipoEmpleado[0].sColonia + " " + LisTipoEmpleado[0].sCiudad + ",Mexico" + " CP." + LisTipoEmpleado[0].iCP, 2);
                app.Selection.Find.Execute("<RFCEmpresa>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sRFC, 2);
                app.Selection.Find.Execute("<RepresentanteLegal>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sRepresentanteLegal, 2);
                app.Selection.Find.Execute("<SueldoDiario>", missing, missing, missing, missing, missing, missing, missing, missing, "$ " + string.Format(CultureInfo.InvariantCulture, "{0:#,###,##0.00}", (LisTipoEmpleado[0].dSalarioMensual) / 30), 2);
                app.Selection.Find.Execute("<SueldoQuincenal>", missing, missing, missing, missing, missing, missing, missing, missing, "$ " + string.Format(CultureInfo.InvariantCulture, "{0:#,###,##0.00}", (LisTipoEmpleado[0].dSalarioMensual) / 2), 2);
                DateTime FNaci = Convert.ToDateTime(LisTipoEmpleado[0].sFechaNacimiento);

                int edad = DateTime.Now.Year - FNaci.Year;
                app.Selection.Find.Execute("<CtaCheques>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sCtaCheques, 2);
                app.Selection.Find.Execute("<Banco>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sDescripcion, 2);
                app.Selection.Find.Execute("<EdadEmpleado>", missing, missing, missing, missing, missing, missing, missing, missing, edad.ToString(), 2);
                app.Selection.Find.Execute("<fechaNacimientoEmpleado>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sFechaNacimiento, 2);
                app.Selection.Find.Execute("<Lugardenacimiento>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sLugarNacimiento, 2);
                app.Selection.Find.Execute("<Direccionempleado>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sDomiciolioEmple, 2);
                string EstadoCivil = "";
                if (LisTipoEmpleado[0].sEstadoCivil == "S") { EstadoCivil = "Soltero"; };
                if (LisTipoEmpleado[0].sEstadoCivil == "C") { EstadoCivil = "Casado"; };
                if (LisTipoEmpleado[0].sEstadoCivil == "V") { EstadoCivil = "Viudo"; };
                if (LisTipoEmpleado[0].sEstadoCivil == "U") { EstadoCivil = "Union Libre"; };
                app.Selection.Find.Execute("<estadocivil>", missing, missing, missing, missing, missing, missing, missing, missing, EstadoCivil, 2);
                string Sexo = "";
                if (LisTipoEmpleado[0].sSexo == "F") { Sexo = "Femenino"; };
                if (LisTipoEmpleado[0].sSexo == "M") { Sexo = "Masculino"; };
                app.Selection.Find.Execute("<sexo>", missing, missing, missing, missing, missing, missing, missing, missing, Sexo, 2);
                app.Selection.Find.Execute("<RFCEmpleado>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sRFCEmpleado, 2);
                app.Selection.Find.Execute("<Curpempleado>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sCURP, 2);
                app.Selection.Find.Execute("<SueldoMensual>", missing, missing, missing, missing, missing, missing, missing, missing, "$ " + string.Format(CultureInfo.InvariantCulture, "{0:#,###,##0.00}", LisTipoEmpleado[0].dSalarioMensual), 2);
                EmpleadosController contolemple = new EmpleadosController();
                app.Selection.Find.Execute("<CantLetra>", missing, missing, missing, missing, missing, missing, missing, missing, contolemple.NumeroALetras(Convert.ToString((LisTipoEmpleado[0].dSalarioMensual) / 30)), 2);
                app.Selection.Find.Execute("<CantLetraQuin>", missing, missing, missing, missing, missing, missing, missing, missing, contolemple.NumeroALetras(Convert.ToString((LisTipoEmpleado[0].dSalarioMensual) / 2)), 2);
                app.Selection.Find.Execute("<Departamento>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sDescripcionDepartamento, 2);
                app.Selection.Find.Execute("<LocalidadEmple>", missing, missing, missing, missing, missing, missing, missing, missing, LisTipoEmpleado[0].sLocalidademple, 2);
                app.Selection.Find.Execute("<NoNomina>", missing, missing, missing, missing, missing, missing, missing, missing, Convert.ToString(iNomina), 2);



                if (iCategoria == 1)
                {
                    path = path.Replace("Kitcontra", "Kit");
                }
                if (iCategoria == 2)
                {
                    path = path.Replace("CONVBAJA2", "Kitbaja");

                }


                object SaveAsFile = (object)path;

                LisTipoEmpleado[0].sUrl = path;
                doc.SaveAs2(SaveAsFile, missing, missing, missing);

                doc.Close(false, missing, missing);
                app.Quit(false, false, false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                string URLs = " ";
                URLs=Server.MapPath("~/Content/");
                string Archivos = path;
                string NomCarp;
                string NombreArchivo;
                if (iCategoria == 2)
                {
                    BajasEmpleadosController obj = new BajasEmpleadosController();
                    List<ReciboNominaBean> NoFit = new List<ReciboNominaBean>();
                      NoFit = Dao.sp_NoIdFiniquito_Retrieve_TFiniquitos(iIdempresa, iNomina);
                    //NoFit = Dao.sp_NoIdFiniquito_Retrieve_TFiniquitos(4, 10000);
                    if (NoFit != null)
                    {
                        for (int i = 0; i < NoFit.Count; i++)
                        {
                            //string bandera= obj.GeneraPdf(Convert.ToInt32(NoFit[i].iIdFiniquito), inomina, idmepres, URLs).Data.ToString();
                            string bandera = obj.GeneraPdf(Convert.ToInt32(NoFit[i].iIdFiniquito), inomina, idmepres, 1, URLs).Data.ToString();
                            string[] ban = bandera.Split(',');
                            string[] ban2 = bandera.Split(',');
                            NomCarp = ban[4];
                            NombreArchivo = ban2[2];
                            ban = NomCarp.Split('=');
                            ban2 = NombreArchivo.Split('=');
                            NomCarp = ban[1];
                            NombreArchivo = ban2[1];
                            NomCarp = NomCarp.Replace("}", "");
                            URLs = Server.MapPath("\\Content\\");
                            URLs = URLs + NomCarp + "\\" + NombreArchivo;
                            NomArchios = NomArchios + NombreArchivo + ")";

                            Archivos = Archivos + ")" + URLs;
                            Archivos = Archivos.Replace(" \\ ", "\\");
                            Archivos = Archivos.Replace("\\ ", "\\");


                        }
                    }
                }
                if (iCategoria == 2)
                {
                    string pathZip = Server.MapPath("Archivos\\");
                    pathZip = pathZip.Replace("\\XmlZip", "");
                    pathZip = pathZip.Replace("\\Documentos", "");
                    pathZip = pathZip + "KitBaja.zip";
                    LisTipoEmpleado[0].sUrl = pathZip;
                    if (System.IO.File.Exists(pathZip))
                    {
                        System.IO.File.Delete(pathZip);
                    }

                    //Borra archivo temporal
                    // 1 
                    //DirectoryInfo dir = new DirectoryInfo(@"C:\reportes\");
                    // 2 

                    FileStream stream = new FileStream(pathZip, FileMode.OpenOrCreate);
                    // 3 
                    ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Create);
                    // 4
                    // 10 
                    archive.Dispose();
                    stream.Close();
                    string[] ArchivosZip = Archivos.Split(')');
                    string[] NomArch = NomArchios.Split(')');

                    for (int i = 0; i < ArchivosZip.Length; i++)
                    {
                        using (ZipArchive archivos = ZipFile.Open(pathZip, ZipArchiveMode.Update))
                        {

                            archivos.CreateEntryFromFile(ArchivosZip[i], NomArch[i], CompressionLevel.Fastest);


                        }

                    }

                }

                LisTipoEmpleado[0].sMensaje = "success";

            }

            else {
                EmisorReceptorBean ls = new EmisorReceptorBean();
                ls.sMensaje = "error";
                LisTipoEmpleado.Add(ls);
            }
            


            return Json(LisTipoEmpleado);
        }

        public string Mes(string Mes) {
            string mes="";

            if (Mes == "1") {

                mes = "ENERO"; 
            }
            if (Mes == "2")
            {
                mes = "FEBRERO";
            
            }

            if (Mes == "3") {
                mes = "MARZO";
            }

            if (Mes == "4") {
                mes = "ABRIL";
            }

            if (Mes == "5") {
                mes = "MAYO";
            
            }

            if (Mes == "6") {
                mes = "JUNIO";
            }
            if (Mes == "7")
            {
                mes = "JULIO";
            }
            if (Mes == "8")
            {
                mes = "AGOSTO";
            }
            if (Mes == "9") {
                mes = "Septiembre"; 
            }

            if (Mes == "10") {
                mes = "OCTUBRE";
            }
            if (Mes == "11") {

                mes = "NOVIEMBRE";
            
            }

            if (Mes == "12") {

                mes = "DICIEMBRE";
            }


            return mes;
        
        }

    }
}
