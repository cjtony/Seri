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
    public class ReportesDao : Conexion
    {
        
        public DataTable sp_Datos_Reporte_Nomina(int keyOptionSel, int periodActually)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Reporte_Nomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyOptionSel));
                cmd.Parameters.Add(new SqlParameter("@Periodo", periodActually));
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

    }
}