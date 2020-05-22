using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Payroll.Models.Beans;
using Payroll.Models.Utilerias;

namespace Payroll.Models.Daos
{
    public class BajasEmpleadosDaoD : Conexion
    {

        public BajasEmpleadosBean sp_CNomina_Finiquito(int keyBusiness, int keyEmployee, string dateAntiquityEmp, int idTypeDown, int idReasonsDown, string dateDownEmp, string dateReceipt, int typeDate, int typeCompensation)
        {
            BajasEmpleadosBean downEmployee = new BajasEmpleadosBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CNomina_Finiquito", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@fecha_inicio", dateAntiquityEmp));
                cmd.Parameters.Add(new SqlParameter("@fecha_baja", dateDownEmp));
                cmd.Parameters.Add(new SqlParameter("@Fecha_recibo", dateReceipt));
                cmd.Parameters.Add(new SqlParameter("@Tipo_finiquito_id", idTypeDown));
                cmd.Parameters.Add(new SqlParameter("@Empresa_id", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Empleado_id", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@ban_fecha_ingreso", typeDate));
                cmd.Parameters.Add(new SqlParameter("@ban_compensacion_especial", typeCompensation));
                if (cmd.ExecuteNonQuery() > 0) {
                    downEmployee.sMensaje = "SUCCESS";
                } else {
                    downEmployee.sMensaje = "ERRINSERT";
                }
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return downEmployee;
        }

        public List<BajasEmpleadosBean> sp_Finiquitos_Empleado(int keyEmployee, int keyBusiness)
        {
            List<BajasEmpleadosBean> listDataDownEmpBean = new List<BajasEmpleadosBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Finiquitos_Empleado", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa",  keyBusiness));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        listDataDownEmpBean.Add(new BajasEmpleadosBean {
                            iIdFiniquito      = Convert.ToInt32(data["IdFiniquito"].ToString()),
                            sEffdt            = data["Effdt"].ToString(),
                            sFecha_antiguedad = data["Fecha_antiguedad"].ToString(),
                            sFecha_ingreso    = data["Fecha_ingreso"].ToString(),
                            sFecha_baja       = data["Fecha_baja"].ToString(),
                            iAnios            = Convert.ToInt32(data["Anios"].ToString()),
                            sDias             = data["Dias"].ToString(),
                            iTipo_finiquito_id = Convert.ToInt32(data["Tipo_finiquito_id"].ToString()),
                            sFiniquito_valor   = data["Finiquito_valor"].ToString()
                        });
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return listDataDownEmpBean;
        }

    }
}