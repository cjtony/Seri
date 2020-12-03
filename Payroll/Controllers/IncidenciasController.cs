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
            //var data = new { data = lista };
            return Json(lista);
        }
        [HttpPost]
        public JsonResult SaveCredito(string TipoDescuento, string Descuento, string NoCredito, string FechaAprovacion, string Descontar, string FechaBaja, string FechaReinicio, string FactorDesc)
        {
            List<string> lista = new List<string>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int id1 = int.Parse(Session["Empleado_id"].ToString());
            int id2 = int.Parse(Session["IdEmpresa"].ToString());
            lista = Dao.sp_TCreditos_Insert_Credito(id1, id2, TipoDescuento, Descuento, NoCredito, FechaAprovacion, Descontar, FechaBaja, FechaReinicio, FactorDesc);
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
            int id1 = int.Parse(Session["Empleado_id"].ToString());
            int id2 = int.Parse(Session["IdEmpresa"].ToString());
            lista = Dao.sp_TAusentismos_Retrieve_Ausentismo_Empleado(id2, id1, IdAusentismo);
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
        public JsonResult UpdateAusentismo(int id, int Tipo_Ausentismo_id, string Recupera_Ausentismo, string Fecha_Ausentismo, int Dias_Ausentismo, string Certificado_imss, string Comentarios_imss, string Causa_FaltaInjustificada, string FechaFin, int Tipo, int IncidenciaProgramada_id)
        {
            List<string> lista = new List<string>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int id1 = int.Parse(Session["Empleado_id"].ToString());
            int id2 = int.Parse(Session["IdEmpresa"].ToString());
            int Periodo = int.Parse(Session["Periodo_id"].ToString());
            lista = Dao.sp_TAusentismos_Update_Ausentismo(id, Tipo_Ausentismo_id, id1, id2, Recupera_Ausentismo, Fecha_Ausentismo, Dias_Ausentismo, Certificado_imss, Comentarios_imss, Causa_FaltaInjustificada, Periodo, FechaFin, Tipo, IncidenciaProgramada_id);
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
            res = Dao.sp_TPensiones_Alimenticias_Insert_Pensiones(Empresa_id, Empleado_id, Cuota_fija, Porcentaje, AplicaEn, Descontar_en_finiquito, No_Oficio, Fecha_Oficio, Tipo_Calculo, Aumentar_segun_salario_minimo_general, Aumentar_segun_aumento_de_sueldo, Beneficiaria, Banco, Sucursal, Tarjeta_vales, Cuenta_Cheques, Fecha_baja);
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
        public JsonResult SaveRegistroIncidencia(int inRenglon, decimal inCantidad, int inPlazos, string inLeyenda, string inReferencia, string inFechaA)
        {
            List<string> res = new List<string>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int Empleado_id = int.Parse(Session["Empleado_id"].ToString());
            int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            int Periodo = int.Parse(Session["Periodo_id"].ToString());
            res = Dao.sp_TRegistro_incidencias_Insert_Incidencia(Empresa_id, Empleado_id, inRenglon, inCantidad, inPlazos, inLeyenda, inReferencia, inFechaA, Periodo);
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
        public PartialViewResult CargaMasiva()
        {
            return PartialView();
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
        public JsonResult SearchIncapacidades(string FechaI, string FechaF)
        {
            List<AusentismosEmpleadosBean> lista = new List<AusentismosEmpleadosBean>();
            pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
            int Empleado_id = int.Parse(Session["Empleado_id"].ToString());
            int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
            lista = Dao.sp_TAusentismos_Search_Incapacidades(Empresa_id, Empleado_id, FechaI, FechaF);
            return Json(lista);
        }
        //[HttpPost]
        //public JsonResult LoadDiasRestantesPeriodo(string FechaI, string FechaF)
        //{
        //    List<AusentismosEmpleadosBean> lista = new List<AusentismosEmpleadosBean>();
        //    pruebaEmpleadosDao Dao = new pruebaEmpleadosDao();
        //    int Empresa_id = int.Parse(Session["IdEmpresa"].ToString());
        //    int Periodo = int.Parse(Session["Periodo_id"].ToString());
        //    lista = Dao.sp_TAusentismos_Search_Incapacidades(Empresa_id, Periodo);
        //    return Json(lista);
        //}
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
        public ActionResult PIncidencias()
        {
            return PartialView();
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

            if (!Directory.Exists(RutaSitio + "/Content/FilesCargaMasivaIncidencias/LogsCarga/"))
            {
                Directory.CreateDirectory(RutaSitio + "/Content/FilesCargaMasivaIncidencias/LogsCarga/");
            }

            string pathGuardado = Path.Combine(RutaSitio + "/Content/FilesCargaMasivaIncidencias/" + dia + "_" + mes + "_" + año + "_" + hora + "_" + minuto + "_" + fileUpload.FileName);
            string pathLogs = Path.Combine(RutaSitio + "/Content/FilesCargaMasivaIncidencias/LogsCarga/Notas_de_carga.txt");

            fileUpload.SaveAs(pathGuardado);

            CargaMasivaDao Dao = new CargaMasivaDao();

            DataTable table = Dao.ValidaArchivo(pathGuardado, fileType);

            List<string> ResutLog = new List<string>();

            int i;
            int j;

            string errorh = "Error en la linea: ";

            for (i = 0; i < table.Rows.Count; i++)
            {

                var resultvEmpresa = Dao.ValidaEmpresa(table.Rows[i]["Empresa_id"].ToString());
                if (resultvEmpresa == 0) { ResutLog.Add(errorh + (i + 1) + ", La empresa" + table.Rows[i]["Empresa_id"].ToString() + " no existe"); }

                var resultvEmpleado = Dao.Valida_Empleado(table.Rows[i]["Empresa_id"].ToString(), table.Rows[i]["Empleado_id"].ToString());
                if (resultvEmpleado == 0) { ResutLog.Add(errorh + (i + 1) + ", El empleado " + table.Rows[i]["Empleado_id"].ToString() + " no existe"); }

                var resultvPeriodo = Dao.Valida_Periodo(table.Rows[i]["Empresa_id"].ToString(), table.Rows[i]["Periodo"].ToString(), table.Rows[i]["Año"].ToString());
                if (resultvPeriodo == 0) { ResutLog.Add(errorh + (i + 1) + ", El Periodo " + table.Rows[i]["Periodo"].ToString() + " es incorrecto"); }
                if (resultvPeriodo == 2) { ResutLog.Add(errorh + (i + 1) + ", El año " + table.Rows[i]["Año"].ToString() + " del periodo es incorrecto"); }

                var resultvRenglon = Dao.Valida_Renglon(table.Rows[i]["Empresa_id"].ToString(), table.Rows[i]["Renglon_id"].ToString());
                if (resultvRenglon == 0) { ResutLog.Add(errorh + (i + 1) + ", El Renglon "+ table.Rows[i]["Renglon_id"].ToString() + " no existe"); }

            }

            if (ResutLog.Count == 0)
            {
                //Console.WriteLine("Se manda al modulo de insercion la tabla");

                for (int k = 0; k < table.Rows.Count; k++)
                {
                    Dao.InsertaCargaMasiva(table.Rows[k]);
                }
                list.Add("1");
                list.Add("Carga subida correctamente");

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

            return Json(list);
        }
    }
}