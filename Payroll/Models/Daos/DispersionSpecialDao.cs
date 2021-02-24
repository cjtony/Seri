using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Payroll.Models.Daos;
using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System.Data.SqlClient;
using System.Data;

namespace Payroll.Models.Daos
{
    public class DispersionSpecialDao : Conexion
    {

        public List<BancosBean> sp_Select_Config_Banks(string type, int active, int group)
        {
            List<BancosBean> bancosBeans = new List<BancosBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Select_Config_Banks", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Tipo", type));
                cmd.Parameters.Add(new SqlParameter("@Activo", active));
                cmd.Parameters.Add(new SqlParameter("@Grupo", group));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows) {
                    while (dataReader.Read()) {
                        BancosBean bancos = new BancosBean();
                        bancos.iIdBanco        = Convert.ToInt32(dataReader["Banco_id"]);
                        bancos.sNombreBanco    = dataReader["Descripcion"].ToString();
                        bancos.iConfiguracion  = Convert.ToInt32(dataReader["IdConfiguracion"]);
                        bancos.iGrupoId        = Convert.ToInt32(dataReader["Grupo_id"]);
                        bancos.sNombreBanco    = dataReader["Nombre"].ToString();
                        bancos.sNCliente       = dataReader["NCliente"].ToString();
                        bancos.sNCuenta        = dataReader["NCuenta"].ToString();
                        bancos.sNClabe         = dataReader["NClabe"].ToString();
                        bancos.sNPlaza         = dataReader["Plaza"].ToString();
                        bancosBeans.Add(bancos);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return bancosBeans;
        }

        public List<EmpresasBean> sp_Select_Business_Group(int keyGroup)
        {
            List<EmpresasBean> empresasBeans = new List<EmpresasBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Select_Business_Group", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@GrupoEmpresa_id", keyGroup));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows) {
                    while (dataReader.Read()) {
                        EmpresasBean empresas = new EmpresasBean();
                        empresas.iIdEmpresa   = Convert.ToInt32(dataReader["Empresa_id"]);
                        empresasBeans.Add(empresas);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return empresasBeans;
        }

        public List<DatosDepositosBancariosBean> sp_Procesa_Cheques_Total_Nomina_Special(int keyConfig, int typePeriod, int numberPeriod, int year, int group, int mirror, int bank)
        {
            List<DatosDepositosBancariosBean> datosDepositos = new List<DatosDepositosBancariosBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Procesa_Cheques_Total_Nomina_Special", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Configuracion_id", keyConfig));
                cmd.Parameters.Add(new SqlParameter("@Tipo_Periodo_id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", numberPeriod));
                cmd.Parameters.Add(new SqlParameter("@Anio", year));
                cmd.Parameters.Add(new SqlParameter("@Grupo_id", group));
                cmd.Parameters.Add(new SqlParameter("@Espejo", mirror));
                cmd.Parameters.Add(new SqlParameter("@Banco_id", bank));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows) {
                    while (dataReader.Read()) {
                        DatosDepositosBancariosBean datos = new DatosDepositosBancariosBean();
                        datos.iIdBanco  = Convert.ToInt32(dataReader["Banco"]);
                        datos.iCantidad = Convert.ToInt32(dataReader["Cantidad"]);
                        datos.sImporte  = dataReader["Importe"].ToString();
                        datosDepositos.Add(datos);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosDepositos;
        }

        public List<DatosProcesaChequesNominaBean> sp_Procesa_Cheques_Nomina_Special(int keyGroup, int keyConfig, int keyBank, int mirror, int year, int typePeriod, int period)
        {
            List<DatosProcesaChequesNominaBean> datosProcesaCheques = new List<DatosProcesaChequesNominaBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Procesa_Cheques_Nomina_Special", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Grupo_id", keyGroup));
                cmd.Parameters.Add(new SqlParameter("@Configuracion_id", keyConfig));
                cmd.Parameters.Add(new SqlParameter("@Banco_id", keyBank));
                cmd.Parameters.Add(new SqlParameter("@Espejo", mirror));
                cmd.Parameters.Add(new SqlParameter("@Anio", year));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodo_id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", period));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows) {
                    while (dataReader.Read()) {
                        DatosProcesaChequesNominaBean datos = new DatosProcesaChequesNominaBean();
                        datos.iIdBanco   = Convert.ToInt32(dataReader["IdBanco"]);
                        datos.sBanco     = dataReader["Banco"].ToString();
                        datos.iIdEmpresa = Convert.ToInt32(dataReader["Empresa"]);
                        datos.sNomina    = dataReader["Nomina"].ToString();
                        datos.sCuenta    = dataReader["Cuenta"].ToString();
                        datos.dImporte   = Convert.ToDecimal(dataReader["Importe"]);
                        datos.doImporte  = Convert.ToDouble(dataReader["ImporteDo"]);
                        datos.sNombre    = dataReader["Nombre"].ToString();
                        datos.sPaterno   = dataReader["Paterno"].ToString();
                        datos.sMaterno   = dataReader["Materno"].ToString();
                        datos.sRfc       = dataReader["RFC"].ToString();
                        datos.iTipoPago  = Convert.ToInt32(dataReader["TipoPago"]);
                        datosProcesaCheques.Add(datos);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosProcesaCheques;
        }

        public DataTable sp_Reporte_Dispersion_Especial(int keyGroup, int keyBank, int mirror, int year, int typePeriod, int period)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Reporte_Dispersion_Especial", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Grupo_id", keyGroup));
                cmd.Parameters.Add(new SqlParameter("@Banco_id", keyBank));
                cmd.Parameters.Add(new SqlParameter("@Espejo", mirror));
                cmd.Parameters.Add(new SqlParameter("@Anio", year));
                cmd.Parameters.Add(new SqlParameter("@TipoPeriodo_id", typePeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", period));
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

        public DataTable sp_Reporte_Dispersion(int keyBusines, int yearPeriod, int typePeriod, int period)
        {
            DataTable dataTable = new DataTable();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Reporte_Dispersion", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusines));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearPeriod));
                cmd.Parameters.Add(new SqlParameter("@Periodo", period));
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

    }
}