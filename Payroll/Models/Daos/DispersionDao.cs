using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Payroll.Models.Daos
{
    public class DispersionDao { }

    public class LoadTypePeriodPayrollDaoD : Conexion
    {
        public LoadTypePeriodPayrollBean sp_Load_Info_Periodo_Empr(int keyBusiness, int year)
        {
            LoadTypePeriodPayrollBean periodBean = new LoadTypePeriodPayrollBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Load_Info_Periodo_Empr", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@ctrlAnio", year));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    periodBean.iEmpresa_id = Convert.ToInt32(data["Empresa_id"].ToString());
                    periodBean.iAnio = Convert.ToInt32(data["Anio"].ToString());
                    periodBean.iTipoPeriodo = Convert.ToInt32(data["Tipo_Periodo_id"].ToString());
                    periodBean.iPeriodo = Convert.ToInt32(data["Periodo"].ToString());
                    periodBean.sFechaInicio = Convert.ToDateTime(data["Fecha_Inicio"].ToString()).ToString("yyyy-MM-dd");
                    periodBean.sFechaFinal = Convert.ToDateTime(data["Fecha_Final"].ToString()).ToString("yyyy-MM-dd");
                    periodBean.sMensaje = "success";
                }
                else
                {
                    periodBean.sMensaje = "NODATA";
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
            return periodBean;
        }
    }

    public class PayrollRetainedEmployeesDaoD : Conexion
    {

        public List<PayrollRetainedEmployeesBean> sp_Retrieve_NominasRetenidas(int keyBusiness)
        {
            List<PayrollRetainedEmployeesBean> listPayRetained = new List<PayrollRetainedEmployeesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Retrieve_NominasRetenidas", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyBusiness));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        PayrollRetainedEmployeesBean payRetained = new PayrollRetainedEmployeesBean();
                        payRetained.iIdNominaRetenida = Convert.ToInt32(data["IdNominaRetenida"].ToString());
                        payRetained.iIdEmpleado = Convert.ToInt32(data["Empleado_id"].ToString());
                        payRetained.sNombreEmpleado = Convert.ToInt32(data["Empleado_id"].ToString()).ToString() + " - " +
                                                data["Nombre_Empleado"].ToString() + " " +
                                                data["Apellido_Paterno_Empleado"].ToString() + " " +
                                                data["Apellido_Materno_Empleado"].ToString();
                        payRetained.sDescripcion = data["Descripcion"].ToString();
                        listPayRetained.Add(payRetained);
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
            return listPayRetained;
        }

        public PayrollRetainedEmployeesBean sp_Insert_Empleado_Retenida_Nomina(int keyBusiness, int keyEmployee, int typePeriod, int periodPayroll, int yearRetained, string descriptionRetained, int keyUser)
        {
            PayrollRetainedEmployeesBean payRetEmployee = new PayrollRetainedEmployeesBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Insert_Empleado_Retenida_Nomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoPeriId", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@ctrlPeriodo", periodPayroll));
                cmd.Parameters.Add(new SqlParameter("@ctrlAnio", yearRetained));
                cmd.Parameters.Add(new SqlParameter("@ctrlDescripcion", descriptionRetained));
                cmd.Parameters.Add(new SqlParameter("@ctrlUsuarioId", keyUser));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    payRetEmployee.sMensaje = "success";
                }
                else
                {
                    payRetEmployee.sMensaje = "error";
                }
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
            return payRetEmployee;
        }

        public PayrollRetainedEmployeesBean sp_Update_Remove_Nomina_Retenida(int keyPayrollRetained)
        {
            PayrollRetainedEmployeesBean payRetEmployee = new PayrollRetainedEmployeesBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Update_Remove_Nomina_Retenida", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdNominaRetenida", keyPayrollRetained));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    payRetEmployee.sMensaje = "success";
                }
                else
                {
                    payRetEmployee.sMensaje = "error";
                }
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
            return payRetEmployee;
        }

    }

    public class SearchEmployeePayRetainedDaoD : Conexion
    {

        public List<SearchEmployeePayRetainedBean> sp_SearchEmploye_Ret_Nomina(int keyBusiness, string search, string filter)
        {
            List<SearchEmployeePayRetainedBean> listEmployeePayRet = new List<SearchEmployeePayRetainedBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_SearchEmploye_Ret_Nomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@ctrlSearchEmp", search));
                cmd.Parameters.Add(new SqlParameter("@ctrlFiltered", filter));
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyBusiness));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        SearchEmployeePayRetainedBean employee = new SearchEmployeePayRetainedBean();
                        employee.iIdEmpleado = Convert.ToInt32(data["IdEmpleado"].ToString());
                        employee.sNombreEmpleado = data["Nombre_Empleado"].ToString() + " " +
                                                data["Apellido_Paterno_Empleado"].ToString() + " " +
                                                data["Apellido_Materno_Empleado"].ToString();
                        employee.iTipoPeriodo = 3;
                        listEmployeePayRet.Add(employee);
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
            return listEmployeePayRet;
        }

    }

    public class LoadTypePeriodDaoD : Conexion
    {

        public LoadTypePeriodBean sp_Load_Type_Period_Empresa(int keyBusiness, int year, int typePeriod)
        {
            LoadTypePeriodBean loadTypePerBean = new LoadTypePeriodBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Load_Type_Period_Empresa", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@ctrlAnio", year));
                cmd.Parameters.Add(new SqlParameter("@ctrlTipoPeriodo", typePeriod));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    loadTypePerBean.iEmpresa_id = Convert.ToInt32(data["Empresa_id"].ToString());
                    loadTypePerBean.iAnio = Convert.ToInt32(data["Anio"].ToString());
                    loadTypePerBean.iTipoPeriodo = Convert.ToInt32(data["Tipo_Periodo_id"].ToString());
                    loadTypePerBean.iPeriodo = Convert.ToInt32(data["Periodo"].ToString());
                    loadTypePerBean.sFechaInicio = Convert.ToDateTime(data["Fecha_Inicio"].ToString()).ToString("yyyy-MM-dd");
                    loadTypePerBean.sFechaFinal = Convert.ToDateTime(data["Fecha_Final"].ToString()).ToString("yyyy-MM-dd");
                    loadTypePerBean.sMensaje = "success";
                }
                else
                {
                    loadTypePerBean.sMensaje = "NODATA";
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
            return loadTypePerBean;
        }

    }

    public class DataDispersionBusiness : Conexion
    {

        public List<DataDepositsBankingBean> sp_Obtiene_Depositos_Bancarios(int keyBusiness, string yearDispersion, string periodDispersion, string type)
        {
            List<DataDepositsBankingBean> listDaDepBankingBean = new List<DataDepositsBankingBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Obtiene_Depositos_Bancarios", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", Convert.ToInt32(keyBusiness)));
                cmd.Parameters.Add(new SqlParameter("@AnioAct", Convert.ToInt32(yearDispersion)));
                if (type == "test")
                {
                    cmd.Parameters.Add(new SqlParameter("@Periodo", Convert.ToInt32(3)));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@Periodo", Convert.ToInt32(periodDispersion)));
                }
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        listDaDepBankingBean.Add(new DataDepositsBankingBean
                        {
                            iIdEmpresa = keyBusiness,
                            iIdBanco = Convert.ToInt32(data["banco"].ToString()),
                            iIdRenglon = Convert.ToInt32(data["Renglon_id"].ToString()),
                            iDepositos = Convert.ToInt32(data["depositos"].ToString()),
                            dImporte = Convert.ToDecimal(data["importe"].ToString())
                        });
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
            return listDaDepBankingBean;
        }

        public List<BankDetailsBean> sp_Datos_Banco(List<DataDepositsBankingBean> listData)
        {
            List<BankDetailsBean> listBankDetailsBean = new List<BankDetailsBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Banco", this.conexion) { CommandType = CommandType.StoredProcedure };
                foreach (DataDepositsBankingBean data in listData)
                {
                    cmd.Parameters.Add(new SqlParameter("@IdBanco", Convert.ToInt32(data.iIdBanco.ToString())));
                    SqlDataReader dataBank = cmd.ExecuteReader();
                    if (dataBank.Read())
                    {
                        listBankDetailsBean.Add(new BankDetailsBean
                        {
                            iIdBanco = Convert.ToInt32(dataBank["IdBanco"].ToString()),
                            sNombreBanco = dataBank["Descripcion"].ToString(),
                            sSufijo = (Convert.ToInt32(data.iIdBanco.ToString()) != 0) ? "Nomina" : "Efectivo"
                        });
                    }
                    cmd.Parameters.Clear(); cmd.Dispose(); dataBank.Close();
                }
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
            return listBankDetailsBean;
        }

    }

}