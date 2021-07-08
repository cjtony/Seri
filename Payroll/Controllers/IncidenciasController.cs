using Payroll.Models.Beans;
using Payroll.Models.Daos;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Payroll.Controllers
{
    public class IncidenciasController : Controller
    {
        // Vistas parciales
        public PartialViewResult Incidencias()
        {
            return PartialView();
        }
        public PartialViewResult Creditos()
        {
            return PartialView();
        }
        public PartialViewResult Ausentismos()
        {
            return PartialView();
        }
        public PartialViewResult PensionesAlimenticias()
        {
            return PartialView();
        }
        public PartialViewResult Vacaciones()
        {
            return PartialView();
        }
        public PartialViewResult TablaIncidencias()
        {
            return PartialView();
        }
        public PartialViewResult CargaMasiva()
        {
            return PartialView();
        }
        //Retorno de datos
        [HttpPost]
        public JsonResult ResumenVacaciones(int IdEmpleado = 4)
        {
            List<PeriodoVacacionesBean> empleados = new List<PeriodoVacacionesBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            empleados = Dao.sp_Retrieve_PeriodosVacaciones(IdEmpleado);
            string tabla = "";
            foreach (var emp in empleados)
            {
                tabla += "<tr>" +
                    "<td>" + emp.iAnio + "</td>" +
                    "<td>" + emp.iDiasDisfrutados + "</td>" +
                    "<td>" + emp.iDiasPrima + "</td>" +
                    "<td>" + emp.iIdEmpleado + "</td>" +
                    "<td>" + emp.sFechaInicio.ToString().Substring(0, 10) + "</td>" +
                    "<td>" + emp.sFechaTermino.ToString().Substring(0, 10) + "</td>" +
                    "</tr>";
            }
            return Json(tabla);
        }
        [HttpPost]
        public JsonResult LoadAusentismos()
        {
            List<AusentismosBean> lista = new List<AusentismosBean>();
            PruebaEmpresaDao Dao = new PruebaEmpresaDao();
            int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            lista = Dao.sp_TiposAusentimo_Retrieve_TiposAusentismo(Empresa_id);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult LoadTipoIncidencia(string txtSearch)
        {
            List<VW_TipoIncidenciaBean> lista = new List<VW_TipoIncidenciaBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int IdEmpresa = int.Parse(Session["IdEmpresa"].ToString());
            lista = Dao.sp_VW_tipo_Incidencia_Retrieve_LoadTipoIncidencia(IdEmpresa, txtSearch);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult LoadPeriodosVac()
        {
            List<PVacacionesBean> lista = new List<PVacacionesBean>();
            PruebaEmpresaDao Dao = new PruebaEmpresaDao();
            lista = Dao.sp_TperiodosVacaciones_Retrieve_PeriodosVacaciones(int.Parse(Session["Empleado_id"].ToString()), int.Parse(Session["IdEmpresa"].ToString()));
            return Json(lista);
        }
        [HttpPost]
        public JsonResult LoadPeriodosDist(int PerVacLn_id)
        {
            List<PeriodosVacacionesBean> lista = new List<PeriodosVacacionesBean>();
            PruebaEmpresaDao Dao = new PruebaEmpresaDao();
            lista = Dao.sp_Retrieve_TPeriodosVacacionesDist_Retrieve_VacacionesDist(PerVacLn_id);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult setDisfrutadas(int IdPer_vac_Dist)
        {
            List<string> lista = new List<string>();
            PruebaEmpresaDao Dao = new PruebaEmpresaDao();
            lista = Dao.sp_TPeriodosVacaciones_Dist_Set_PeriodoDisfrutado(IdPer_vac_Dist);
            return Json(lista);

        }
        [HttpPost]
        public JsonResult SavePeriodosVac(int PerVacLn_id, string FechaInicio, string FechaFin, int Dias)
        {
            List<string> lista = new List<string>();
            PruebaEmpresaDao Dao = new PruebaEmpresaDao();
            int Usuario_id = int.Parse(Session["iIdUsuario"].ToString());
            lista = Dao.sp_TPeriodosDist_Insert_Periodo(PerVacLn_id, FechaInicio, FechaFin, Dias, Usuario_id);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult LoadCreditosEmpleado()
        {
            List<CreditosBean> lista = new List<CreditosBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int id1 = int.Parse(Session["Empleado_id"].ToString());
            int id2 = int.Parse(Session["IdEmpresa"].ToString());
            lista = Dao.sp_TCreditos_Retrieve_Creditos(id1, id2);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult SaveCredito(string TipoDescuento, string Descuento, string NoCredito, string FechaAprovacion, string Descontar, string FechaBaja, string FechaReinicio, string FactorDesc)
        {
            List<string> lista = new List<string>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int id1 = int.Parse(Session["Empleado_id"].ToString());
            int id2 = int.Parse(Session["IdEmpresa"].ToString());
            int Periodo = int.Parse(Session["Periodo_id"].ToString());
            lista = Dao.sp_TCreditos_Insert_Credito(id1, id2, TipoDescuento, Descuento, NoCredito, FechaAprovacion, Descontar, FechaBaja, FechaReinicio, Periodo);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult LoadAusentismosTab()
        {
            List<AusentismosEmpleadosBean> lista = new List<AusentismosEmpleadosBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int id1 = int.Parse(Session["Empleado_id"].ToString());
            int id2 = int.Parse(Session["IdEmpresa"].ToString());
            lista = Dao.sp_TAusentismos_Retrieve_Ausentismos_Empleado(id2, id1);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult LoadAusentismo(int IdAusentismo)
        {
            List<AusentismosEmpleadosBean> lista = new List<AusentismosEmpleadosBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int Empleado_id = int.Parse(Session["Empleado_id"].ToString());
            int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            lista = Dao.sp_TAusentismos_Retrieve_Ausentismo_Empleado(Empresa_id, Empleado_id, IdAusentismo);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult LoadAusentismosAll()
        {
            List<AusentismosEmpleadosBean> lista = new List<AusentismosEmpleadosBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            lista = Dao.sp_TAusentismos_Retrieve_Ausentismos();
            var data = new { data = lista };
            return Json(lista);
        }
        [HttpPost]
        public JsonResult DeleteAusentismo(int IdAusentismo)
        {
            List<string> res;
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            res = Dao.sp_TAusentismos_Delete_Ausentismos(int.Parse(Session["IdEmpresa"].ToString()), int.Parse(Session["Empleado_id"].ToString()), IdAusentismo);
            return Json(res);
        }
        [HttpPost]
        public JsonResult SaveAusentismo(int Tipo_Ausentismo_id, string Recupera_Ausentismo, string Fecha_Ausentismo, int Dias_Ausentismo, string Certificado_imss, string Comentarios_imss, string Causa_FaltaInjustificada, string FechaFin, int Tipo)
        {
            List<string> lista = new List<string>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int id1 = int.Parse(Session["Empleado_id"].ToString());
            int id2 = int.Parse(Session["IdEmpresa"].ToString());
            int Periodo = int.Parse(Session["Periodo_id"].ToString());
            lista = Dao.sp_TAusentismos_Insert_Ausentismo(Tipo_Ausentismo_id, id1, id2, Recupera_Ausentismo, Fecha_Ausentismo, Dias_Ausentismo, Certificado_imss, Comentarios_imss, Causa_FaltaInjustificada, Periodo, FechaFin, Tipo);
            //lista.Add("Ausentismo registrado con éxito");
            return Json(lista);
        }
        [HttpPost]
        public JsonResult UpdateAusentismo(int id, int Tipo_Ausentismo_id, string Recupera_Ausentismo, string Fecha_Ausentismo, int Dias_Ausentismo, int Saldo_Dias_Ausentismo, string Certificado_imss, string Comentarios_imss, string Causa_FaltaInjustificada, string FechaFin, int Tipo, int IncidenciaProgramada_id)
        {
            List<string> lista = new List<string>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int id1 = int.Parse(Session["Empleado_id"].ToString());
            int id2 = int.Parse(Session["IdEmpresa"].ToString());
            int Periodo = int.Parse(Session["Periodo_id"].ToString());
            lista = Dao.sp_TAusentismos_Update_Ausentismo(id, Tipo_Ausentismo_id, id1, id2, Recupera_Ausentismo, Fecha_Ausentismo, Dias_Ausentismo, Saldo_Dias_Ausentismo, Certificado_imss, Comentarios_imss, Causa_FaltaInjustificada, Periodo, FechaFin, Tipo, IncidenciaProgramada_id);
            //lista.Add("Ausentismo registrado con éxito");
            return Json(lista);
        }
        [HttpPost]
        public JsonResult SavePension(string Cuota_fija, int Porcentaje, string AplicaEn, string Descontar_en_finiquito, string No_Oficio, string Fecha_Oficio, int Tipo_Calculo, string Aumentar_segun_salario_minimo_general, string Aumentar_segun_aumento_de_sueldo, string Beneficiaria, int Banco, string Sucursal, string Tarjeta_vales, string Cuenta_Cheques, string Fecha_baja)
        {
            List<string> res = new List<string>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int Empleado_id = int.Parse(Session["Empleado_id"].ToString());
            int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            int Periodo = int.Parse(Session["Periodo_id"].ToString());
            res = Dao.sp_TPensiones_Alimenticias_Insert_Pensiones(Empresa_id, Empleado_id, Cuota_fija, Porcentaje, AplicaEn, Descontar_en_finiquito, No_Oficio, Fecha_Oficio, Tipo_Calculo, Aumentar_segun_salario_minimo_general, Aumentar_segun_aumento_de_sueldo, Beneficiaria, Banco, Sucursal, Tarjeta_vales, Cuenta_Cheques, Periodo);
            return Json(res);
        }
        [HttpPost]
        public JsonResult LoadPensiones()
        {
            List<PensionesAlimentariasBean> res = new List<PensionesAlimentariasBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int Empleado_id = int.Parse(Session["Empleado_id"].ToString());
            int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            res = Dao.sp_TPensiones_Alimenticias_Retrieve_Pensiones(Empresa_id, Empleado_id);
            return Json(res);
        }
        [HttpPost]
        public JsonResult SaveRegistroIncidencia(int inRenglon, string inCantidad, int inDias, int inPlazos, string inLeyenda, string inReferencia, string inFechaA, int inDiashrs)
        {
            List<string> res = new List<string>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int Empleado_id = int.Parse(Session["Empleado_id"].ToString());
            int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            int Periodo = int.Parse(Session["Periodo_id"].ToString());
            res = Dao.sp_TRegistro_incidencias_Insert_Incidencia(Empresa_id, Empleado_id, inRenglon, inCantidad, inDias, inPlazos, inLeyenda, inReferencia, inFechaA, Periodo, inDiashrs);
            return Json(res);
        }
        [HttpPost]
        public JsonResult LoadIncidenciasEmpleado()
        {
            List<TabIncidenciasBean> res = new List<TabIncidenciasBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int Empleado_id = int.Parse(Session["Empleado_id"].ToString());
            int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            int Periodo = int.Parse(Session["Periodo_id"].ToString());
            res = Dao.sp_TIncidencias_Retrieve_Incidencias_Empleado(Empresa_id, Empleado_id, Periodo);
            return Json(res);
        }
        [HttpPost]
        public JsonResult LoadIncidenciasProgramadas()
        {
            List<IncidenciasProgramadasBean> res = new List<IncidenciasProgramadasBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            int Periodo = int.Parse(Session["Periodo_id"].ToString());
            res = Dao.sp_TIncidencias_Programadas_Retrieve_Incidencias_Programadas(Empresa_id, Periodo);
            return Json(res);
        }
        [HttpPost]
        public JsonResult DeleteIncidencia(int Incidencia_id, int IncidenciaP_id)
        {
            List<string> res;
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            res = Dao.sp_TRegistro_Incidencias_Delete_Incidencias(Incidencia_id, IncidenciaP_id);
            return Json(res);
        }
        [HttpPost]
        public JsonResult LoadTipoDescuento()
        {
            List<TipoDescuentoBean> res;
            ModCatalogosDao Dao = new ModCatalogosDao();
            res = Dao.sp_TipoDescuento_Retrieve_TipoDescuentos();
            return Json(res);
        }
        [HttpPost]
        public JsonResult DeleteCredito(int Credito_id)
        {
            List<string> res;
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            res = Dao.sp_TCreditos_delete_Credito(int.Parse(Session["IdEmpresa"].ToString()), int.Parse(Session["Empleado_id"].ToString()), Credito_id);
            return Json(res);
        }
        [HttpPost]
        public JsonResult DeletePension(int Pension_id, int IncidenciaP_id)
        {
            List<string> res;
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            res = Dao.sp_TPensiones_Alimenticias_Delete_Pension(int.Parse(Session["IdEmpresa"].ToString()), int.Parse(Session["Empleado_id"].ToString()), Pension_id, IncidenciaP_id);
            return Json(res);
        }
        [HttpPost]
        public JsonResult LoadIncapacidadesTab()
        {
            List<AusentismosEmpleadosBean> lista = new List<AusentismosEmpleadosBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int Empleado_id = int.Parse(Session["Empleado_id"].ToString());
            int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            lista = Dao.sp_TAusentismos_Retrieve_IncapacidadesPeriodo(Empresa_id, Empleado_id);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult LoadHistorialIncapacidadesTab()
        {
            List<AusentismosEmpleadosBean> lista = new List<AusentismosEmpleadosBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int Empleado_id = int.Parse(Session["Empleado_id"].ToString());
            int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            lista = Dao.sp_TAusentismos_Retrieve_HistorialAusentismos_Empleado(Empresa_id, Empleado_id);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult SearchIncapacidades(string FechaI, string FechaF)
        {
            List<AusentismosEmpleadosBean> lista = new List<AusentismosEmpleadosBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int Empleado_id = int.Parse(Session["Empleado_id"].ToString());
            int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            lista = Dao.sp_TAusentismos_Search_Incapacidades(Empresa_id, Empleado_id, FechaI, FechaF);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult LoadCreditoEmpleado(int Credito_id)
        {
            List<CreditosBean> lista = new List<CreditosBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int id1 = int.Parse(Session["Empleado_id"].ToString());
            int id2 = int.Parse(Session["IdEmpresa"].ToString());
            lista = Dao.sp_TCreditos_Retrieve_Credito(id1, id2, Credito_id);
            //var data = new { data = lista };
            return Json(lista);
        }
        [HttpPost]
        public JsonResult LoadDescontar(int catalogoid)
        {
            List<InfDomicilioBean> lista = new List<InfDomicilioBean>();
            InfDomicilioDao Dao = new InfDomicilioDao();
            lista = Dao.sp_CatalogoGeneral_Retrieve_CatalogoGeneral(catalogoid);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult LoadPeriodosVac2()
        {
            List<PVacacionesBean> lista = new List<PVacacionesBean>();
            PruebaEmpresaDao Dao = new PruebaEmpresaDao();
            lista = Dao.sp_TperiodosVacaciones_Retrieve_PeriodosVacaciones2(int.Parse(Session["Empleado_id"].ToString()), int.Parse(Session["IdEmpresa"].ToString()));
            return Json(lista);
        }
        [HttpPost]
        public JsonResult LoadIncidencia(int Incidencia_id)
        {
            List<IncidenciasBean> lista = new List<IncidenciasBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            int Empleado_id = int.Parse(Session["Empleado_id"].ToString());
            lista = Dao.sp_TRegistro_Incidencias_retrieve_incidencia(Empresa_id, Empleado_id, Incidencia_id);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult UpdateIncidencia(int Incidencia_id, int Renglon_id, string Cantidad, string Saldo, int Plazos, int Pagos_restantes, string Leyenda, string Referencia, string Fecha_Aplicacion, string Numero_dias)
        {
            List<string> lista = new List<string>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            int Empleado_id = int.Parse(Session["Empleado_id"].ToString());
            lista = Dao.sp_TRegistro_Incidencias_update_incidencia(Empresa_id, Empleado_id, Incidencia_id, Renglon_id, Cantidad, Saldo, Plazos, Pagos_restantes, Leyenda, Referencia, Fecha_Aplicacion, Numero_dias);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult LoadFile(HttpPostedFileBase fileUpload, string fileType)
        {
            List<object> list = new List<object>();

            string RutaSitio = Server.MapPath("~/");
            string dia = DateTime.Today.ToString("dd");
            string mes = DateTime.Today.ToString("MM");
            string año = DateTime.Today.ToString("yyyy");
            string hora = DateTime.Now.ToString("HH");
            string minuto = DateTime.Now.ToString("mm");

            string Referencia = dia + "/" + mes + "/" + año + " " + hora + ":" + minuto + " " + Session["sUsuario"].ToString();
            if (!Directory.Exists(RutaSitio + "/Content/FilesCargaMasivaIncidencias/LogsCarga/"))
            {
                Directory.CreateDirectory(RutaSitio + "/Content/FilesCargaMasivaIncidencias/LogsCarga/");
            }

            string pathGuardado = Path.Combine(RutaSitio + "/Content/FilesCargaMasivaIncidencias/" + dia + "_" + mes + "_" + año + "_" + hora + "_" + minuto + "_" + fileType + Path.GetExtension(fileUpload.FileName));
            string pathLogs = Path.Combine(RutaSitio + "/Content/FilesCargaMasivaIncidencias/LogsCarga/Notas_de_carga.txt");
            fileUpload.SaveAs(pathGuardado);
            CargaMasivaDao Dao = new CargaMasivaDao();
            DataTable table = Dao.ValidaArchivo(pathGuardado, fileType);

            List<string> ResutLog = new List<string>();
            int i;
            string errorh = "Error en la linea: ";
            int IsCargaMasiva = 1;
            int Periodo = int.Parse(Session["Periodo_id"].ToString());
            switch (fileType)
            {
                case "incidencias":

                    for (i = 0; i < table.Rows.Count; i++)
                    {
                        var resultvEmpresa = Dao.ValidaEmpresa(table.Rows[i]["Empresa_id"].ToString());
                        if (resultvEmpresa == 0) { ResutLog.Add(errorh + (i + 1) + ", La empresa" + table.Rows[i]["Empresa_id"].ToString() + " no existe"); }

                        var resultvEmpleado = Dao.Valida_Empleado(table.Rows[i]["Empresa_id"].ToString(), table.Rows[i]["Empleado_id"].ToString());
                        if (resultvEmpleado == 0) { ResutLog.Add(errorh + (i + 1) + ", El empleado " + table.Rows[i]["Empleado_id"].ToString() + " no existe o esta dado de baja"); }

                        var isValidDate1 = Dao.Valida_FormatoFechas(table.Rows[i]["Fecha_Aplicacion"].ToString());
                        if (isValidDate1 == 0) { ResutLog.Add(errorh + (i + 1) + ", El formato de fecha para Fecha_Aplicacion es incorrecto a dd/mm/aaaa"); }

                        var resultvRenglon = Dao.Valida_Renglon(table.Rows[i]["Empresa_id"].ToString(), table.Rows[i]["Renglon_id"].ToString());
                        if (resultvRenglon == 0) { ResutLog.Add(errorh + (i + 1) + ", El Renglon " + table.Rows[i]["Renglon_id"].ToString() + " no existe"); }

                        if (int.Parse(table.Rows[i]["Periodo_especial"].ToString()) == 1) // CAMBIO DEL PERIODO NORMAL POR EL PERIODO ESPECIAL MENOR DISPONIBLE
                        {
                            var resultvRenglonEspecial = Dao.Valida_Periodo_Especial_Existe(table.Rows[i]["Empresa_id"].ToString(), table.Rows[i]["Periodo_especial"].ToString());
                            if (resultvRenglonEspecial.iFlag == 0) { ResutLog.Add(errorh + (i + 1) + "," + resultvRenglonEspecial.sRespuesta); }
                        }
                    }

                    if (ResutLog.Count == 0)
                    {
                        for (int k = 0; k < table.Rows.Count; k++)
                        {
                            if (int.Parse(table.Rows[k]["Periodo_especial"].ToString()) == 0)
                            {
                                Dao.InsertaCargaMasivaIncidencias(table.Rows[k], IsCargaMasiva, Periodo, Referencia);
                            }
                            else if (int.Parse(table.Rows[k]["Periodo_especial"].ToString()) == 1) // CAMBIO DEL PERIODO NORMAL POR EL PERIODO ESPECIAL DISPONIBLE
                            {
                                var periodoEspecial = Dao.Valida_Periodo_Especial_Existe(table.Rows[k]["Empresa_id"].ToString(), table.Rows[k]["Periodo_especial"].ToString());

                                Dao.InsertaCargaMasivaIncidencias(table.Rows[k], IsCargaMasiva, periodoEspecial.iFlag, Referencia);
                            }
                        }
                        list.Add("1");
                        list.Add("Carga de Incidencias correcta, se cargaron " + i + " registos.");
                    }
                    else
                    {
                        StreamWriter txtfile = new StreamWriter(pathLogs);
                        foreach (var error in ResutLog)
                        {
                            txtfile.WriteLine(error);
                        }
                        txtfile.Close();
                        list.Clear();
                        list.Add("0");
                        list.Add("/Content/FilesCargaMasivaIncidencias/LogsCarga/Notas_de_carga.txt");
                    }
                    break;
                case "ausentismos":

                    for (i = 0; i < table.Rows.Count; i++)
                    {
                        var resultvEmpresa = Dao.ValidaEmpresa(table.Rows[i]["Empresa_id"].ToString());
                        if (resultvEmpresa == 0) { ResutLog.Add(errorh + (i + 1) + ", La empresa" + table.Rows[i]["Empresa_id"].ToString() + " no existe"); }

                        var isValidDate1 = Dao.Valida_FormatoFechas(table.Rows[i]["Fecha Ausentismo"].ToString());
                        if (isValidDate1 == 0) { ResutLog.Add(errorh + (i + 1) + ", El formato de fecha para Fecha Ausentismo es incorrecto a dd/mm/aaaa"); }

                        var resultvEmpleado = Dao.Valida_Empleado(table.Rows[i]["Empresa_id"].ToString(), table.Rows[i]["Empleado_id"].ToString());
                        if (resultvEmpleado == 0) { ResutLog.Add(errorh + (i + 1) + ", El empleado " + table.Rows[i]["Empleado_id"].ToString() + " no existe"); }
                        
                        var resultvCertificado = Dao.ValidaCertificado(table.Rows[i]["Certificado IMSS"].ToString());
                        if (resultvCertificado.iFlag == 1) { ResutLog.Add(errorh + (i + 1) + ", El folio del certificado " + table.Rows[i]["Certificado IMSS"].ToString() + " ya existe"); }

                    }
                    if (ResutLog.Count == 0)
                    {
                        for (int k = 0; k < table.Rows.Count; k++)
                        {
                            Dao.InsertaCargaMasivaAusentismo(table.Rows[k], Periodo, IsCargaMasiva, Referencia);
                            list.Add("1");
                            list.Add("Carga de Ausentismos correcta, se cargaron " + i + " registos.");
                        }
                    }
                    else
                    {
                        StreamWriter txtfile = new StreamWriter(pathLogs);
                        foreach (var error in ResutLog)
                        {
                            txtfile.WriteLine(error);
                        }
                        txtfile.Close();
                        list.Clear();
                        list.Add("0");
                        list.Add("/Content/FilesCargaMasivaIncidencias/LogsCarga/Notas_de_carga.txt");
                    }

                    break;
                case "créditos":

                    for (i = 0; i < table.Rows.Count; i++)
                    {
                        var resultvEmpresa = Dao.ValidaEmpresa(table.Rows[i]["Empresa_id"].ToString());
                        if (resultvEmpresa == 0) { ResutLog.Add(errorh + (i + 1) + ", La empresa" + table.Rows[i]["Empresa_id"].ToString() + " no existe"); }

                        var isValidDate1 = Dao.Valida_FormatoFechas(table.Rows[i]["Fecha Aprobación"].ToString());
                        if (isValidDate1 == 0) { ResutLog.Add(errorh + (i + 1) + ", El formato de fecha para Fecha Aprobación es incorrecto a dd/mm/aaaa"); }

                        var resultvEmpleado = Dao.Valida_Empleado(table.Rows[i]["Empresa_id"].ToString(), table.Rows[i]["Empleado_id"].ToString());
                        if (resultvEmpleado == 0) { ResutLog.Add(errorh + (i + 1) + ", El empleado " + table.Rows[i]["Empleado_id"].ToString() + " no existe"); }

                    }

                    if (ResutLog.Count == 0)
                    {
                        for (int k = 0; k < table.Rows.Count; k++)
                        {
                            Dao.InsertaCargaMasivaCreditos(table.Rows[k], Periodo, IsCargaMasiva, Referencia);
                        }
                        list.Add("1");
                        list.Add("Carga de Créditos correcta, se cargaron " + i + " registos.");
                    }
                    else
                    {
                        StreamWriter txtfile = new StreamWriter(pathLogs);
                        foreach (var error in ResutLog)
                        {
                            txtfile.WriteLine(error);
                        }
                        txtfile.Close();
                        list.Clear();
                        list.Add("0");
                        list.Add("/Content/FilesCargaMasivaIncidencias/LogsCarga/Notas_de_carga.txt");
                    }

                    break;
                case "pensiones":

                    for (i = 0; i < table.Rows.Count; i++)
                    {
                        var resultvEmpresa = Dao.ValidaEmpresa(table.Rows[i]["Empresa_id"].ToString());
                        if (resultvEmpresa == 0) { ResutLog.Add(errorh + (i + 1) + ", La empresa" + table.Rows[i]["Empresa_id"].ToString() + " no existe"); }

                        var isValidDate1 = Dao.Valida_FormatoFechas(table.Rows[i]["Fecha_oficio"].ToString());
                        if (isValidDate1 == 0) { ResutLog.Add(errorh + (i + 1) + ", El formato de fecha para Fecha Oficio es incorrecto a dd/mm/aaaa"); }

                        var resultvEmpleado = Dao.Valida_Empleado(table.Rows[i]["Empresa_id"].ToString(), table.Rows[i]["Empleado_id"].ToString());
                        if (resultvEmpleado == 0) { ResutLog.Add(errorh + (i + 1) + ", El empleado " + table.Rows[i]["Empleado_id"].ToString() + " no existe"); }
                    }

                    if (ResutLog.Count == 0)
                    {
                        for (int k = 0; k < table.Rows.Count; k++)
                        {
                            Dao.InsertaCargaMasivaPensionesAlimenticias(table.Rows[k], Periodo, IsCargaMasiva, Referencia);
                        }
                        list.Add("1");
                        list.Add("Carga de Pensiones correcta, se cargaron " + i + " registos.");
                    }
                    else
                    {
                        StreamWriter txtfile = new StreamWriter(pathLogs);
                        foreach (var error in ResutLog)
                        {
                            txtfile.WriteLine(error);
                        }
                        txtfile.Close();
                        list.Clear();
                        list.Add("0");
                        list.Add("/Content/FilesCargaMasivaIncidencias/LogsCarga/Notas_de_carga.txt");
                    }
                    break;
                case "vacaciones":
                    for (i = 0; i < table.Rows.Count; i++)
                    {
                        var resultvEmpresa = Dao.ValidaEmpresa(table.Rows[i]["Empresa_id"].ToString());
                        if (resultvEmpresa == 0) { ResutLog.Add(errorh + (i + 1) + ", La empresa" + table.Rows[i]["Empleado_id"].ToString() + " no existe"); }

                        var resultvEmpleado = Dao.Valida_Empleado(table.Rows[i]["Empresa_id"].ToString(), table.Rows[i]["Empleado_id"].ToString());
                        if (resultvEmpleado == 0) { ResutLog.Add(errorh + (i + 1) + ", El empleado " + table.Rows[i]["Empleado_id"].ToString() + " no existe"); }

                        var isValidDate1 = Dao.Valida_FormatoFechas(table.Rows[i]["Fecha_inicio"].ToString());
                        if (isValidDate1 == 0) { ResutLog.Add(errorh + (i + 1) + ", El formato de fecha para Fecha Inicio es incorrecto a dd/mm/aaaa"); }

                        var isValidDate2 = Dao.Valida_FormatoFechas(table.Rows[i]["Fecha_fin"].ToString());
                        if (isValidDate2 == 0) { ResutLog.Add(errorh + (i + 1) + ", El formato de fecha para Fecha Fin es incorrecto a dd/mm/aaaa"); }

                        List<string> resultvVacaciones = Dao.Valida_Vacaciones(table.Rows[i]["Empleado_id"].ToString(), table.Rows[i]["Empresa_id"].ToString(), table.Rows[i]["Año"].ToString(), table.Rows[i]["Dias"].ToString());
                        if (resultvVacaciones[0] == "0") { ResutLog.Add(errorh + (i + 1) + ", " + resultvVacaciones[1]); }
                    }
                    int Usuario_id = int.Parse(Session["iIdUsuario"].ToString());
                    if (ResutLog.Count == 0)
                    {
                        for (int k = 0; k < table.Rows.Count; k++)
                        {
                            Dao.InsertaCargaMasivaVacaciones(table.Rows[k], Periodo, IsCargaMasiva, Referencia, Usuario_id);
                        }
                        list.Add("1");
                        list.Add("Carga de Vacaciones correcta, se cargaron " + i + " registos.");
                    }
                    else
                    {
                        StreamWriter txtfile = new StreamWriter(pathLogs);
                        foreach (var error in ResutLog)
                        {
                            txtfile.WriteLine(error);
                        }
                        txtfile.Close();
                        list.Clear();
                        list.Add("0");
                        list.Add("/Content/FilesCargaMasivaIncidencias/LogsCarga/Notas_de_carga.txt");
                    }
                    break;
            }


            return Json(list);
        }
        [HttpPost]
        public JsonResult LoadAplicaEn(int CampoCatalogo_id)
        {
            List<CatalogoGeneralBean> lista = new List<CatalogoGeneralBean>();
            PruebaEmpresaDao Dao = new PruebaEmpresaDao();
            lista = Dao.sp_Cgeneral_Retrieve_Cgeneral(CampoCatalogo_id);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult loadLayout()
        {
            List<string> list = new List<string>();
            string RutaSitio = Server.MapPath("~/");
            list.Add(RutaSitio + "/Content/FilesCargaMasivaIncidencias/FormatoDeIncidencias/Layout_Carga_Incidencias.xlsx");
            return Json(list);
        }
        [HttpPost]
        public JsonResult UpdateCredito(int Credito_id, int TipoDescuento_id, int Descontar_id, string Descuento, string NoCredito, string FechaAprovacion, string FechaBaja)
        {
            List<string> Lista;
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int Empleado_id = int.Parse(Session["Empleado_id"].ToString());
            int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            Lista = Dao.sp_TCreditos_update_credito(Empresa_id, Credito_id, Empleado_id, TipoDescuento_id, Descontar_id, Descuento, NoCredito, FechaAprovacion, FechaBaja);
            return Json(Lista);
        }
        [HttpPost]
        public JsonResult LoadCargasMasivas()
        {
            List<List<string>> lista = new List<List<string>>();
            PruebaEmpresaDao Dao = new PruebaEmpresaDao();
            lista = Dao.sp_Retrieve_CargasMasivas();
            return Json(lista);
        }
        [HttpPost]
        public JsonResult AplazarIncidencia(int Incidencia_id, int Aplazar)
        {
            List<string> lista = new List<string>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            lista = Dao.TRegistro_Incidencias_aplaza_Incidencia(Incidencia_id, Aplazar);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult CancelaCargaMasiva(string tabla, string referencia)
        {
            List<string> lista = new List<string>();
            CargaMasivaDao Dao = new CargaMasivaDao();
            lista = Dao.sp_Cancela_CargaMasiva(tabla, referencia);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult LoadPension(int Pension_id)
        {
            List<PensionesAlimentariasBean> lista = new List<PensionesAlimentariasBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            lista = Dao.sp_TPensiones_Alimenticias_retrieve_pension(Pension_id);
            return Json(lista);
        }
        [HttpPost]
        public JsonResult ValidaCertificado(string Certificado) {
            CargaMasivaDao Dao = new CargaMasivaDao();
            ReturnBean bean = Dao.ValidaCertificado(Certificado);
            return Json(bean);
        }
    }
}