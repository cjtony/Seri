using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Payroll.Models.Utilerias;
using Payroll.Models.Beans;
using System.Data.SqlClient;
using System.Data;

namespace Payroll.Models.Daos
{
    public class GruposEmpresasDao : Conexion
    {
        public List<GruposEmpresasBean> sp_Datos_GruposEmpresas (int stateGrpBusiness)
        {
            List<GruposEmpresasBean> listGrpBusinessBean = new List<GruposEmpresasBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_GruposEmpresas", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Estado", stateGrpBusiness));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        GruposEmpresasBean grpBusinessBean = new GruposEmpresasBean();
                        grpBusinessBean.iIdGrupoEmpresa    = Convert.ToInt32(data["IdGrupoEmpresa"].ToString());
                        grpBusinessBean.sNombreGrupo       = data["NombreGrupo"].ToString();
                        grpBusinessBean.iEstadoGrupo       = Convert.ToInt32(data["EstadoGrupo"].ToString());
                        listGrpBusinessBean.Add(grpBusinessBean);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return listGrpBusinessBean;
        }

        public List<GruposEmpresasBean> sp_Datos_EmpresasGrupo (int keyBusinessGroup)
        {
            List<GruposEmpresasBean> listBusinessGroupBean = new List<GruposEmpresasBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_EmpresasGrupo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdGrupoEmpresa", keyBusinessGroup));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        GruposEmpresasBean businessGroup = new GruposEmpresasBean();
                        businessGroup.iIdGrupoEmpresa    = Convert.ToInt32(data["IdGrupoEmpresa"].ToString());
                        businessGroup.sNombreGrupo       = data["NombreGrupo"].ToString();
                        businessGroup.iEmpresa_id        = Convert.ToInt32(data["IdEmpresa"].ToString());
                        businessGroup.sNombre_empresa    = data["NombreEmpresa"].ToString();
                        businessGroup.sRfc               = data["RFC"].ToString();
                        listBusinessGroupBean.Add(businessGroup);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return listBusinessGroupBean;
        }
    }

    public class ReportesCatalogos : Conexion
    {

        // REPORTES GENERALES \\

        public DataTable sp_Reporte_Catalogos_General(string report)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                string nameStored = "";
                if (report == "DEPARTAMENTOS") {
                    nameStored = "sp_Reporte_Departamentos_General";
                } else if (report == "PUESTOS") {
                    nameStored = "sp_Reporte_Puestos_General";
                } else if (report == "LOCALIDADES") {
                    nameStored = "sp_Reporte_Localidades_General"; 
                } else if (report == "SUCURSALES") {
                    nameStored = "sp_Reporte_Sucursales_General";
                } else if (report == "POSICIONES") {
                    nameStored = "sp_Reporte_Posiciones_General";
                } else if (report == "CENTROSCOSTO") {
                    nameStored = "sp_Reporte_CentrosCosto_General";
                } else if (report == "REGIONALES") {
                    nameStored = "sp_Reporte_Regionales_General";
                }
                SqlCommand cmd = new SqlCommand(nameStored, this.conexion) { CommandType = CommandType.StoredProcedure };
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear();
                cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }


        public DataTable sp_Reporte_Posiciones (int keyBusiness)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Reporte_Posiciones", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear();
                cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Reporte_Puestos(int keyBusiness)
        {
            DataTable dataTable = new DataTable();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Reporte_Puestos", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }
            finally
            {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Reporte_Departamentos(int keyBusiness)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Reporte_Departamentos", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear();
                cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Reporte_Localidades(int keyBusiness)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Reporte_Localidades", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear();
                cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Reporte_CentrosCosto(int keyBusiness)
        {
            DataTable dataTable = new DataTable();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Reporte_CentrosCosto", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }
            finally
            {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

    }

    public class ReportesDao : Conexion
    {
        public Boolean sp_Elimina_Version_Hoja_Calculo (int idControl)
        {
            Boolean resultado = false;
            try {
                SqlCommand cmd = new SqlCommand("sp_Elimina_Version_Hoja_Calculo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdControl", idControl));
                if (cmd.ExecuteNonQuery() > 0) {
                    resultado = true;
                }
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return resultado;
        }

        public VersionesHC sp_Comprueba_Ultima_Version_Hoja_Calculo (int periodo, int anio, int empresaOGrupoId, string tipoHC, int tipoPeriodoId, string nombreArchivo)
        {
            VersionesHC versiones = new VersionesHC();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Comprueba_Ultima_Version_Hoja_Calculo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Periodo", periodo));
                cmd.Parameters.Add(new SqlParameter("@Anio", anio));
                cmd.Parameters.Add(new SqlParameter("@EmpresaOGrupoId", empresaOGrupoId));
                cmd.Parameters.Add(new SqlParameter("@TipoHojaCalculo", tipoHC));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodoId", tipoPeriodoId));
                cmd.Parameters.Add(new SqlParameter("@NombreArchivo", nombreArchivo));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    if (dataReader["Version"].ToString() != "0") {
                        versiones.iBandera = 1;
                        versiones.iVersion = Convert.ToInt32(dataReader["Version"].ToString());
                        versiones.sNombreArchioVersion = dataReader["NombreArchivo"].ToString();
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                versiones.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return versiones;
        }

        public VersionesHC sp_Inserta_Ultima_Version_Hoja_Calculo(string tipoHC, int empresaOGrupoId, int periodo, int anio, int tipoPeriodoId, string nombreArchivo, int usuarioId, string rutaArchivo)
        {
            VersionesHC versionesHC = new VersionesHC();
            versionesHC.iBandera    = 0;
            versionesHC.sMensaje    = "none";
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Inserta_Ultima_Version_Hoja_Calculo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@TipoHojaCalculo", tipoHC));
                cmd.Parameters.Add(new SqlParameter("@EmpresaOGrupoId", empresaOGrupoId));
                cmd.Parameters.Add(new SqlParameter("@Periodo", periodo));
                cmd.Parameters.Add(new SqlParameter("@Anio", anio));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodoId", tipoPeriodoId));
                cmd.Parameters.Add(new SqlParameter("@NombreArchivo", nombreArchivo));
                cmd.Parameters.Add(new SqlParameter("@RutaArchivo", rutaArchivo));
                cmd.Parameters.Add(new SqlParameter("@UsuarioId", usuarioId));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    if (dataReader["Bandera"].ToString() == "1") {
                        versionesHC.iBandera = 1;
                        versionesHC.iVersion = Convert.ToInt32(dataReader["Version"].ToString());
                        versionesHC.sNombreArchioVersion = dataReader["NombreArchivo"].ToString();
                        versionesHC.iIdControl = Convert.ToInt32(dataReader["IdControl"].ToString());
                        versionesHC.sMensaje   = "success";
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                versionesHC.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return versionesHC;
        }
        public ListRenglonesGruposRestas sp_Genera_Resta_Importes_Reporte_Dispersion(int IdEmpresa, int IdEmpleado, int Periodo, int TipoPeriodo, int Anio)
        {
            ListRenglonesGruposRestas r = new ListRenglonesGruposRestas();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Genera_Resta_Importes_Reporte_Dispersion", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodoId", TipoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@Anio", Anio));
                SqlDataReader data = cmd.ExecuteReader(); 
                if (data.Read()) {
                    if (data["Bandera"].ToString() == "1") {
                        r.dTotal = Convert.ToDouble(data["Total"].ToString());
                        r.decimalTotalDispersion = Convert.ToDecimal(data["TOTALDIS"].ToString());
                        r.doubleTotalDispersion  = Convert.ToDouble(data["TOTALDIS"].ToString());
                    } else {
                        r.dTotal = Convert.ToDouble(data["Total"].ToString());
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            }catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return r;
        }

        public List<ListRenglonesGruposRestas> sp_Reporte_Dispersion_Empresas_Resta (int keyBusiness, int yearPeriod, int typePeriod, int numberPeriod)
        {
            List<ListRenglonesGruposRestas> list = new List<ListRenglonesGruposRestas>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Reporte_Dispersion_Empresas_Resta", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodo", typePeriod));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        ListRenglonesGruposRestas r = new ListRenglonesGruposRestas();
                        r.iAnio = Convert.ToInt32(data["Anio"].ToString());
                        r.iPeriodo = Convert.ToInt32(data["Periodo"].ToString());
                        r.sEspejo = data["Espejo"].ToString();
                        r.iEmpresa = Convert.ToInt32(data["Empresa"].ToString());
                        r.sNombreEmpresa = data["NombreEmpresa"].ToString();
                        r.iNomina = Convert.ToInt32(data["Nomina"].ToString());
                        r.sPaterno = data["ApellidoPaterno"].ToString();
                        r.sMaterno = data["ApellidoMaterno"].ToString();
                        r.sNombre = data["NombreEmpleado"].ToString();
                        r.sTipoPago = data["TipodePago"].ToString();
                        r.iBanco = Convert.ToInt32(data["IdBanco"].ToString());
                        r.sNombreBanco = data["NombreBanco"].ToString();
                        r.sCuenta = data["Cuenta"].ToString();
                        list.Add(r);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return list;
        }

        public int sp_Comprueba_Empresa_Existencia_Grupo(int IdEmpresa)
        {
            int resultado = 0;
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Comprueba_Empresa_Existencia_Grupo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    if (dataReader["Bandera"].ToString() == "1") {
                        resultado = 1;
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return resultado;
        }

        // Tipos de periodos disponibles
        public List<TipoPeriodoBean> sp_Available_Type_Periods_Business(int year, int key, int type, int period)
        {
            List<TipoPeriodoBean> tipoPeriodos = new List<TipoPeriodoBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Available_Type_Periods_Business", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Anio", year));
                cmd.Parameters.Add(new SqlParameter("@Empresa", key));
                cmd.Parameters.Add(new SqlParameter("@Tipo", type));
                cmd.Parameters.Add(new SqlParameter("@Periodo", period));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows) {
                    while (dataReader.Read()) {
                        TipoPeriodoBean bean = new TipoPeriodoBean();
                        bean.iTipoPeriodo    = Convert.ToInt32(dataReader["Tipo_Periodo_id"]);
                        bean.sValor = dataReader["Valor"].ToString();
                        tipoPeriodos.Add(bean);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return tipoPeriodos;
        }


        // Periodos disponibles
        public List<PeriodoBean> sp_Available_Periods_Business (int year, int key, int type)
        {
            List<PeriodoBean> periodos = new List<PeriodoBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Available_Periods_Business", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Anio", year));
                cmd.Parameters.Add(new SqlParameter("@Empresa", key));
                cmd.Parameters.Add(new SqlParameter("@Tipo", type));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows) {
                    while (dataReader.Read()) {
                        PeriodoBean bean = new PeriodoBean();
                        bean.iPeriodo    = Convert.ToInt32(dataReader["Periodo"]);
                        bean.sFechaInicio = dataReader["Fecha_Inicio"].ToString();
                        bean.sFechaFinal  = dataReader["Fecha_Final"].ToString();
                        periodos.Add(bean);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return periodos;
        }

        // REPORTE BAJAS
        public List<RenglonesHCBean> sp_Renglones_Hoja_Calculo_BAJAS(int keyBusiness, int typePeriod, int numberPeriod, int yearPeriod, int ismirror, int start, int end)
        {
            List<RenglonesHCBean> renglonesHCBeans = new List<RenglonesHCBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Renglones_Hoja_Calculo_BAJAS", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Espejo", ismirror));
                cmd.Parameters.Add(new SqlParameter("@Inicio", start));
                cmd.Parameters.Add(new SqlParameter("@Final", end));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        RenglonesHCBean renglones = new RenglonesHCBean();
                        renglones.iIdRenglon      = Convert.ToInt32(data["Renglon_id"].ToString());
                        renglones.sNombreRenglon  = data["Nombre_Renglon"].ToString();
                        renglonesHCBeans.Add(renglones);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return renglonesHCBeans;
        }

        public List<DatosGeneralesHC> sp_Datos_Generales_HC_BAJAS(int keyBusiness, int typePeriod, int numberPeriod, int yearPeriod, int typeSend, string option)
        {
            List<DatosGeneralesHC> datosGenerales = new List<DatosGeneralesHC>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Generales_HC_BAJAS", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Opcion", option));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        DatosGeneralesHC dato = new DatosGeneralesHC();
                        dato.iAnio = (data["Anio"].ToString() != "") ? Convert.ToInt32(data["Anio"].ToString()) : 0;
                        dato.iPeriodo = (data["Periodo"].ToString() != "") ? Convert.ToInt32(data["Periodo"].ToString()) : 0;
                        dato.iEmpresa = (data["EMPRESA"].ToString() != "") ? Convert.ToInt32(data["EMPRESA"].ToString()) : 0;
                        dato.sEmpresa = (data["NombreEmpresa"].ToString() != "") ? data["NombreEmpresa"].ToString() : "NA";
                        dato.iNomina = (data["NOMINA"].ToString() != "") ? Convert.ToInt32(data["NOMINA"].ToString()) : 0;
                        dato.sPaterno = (data["Apellido_Paterno_Empleado"].ToString() != "") ? data["Apellido_Paterno_Empleado"].ToString() : "NA";
                        dato.sMaterno = (data["Apellido_Materno_Empleado"].ToString() != "") ? data["Apellido_Materno_Empleado"].ToString() : "NA";
                        dato.sNombreE = (data["Nombre_Empleado"].ToString() != "") ? data["Nombre_Empleado"].ToString() : "NA";
                        dato.sRegImss = (data["RegistroImss"].ToString() != "") ? data["RegistroImss"].ToString() : "NA";
                        dato.sRfc = (data["RFC"].ToString() != "") ? data["RFC"].ToString() : "NA";
                        dato.sCurp = (data["CURP"].ToString() != "") ? data["CURP"].ToString() : "NA";
                        dato.sPuesto = (data["PuestoCodigo"].ToString() != "") ? data["PuestoCodigo"].ToString() : "NA";
                        dato.sNombrePuesto = (data["NombrePuesto"].ToString() != "") ? data["NombrePuesto"].ToString() : "NA";
                        dato.sNivelJerarquico = (data["NivelJerarquico"].ToString() != "") ? data["NivelJerarquico"].ToString() : "NA";
                        dato.sDepto = (data["Depto_Codigo"].ToString() != "") ? data["Depto_Codigo"].ToString() : "NA";
                        dato.sNombreDepto = (data["DescripcionDepartamento"].ToString() != "") ? data["DescripcionDepartamento"].ToString() : "NA";
                        dato.sCentrCosto = (data["CentroCosto"].ToString() != "") ? data["CentroCosto"].ToString() : "NA";
                        dato.sDescCentrCosto = (data["DescripcionCentroCosto"].ToString() != "") ? data["DescripcionCentroCosto"].ToString() : "NA";
                        dato.iRegional = (data["IdRegional"].ToString() != "") ? Convert.ToInt32(data["IdRegional"].ToString()) : 0;
                        dato.sClvRegional = (data["Clave_Regional"].ToString() != "") ? data["Clave_Regional"].ToString() : "NA";
                        dato.sDescRegional = (data["Descripcion_Regional"].ToString() != "") ? data["Descripcion_Regional"].ToString() : "NA";
                        dato.iSucursal = (data["IdSucursal"].ToString() != "") ? Convert.ToInt32(data["IdSucursal"].ToString()) : 0;
                        dato.sClvSucursal = (data["Clave_Sucursal"].ToString() != "") ? data["Clave_Sucursal"].ToString() : "NA";
                        dato.sDescSucursal = (data["Descripcion_Sucursal"].ToString() != "") ? data["Descripcion_Sucursal"].ToString() : "NA";
                        dato.sFechaAnt = (data["FechaAntiguedad"].ToString() != "") ? data["FechaAntiguedad"].ToString() : "NA";
                        dato.sFechaIng = (data["FechaIngreso"].ToString() != "") ? data["FechaIngreso"].ToString() : "NA";
                        dato.dSueldo = (data["SalarioMensual"].ToString() != "") ? Convert.ToDecimal(data["SalarioMensual"].ToString()) : 0;
                        dato.iVacanteC = (data["Posicion_id"].ToString() != "") ? Convert.ToInt32(data["Posicion_id"].ToString()) : 0;
                        dato.iUltimaPos = (data["UltimaPos"].ToString() != "") ? Convert.ToInt32(data["UltimaPos"].ToString()) : 0;
                        dato.dUltSdi = (data["Ult_sdi"].ToString() != "") ? Convert.ToDecimal(data["Ult_sdi"].ToString()) : 0;
                        datosGenerales.Add(dato);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }
            finally
            {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosGenerales;
        }

        public DetallesRenglon sp_Detalle_Renglones_BAJAS(int keyBusiness, int keyEmployee, int numberPeriod, int typePeriod, int yearPeriod, int keyRenglon, int ismirror)
        {
            DetallesRenglon detallesRenglon = new DetallesRenglon();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Detalle_Renglones_BAJAS", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Renglon", keyRenglon));
                cmd.Parameters.Add(new SqlParameter("@Espejo", ismirror));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    detallesRenglon.dSaldo   = (data["Saldo"].ToString() != "") ? Convert.ToDecimal(data["Saldo"].ToString()) : 0;
                    detallesRenglon.iRenglon = (data["Renglon_id"].ToString() != "") ? Convert.ToInt32(data["Renglon_id"].ToString()) : 0;
                } else {
                    detallesRenglon.dSaldo   = 0;
                    detallesRenglon.iRenglon = 0;
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return detallesRenglon;
        }

        public DetallesRenglon sp_Liquidos_Espejo_No_Espejo_BAJAS(int keyBusiness, int typePeriod, int numberPeriod, int yearPeriod, int ismirror, int keyEmployee)
        {
            DetallesRenglon renglonesHC = new DetallesRenglon();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Liquidos_Espejo_No_Espejo_BAJAS", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Espejo", ismirror));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    if (Convert.ToInt32(data["Saldo"]) > 0) {
                        renglonesHC.iRenglon = 9999;
                        renglonesHC.dSaldo = Convert.ToDecimal(data["Saldo"]);
                    } else {
                        renglonesHC.iRenglon = 9999;
                    }
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return renglonesHC;
        }

        public DetallesRenglon sp_Liquidos_Espejo_No_Espejo_Acumulados(int keyBusiness, int typePeriod, int numberPeriod, int periodEnd, int yearPeriod, int ismirror, int keyEmployee)
        {
            DetallesRenglon renglonesHC = new DetallesRenglon();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Liquidos_Espejo_No_Espejo_Acumulados", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@PeriodoInicio", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@PeriodoFinal", periodEnd));
                cmd.Parameters.Add(new SqlParameter("TipoPeriodo", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Espejo", ismirror));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    if (Convert.ToInt32(data["Saldo"]) > 0) {
                        renglonesHC.iRenglon = 9999;
                        renglonesHC.dSaldo = Convert.ToDecimal(data["Saldo"]);
                    } else {
                        renglonesHC.iRenglon = 9999;
                    }
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return renglonesHC;
        }

        public DetallesRenglon sp_Liquidos_Espejo_No_Espejo(int keyBusiness, int typePeriod, int numberPeriod, int yearPeriod, int ismirror, int keyEmployee)
        {
            DetallesRenglon renglonesHC = new DetallesRenglon();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Liquidos_Espejo_No_Espejo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("TipoPeriodo", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Espejo", ismirror));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    if (Convert.ToInt32(data["Saldo"]) > 0) {
                        renglonesHC.iRenglon = 9999;
                        renglonesHC.dSaldo   = Convert.ToDecimal(data["Saldo"]);
                    } else {
                        renglonesHC.iRenglon = 9999;
                    }
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return renglonesHC;
        }

        public List<RenglonesHCBean> sp_Renglones_Hoja_Calculo_Acumulados(int keyBusiness, int typePeriod, int numberPeriod,  int periodEnd, int yearPeriod, int ismirror, int start, int end, string tipo)
        {
            List<RenglonesHCBean> renglonesHCBeans = new List<RenglonesHCBean>();
            List<RenglonesHCBean> renglonesHCBeans1 = new List<RenglonesHCBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Renglones_Hoja_Calculo_Acumulados", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodo", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@PeriodoInicio", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@PeriodoFinal", periodEnd));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Espejo", ismirror));
                cmd.Parameters.Add(new SqlParameter("@Inicio", start));
                cmd.Parameters.Add(new SqlParameter("@Final", end));
                cmd.Parameters.Add(new SqlParameter("@Tipo", tipo));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        RenglonesHCBean renglones = new RenglonesHCBean();
                        renglones.iIdRenglon = Convert.ToInt32(data["Renglon_id"].ToString());
                        renglones.sNombreRenglon = data["Nombre_Renglon"].ToString();
                        if (ismirror == 0 && start == 0) {
                            if (Convert.ToInt32(data["Renglon_id"].ToString()) == 24 || Convert.ToInt32(data["Renglon_id"].ToString()) == 31 ||
                                Convert.ToInt32(data["Renglon_id"].ToString()) == 32 || Convert.ToInt32(data["Renglon_id"].ToString()) == 34 ||
                                Convert.ToInt32(data["Renglon_id"].ToString()) == 36 || Convert.ToInt32(data["Renglon_id"].ToString()) == 45 ||
                                Convert.ToInt32(data["Renglon_id"].ToString()) == 46) {
                                renglonesHCBeans.Add(renglones);
                            } else {
                                renglonesHCBeans1.Add(new RenglonesHCBean { iIdRenglon = Convert.ToInt32(data["Renglon_id"].ToString()), sNombreRenglon = data["Nombre_Renglon"].ToString() });
                            }
                        } else if (ismirror == 0 && start > 0) {
                            if (Convert.ToInt32(data["Renglon_id"].ToString()) == 1201) {
                                renglonesHCBeans.Add(renglones);
                            } else {
                                renglonesHCBeans1.Add(new RenglonesHCBean { iIdRenglon = Convert.ToInt32(data["Renglon_id"].ToString()), sNombreRenglon = data["Nombre_Renglon"].ToString() });
                            }
                        } else {
                            renglonesHCBeans.Add(renglones);
                        }
                    }
                }
                if (renglonesHCBeans1.Count > 0) {
                    foreach (RenglonesHCBean hc in renglonesHCBeans1) {
                        renglonesHCBeans.Add(new RenglonesHCBean { iIdRenglon = hc.iIdRenglon, sNombreRenglon = hc.sNombreRenglon });
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return renglonesHCBeans;
        }

        // Obtenemos los renglones de la hc que fueron calculados
        public List<RenglonesHCBean> sp_Renglones_Hoja_Calculo(int keyBusiness, int typePeriod, int numberPeriod, int yearPeriod, int ismirror, int start, int end, string tipo)
        {
            List<RenglonesHCBean> renglonesHCBeans  = new List<RenglonesHCBean>();
            List<RenglonesHCBean> renglonesHCBeans1 = new List<RenglonesHCBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Renglones_Hoja_Calculo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodo", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Espejo", ismirror));
                cmd.Parameters.Add(new SqlParameter("@Inicio", start));
                cmd.Parameters.Add(new SqlParameter("@Final", end));
                cmd.Parameters.Add(new SqlParameter("@Tipo", tipo));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        RenglonesHCBean renglones = new RenglonesHCBean();
                        renglones.iIdRenglon      = Convert.ToInt32(data["Renglon_id"].ToString());
                        renglones.sNombreRenglon  = data["Nombre_Renglon"].ToString();
                        if (ismirror == 0 && start == 0) {
                            if (Convert.ToInt32(data["Renglon_id"].ToString()) == 24    || Convert.ToInt32(data["Renglon_id"].ToString()) == 31    || 
                                Convert.ToInt32(data["Renglon_id"].ToString()) == 32    || Convert.ToInt32(data["Renglon_id"].ToString()) == 34    || 
                                Convert.ToInt32(data["Renglon_id"].ToString()) == 36    || Convert.ToInt32(data["Renglon_id"].ToString()) == 45    ||
                                Convert.ToInt32(data["Renglon_id"].ToString()) == 46) {
                                renglonesHCBeans.Add(renglones);
                            } else {
                                renglonesHCBeans1.Add(new RenglonesHCBean { iIdRenglon = Convert.ToInt32(data["Renglon_id"].ToString()), sNombreRenglon = data["Nombre_Renglon"].ToString() });
                            }
                        } else if (ismirror == 0 && start > 0) {
                            if (Convert.ToInt32(data["Renglon_id"].ToString()) == 1201) {
                                renglonesHCBeans.Add(renglones);
                            } else {
                                renglonesHCBeans1.Add(new RenglonesHCBean { iIdRenglon = Convert.ToInt32(data["Renglon_id"].ToString()), sNombreRenglon = data["Nombre_Renglon"].ToString() });
                            }
                        } else {
                            renglonesHCBeans.Add(renglones);
                        }
                    }
                }
                if (renglonesHCBeans1.Count > 0) {
                    foreach (RenglonesHCBean hc in renglonesHCBeans1) {
                        renglonesHCBeans.Add(new RenglonesHCBean { iIdRenglon = hc.iIdRenglon, sNombreRenglon = hc.sNombreRenglon });
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return renglonesHCBeans;
        }

        // Obtenemos los renglones de la hc que fueron calculados de forma consecutiva
        public List<RenglonesHCBean> sp_Renglones_HC_Consecutivo(int keyBusiness, int numberPeriod, int typePeriod, int yearPeriod, int isMirror)
        {
            List<RenglonesHCBean> renglonesHCBeans = new List<RenglonesHCBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Renglones_HC_Consecutivo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Empresa_id", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodo", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Espejo", isMirror));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        RenglonesHCBean hCBean = new RenglonesHCBean();
                        hCBean.iIdRenglon      = Convert.ToInt32(data["Renglon_id"].ToString());
                        hCBean.iConsecutivo    = Convert.ToInt32(data["Consecutivo"].ToString());
                        hCBean.sNombreRenglon  = data["Nombre_Renglon"].ToString();
                        renglonesHCBeans.Add(hCBean);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return renglonesHCBeans;
        }

        public List<DatosGeneralesHC> sp_Datos_Generales_HC_Acumulados(int keyBusiness, int typePeriod, int numberPeriod, int periodEnd, int yearPeriod, int typeSend, string type)
        {
            List<DatosGeneralesHC> datosGenerales = new List<DatosGeneralesHC>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Generales_HC_Acumulados", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@PeriodoInicio", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@PeriodoFinal", periodEnd));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodo", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@TipoEnvio", typeSend));
                cmd.Parameters.Add(new SqlParameter("@Opcion", type));
                SqlDataReader data = cmd.ExecuteReader();
                int i = 0;
                if (data.HasRows) {
                    while (data.Read()) {
                        DatosGeneralesHC dato = new DatosGeneralesHC();
                        dato.iAnio = (data["Anio"].ToString() != "") ? Convert.ToInt32(data["Anio"].ToString()) : 0;
                        dato.sPeriodo = data["Periodo_Inicio_Periodo_Final"].ToString();
                        dato.iEmpresa = (data["EMPRESA"].ToString() != "") ? Convert.ToInt32(data["EMPRESA"].ToString()) : 0;
                        dato.sEmpresa = (data["NombreEmpresa"].ToString() != "") ? data["NombreEmpresa"].ToString() : "NA";
                        dato.iNomina = (data["NOMINA"].ToString() != "") ? Convert.ToInt32(data["NOMINA"].ToString()) : 0;
                        dato.sPaterno = (data["Apellido_Paterno_Empleado"].ToString() != "") ? data["Apellido_Paterno_Empleado"].ToString() : "NA";
                        dato.sMaterno = (data["Apellido_Materno_Empleado"].ToString() != "") ? data["Apellido_Materno_Empleado"].ToString() : "NA";
                        dato.sNombreE = (data["Nombre_Empleado"].ToString() != "") ? data["Nombre_Empleado"].ToString() : "NA";
                        dato.sRegImss = (data["RegistroImss"].ToString() != "") ? data["RegistroImss"].ToString() : "NA";
                        dato.sRfc = (data["RFC"].ToString() != "") ? data["RFC"].ToString() : "NA";
                        dato.sCurp = (data["CURP"].ToString() != "") ? data["CURP"].ToString() : "NA";
                        dato.sPuesto = (data["PuestoCodigo"].ToString() != "") ? data["PuestoCodigo"].ToString() : "NA";
                        dato.sNombrePuesto = (data["NombrePuesto"].ToString() != "") ? data["NombrePuesto"].ToString() : "NA";
                        dato.sNivelJerarquico = (data["NivelJerarquico"].ToString() != "") ? data["NivelJerarquico"].ToString() : "NA";
                        dato.sDepto = (data["Depto_Codigo"].ToString() != "") ? data["Depto_Codigo"].ToString() : "NA";
                        dato.sNombreDepto = (data["DescripcionDepartamento"].ToString() != "") ? data["DescripcionDepartamento"].ToString() : "NA";
                        dato.sCentrCosto = (data["CentroCosto"].ToString() != "") ? data["CentroCosto"].ToString() : "NA";
                        dato.sDescCentrCosto = (data["DescripcionCentroCosto"].ToString() != "") ? data["DescripcionCentroCosto"].ToString() : "NA";
                        dato.iRegional = (data["IdRegional"].ToString() != "") ? Convert.ToInt32(data["IdRegional"].ToString()) : 0;
                        dato.sClvRegional = (data["Clave_Regional"].ToString() != "") ? data["Clave_Regional"].ToString() : "NA";
                        dato.sDescRegional = (data["Descripcion_Regional"].ToString() != "") ? data["Descripcion_Regional"].ToString() : "NA";
                        dato.iSucursal = (data["IdSucursal"].ToString() != "") ? Convert.ToInt32(data["IdSucursal"].ToString()) : 0;
                        dato.sClvSucursal = (data["Clave_Sucursal"].ToString() != "") ? data["Clave_Sucursal"].ToString() : "NA";
                        dato.sDescSucursal = (data["Descripcion_Sucursal"].ToString() != "") ? data["Descripcion_Sucursal"].ToString() : "NA";
                        dato.sFechaAnt = (data["FechaAntiguedad"].ToString() != "") ? Convert.ToDateTime(data["FechaAntiguedad"]).ToString("yyyy-MM-dd") : "NA";
                        dato.sFechaIng = (data["FechaIngreso"].ToString() != "") ? Convert.ToDateTime(data["FechaIngreso"]).ToString("yyyy-MM-dd") : "NA";
                        dato.dSueldo = (data["SalarioMensual"].ToString() != "") ? Convert.ToDecimal(data["SalarioMensual"].ToString()) : 0;
                        dato.iVacanteC = (data["Posicion_id"].ToString() != "") ? Convert.ToInt32(data["Posicion_id"].ToString()) : 0;
                        dato.iUltimaPos = (data["UltimaPos"].ToString() != "") ? Convert.ToInt32(data["UltimaPos"].ToString()) : 0;
                        dato.dUltSdi = (data["Ult_sdi"].ToString() != "") ? Convert.ToDecimal(data["Ult_sdi"].ToString()) : 0;
                        dato.sRegistroPatronal = data["Registro_Patronal"].ToString();
                        datosGenerales.Add(dato);
                        i += 1;
                        //if (i == 5) break;
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosGenerales;
        }

        // Obtenemos los datos generales de la hc
        public List<DatosGeneralesHC> sp_Datos_Generales_HC(int keyBusiness, int typePeriod, int numberPeriod, int yearPeriod, int typeSend, string type)
        {
            List<DatosGeneralesHC> datosGenerales = new List<DatosGeneralesHC>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Generales_HC", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodo", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@TipoEnvio", typeSend));
                cmd.Parameters.Add(new SqlParameter("@Opcion", type));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        DatosGeneralesHC dato = new DatosGeneralesHC();
                        dato.iAnio    = (data["Anio"].ToString() != "") ? Convert.ToInt32(data["Anio"].ToString()) : 0 ;
                        dato.iPeriodo = (data["Periodo"].ToString() != "") ? Convert.ToInt32(data["Periodo"].ToString()) : 0;
                        dato.iEmpresa = (data["EMPRESA"].ToString() != "") ? Convert.ToInt32(data["EMPRESA"].ToString()) : 0;
                        dato.sEmpresa = (data["NombreEmpresa"].ToString() != "") ? data["NombreEmpresa"].ToString() : "NA";
                        dato.iNomina  = (data["NOMINA"].ToString() != "") ? Convert.ToInt32(data["NOMINA"].ToString()) : 0;
                        dato.sPaterno = (data["Apellido_Paterno_Empleado"].ToString() != "") ? data["Apellido_Paterno_Empleado"].ToString() : "NA";
                        dato.sMaterno = (data["Apellido_Materno_Empleado"].ToString() != "") ? data["Apellido_Materno_Empleado"].ToString() : "NA";
                        dato.sNombreE = (data["Nombre_Empleado"].ToString() != "") ? data["Nombre_Empleado"].ToString() : "NA";
                        dato.sRegImss = (data["RegistroImss"].ToString() != "") ? data["RegistroImss"].ToString() : "NA";
                        dato.sRfc     = (data["RFC"].ToString() != "") ? data["RFC"].ToString() : "NA";
                        dato.sCurp    = (data["CURP"].ToString() != "") ? data["CURP"].ToString() : "NA";
                        dato.sPuesto  = (data["PuestoCodigo"].ToString() != "") ? data["PuestoCodigo"].ToString() : "NA";
                        dato.sNombrePuesto = (data["NombrePuesto"].ToString() != "") ? data["NombrePuesto"].ToString() : "NA";
                        dato.sNivelJerarquico = (data["NivelJerarquico"].ToString() != "") ? data["NivelJerarquico"].ToString() : "NA";
                        dato.sDepto = (data["Depto_Codigo"].ToString() != "") ? data["Depto_Codigo"].ToString() : "NA";
                        dato.sNombreDepto = (data["DescripcionDepartamento"].ToString() != "") ? data["DescripcionDepartamento"].ToString() : "NA" ;
                        dato.sCentrCosto = (data["CentroCosto"].ToString() != "") ? data["CentroCosto"].ToString() : "NA";
                        dato.sDescCentrCosto = (data["DescripcionCentroCosto"].ToString() != "") ? data["DescripcionCentroCosto"].ToString() : "NA";
                        dato.iRegional = (data["IdRegional"].ToString() != "") ? Convert.ToInt32(data["IdRegional"].ToString()) : 0;
                        dato.sClvRegional = (data["Clave_Regional"].ToString() != "") ? data["Clave_Regional"].ToString() : "NA";
                        dato.sDescRegional = (data["Descripcion_Regional"].ToString() != "") ? data["Descripcion_Regional"].ToString() : "NA";
                        dato.iSucursal = (data["IdSucursal"].ToString() != "") ? Convert.ToInt32(data["IdSucursal"].ToString()) : 0;
                        dato.sClvSucursal = (data["Clave_Sucursal"].ToString() != "") ? data["Clave_Sucursal"].ToString() : "NA";
                        dato.sDescSucursal = (data["Descripcion_Sucursal"].ToString() != "") ? data["Descripcion_Sucursal"].ToString() : "NA";
                        dato.sFechaAnt = (data["FechaAntiguedad"].ToString() != "") ? Convert.ToDateTime(data["FechaAntiguedad"]).ToString("yyyy-MM-dd") : "NA";
                        dato.sFechaIng = (data["FechaIngreso"].ToString() != "") ? Convert.ToDateTime(data["FechaIngreso"]).ToString("yyyy-MM-dd") : "NA";
                        dato.dSueldo = (data["SalarioMensual"].ToString() != "") ? Convert.ToDecimal(data["SalarioMensual"].ToString()) : 0;
                        dato.iVacanteC = (data["Posicion_id"].ToString() != "") ? Convert.ToInt32(data["Posicion_id"].ToString()) : 0;
                        dato.iUltimaPos = (data["UltimaPos"].ToString() != "") ? Convert.ToInt32(data["UltimaPos"].ToString()) : 0;
                        dato.dUltSdi = (data["Ult_sdi"].ToString() != "") ? Convert.ToDecimal(data["Ult_sdi"].ToString()) : 0;
                        dato.sRegistroPatronal = data["Registro_Patronal"].ToString();
                        dato.iGrupoEmpresaId = (data["GrupoEmpresa"].ToString() != "") ? Convert.ToInt32(data["GrupoEmpresa"].ToString()) : 0;
                        datosGenerales.Add(dato);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosGenerales;
        }

        public DetallesRenglon sp_Detalle_Renglones_Acumulados(int keyBusiness, int keyEmployee, int numberPeriod, int periodEnd, int typePeriod, int yearPeriod, int keyRenglon, int ismirror)
        {
            DetallesRenglon detallesRenglon = new DetallesRenglon();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Detalle_Renglones_Acumulados", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@PeriodoInicio", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@PeriodoFinal", periodEnd));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodo", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Renglon", keyRenglon));
                cmd.Parameters.Add(new SqlParameter("@Espejo", ismirror));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    detallesRenglon.iRenglon = (data["Renglon_id"].ToString() != "") ? Convert.ToInt32(data["Renglon_id"].ToString()) : 0;
                    detallesRenglon.dSaldo = (data["Saldo"].ToString() != "") ? Convert.ToDecimal(data["Saldo"].ToString()) : 0;
                } else {
                    detallesRenglon.dSaldo = 0;
                    detallesRenglon.iRenglon = 0;
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return detallesRenglon;
        }

        public DetallesRenglon sp_Detalle_Renglones(int keyBusiness, int keyEmployee, int numberPeriod, int typePeriod, int yearPeriod, int keyRenglon, int ismirror)
        {
            DetallesRenglon detallesRenglon = new DetallesRenglon();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Detalle_Renglones", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodo", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Renglon", keyRenglon));
                cmd.Parameters.Add(new SqlParameter("@Espejo", ismirror));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    detallesRenglon.dSaldo   = (data["Saldo"].ToString() != "") ? Convert.ToDecimal(data["Saldo"].ToString()) : 0;
                    detallesRenglon.iRenglon = (data["Renglon_id"].ToString() != "") ? Convert.ToInt32(data["Renglon_id"].ToString()) : 0;
                } else {
                    detallesRenglon.dSaldo   = 0;
                    detallesRenglon.iRenglon = 0;
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return detallesRenglon;
        }

        public DataTable sp_Datos_Generales_HC_DataTable(int keyBusiness, int typePeriod, int numberPeriod, int yearPeriod)
        {
            DataTable dataTable = new DataTable();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Generales_HC", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodo", typePeriod));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }
            finally
            {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Movimientos_Empleados(string typeOption, int keyOptionSel, string paramDateS, string paramDateE)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Movimientos_Empleados", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Empresa_id", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@Opcion", typeOption));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", paramDateS));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", paramDateE));
                //cmd.Parameters.Add(new SqlParameter("@Periodo_id", typePeriod));
                //cmd.Parameters.Add(new SqlParameter("@Periodo", period));
                //cmd.Parameters.Add(new SqlParameter("@Anio", year));
                cmd.Parameters.Add(new SqlParameter("@Tipo", "D"));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand  = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); 
                cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public Boolean sp_Comprueba_Existe_Calculos_Nomina(string typeOption, int keyOptionSel, int typePeriod, int numberPeriod, int yearPeriod)
        {
            Boolean flag = false;
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Comprueba_Existe_Calculos_Nomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@TipoOpcion", typeOption.Trim()));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Tipo_periodo_id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    if (data["Bandera"].ToString() == "EXISTS") {
                        flag = true;
                    } else {
                        flag = false;
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
                flag = false;
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return flag;
        }

        public Boolean sp_Consulta_Existe_Reporte_Nomina(string typeOption, int keyOptionSel, int typePeriod, int numberPeriod, int yearPeriod, int keyUser)
        {
            Boolean flag = false;
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Consulta_Existe_Reporte_Nomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@IdPeriodo", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Usuario_id", keyUser));
                cmd.Parameters.Add(new SqlParameter("@TipoOpcion", typeOption));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    if (data["Respuesta"].ToString() == "EXISTS") {
                        flag = true;
                    } else {
                        flag = false;
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
                flag = false;
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return flag;
        }

        public Boolean sp_Cursor_Genera_Datos_Reporte_Nomina(string typeOption, int keyOptionSel, int typePeriod, int numberPeriod, int yearPeriod, int keyUser)
        {
            Boolean flag = false;
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Cursor_Genera_Datos_Reporte_Nomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Tipo_periodo_Id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Usuario_Id", keyUser));
                if (cmd.ExecuteNonQuery() > 0) {
                    flag = true;
                }
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
                flag = false;
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return flag;
        }


        public Boolean sp_Refresca_Datos_Reporte_Nomina(string typeOption, int keyOptionSel, int typePeriod, int numberPeriod, int yearPeriod, int keyUser)
        {
            Boolean flag = false;
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Refresca_Datos_Reporte_Nomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@TipoOpcion", typeOption));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Tipo_periodo_id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Usuario", keyUser));
                if (cmd.ExecuteNonQuery() > 0) {
                    flag = true;
                }
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
                flag = false;
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return flag;
        }

        // CURSOR PARA GRUPO DE EMPRESAS
        public Boolean sp_Cursor_Genera_Datos_Reporte_Nomina_Grupo_Empresas(int keyOptionSel, int typePeriod, int numberPeriod, int yearPeriod, int keyUser)
        {
            Boolean flag = false;
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Cursor_Genera_Datos_Reporte_Nomina_Grupo_Empresas", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Tipo_periodo_Id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Usuario_Id", keyUser));
                if (cmd.ExecuteNonQuery() > 0) {
                    flag = true;
                }
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
                flag = false;
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return flag;
        }

        public DataTable sp_Datos_Reporte_Nomina(string typeOption,int keyOptionSel, int typePeriod, int numberPeriod, int yearPeriod, int keyUser)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Nomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Tipo_periodo_id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Usuario_id", keyUser));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand  = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Nomina_Grupo_Empresas(string typeOption, int keyOptionSel, int typePeriod, int numberPeriod, int yearPeriod, int keyUser)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Nomina_Grupo_Empresas", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Tipo_periodo_id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Usuario_id", keyUser));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Altas_Empleado_Fechas(string typeOption, int keyOptionSel, string dateS, string dateE)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Altas_Empleado_Fechas", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", dateS));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", dateE));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@TipoOpcion", typeOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand  = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Generales_Empleados(string typeOption, int keyOptionSel)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Generales_Empleados", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@TipoOpcion", typeOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand  = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Bajas_Empleados_Fechas(string typeOption, int keyOptionSel, string dateS, string dateE)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Bajas_Empleados_Fechas", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", dateS));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", dateE));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@TipoOpcion", typeOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand  = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Faltas_SIC(string typeOption, int keyOptionSel, string dateS, string dateE)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Faltas_SIC", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@Opcion", typeOption));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", dateS));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", dateE));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Incapacidades_SIC(string typeOption, int keyOptionSel, string dateS, string dateE)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Incapacidades_SIC", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@Opcion", typeOption));
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", dateS));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", dateE));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Empleados_Activos_Con_Sueldo(string typeOption, int keyOptionSel, string dateActive)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Empleados_Activos_Con_Sueldo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@FechaActivo", dateActive));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@TipoOpcion", typeOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand  = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Empleados_Activos_Sin_Sueldo(string typeOption, int keyOptionSel, string dateActive)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Empleados_Activos_Sin_Sueldo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@FechaActivo", dateActive));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@TipoOpcion", typeOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand  = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Cuenta_Cheques_Detalle(string typeOption, int keyOptionSel, int year, int period, int typePeriod)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Cuenta_Cheques_Detalle", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Anio", year));
                cmd.Parameters.Add(new SqlParameter("@Periodo", period));
                cmd.Parameters.Add(new SqlParameter("@TPeriodo", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@TipoOpcion", typeOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Cuenta_Cheques_Totales(string typeOption, int keyOptionSel, int year, int period, int typePeriod)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Cuenta_Cheques_Totales", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Anio", year));
                cmd.Parameters.Add(new SqlParameter("@Periodo", period));
                cmd.Parameters.Add(new SqlParameter("@TPeriodo", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@TipoOpcion", typeOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Acumulados_Por_Periodo_Y_Nomina(int year, string periods, int payroll, int typePeriod, int business)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Acumulados_Por_Periodo_Y_Nomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", business));
                cmd.Parameters.Add(new SqlParameter("@Anio", year));
                cmd.Parameters.Add(new SqlParameter("@Periodos", periods));
                cmd.Parameters.Add(new SqlParameter("@Nomina", payroll));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodo", typePeriod));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Acumulados_Matrix(int year, int periodStart, int periodEnd, int typePeriod, string option, int keyOption)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Acumulados_Matrix", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Anio", year));
                cmd.Parameters.Add(new SqlParameter("@PeriodoInicio", periodStart));
                cmd.Parameters.Add(new SqlParameter("@PeriodoFinal", periodEnd));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodo", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Opcion", option));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Recibos_Nomina(int year, int periodStart, int periodEnd, int typePeriod, string option, int keyOption)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Recibos_Nomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Anio", year));
                cmd.Parameters.Add(new SqlParameter("@PeriodoInicio", periodStart));
                cmd.Parameters.Add(new SqlParameter("@PeriodoFinal", periodEnd));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodo", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Opcion", option));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Estructura(string date, string option, int keyOption)
        {
            DataTable dataTable = new DataTable();
            try{
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Estructura", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@FechaPersonalActivo", date));
                cmd.Parameters.Add(new SqlParameter("@Opcion", option));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Empleados_Pago_Efectivo(string option, int keyOption)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Empleados_Pago_Efectivo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Opcion", option));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc){
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Adeudos(string option, int keyOption)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Adeudos", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOption));
                cmd.Parameters.Add(new SqlParameter("@Opcion", option));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Vacaciones(string option, int keyOption)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Vacaciones", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Opcion", option));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Incapacidades_TCR(string option, int keyOption) {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Incapacidades_TCR", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOption));
                cmd.Parameters.Add(new SqlParameter("@Opcion", option));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Pensiones_Alimenticias(string option, int keyOption)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Pensiones_Alimenticias", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Opcion", option));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Creditos_Infonavit_Historico(string option, int keyOption)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Creditos_Infonavit_Historico", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Opcion", option));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Creditos_Infonavit_Activos(string option, int keyOption)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Creditos_Infonavit_Activos", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Opcion", option));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Bajas_Credito_Infonavit (string dateStart, string dateEnd, string option, int keyOption)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Bajas_Credito_Infonavit", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", dateStart));
                cmd.Parameters.Add(new SqlParameter("@FechaFin", dateEnd));
                cmd.Parameters.Add(new SqlParameter("@Opcion", option));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Aumentos_Sueldos(string dateStart, string dateEnd, string option, int keyOption)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Aumentos_Sueldos", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", dateStart));
                cmd.Parameters.Add(new SqlParameter("@FechaFinal", dateEnd));
                cmd.Parameters.Add(new SqlParameter("@Opcion", option));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

        public DataTable sp_Datos_Reporte_Detalle_Renglones_Nomina(string periodStart, string periodEnd, string lines, int typePeriod, string option, int keyOption)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Detalle_Renglones_Nomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@PeriodoInicio", periodStart));
                cmd.Parameters.Add(new SqlParameter("@PeriodoFinal", periodEnd));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodo", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Renglones", lines));
                cmd.Parameters.Add(new SqlParameter("@Anio", DateTime.Now.Year));
                cmd.Parameters.Add(new SqlParameter("@Opcion", option));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOption));
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return dataTable;
        }

    }
}