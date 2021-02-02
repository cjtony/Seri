using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Payroll.Models.Daos
{
    public class pruebaEmpleadosDao : Conexion
    {
        public List<DescEmpleadoVacacionesBean> sp_Retrieve_liveSearchEmpleado(int IdEmp, string txtSearch)
        {
            
            List<DescEmpleadoVacacionesBean> list = new List<DescEmpleadoVacacionesBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Retrieve_liveSearchEmpleado", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrliIdEmpresa", IdEmp));
            cmd.Parameters.Add(new SqlParameter("@ctrlsNombreEmpleado", txtSearch));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    DescEmpleadoVacacionesBean listEmpleados = new DescEmpleadoVacacionesBean();
                    if (int.Parse(data["iFlag"].ToString()) == 0)
                    {
                        listEmpleados.iFlag = int.Parse(data["iFlag"].ToString());
                        listEmpleados.IdEmpleado = int.Parse(data["IdEmpleado"].ToString());
                        listEmpleados.Nombre_Empleado = data["Nombre_Empleado"].ToString();
                        listEmpleados.Apellido_Paterno_Empleado = data["Apellido_Paterno_Empleado"].ToString();
                        listEmpleados.Apellido_Materno_Empleado = data["Apellido_Materno_Empleado"].ToString();
                        listEmpleados.DescripcionDepartamento = data["DescripcionDepartamento"].ToString();
                        listEmpleados.DescripcionPuesto = data["DescripcionPuesto"].ToString();
                        listEmpleados.FechaIngreso = data["FechaIngreso"].ToString();
                        listEmpleados.TipoEmpleado = int.Parse(data["Cg_TipoEmpleado_id"].ToString());
                        listEmpleados.DescTipoEmpleado = data["Descripcion"].ToString();
                    }
                    else
                    {
                        listEmpleados.iFlag = int.Parse(data["iFlag"].ToString());
                        listEmpleados.Nombre_Empleado = data["title"].ToString();
                        listEmpleados.DescripcionPuesto = data["resume"].ToString();
                    }
                    list.Add(listEmpleados);
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<string> sp_Templeado_Retrieve_DatosEmpleado(int Empresa_id, int Empleado_id, int Perfil_id)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Templeado_Retrieve_DatosEmpleado", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlPerfil_id", Perfil_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    //DATOS GENERALES
                    list.Add(data["Empleado_id"].ToString());
                    list.Add(data["Nombre_Empleado"].ToString());
                    list.Add(data["Apellido_Paterno_Empleado"].ToString());
                    list.Add(data["Apellido_Materno_Empleado"].ToString());
                    list.Add(data["Fecha_Nacimiento_Empleado"].ToString());
                    list.Add(data["Lugar_Nacimiento_Empleado"].ToString());
                    list.Add(data["Titulo"].ToString());
                    list.Add(data["Genero"].ToString());
                    list.Add(data["Nacionalidad"].ToString());
                    list.Add(data["Estado_civil"].ToString());
                    list.Add(data["Codigo_Postal"].ToString());
                    list.Add(data["Estado"].ToString());
                    list.Add(data["Ciudad"].ToString());
                    list.Add(data["Colonia"].ToString());
                    list.Add(data["Calle"].ToString());
                    list.Add(data["Numero_Calle"].ToString());
                    list.Add(data["Telefono_Fijo"].ToString());
                    list.Add(data["Telefono_Movil"].ToString());
                    list.Add(data["Correo_Electronico"].ToString());
                    list.Add(data["Fecha_Matrimonio"].ToString());
                    list.Add(data["Tipo_Sangre"].ToString());
                    list.Add(data["Fecha_Alta_Emp"].ToString());
                    //IMSS
                    list.Add(data["Effdt_imss"].ToString());
                    list.Add(data["RegistroImss"].ToString());
                    list.Add(data["RFC"].ToString());
                    list.Add(data["CURP"].ToString());
                    list.Add(data["NivelEstudio"].ToString());
                    list.Add(data["NivelSocioeconomico"].ToString());
                    list.Add(data["Fecha_Alta_imss"].ToString());
                    list.Add(data["IdNomina"].ToString());
                    list.Add(data["Effdt_Nom"].ToString());
                    list.Add(data["Tipo_Periodo"].ToString());
                    list.Add(data["SalarioMensual"].ToString());
                    list.Add(data["TipoEmpleado"].ToString());
                    list.Add(data["NivelEmpleado"].ToString());
                    list.Add(data["TipoJornada"].ToString());
                    list.Add(data["TipoContrato"].ToString());
                    list.Add(data["TipoContratacion"].ToString());
                    list.Add(data["MotivoIncremento"].ToString());
                    list.Add(data["FechaIngreso"].ToString());
                    list.Add(data["FechaAntiguedad"].ToString());
                    list.Add(data["VencimientoContrato"].ToString());
                    list.Add(data["TipoPago"].ToString());
                    list.Add(data["Banco"].ToString());
                    list.Add(data["Cta_Cheques"].ToString());
                    list.Add(data["Fecha_Alta_Nom"].ToString());
                    list.Add(data["Ult_sdi"].ToString());
                    list.Add(data["PosicionCodigo"].ToString());
                    list.Add(data["NombreLocalidad"].ToString());
                    list.Add(data["CodDepartamento"].ToString());
                    list.Add(data["Afiliacion_IMSS"].ToString());
                    list.Add(data["NomPuesto"].ToString());
                    list.Add(data["ReportaA"].ToString());
                    list.Add(data["DescripcionDepartamento"].ToString());
                    list.Add(data["TipoEmpleado_id"].ToString());
                    list.Add(data["Fecha_Baja"].ToString());
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<DescEmpleadoVacacionesBean> sp_CEmpleado_Retrieve_Empleado(int IdEmpleado, int IdEmpresa)
        {
            List<DescEmpleadoVacacionesBean> list = new List<DescEmpleadoVacacionesBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TPeriodos_Verify_AllPeriods_Vacaciones", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrliIdEmpleado", IdEmpleado));
            cmd.Parameters.Add(new SqlParameter("@ctrliIdEmpresa", IdEmpresa));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows) {
                while (data.Read()) {
                    DescEmpleadoVacacionesBean listEmpleados = new DescEmpleadoVacacionesBean(); 
                    listEmpleados.iFlag = int.Parse(data["iFlag"].ToString());
                    listEmpleados.IdEmpleado = int.Parse(data["IdEmpleado"].ToString());
                    listEmpleados.Nombre_Empleado = data["Nombre_Empleado"].ToString();
                    listEmpleados.Apellido_Paterno_Empleado = data["Apellido_Paterno_Empleado"].ToString();
                    listEmpleados.Apellido_Materno_Empleado = data["Apellido_Materno_Empleado"].ToString();
                    listEmpleados.DescripcionDepartamento = data["DescripcionDepartamento"].ToString();
                    listEmpleados.DescripcionPuesto = data["DescripcionPuesto"].ToString();
                    listEmpleados.FechaIngreso = data["FechaIngreso"].ToString();
                    if (data["IdPer_Vac"].ToString() != "") {
                        listEmpleados.Id_Per_Vac = int.Parse(data["IdPer_Vac"].ToString());
                    } else {
                        listEmpleados.Id_Per_Vac = 0;

                    }
                    if (data["IdPer_Vac_Ln"].ToString() != "") {
                        listEmpleados.Id_Per_Vac_Ln = int.Parse(data["IdPer_Vac_Ln"].ToString());
                    } else {
                        listEmpleados.Id_Per_Vac_Ln = 0;
                    }
                    if (data["Anio"].ToString() != "") {
                        listEmpleados.Anio = int.Parse(data["Anio"].ToString());
                    } else {
                        listEmpleados.Anio = 0;
                    } 
                    listEmpleados.Fecha_Aniversario = data["FechaAntiguedad"].ToString();
                    listEmpleados.DiasPrima = (data["DiasPrima"].ToString().Length > 0) ? int.Parse(data["DiasPrima"].ToString()) : 0;
                    listEmpleados.DiasDisfrutados = (data["DiasDisfrutados"].ToString().Length > 0) ? int.Parse(data["DiasDisfrutados"].ToString()) : 0;
                    listEmpleados.DiasRestantes = (data["DiasRestantes"].ToString().Length > 0) ? int.Parse(data["DiasRestantes"].ToString()) : 0;
                    listEmpleados.Empresa_id = IdEmpresa.ToString();
                    listEmpleados.TipoEmpleado = int.Parse(data["Cg_TipoEmpleado_id"].ToString());
                    listEmpleados.DescTipoEmpleado = data["DescripcionTipoEmpleado"].ToString();
                    list.Add(listEmpleados);
                }
            } else {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();
            return list;
        }
        public List<PeriodoVacacionesBean> sp_Retrieve_PeriodosVacaciones(int IdEmpleado)
        {
            List<PeriodoVacacionesBean> list = new List<PeriodoVacacionesBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Retrieve_PeriodosVacaciones", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@IdEmpleado", IdEmpleado));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    PeriodoVacacionesBean listEmpleados = new PeriodoVacacionesBean
                    {
                        iIdEmpleado = int.Parse(data["IdEmpleado"].ToString()),
                        iAnio = int.Parse(data["Anio"].ToString()),
                        sFechaInicio = DateTime.Parse(data["FechaInicio"].ToString()),
                        sFechaTermino = DateTime.Parse(data["FechaTermino"].ToString()),
                        iDiasDisfrutados = int.Parse(data["DiasDisfrutados"].ToString()),
                        iDiasPrima = decimal.Parse(data["DiasPrima"].ToString())

                    };
                    list.Add(listEmpleados);
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<PoliticasVacacionesBean> sp_Retrieve_PoliticasVacaciones()
        {

            List<PoliticasVacacionesBean> list = new List<PoliticasVacacionesBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_Retrieve_PoliticasVacaciones", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    PoliticasVacacionesBean listPoliticas = new PoliticasVacacionesBean
                    {
                        iIdPolitica = int.Parse(data["iIdPolitica"].ToString()),
                        iAnio = int.Parse(data["iAnio"].ToString()),
                        iDias = int.Parse(data["iDias"].ToString()),
                        iDiasAdicionales = int.Parse(data["iDiasAdicionales"].ToString()),
                        iDiasPrima = decimal.Parse(data["iDiasPrima"].ToString())

                    };
                    list.Add(listPoliticas);
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<CreditosBean> sp_TCreditos_Retrieve_Creditos(int IdEmpleado, int IdEmpresa)
        {
            List<CreditosBean> list = new List<CreditosBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TCreditos_Retrieve_Creditos", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", IdEmpresa));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", IdEmpleado));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    CreditosBean listCreditos = new CreditosBean();
                    listCreditos.IdCredito = int.Parse(data["IdCredito"].ToString());
                    listCreditos.Empleado_id = int.Parse(data["Empleado_id"].ToString());
                    listCreditos.Empresa_id = int.Parse(data["Empresa_id"].ToString());
                    listCreditos.TipoDescuento = data["TipoDescuento"].ToString();
                    listCreditos.Cancelado = data["Cancelado"].ToString();
                    listCreditos.Descuento = data["Descuento"].ToString();
                    listCreditos.NoCredito = data["NoCredito"].ToString();
                    listCreditos.FechaAprovacionCredito = data["FechaAprobacionCredito"].ToString();
                    listCreditos.Descontar = data["Descontar"].ToString();
                    //if (data["FactorDescuento"].ToString().Length < 1 || data["FactorDescuento"].ToString() == null)
                    //{ listCreditos.FactorDescuento = ""; }
                    //else
                    //{ listCreditos.FactorDescuento = data["FactorDescuento"].ToString(); }

                    if (data["FechaBajaCredito"].ToString().Length < 1)
                    { listCreditos.FechaBaja = ""; }
                    else
                    { listCreditos.FechaBaja = data["FechaBajaCredito"].ToString(); }
                    if (data["FechaReinicioCredito"].ToString().Length < 1)
                    { listCreditos.FechaReinicio = ""; }
                    else
                    { listCreditos.FechaReinicio = data["FechaReinicioCredito"].ToString(); }

                    listCreditos.Finalizado = data["Finalizado"].ToString();
                    //listCreditos.Effdt = data["Effdt"].ToString();
                    if (data["Incidencia_Programada_id"].ToString().Length == 0 || data["Incidencia_Programada_id"] == null)
                    { }
                    else { listCreditos.IncidenciaProgramada_id = int.Parse(data["Incidencia_Programada_id"].ToString()); }
                    //listCreditos.IncidenciaProgramada_id = int.Parse(data["Incidencia_Programada_id"].ToString());
                    list.Add(listCreditos);
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<string> sp_TCreditos_Insert_Credito(int Empleado_id, int Empresa_id, string TipoDescuento, string Descuento, string NoCredito, string FechaAprovacion, string Descontar, string FechaBaja, string FechaReinicio, int Periodo)
        {
            if (FechaBaja == null)
            {
                FechaBaja = "";
            }
            if (FechaReinicio == null)
            {
                FechaReinicio = "";
            }
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TCreditos_Insert_Credito", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlTipoDescuento", TipoDescuento));
            cmd.Parameters.Add(new SqlParameter("@ctrlDescuento", Descuento));
            cmd.Parameters.Add(new SqlParameter("@ctrlNoCredito", NoCredito));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaAprovacionCredito", FechaAprovacion));
            cmd.Parameters.Add(new SqlParameter("@ctrlDescontar", Descontar));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaBajaCredito", FechaBaja));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaReinicioCredito", FechaReinicio));
            cmd.Parameters.Add(new SqlParameter("@ctrlAplicaFiniquito", "0"));
            cmd.Parameters.Add(new SqlParameter("@ctrlCargaMasiva", "0"));
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", ""));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["iFlag"].ToString());
                    list.Add(data["sRespuesta"].ToString());
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<AusentismosEmpleadosBean> sp_TAusentismos_Retrieve_Ausentismos_Empleado(int Empresa_id, int Empleado_id)
        {
            List<AusentismosEmpleadosBean> list = new List<AusentismosEmpleadosBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TAusentismos_Retrieve_Ausentismos_Empleado", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    AusentismosEmpleadosBean lista = new AusentismosEmpleadosBean();

                    lista.IdAusentismo = int.Parse(data["IdAusentismo"].ToString());
                    lista.Tipo_Ausentismo_id = int.Parse(data["Tipo_Ausentismo_id"].ToString());
                    lista.Nombre_Ausentismo = data["Descripcion"].ToString();
                    lista.Empleado_id = int.Parse(data["Empleado_id"].ToString());
                    lista.Empresa_id = int.Parse(data["Empresa_id"].ToString());
                    lista.Fecha_Ausentismo = data["Fecha_Ausentismo"].ToString();
                    lista.Dias_Ausentismo = int.Parse(data["Dias_Ausentismo"].ToString());
                    lista.Certificado_imss = data["Certificado_imss"].ToString();
                    lista.Comentarios_imss = data["Comentarios_imss"].ToString();
                    lista.Causa_FaltaInjustificada = data["Causa_FaltaInjustificada"].ToString();
                    lista.RecuperaAusentismo = data["Recupera_Ausentismo"].ToString();

                    list.Add(lista);
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<AusentismosEmpleadosBean> sp_TAusentismos_Retrieve_Ausentismo_Empleado(int Empresa_id, int Empleado_id, int IdAusentismo)
        {
            List<AusentismosEmpleadosBean> list = new List<AusentismosEmpleadosBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TAusentismos_Retrieve_Ausentismo_Empleado", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlAusentismo_id", IdAusentismo));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    AusentismosEmpleadosBean lista = new AusentismosEmpleadosBean();

                    lista.IdAusentismo = int.Parse(data["IdAusentismo"].ToString());
                    lista.Tipo_Ausentismo_id = int.Parse(data["Tipo_Ausentismo_id"].ToString());
                    lista.RecuperaAusentismo = data["Recupera_Ausentismo"].ToString();
                    lista.Empleado_id = int.Parse(data["Empleado_id"].ToString());
                    lista.Empresa_id = int.Parse(data["Empresa_id"].ToString());
                    lista.Fecha_Ausentismo = data["Fecha_Ausentismo"].ToString();
                    lista.Dias_Ausentismo = int.Parse(data["Dias_Ausentismo"].ToString());
                    lista.Certificado_imss = data["Certificado_imss"].ToString();
                    lista.Comentarios_imss = data["Comentarios_imss"].ToString();
                    lista.Causa_FaltaInjustificada = data["Causa_FaltaInjustificada"].ToString();
                    lista.IncidenciaProgramada_id = int.Parse(data["IncidenciaProgramada_id"].ToString());
                    lista.FechaFin = data["Fecha_fin"].ToString();
                    lista.Tipo = data["Tipo"].ToString();

                    list.Add(lista);
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<AusentismosEmpleadosBean> sp_TAusentismos_Retrieve_Ausentismos()
        {
            List<AusentismosEmpleadosBean> list = new List<AusentismosEmpleadosBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TAusentismos_Retrieve_Ausentismos", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };

            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    AusentismosEmpleadosBean lista = new AusentismosEmpleadosBean();

                    lista.IdAusentismo = int.Parse(data["IdAusentismo"].ToString());
                    lista.Tipo_Ausentismo_id = int.Parse(data["Tipo_Ausentismo_id"].ToString());
                    lista.Empleado_id = int.Parse(data["Empleado_id"].ToString());
                    lista.Empresa_id = int.Parse(data["Empresa_id"].ToString());
                    lista.Fecha_Ausentismo = data["Fecha_Ausentismo"].ToString();
                    lista.Dias_Ausentismo = int.Parse(data["Dias_Ausentismo"].ToString());
                    lista.Certificado_imss = data["Fecha_Ausentismo"].ToString();
                    lista.Comentarios_imss = data["Fecha_Ausentismo"].ToString();
                    lista.Causa_FaltaInjustificada = data["Fecha_Ausentismo"].ToString();

                    list.Add(lista);
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<string> sp_TAusentismos_Delete_Ausentismos(int Empresa_id, int Empleado_id, int IdAusentismo)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TAusentismos_Delete_Ausentismos", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlIdAusentismo", IdAusentismo));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["iFlag"].ToString());
                    list.Add(data["sRespuesta"].ToString());
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<string> sp_TAusentismos_Insert_Ausentismo(int Tipo_Ausentismo_id, int Empleado_id, int Empresa_id, string Recupera_Ausentismo, string Fecha_Ausentismo, int Dias_Ausentismo, string Certificado_imss, string Comentarios_imss, string Causa_FaltaInjustificada, int Periodo, string FechaFin, int Tipo)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TAusentismos_Insert_Ausentismo", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlTipo_Ausentismo_id", Tipo_Ausentismo_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlRecupera_Ausentismo", Recupera_Ausentismo));
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Ausentismo", Fecha_Ausentismo));
            cmd.Parameters.Add(new SqlParameter("@ctrlDias_Ausentismo", Dias_Ausentismo));
            cmd.Parameters.Add(new SqlParameter("@ctrlCertificado_imss", Certificado_imss));
            cmd.Parameters.Add(new SqlParameter("@ctrlComentarios_imss", Comentarios_imss));
            cmd.Parameters.Add(new SqlParameter("@ctrlCausa_FaltaInjustificada", Causa_FaltaInjustificada));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            cmd.Parameters.Add(new SqlParameter("@ctrlCargaMasiva", "0"));
            cmd.Parameters.Add(new SqlParameter("@ctrlTipo", Tipo));
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", "0"));
            cmd.Parameters.Add(new SqlParameter("@ctrlAplicaEnFiniquito", "0"));

            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["iFlag"].ToString());
                    list.Add(data["sRespuesta"].ToString());
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<string> sp_TAusentismos_Update_Ausentismo(int id, int Tipo_Ausentismo_id, int Empleado_id, int Empresa_id, string Recupera_Ausentismo, string Fecha_Ausentismo, int Dias_Ausentismo, string Certificado_imss, string Comentarios_imss, string Causa_FaltaInjustificada, int Periodo, string FechaFin, int Tipo, int IncidenciaProgramada_id)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TAusentismos_Update_Ausentismo", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlId", id));
            cmd.Parameters.Add(new SqlParameter("@ctrlTipo_Ausentismo_id", Tipo_Ausentismo_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlRecupera_Ausentismo", Recupera_Ausentismo));
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Ausentismo", Fecha_Ausentismo));
            cmd.Parameters.Add(new SqlParameter("@ctrlDias_Ausentismo", Dias_Ausentismo));
            cmd.Parameters.Add(new SqlParameter("@ctrlCertificado_imss", Certificado_imss));
            cmd.Parameters.Add(new SqlParameter("@ctrlComentarios_imss", Comentarios_imss));
            cmd.Parameters.Add(new SqlParameter("@ctrlCausa_FaltaInjustificada", Causa_FaltaInjustificada));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaFin", FechaFin));
            cmd.Parameters.Add(new SqlParameter("@ctrlTipo", Tipo));
            cmd.Parameters.Add(new SqlParameter("@ctrlIncidenciaProg_id", IncidenciaProgramada_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["iFlag"].ToString());
                    list.Add(data["sRespuesta"].ToString());
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<string> sp_TPensiones_Alimenticias_Insert_Pensiones(int Empresa_id, int Empleado_id, string Cuota_fija, int Porcentaje, string AplicaEn, string Descontar_en_finiquito, string No_Oficio, string Fecha_Oficio, int Tipo_Calculo, string Aumentar_segun_salario_minimo_general, string Aumentar_segun_aumento_de_sueldo, string Beneficiaria, int Banco, string Sucursal, string Tarjeta_vales, string Cuenta_Cheques, int Periodo)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TPensiones_Alimenticias_Insert_Pensiones", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlCuota_fija", Cuota_fija));
            cmd.Parameters.Add(new SqlParameter("@ctrlPorcentaje", Porcentaje));
            cmd.Parameters.Add(new SqlParameter("@ctrlAplicaEn", AplicaEn));
            cmd.Parameters.Add(new SqlParameter("@ctrlDescontar_en_Finiquito", Descontar_en_finiquito));
            cmd.Parameters.Add(new SqlParameter("@ctrlNo_Oficio", No_Oficio));
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Oficio", Fecha_Oficio));
            cmd.Parameters.Add(new SqlParameter("@ctrlBeneficiaria", Beneficiaria));
            cmd.Parameters.Add(new SqlParameter("@ctrlBanco", Banco));
            cmd.Parameters.Add(new SqlParameter("@ctrlSucursal", Sucursal));
            cmd.Parameters.Add(new SqlParameter("@ctrlTarjeta_Vales", Tarjeta_vales));
            cmd.Parameters.Add(new SqlParameter("@ctrlCuenta_Cheques", Cuenta_Cheques));
            cmd.Parameters.Add(new SqlParameter("@ctrlCargaMasiva", "0"));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", ""));
            cmd.Parameters.Add(new SqlParameter("@ctrlAplicaEnFiniquito", "0"));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["iFlag"].ToString());
                    list.Add(data["sRespuesta"].ToString());
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<PensionesAlimentariasBean> sp_TPensiones_Alimenticias_Retrieve_Pensiones(int Empresa_id, int Empleado_id)
        {
            List<PensionesAlimentariasBean> lista = new List<PensionesAlimentariasBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TPensiones_Alimenticias_Retrieve_Pensiones", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                //list.Add("1");
                //list.Add("Pension  Eliminado con éxito");
                while (data.Read())
                {
                    PensionesAlimentariasBean list = new PensionesAlimentariasBean();
                    list.IdPension = int.Parse(data["IdPension"].ToString());
                    list.Empresa_id = int.Parse(data["Empresa_id"].ToString());
                    list.Empleado_id = int.Parse(data["Empleado_id"].ToString());
                    if (data["Cuota_fija"].ToString().Length == 0) { list.Cuota_Fija = ""; } else { list.Cuota_Fija = data["Cuota_fija"].ToString(); }
                    if (data["Porcentaje"].ToString().Length == 0) { list.Porcentaje = 0; } else { list.Porcentaje = int.Parse(data["Porcentaje"].ToString()); }
                    if (data["Cg_AplicaEn_id"].ToString().Length == 0) { list.AplicaEn = ""; } else { list.AplicaEn = data["Cg_AplicaEn_id"].ToString(); }

                    list.Descontar_en_Finiquito = data["Descontar_en_Finiquito"].ToString();
                    list.No_Oficio = data["No_Oficio"].ToString();
                    list.Fecha_Oficio = data["Fecha_Oficio"].ToString().Substring(0, 10);
                    //list.Tipo_Calculo = data["Tipo_Calculo"].ToString();
                    //list.Aumentar_segun_salario_minimo_general = data["Aumentar_segun_salario_minimo_general"].ToString();
                    //list.Aumentar_segun_aumento_de_sueldo = data["Aumentar_segun_aumento_de_sueldo"].ToString();
                    list.Beneficiaria = data["Beneficiaria"].ToString();
                    list.Banco = int.Parse(data["Banco_id"].ToString());
                    if (data["Sucursal"].ToString().Length == 0) { list.Sucursal = ""; } else { list.Sucursal = data["Sucursal"].ToString(); }
                    if (data["Tarjeta_vales"].ToString().Length == 0) { list.Tarjeta_vales = ""; } else { list.Tarjeta_vales = data["Tarjeta_vales"].ToString(); }
                    if (data["Cuenta_cheques"].ToString().Length == 0) { list.Cuenta_cheques = ""; } else { list.Cuenta_cheques = data["Cuenta_cheques"].ToString(); }
                    //if (data["Fecha_baja"].ToString().Length == 0) { list.Fecha_baja = ""; } else { list.Fecha_baja = data["Fecha_baja"].ToString().Substring(0, 10); }
                    if (data["IncidenciaProgramada_id"].ToString().Length == 0 || data["IncidenciaProgramada_id"] == null)
                    { list.IncidenciaProgramada_id = "0"; }
                    else { list.IncidenciaProgramada_id = data["IncidenciaProgramada_id"].ToString(); }


                    lista.Add(list);

                }
            }
            else
            {
                lista = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return lista;
        }
        public List<VW_TipoIncidenciaBean> sp_VW_tipo_Incidencia_Retrieve_LoadTipoIncidencia(int Empresa_id, string txtSearch)
        {
            List<VW_TipoIncidenciaBean> list = new List<VW_TipoIncidenciaBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_VW_tipo_Incidencia_Retrieve_LoadTipoIncidencia", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrltxtSearch", txtSearch));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    VW_TipoIncidenciaBean lista = new VW_TipoIncidenciaBean();
                    lista.Ren_incid_id = int.Parse(data["Ren_incid_id"].ToString());
                    lista.Descripcion = data["Descripcion"].ToString();
                    list.Add(lista);
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<string> sp_TRegistro_incidencias_Insert_Incidencia(int Empresa_id, int Empleado_id, int Renglon, string Cantidad, int Dias, int Plazos, string Leyenda, string Referencia, string Fecha_Aplicacion, int Periodo)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TRegistro_incidencias_Insert_Incidencia", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlTipoIncidencia", Renglon));
            cmd.Parameters.Add(new SqlParameter("@ctrlCantidad", Cantidad));
            cmd.Parameters.Add(new SqlParameter("@ctrlDias", Dias));
            cmd.Parameters.Add(new SqlParameter("@ctrlPlazos", Plazos));
            cmd.Parameters.Add(new SqlParameter("@ctrlLeyenda", Leyenda));
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", Referencia));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaAplicacion", Fecha_Aplicacion));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            cmd.Parameters.Add(new SqlParameter("@ctrlCargaMasiva", "0"));
            cmd.Parameters.Add(new SqlParameter("@ctrlAplicaEnFiniquito", "0"));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["iFlag"].ToString());
                    list.Add(data["Descripcion"].ToString());
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<VW_TipoIncidenciaBean> sp_TRegistro_incidencias_Update_Incidencia(int Empresa_id, int Empleado_id, int Renglon, int Cantidad, int Plazos, string Descripcion, string Referencia, string Fecha_Aplicacion)
        {
            List<VW_TipoIncidenciaBean> list = new List<VW_TipoIncidenciaBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TRegistro_incidencias_Update_Incidencia", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    VW_TipoIncidenciaBean lista = new VW_TipoIncidenciaBean();
                    lista.Ren_incid_id = int.Parse(data["Ren_incid_id"].ToString());
                    lista.Descripcion = data["Descripcion"].ToString();
                    list.Add(lista);
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<TabIncidenciasBean> sp_TIncidencias_Retrieve_Incidencias_Empleado(int Empresa_id, int Empleado_id, int Periodo)
        {
            List<TabIncidenciasBean> list = new List<TabIncidenciasBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TIncidencias_Retrieve_Incidencias_Empleado", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    TabIncidenciasBean lista = new TabIncidenciasBean();
                    lista.Incidencia_id = int.Parse(data["Incidencia_id"].ToString());

                    if (data["IncidenciaP_id"] == null || data["IncidenciaP_id"].ToString().Length == 0)
                    {
                        lista.IncidenciaP_id = 0;
                    }
                    else
                    {
                        lista.IncidenciaP_id = int.Parse(data["IncidenciaP_id"].ToString()); //SE TIENE QUE VER COMO SE RELACIONARA CON LAS PROGRAMADAS
                    }
                    lista.Nombre_Renglon = data["Nombre_Renglon"].ToString();
                    lista.VW_TipoIncidencia_id = data["VW_Tipo_Incidencia_id"].ToString();
                    lista.Numero_dias = data["Numero_dias"].ToString();

                    //lista.Cantidad = data["Cantidad"].ToString();

                    if (decimal.Parse(data["Cantidad"].ToString()) % 1 == 0)
                    {
                        lista.Cantidad = data["Cantidad"].ToString().Substring(0, data["Cantidad"].ToString().Length - 5);
                    }
                    else
                    {
                        lista.Cantidad = data["Cantidad"].ToString().Substring(0, data["Cantidad"].ToString().Length - 2);
                    }


                    lista.Plazos = int.Parse(data["Plazos"].ToString());
                    lista.Descripcion = data["Descripcion"].ToString();
                    lista.Fecha_Aplicacion = data["Fecha_Aplicacion"].ToString();
                    lista.NPeriodo = data["NPeriodo"].ToString();
                    lista.Cancelado = data["Cancelado"].ToString();
                    list.Add(lista);
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<IncidenciasProgramadasBean> sp_TIncidencias_Programadas_Retrieve_Incidencias_Programadas(int Empresa_id, int Periodo)
        {
            List<IncidenciasProgramadasBean> list = new List<IncidenciasProgramadasBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TIncidencias_Programadas_Retrieve_Incidencias_Programadas", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", Periodo));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    IncidenciasProgramadasBean lista = new IncidenciasProgramadasBean();

                    lista.Id = int.Parse(data["id"].ToString());
                    lista.Nombre_Empleado = data["Nombre_Empleado"].ToString();
                    lista.Apellido_Paterno_Empleado = data["Apellido_Paterno_Empleado"].ToString();
                    lista.Apellido_Materno_Empleado = data["Apellido_Materno_Empleado"].ToString();
                    lista.Empleado_id = int.Parse(data["Empleado_id"].ToString());
                    lista.Nombre_Renglon = data["Nombre_Renglon"].ToString();
                    lista.Renglon_id = int.Parse(data["Renglon_id"].ToString());
                    lista.Monto_aplicar = data["Monto_aplicar"].ToString();
                    if (data["Numero_dias"].ToString().Length == 0) { lista.Numero_dias = 0; } else { lista.Numero_dias = int.Parse(data["Numero_dias"].ToString()); }
                    //lista.Numero_dias = int.Parse(data["Numero_dias"].ToString());

                    list.Add(lista);
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<string> sp_TRegistro_Incidencias_Delete_Incidencias(int Incidencias_id, int IncidenciaP_id)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TRegistro_Incidencias_Delete_Incidencias", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlIncidencia_id", Incidencias_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlIncidenciaP_id", IncidenciaP_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["iFlag"].ToString());
                    list.Add(data["sMensaje"].ToString());
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<string> sp_TCreditos_delete_Credito(int Empresa_id, int Empleado_id, int Credito_id)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TCreditos_delete_Credito", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlCredito_id", Credito_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["iFlag"].ToString());
                    list.Add(data["sMensaje"].ToString());
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<string> sp_TPensiones_Alimenticias_Delete_Pension(int Empresa_id, int Empleado_id, int Pension_id, int IncidenciaP_id)
        {
            List<string> list = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TPensiones_Alimenticias_Delete_Pension", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlPension_id", Pension_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlIncidenciaP_id", IncidenciaP_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    list.Add(data["iFlag"].ToString());
                    list.Add(data["sMensaje"].ToString());
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<AusentismosEmpleadosBean> sp_TAusentismos_Retrieve_IncapacidadesPeriodo(int Empresa_id, int Empleado_id)
        {
            List<AusentismosEmpleadosBean> list = new List<AusentismosEmpleadosBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TAusentismos_Retrieve_IncapacidadesPeriodo", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    AusentismosEmpleadosBean lista = new AusentismosEmpleadosBean();

                    lista.IdAusentismo = int.Parse(data["IdAusentismo"].ToString());
                    lista.Tipo_Ausentismo_id = int.Parse(data["Tipo_Ausentismo_id"].ToString());
                    lista.Nombre_Ausentismo = data["Descripcion"].ToString();
                    lista.Empleado_id = int.Parse(data["Empleado_id"].ToString());
                    lista.Empresa_id = int.Parse(data["Empresa_id"].ToString());
                    lista.Fecha_Ausentismo = data["Fecha_Ausentismo"].ToString();
                    lista.Dias_Ausentismo = int.Parse(data["Dias_Ausentismo"].ToString());
                    lista.Certificado_imss = data["Certificado_imss"].ToString();
                    lista.Comentarios_imss = data["Comentarios_imss"].ToString();
                    lista.Causa_FaltaInjustificada = data["Causa_FaltaInjustificada"].ToString();
                    //lista.RecuperaAusentismo = data["Recupera_Ausentismo"].ToString();
                    //lista.FechaFin = data["Fechaf"].ToString();
                    list.Add(lista);
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<AusentismosEmpleadosBean> sp_TAusentismos_Search_Incapacidades(int Empresa_id, int Empleado_id, string FechaI, string FechaF)
        {
            List<AusentismosEmpleadosBean> list = new List<AusentismosEmpleadosBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TAusentismos_Search_Incapacidades", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaInicio", FechaI));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaFin", FechaF));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    AusentismosEmpleadosBean lista = new AusentismosEmpleadosBean();

                    lista.IdAusentismo = int.Parse(data["IdAusentismo"].ToString());
                    lista.Tipo_Ausentismo_id = int.Parse(data["Tipo_Ausentismo_id"].ToString());
                    lista.Nombre_Ausentismo = data["Descripcion"].ToString();
                    lista.Empleado_id = int.Parse(data["Empleado_id"].ToString());
                    lista.Empresa_id = int.Parse(data["Empresa_id"].ToString());
                    lista.Fecha_Ausentismo = data["Fecha_Ausentismo"].ToString();
                    lista.Dias_Ausentismo = int.Parse(data["Dias_Ausentismo"].ToString());
                    lista.Certificado_imss = data["Certificado_imss"].ToString();
                    lista.Comentarios_imss = data["Comentarios_imss"].ToString();
                    lista.Causa_FaltaInjustificada = data["Causa_FaltaInjustificada"].ToString();
                    lista.RecuperaAusentismo = data["Recupera_Ausentismo"].ToString();
                    lista.FechaFin = data["Fechaf"].ToString();
                    list.Add(lista);
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }
        public List<CreditosBean> sp_TCreditos_Retrieve_Credito(int IdEmpleado, int IdEmpresa, int Credito_id)
        {
            List<CreditosBean> list = new List<CreditosBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TCreditos_Retrieve_Credito", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", IdEmpresa));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", IdEmpleado));
            cmd.Parameters.Add(new SqlParameter("@ctrlCredito_id", Credito_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    CreditosBean listCreditos = new CreditosBean();
                    listCreditos.IdCredito = int.Parse(data["IdCredito"].ToString());
                    listCreditos.Empleado_id = int.Parse(data["Empleado_id"].ToString());
                    listCreditos.Empresa_id = int.Parse(data["Empresa_id"].ToString());
                    listCreditos.TipoDescuento = data["TipoDescuento"].ToString();
                    //listCreditos.SeguroVivienda = data["SeguroVivienda"].ToString();
                    listCreditos.Descuento = data["Descuento"].ToString();
                    listCreditos.NoCredito = data["NoCredito"].ToString();
                    listCreditos.FechaAprovacionCredito = data["FechaAprobacionCredito"].ToString();
                    listCreditos.Descontar = data["Descontar"].ToString();
                    if (data["FechaBajaCredito"].ToString().Length < 1)
                    { listCreditos.FechaBaja = ""; }
                    else
                    { listCreditos.FechaBaja = data["FechaBajaCredito"].ToString(); }
                    if (data["FechaReinicioCredito"].ToString().Length < 1)
                    { listCreditos.FechaReinicio = ""; }
                    else
                    { listCreditos.FechaReinicio = data["FechaReinicioCredito"].ToString(); }

                    listCreditos.Finalizado = data["Finalizado"].ToString();
                    //listCreditos.Effdt = data["Effdt"].ToString();
                    //listCreditos.IncidenciaProgramada_id = int.Parse(data["IncidenciaProgramada_id"].ToString());
                    list.Add(listCreditos);
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return list;
        }

        public List<IncidenciasBean> sp_TRegistro_Incidencias_retrieve_incidencia(int Empresa_id, int Empleado_id, int Incidencia_id)
        {
            List<IncidenciasBean> list = new List<IncidenciasBean>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TRegistro_Incidencias_retrieve_incidencia", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlIncidencia_id", Incidencia_id));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    IncidenciasBean lista = new IncidenciasBean();
                    lista.IdTRegistro_Incidencia = int.Parse(data["IdTRegistro_Incidencia"].ToString());
                    lista.Concepto = data["Concepto"].ToString();
                    lista.Renglon = int.Parse(data["VW_Tipo_Incidencia_id"].ToString());
                    lista.Plazos = int.Parse(data["Plazos"].ToString());
                    lista.Pagos_restantes = data["Pagos_restantes"].ToString();
                    lista.Cantidad = data["Cantidad"].ToString();
                    lista.Saldo = data["Saldo"].ToString();
                    lista.Descripcion = data["Descripcion"].ToString();
                    lista.Referencia = data["Referencia"].ToString();
                    lista.Fecha_Aplicacion = data["Fecha_Aplicacion"].ToString();
                    lista.Numero_dias = data["Numero_dias"].ToString();
                    list.Add(lista);
                }
            }
            else
            {
                list = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();
            return list;
        }
        public List<string> sp_TRegistro_Incidencias_update_incidencia(int Empresa_id, int Empleado_id, int Incidencia_id, int Renglon_id, string Cantidad, string Saldo, int Plazos, int Pagos_restantes, string Leyenda, string Referencia, string Fecha_Aplicacion, string Numero_dias)
        {
            List<string> lista = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TRegistro_Incidencias_update_incidencia", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlIncidencia_id", Incidencia_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlRenglon_id", Renglon_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlCantidad", Cantidad));
            cmd.Parameters.Add(new SqlParameter("@ctrlSaldo", Saldo));
            cmd.Parameters.Add(new SqlParameter("@ctrlPlazos", Plazos));
            cmd.Parameters.Add(new SqlParameter("@ctrlPagos_restantes", Pagos_restantes));
            cmd.Parameters.Add(new SqlParameter("@ctrlDescripcion", Leyenda));
            cmd.Parameters.Add(new SqlParameter("@ctrlReferencia", Referencia));
            cmd.Parameters.Add(new SqlParameter("@ctrlFecha_Aplicacion", Fecha_Aplicacion));
            cmd.Parameters.Add(new SqlParameter("@ctrlNumero_dias", Numero_dias));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    lista.Add(data["Flag"].ToString());
                    lista.Add(data["Message"].ToString());
                }
            }
            else
            {
                lista = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return lista;
        }
        public List<string> sp_TCreditos_update_credito(int Empresa_id, int Credito_id, int Empleado_id, int TipoDescuento_id, int Descontar_id, string Descuento, string NoCredito, string FechaAprovacion, string FechaBaja)
        {
            List<string> lista = new List<string>();
            this.Conectar();
            SqlCommand cmd = new SqlCommand("sp_TCreditos_update_credito", this.conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpresa_id", Empresa_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlCredito_id", Credito_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlEmpleado_id", Empleado_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlTipoDescuento_id", TipoDescuento_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlDescontar_id", Descontar_id));
            cmd.Parameters.Add(new SqlParameter("@ctrlDescuento", Descuento));
            cmd.Parameters.Add(new SqlParameter("@ctrlNoCredito", NoCredito));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaAprovacion", FechaAprovacion));
            cmd.Parameters.Add(new SqlParameter("@ctrlFechaBaja", FechaBaja));
            SqlDataReader data = cmd.ExecuteReader();
            cmd.Dispose();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    lista.Add(data["iFlag"].ToString());
                    lista.Add(data["sRespuesta"].ToString());
                }
            }
            else
            {
                lista = null;
            }
            data.Close();
            this.conexion.Close(); this.Conectar().Close();

            return lista;
        }

    }
}