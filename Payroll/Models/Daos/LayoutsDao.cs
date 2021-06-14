using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Payroll.Models.Utilerias;
using Payroll.Models.Daos;
using Payroll.Models.Beans;
using System.Data.SqlClient;
using System.Data;

namespace Payroll.Models.Daos
{
    public class LayoutsDao : Conexion
    {

        public LayoutSalarioMasivoBean sp_Actualiza_Salario_Carga_Masiva(int IdEmpresa, int IdEmpleado, int TipoMovimiento, double Salario, string FechaMovimiento, int UsuarioId, int PeriodoId, int Periodo, int Anio)
        {
            LayoutSalarioMasivoBean bean = new LayoutSalarioMasivoBean();
            bean.iBandera1 = 0;
            bean.iBandera2 = 0;
            bean.iBandera3 = 0;
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Actualiza_Salario_Carga_Masiva", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", IdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@TipoMovimiento", TipoMovimiento));
                cmd.Parameters.Add(new SqlParameter("@Salario", Salario));
                cmd.Parameters.Add(new SqlParameter("@FechaMovimiento", FechaMovimiento));
                cmd.Parameters.Add(new SqlParameter("@UsuarioId", UsuarioId));
                cmd.Parameters.Add(new SqlParameter("@PeriodoId", PeriodoId));
                cmd.Parameters.Add(new SqlParameter("@Periodo", Periodo));
                cmd.Parameters.Add(new SqlParameter("@Anio", Anio));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    if (dataReader["Coincidencia"].ToString() == "1" && dataReader["Nomina"].ToString() == "1" && 
                        dataReader["Historial"].ToString() == "1") {
                        bean.sMensaje = "SUCCESS";
                    } else {
                        bean.sMensaje = (dataReader["Mensaje"].ToString() == "NONE") ? "ERROR" : dataReader["Mensaje"].ToString();
                    }
                    if (dataReader["Coincidencia"].ToString() == "1") {
                        bean.iBandera1 = 1;
                    }
                    if (dataReader["Nomina"].ToString() == "1") {
                        bean.iBandera2 = 1;
                    }
                    if (dataReader["Historial"].ToString() == "1") {
                        bean.iBandera3 = 1;
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                bean.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return bean;
        }

    }
}