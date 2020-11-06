using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using Payroll.Models.Beans;
using Payroll.Models.Utilerias;

namespace Payroll.Models.Daos
{
    public class BajasEmpleadosDaoD : Conexion
    {

        public PeriodoActualBean sp_Load_Info_Periodo_Empr(int keyBusiness, int yearAct)
        {
            PeriodoActualBean periodoActual = new PeriodoActualBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Load_Info_Periodo_Empr", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@ctrlAnio", yearAct));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    periodoActual.iEmpresa_id = Convert.ToInt32(data["Empresa_id"].ToString());
                    periodoActual.iAnio = Convert.ToInt32(data["Anio"].ToString());
                    periodoActual.iTipoPeriodo = Convert.ToInt32(data["Tipo_Periodo_id"].ToString());
                    periodoActual.iPeriodo = Convert.ToInt32(data["Periodo"].ToString());
                    periodoActual.sFecha_Inicio = data["Fecha_Inicio"].ToString();
                    periodoActual.sFecha_Final = data["Fecha_Final"].ToString();
                    periodoActual.sMensaje = "success";
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
            return periodoActual;
        }

        public BajasEmpleadosBean sp_CNomina_Finiquito(int keyBusiness, int keyEmployee, string dateAntiquityEmp, int idTypeDown, int idReasonsDown, string dateDownEmp, string dateReceipt, int typeDate, int typeCompensation, int daysPendings, int yearAct, int keyPeriodAct, string dateStartPayment, string dateEndPayment)
        {
            BajasEmpleadosBean downEmployee = new BajasEmpleadosBean();
            try
            {
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
                cmd.Parameters.Add(new SqlParameter("@dias_pendientes", daysPendings));
                cmd.Parameters.Add(new SqlParameter("@anio", yearAct));
                cmd.Parameters.Add(new SqlParameter("@periodo", keyPeriodAct));
                cmd.Parameters.Add(new SqlParameter("@Fecha_Pago_Inicio", dateStartPayment));
                cmd.Parameters.Add(new SqlParameter("@Fecha_Pago_Fin", dateEndPayment));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    downEmployee.sMensaje = "SUCCESS";
                }
                else
                {
                    downEmployee.sMensaje = "ERRINSERT";
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
            return downEmployee;
        }

        public List<BajasEmpleadosBean> sp_Finiquitos_Empleado(int keyEmployee, int keyBusiness, int keySettlement)
        {
            List<BajasEmpleadosBean> listDataDownEmpBean = new List<BajasEmpleadosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Finiquitos_Empleado", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@IdFiniquito", keySettlement));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        listDataDownEmpBean.Add(new BajasEmpleadosBean
                        {
                            iIdFiniquito = Convert.ToInt32(data["IdFiniquito"].ToString()),
                            sEffdt = data["Effdt"].ToString(),
                            sFecha_antiguedad = data["Fecha_antiguedad"].ToString(),
                            sFecha_ingreso = data["Fecha_ingreso"].ToString(),
                            sFecha_baja = data["Fecha_baja"].ToString(),
                            iAnios = Convert.ToInt32(data["Anios"].ToString()),
                            sDias = data["Dias"].ToString(),
                            iTipo_finiquito_id = Convert.ToInt32(data["Tipo_finiquito_id"].ToString()),
                            sFiniquito_valor = data["Finiquito_valor"].ToString(),
                            iEmpleado_id = Convert.ToInt32(data["Empleado_id"].ToString()),
                            sFecha_recibo = data["Fecha_recibo"].ToString(),
                            sEmpresa = data["NombreEmpresa"].ToString(),
                            iEstatus = Convert.ToInt32(data["Estatus"].ToString()),
                            sRFC = data["RFC"].ToString(),
                            iCentro_costo_id = Convert.ToInt32(data["Centro_costo_id"].ToString()),
                            sSalario_diario = data["Salario_diario"].ToString(),
                            sSalario_mensual = data["Salario_mensual"].ToString(),
                            sPuesto = data["NombrePuesto"].ToString(),
                            sPuesto_codigo = data["PuestoCodigo"].ToString(),
                            sCentro_costo = data["CentroCosto"].ToString(),
                            sDepartamento = data["DescripcionDepartamento"].ToString(),
                            sDepto_codigo = data["Depto_Codigo"].ToString(),
                            iAnioPeriodo = Convert.ToInt32(data["Anio"].ToString()),
                            iPeriodo = Convert.ToInt32(data["Periodo"].ToString()),
                            iDias_Pendientes = Convert.ToInt32(data["Dias_pendientes"].ToString()),
                            sCancelado = data["Cancelado"].ToString(),
                            sRegistroImss = data["RegistroImss"].ToString(),
                            sCta_Cheques = data["Cta_Cheques"].ToString(),
                            sFecha_Pago_Inicio = data["Fecha_Pago_Inicio"].ToString(),
                            sFecha_Pago_Fin = data["Fecha_Pago_Fin"].ToString()
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
            return listDataDownEmpBean;
        }

        public List<DatosFiniquito> sp_Info_Finiquito_Empleado(int keySettlement)
        {
            List<DatosFiniquito> listDataDown = new List<DatosFiniquito>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Info_Finiquito_Empleado", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdFiniquito", keySettlement));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        listDataDown.Add(new DatosFiniquito
                        {
                            iIdValor = Convert.ToInt32(data["IdValor"].ToString()),
                            sTipo = data["Tipo"].ToString(),
                            iRenglon_id = Convert.ToInt32(data["Renglon_id"].ToString()),
                            sNombre_Renglon = data["Nombre_Renglon"].ToString(),
                            sGravado = data["Gravado"].ToString(),
                            sExcento = data["Excento"].ToString(),
                            sSaldo = data["Saldo"].ToString(),
                            iEmpresa = Convert.ToInt32(data["Empresa"].ToString()),
                            iNomina = Convert.ToInt32(data["Nomina"].ToString()),
                            sNombre = data["Nombre"].ToString(),
                            sLeyenda = data["Leyenda"].ToString()
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
            return listDataDown;
        }

        public BajasEmpleadosBean sp_Selecciona_Finiquito_Pago(int keySettlement)
        {
            BajasEmpleadosBean selectSettlementPaid = new BajasEmpleadosBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Selecciona_Finiquito_Pago", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdFiniquito", keySettlement));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    selectSettlementPaid.sMensaje = "UPDATE";
                }
                else
                {
                    selectSettlementPaid.sMensaje = "ERRUPD";
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
            return selectSettlementPaid;
        }

        public BajasEmpleadosBean sp_Cancela_Finiquito(int keySetlement, int typeCancel)
        {
            BajasEmpleadosBean downEmployeeBean = new BajasEmpleadosBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Cancela_Finiquito", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdFiniquito", keySetlement));
                cmd.Parameters.Add(new SqlParameter("@Cancelado", typeCancel));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    downEmployeeBean.sMensaje = "success";
                }
                else
                {
                    downEmployeeBean.sMensaje = "ERRORUPD";
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
            return downEmployeeBean;
        }


        /// trae el listado del tipos de empleados

        public List<TipoDeEmpleadoBean> sp_TipoEmpleado_Retrieve_Cgeneral() {
            List<TipoDeEmpleadoBean> list = new List<TipoDeEmpleadoBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TipoEmpleado_Retrieve_Cgeneral", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TipoDeEmpleadoBean ls = new TipoDeEmpleadoBean();
                        {
                            ls.iIdTipoEmpleado = int.Parse(data["IdValor"].ToString());
                            ls.sTipodeEmpleado = data["Valor"].ToString();
                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        /// trae el listado con detalle de los empleados 

        public List<EmisorReceptorBean> sp_EmpladosKitDoc_Retrieve_Cgeneral(int CtrliIdTipoEmpleado, int CtrliBaja)
        {
            List<EmisorReceptorBean> list = new List<EmisorReceptorBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_EmpladosKitDoc_Retrieve_Cgeneral", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdTipoEmpleado", CtrliIdTipoEmpleado));
                cmd.Parameters.Add(new SqlParameter("@CtrliBaja", CtrliBaja));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EmisorReceptorBean ls = new EmisorReceptorBean();
                        {
                            ls.iIdEmpresa = int.Parse(data["Empresa_id"].ToString());
                            ls.iIdNomina = int.Parse(data["IdEmpleado"].ToString());
                            ls.sNombreComp = data["NombreCompleto"].ToString();
                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        /// trae el litado de los Id de Finiquito

        public List<ReciboNominaBean> sp_NoIdFiniquito_Retrieve_TFiniquitos(int CtrliIdEmpresa, int CtrliIdEmpleado)
        {
            List<ReciboNominaBean> list = new List<ReciboNominaBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_NoIdFiniquito_Retrieve_TFiniquitos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpleado", CtrliIdEmpleado));
                
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        ReciboNominaBean ls = new ReciboNominaBean();
                        {
                            ls.iIdFiniquito = int.Parse(data["Idfiniquito"].ToString());
                           
                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

    }
}