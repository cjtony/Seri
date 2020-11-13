using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

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

        public List<GroupBusinessDispersionBean> sp_Load_Group_Business_Dispersion ()
        {
            List<GroupBusinessDispersionBean> groupBusinesses = new List<GroupBusinessDispersionBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Load_Group_Business_Dispersion", this.conexion) { CommandType = CommandType.StoredProcedure };
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows) {
                    while (dataReader.Read()) {
                        GroupBusinessDispersionBean group = new GroupBusinessDispersionBean();
                        group.iIdGrupoEmpresa = Convert.ToInt32(dataReader["IdGrupoEmpresa"]);
                        group.sNombreGrupo    = dataReader["Nombre_Grupo"].ToString();
                        groupBusinesses.Add(group);
                    }
                }
                cmd.Dispose();
                cmd.Parameters.Clear();
                dataReader.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return groupBusinesses;
        }

        public GroupBusinessDispersionBean sp_Save_New_Group_Business_Dispersion (string name, int user)
        {
            GroupBusinessDispersionBean groupBusiness = new GroupBusinessDispersionBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Save_New_Group_Business_Dispersion", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Nombre", name));
                cmd.Parameters.Add(new SqlParameter("@Usuario", user));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    if (dataReader["Bandera"].ToString() == "2") {
                        groupBusiness.sMensaje = "SUCCESS";
                    } else if (dataReader["Bandera"].ToString() == "1") {
                        groupBusiness.sMensaje = "EXISTS";
                    } else {
                        groupBusiness.sMensaje = "ERROR";
                    }
                } else {
                    groupBusiness.sMensaje = "ERROR";
                }
                cmd.Dispose();
                cmd.Parameters.Clear();
                dataReader.Close();
            } catch (Exception exc) {
                groupBusiness.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return groupBusiness;
        }

        public List<EmpresasBean> sp_Load_Business_Not_In_Groups_Dispersion()
        {
            List<EmpresasBean> empresasBeans = new List<EmpresasBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Load_Business_Not_In_Groups_Dispersion", this.conexion) { CommandType = CommandType.StoredProcedure };
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows) {
                    while (dataReader.Read()) {
                        EmpresasBean empresas = new EmpresasBean();
                        empresas.iIdEmpresa   = Convert.ToInt32(dataReader["IdEmpresa"]);
                        empresas.sNombreEmpresa = dataReader["NombreEmpresa"].ToString();
                        empresas.sRazonSocial   = dataReader["RazonSocial"].ToString();
                        empresasBeans.Add(empresas);
                    }
                }
                cmd.Dispose(); 
                cmd.Parameters.Clear();
                dataReader.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return empresasBeans;
        }

        public GroupBusinessDispersionBean sp_Save_Asign_Group_Business(int group, int business)
        {
            GroupBusinessDispersionBean groupBusiness = new GroupBusinessDispersionBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Save_Asign_Group_Business", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdGrupo", group));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", business));
                if (cmd.ExecuteNonQuery() > 0) {
                    groupBusiness.sMensaje = "INSERT";
                } else {
                    groupBusiness.sMensaje = "NOTINSERT";
                }
                cmd.Dispose();
                cmd.Parameters.Clear();
            } catch (Exception exc) {
                groupBusiness.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return groupBusiness;
        }

        public List<EmpresasBean> sp_View_Business_Group_Dispersion(int keyGroup)
        {
            List<EmpresasBean> empresas = new List<EmpresasBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_View_Business_Group_Dispersion", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdGrupo",keyGroup));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows) {
                    while (dataReader.Read()) {
                        EmpresasBean bean    = new EmpresasBean();
                        bean.iIdDetalleGrupo = Convert.ToInt32(dataReader["IdDetalle"]);
                        bean.iIdEmpresa      = Convert.ToInt32(dataReader["IdEmpresa"]);
                        bean.sNombreEmpresa  = dataReader["NombreEmpresa"].ToString();
                        bean.sRazonSocial    = dataReader["RazonSocial"].ToString();
                        bean.fRfc            = dataReader["RFC"].ToString();
                        empresas.Add(bean);
                    }
                }
                cmd.Dispose();
                cmd.Parameters.Clear();
                dataReader.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return empresas;
        }

        public List<DataDepositsBankingBean> sp_Obtiene_Depositos_Bancarios(int keyBusiness, int yearDispersion, int typePeriodDisp, int periodDispersion, string type)
        {
            List<DataDepositsBankingBean> listDaDepBankingBean = new List<DataDepositsBankingBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Obtiene_Depositos_Bancarios", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@AnioAct", yearDispersion));
                cmd.Parameters.Add(new SqlParameter("@Periodo", periodDispersion));
                cmd.Parameters.Add(new SqlParameter("@IdPeriodo", typePeriodDisp));
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
                            sImporte = string.Format(CultureInfo.InvariantCulture, "{0:#,###,##0.00}", Convert.ToDecimal((data["importe"])))
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

        public DatosEmpresaBeanDispersion sp_Datos_Empresa_Dispersion(int keyBusiness)
        {
            DatosEmpresaBeanDispersion datosEmpresaBeanDispersion = new DatosEmpresaBeanDispersion();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Datos_Empresa_Dispersion", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    datosEmpresaBeanDispersion.sNombreEmpresa = data["NombreEmpresa"].ToString();
                    datosEmpresaBeanDispersion.sCalle         = data["Calle"].ToString();
                    datosEmpresaBeanDispersion.sColonia       = data["Colonia"].ToString();
                    datosEmpresaBeanDispersion.sCodigoPostal  = data["CodigoPostal"].ToString();
                    datosEmpresaBeanDispersion.sCiudad        = data["Ciudad"].ToString();
                    datosEmpresaBeanDispersion.sRfc           = data["RFC"].ToString();
                    datosEmpresaBeanDispersion.iRegimen_Fiscal_id = Convert.ToInt32(data["Regimen_Fiscal_id"].ToString());
                    datosEmpresaBeanDispersion.sDelegacion    = data["Delegacion"].ToString();
                    datosEmpresaBeanDispersion.iBanco_id      = Convert.ToInt32(data["Banco_id"].ToString());
                    datosEmpresaBeanDispersion.sDescripcion   = data["Descripcion"].ToString();
                    datosEmpresaBeanDispersion.sMensaje       = "SUCCESS";
                } else {
                    datosEmpresaBeanDispersion.sMensaje = "NOTDATA";
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
                datosEmpresaBeanDispersion.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosEmpresaBeanDispersion;
        }

        public List<DatosDepositosBancariosBean> sp_Procesa_Cheques_Total_Nomina(int keyBusiness, int typePeriod, int numberPeriod, int yearPeriod)
        {
            List<DatosDepositosBancariosBean> datosDepositosBancariosBean = new List<DatosDepositosBancariosBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Procesa_Cheques_Total_Nomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Tipo_periodo_id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        DatosDepositosBancariosBean bancariosBean = new DatosDepositosBancariosBean();
                        bancariosBean.iIdBanco   = Convert.ToInt32(data["Banco"].ToString());
                        bancariosBean.iCantidad  = Convert.ToInt32(data["Cantidad"].ToString());
                        bancariosBean.sImporte   = data["Importe"].ToString().Replace(",","");
                        datosDepositosBancariosBean.Add(bancariosBean);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosDepositosBancariosBean;
        }

        public List<DatosDepositosBancariosBean> sp_Procesa_Cheques_Total_Nomina_Espejo(int keyBusiness, int typePeriod, int numberPeriod, int yearPeriod)
        {
            List<DatosDepositosBancariosBean> datosDepositosBancariosBean = new List<DatosDepositosBancariosBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Procesa_Cheques_Total_Nomina_Espejo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Tipo_periodo_id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        DatosDepositosBancariosBean bancariosBean = new DatosDepositosBancariosBean();
                        bancariosBean.iIdBanco  = Convert.ToInt32(data["Banco"].ToString());
                        bancariosBean.iCantidad = Convert.ToInt32(data["Cantidad"].ToString());
                        bancariosBean.sImporte  = data["Importe"].ToString().Replace(",", "");
                        datosDepositosBancariosBean.Add(bancariosBean);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosDepositosBancariosBean;
        }

        public List<DatosProcesaChequesNominaBean> sp_Procesa_Cheques_Nomina(int keyBusiness, int typePeriod, int numberPeriod, int yearPeriod)
        {
            List<DatosProcesaChequesNominaBean> datosProcesaChequesNominaBeans = new List<DatosProcesaChequesNominaBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Procesa_Cheques_Nomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Tipo_periodo_id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        DatosProcesaChequesNominaBean datosProcesaCheques = new DatosProcesaChequesNominaBean();
                        datosProcesaCheques.iIdBanco = Convert.ToInt32(data["IdBanco"].ToString());
                        datosProcesaCheques.sBanco   = data["Banco"].ToString();
                        datosProcesaCheques.iIdEmpresa = Convert.ToInt32(data["Empresa"].ToString());
                        datosProcesaCheques.sNomina    = data["Nomina"].ToString();
                        datosProcesaCheques.sCuenta    = data["Cuenta"].ToString();
                        datosProcesaCheques.dImporte   = Convert.ToDecimal(data["Importe"].ToString());
                        datosProcesaCheques.sNombre    = data["Nombre"].ToString();
                        datosProcesaCheques.sPaterno   = data["Paterno"].ToString();
                        datosProcesaCheques.sMaterno   = data["Materno"].ToString();
                        datosProcesaCheques.sRfc       = data["RFC"].ToString();
                        datosProcesaCheques.iTipoPago  = Convert.ToInt32(data["TipoPago"].ToString());
                        datosProcesaChequesNominaBeans.Add(datosProcesaCheques);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosProcesaChequesNominaBeans;
        }

        public List<DatosProcesaChequesNominaBean> sp_Procesa_Cheques_Nomina_Espejo(int keyBusiness, int typePeriod, int numberPeriod, int yearPeriod)
        {
            List<DatosProcesaChequesNominaBean> datosProcesaChequesNominaBeans = new List<DatosProcesaChequesNominaBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Procesa_Cheques_Nomina_Espejo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Tipo_periodo_id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        DatosProcesaChequesNominaBean datosProcesaCheques = new DatosProcesaChequesNominaBean();
                        datosProcesaCheques.iIdBanco = Convert.ToInt32(data["IdBanco"].ToString());
                        datosProcesaCheques.sBanco = data["Banco"].ToString();
                        datosProcesaCheques.iIdEmpresa = Convert.ToInt32(data["Empresa"].ToString());
                        datosProcesaCheques.sNomina = data["Nomina"].ToString();
                        datosProcesaCheques.sCuenta = data["Cuenta"].ToString();
                        datosProcesaCheques.dImporte = Convert.ToDecimal(data["Importe"].ToString());
                        datosProcesaCheques.sNombre = data["Nombre"].ToString();
                        datosProcesaCheques.sPaterno = data["Paterno"].ToString();
                        datosProcesaCheques.sMaterno = data["Materno"].ToString();
                        datosProcesaCheques.sRfc = data["RFC"].ToString();
                        datosProcesaCheques.iTipoPago = Convert.ToInt32(data["TipoPago"].ToString());
                        datosProcesaChequesNominaBeans.Add(datosProcesaCheques);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosProcesaChequesNominaBeans;
        }

        public DatosCuentaClienteBancoEmpresaBean sp_Cuenta_Cliente_Banco_Empresa(int keyBusiness, int bankResult)
        {
            DatosCuentaClienteBancoEmpresaBean datosCuentaCliente = new DatosCuentaClienteBancoEmpresaBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Cuenta_Cliente_Banco_Empresa", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@IdBanco", bankResult));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    datosCuentaCliente.sNumeroCliente = data["NumeroCliente"].ToString();
                    datosCuentaCliente.sNumeroCuenta  = data["NumeroCuenta"].ToString();
                    datosCuentaCliente.sVacio         = data["Vacio"].ToString();
                    datosCuentaCliente.iPlaza   = Convert.ToInt32(data["Plaza"].ToString());
                    datosCuentaCliente.sClabe   = data["Clabe"].ToString();
                    datosCuentaCliente.iTipo    = Convert.ToInt32(data["Tipo"].ToString());
                    datosCuentaCliente.iCodigo  = Convert.ToInt32(data["Codigo"].ToString());
                    datosCuentaCliente.sMensaje = "SUCCESS";
                } else {
                    datosCuentaCliente.sMensaje = "NOTDATA";
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosCuentaCliente;
        }

        // INTERBANCARIOS

        public List<DatosDepositosBancariosBean> sp_Procesa_Cheques_Total_Interbancarios(int keyBusiness, int typePeriod, int numberPeriod, int yearPeriod)
        {
            List<DatosDepositosBancariosBean> datosDepositosBancariosBean = new List<DatosDepositosBancariosBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Procesa_Cheques_Total_Interbancarios", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Tipo_periodo_id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        DatosDepositosBancariosBean bancariosBean = new DatosDepositosBancariosBean();
                        bancariosBean.iCantidad = Convert.ToInt32(data["Cantidad"].ToString());
                        bancariosBean.sImporte  = data["Importe"].ToString().Replace(",", "");
                        datosDepositosBancariosBean.Add(bancariosBean);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosDepositosBancariosBean;
        }

        public List<DatosDepositosBancariosBean> sp_Procesa_Cheques_Total_Interbancarios_Espejo(int keyBusiness, int typePeriod, int numberPeriod, int yearPeriod)
        {
            List<DatosDepositosBancariosBean> datosDepositosBancarios = new List<DatosDepositosBancariosBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Procesa_Cheques_Total_Interbancarios_Espejo", this.conexion)
                { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Tipo_periodo_id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        DatosDepositosBancariosBean bancariosBean = new DatosDepositosBancariosBean();
                        bancariosBean.iCantidad = Convert.ToInt32(data["Cantidad"].ToString());
                        bancariosBean.sImporte = data["Importe"].ToString().Replace(",", "");
                        datosDepositosBancarios.Add(bancariosBean);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosDepositosBancarios;
        }

        public List<DatosProcesaChequesNominaBean> sp_Procesa_Cheques_Interbancarios(int keyBusiness, int typePeriod, int numberPeriod, int yearPeriod)
        {
            List<DatosProcesaChequesNominaBean> datosProcesaChequesNominaBeans = new List<DatosProcesaChequesNominaBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Procesa_Cheques_Interbancarios", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Tipo_periodo_id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        DatosProcesaChequesNominaBean datosProcesaCheques = new DatosProcesaChequesNominaBean();
                        datosProcesaCheques.iIdBanco = Convert.ToInt32(data["IdBanco"].ToString());
                        datosProcesaCheques.sBanco = data["Banco"].ToString();
                        datosProcesaCheques.iIdEmpresa = Convert.ToInt32(data["Empresa"].ToString());
                        datosProcesaCheques.sNomina = data["Nomina"].ToString();
                        datosProcesaCheques.sCuenta = data["Cuenta"].ToString();
                        datosProcesaCheques.dImporte = Convert.ToDecimal(data["Importe"].ToString());
                        datosProcesaCheques.sNombre = data["Nombre"].ToString();
                        datosProcesaCheques.sPaterno = data["Paterno"].ToString();
                        datosProcesaCheques.sMaterno = data["Materno"].ToString();
                        datosProcesaCheques.sRfc = data["RFC"].ToString();
                        datosProcesaCheques.iTipoPago = Convert.ToInt32(data["TipoPago"].ToString());
                        datosProcesaChequesNominaBeans.Add(datosProcesaCheques);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosProcesaChequesNominaBeans;
        }

        public List<DatosProcesaChequesNominaBean> sp_Procesa_Cheques_Interbancarios_Espejo(int keyBusiness, int typePeriod, int numberPeriod, int yearPeriod)
        {
            List<DatosProcesaChequesNominaBean> datosProcesaChequesNominaBeans = new List<DatosProcesaChequesNominaBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Procesa_Cheques_Interbancarios_Espejo", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Tipo_periodo_id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        DatosProcesaChequesNominaBean datosProcesaCheques = new DatosProcesaChequesNominaBean();
                        datosProcesaCheques.iIdBanco = Convert.ToInt32(data["IdBanco"].ToString());
                        datosProcesaCheques.sBanco = data["Banco"].ToString();
                        datosProcesaCheques.iIdEmpresa = Convert.ToInt32(data["Empresa"].ToString());
                        datosProcesaCheques.sNomina = data["Nomina"].ToString();
                        datosProcesaCheques.sCuenta = data["Cuenta"].ToString();
                        datosProcesaCheques.dImporte = Convert.ToDecimal(data["Importe"].ToString());
                        datosProcesaCheques.sNombre = data["Nombre"].ToString();
                        datosProcesaCheques.sPaterno = data["Paterno"].ToString();
                        datosProcesaCheques.sMaterno = data["Materno"].ToString();
                        datosProcesaCheques.sRfc = data["RFC"].ToString();
                        datosProcesaCheques.iTipoPago = Convert.ToInt32(data["TipoPago"].ToString());
                        datosProcesaChequesNominaBeans.Add(datosProcesaCheques);
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
            return datosProcesaChequesNominaBeans;
        }

    }


}