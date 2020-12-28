using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using Payroll.Models.Beans;
using Payroll.Models.Utilerias;

namespace Payroll.Models.Daos
{
    public class BajasEmpleadosDaoD : Conexion
    {

        public List<ComplementosFiniquitos> sp_View_Details_Complement(int keySettlement, int keyBusiness, int keySeq)
        {
            List<ComplementosFiniquitos> complementos = new List<ComplementosFiniquitos>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_View_Details_Complement", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@FiniquitoId", keySettlement));
                cmd.Parameters.Add(new SqlParameter("@EmpresaId", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Seq", keySeq));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows) {
                    while (dataReader.Read()) {
                        ComplementosFiniquitos finiquitos = new ComplementosFiniquitos();
                        finiquitos.iFiniquitoId = Convert.ToInt32(dataReader["Finiquito_id"]);
                        finiquitos.iSeq = Convert.ToInt32(dataReader["Seq"]);
                        finiquitos.iEmpresaId = Convert.ToInt32(dataReader["Empresa_id"]);
                        finiquitos.iRenglonId = Convert.ToInt32(dataReader["Renglon_id"]);
                        finiquitos.dImporte   = Convert.ToDecimal(dataReader["Importe"]);
                        finiquitos.sImporte   = string.Format(CultureInfo.InvariantCulture, "{0:#,###,##0.00}", Convert.ToDecimal(Convert.ToDecimal(dataReader["Importe"])));
                        finiquitos.sFechaComplemento = dataReader["FechaComplemento"].ToString();
                        finiquitos.sNombreRenglon    = dataReader["Nombre_Renglon"].ToString();
                        finiquitos.iTipoRenglonId    = Convert.ToInt32(dataReader["Tipo_renglon_id"]);
                        complementos.Add(finiquitos);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return complementos;
        }

        public List<ComplementosFiniquitos> sp_View_Complement_Settlement(int keyBusiness, int keySettlement)
        {
            List<ComplementosFiniquitos> complementos = new List<ComplementosFiniquitos>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_View_Complement_Settlement", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@EmpresaId", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@FiniquitoId", keySettlement));
                SqlDataReader dataReader = cmd.ExecuteReader(); 
                if (dataReader.HasRows) {
                    while (dataReader.Read()) {
                        ComplementosFiniquitos finiquitos = new ComplementosFiniquitos();
                        finiquitos.iSeq       = Convert.ToInt32(dataReader["Seq"]);
                        finiquitos.iConceptos = Convert.ToInt32(dataReader["Conceptos"]);
                        complementos.Add(finiquitos);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return complementos;
        }

        public BajasEmpleadosBean sp_Max_Sequence_Number_Complement_Settlement(int keySettlement, int keyBusiness)
        {
            BajasEmpleadosBean bajasEmpleados = new BajasEmpleadosBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Max_Sequence_Number_Complement_Settlement", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@FiniquitoId", keySettlement));
                cmd.Parameters.Add(new SqlParameter("@EmpresaId", keyBusiness));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    bajasEmpleados.iEstatus = Convert.ToInt32(dataReader["Seq"].ToString());
                } else {
                    bajasEmpleados.sMensaje = "NOTDATA";
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                bajasEmpleados.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return bajasEmpleados;
        }

        public BajasEmpleadosBean sp_Add_Complement_Settlement(List<ListConcepts> items, int keySettlement, int keyBusiness, int seq)
        {
            BajasEmpleadosBean bajasEmpleados = new BajasEmpleadosBean();
            int quantity = 0;
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Add_Complement_Settlement", this.conexion) { CommandType = CommandType.StoredProcedure };
                foreach (ListConcepts concepts in items) {
                    cmd.Parameters.Add(new SqlParameter("@FiniquitoId", keySettlement));
                    cmd.Parameters.Add(new SqlParameter("@Seq", seq));
                    cmd.Parameters.Add(new SqlParameter("@EmpresaId", keyBusiness));
                    cmd.Parameters.Add(new SqlParameter("@Importe", concepts.import));
                    cmd.Parameters.Add(new SqlParameter("@Concepto", concepts.concept));
                    if (cmd.ExecuteNonQuery() > 0) {
                        quantity += 1;
                    }
                    cmd.Parameters.Clear(); cmd.Dispose();
                }
                if (quantity == items.Count) {
                    bajasEmpleados.sMensaje = "success";
                }
            } catch (Exception exc) {
                bajasEmpleados.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return bajasEmpleados;
        }

        public DatosFiniquito sp_Consulta_Info_Finiquito(int keySettlement, int keyBusiness, int keyEmploye)
        {
            DatosFiniquito datosFiniquito = new DatosFiniquito();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Consulta_Info_Finiquito", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdFiniquito", keySettlement));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmploye));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    if (dataReader["Bandera"].ToString() == "1") {

                        datosFiniquito.sFechaBaja       = dataReader["Fecha_Baja"].ToString();
                        datosFiniquito.sFechaRecibo     = dataReader["Fecha_recibo"].ToString();
                        datosFiniquito.iTipoFiniquitoId = Convert.ToInt32(dataReader["Tipo_finiquito_id"]);
                        datosFiniquito.iBanFechaIngreso = Convert.ToInt32(dataReader["ban_fecha_ingreso"]);
                        datosFiniquito.iBanCompEspecial = Convert.ToInt32(dataReader["ban_compensacion_especial"]);
                        datosFiniquito.iDiasPendientes  = Convert.ToInt32(dataReader["Dias_pendientes"]);
                        datosFiniquito.iAnio    = Convert.ToInt32(dataReader["Anio"]);
                        datosFiniquito.iPeriodo = Convert.ToInt32(dataReader["Periodo"]);
                        datosFiniquito.sFechaPagoInicio = dataReader["Fecha_Pago_Inicio"].ToString();
                        datosFiniquito.sFechaPFin       = dataReader["Fecha_Pago_Fin"].ToString();
                        datosFiniquito.iMotivoBajaId    = Convert.ToInt32(dataReader["Cg_motivo_baja_id"]);
                        datosFiniquito.iTipoOperacion   = Convert.ToInt32(dataReader["Tipo_operacion"]);
                        datosFiniquito.sFechaAntiguedad = dataReader["Fecha_antiguedad"].ToString();
                        datosFiniquito.sMensaje         = "SUCCESS";

                    } else {
                        datosFiniquito.sMensaje = "ERROR";
                    }
                } else {
                    datosFiniquito.sMensaje = "NOTDATA";
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return datosFiniquito;
        }

        public BajasEmpleadosBean sp_Cancel_Settlement_Employee_Reactive (int keyEmployee, int keyBusiness)
        {
            BajasEmpleadosBean bajasEmpleados = new BajasEmpleadosBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Cancel_Settlement_Employee_Reactive", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@Empresa_id", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Empleado_id", keyEmployee));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    if (dataReader["Bandera"].ToString() == "1") {
                        bajasEmpleados.sMensaje = "SUCCESS";
                    } else {
                        bajasEmpleados.sMensaje = "ERROR";
                    }
                } else {
                    bajasEmpleados.sMensaje = "NOTDATA";
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                bajasEmpleados.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return bajasEmpleados;
        }

        public BajasEmpleadosBean sp_Apply_Down_Employee(int keySettlement, int keyEmployee, int keyBusiness)
        {
            BajasEmpleadosBean bajasEmpleados = new BajasEmpleadosBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Apply_Down_Employee", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdFiniquito", keySettlement));
                cmd.Parameters.Add(new SqlParameter("@Empleado_id", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@Empresa_id", keyBusiness));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    if (dataReader["Bandera"].ToString() == "1") {
                        bajasEmpleados.sMensaje = "SUCCESS";
                    } else {
                        bajasEmpleados.sMensaje = "ERROR";
                    }
                } else {
                    bajasEmpleados.sMensaje = "NOTDATA";
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                bajasEmpleados.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return bajasEmpleados;
        }
        public BajasEmpleadosBean sp_Valida_Existencia_Finiquito(int keyEmployee, int keyBusiness, int yearAct, int keyPeriodAct)
        {
            BajasEmpleadosBean downVerify = new BajasEmpleadosBean();
            Boolean validation = false;
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Valida_Existencia_Finiquito", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearAct));
                cmd.Parameters.Add(new SqlParameter("@Periodo", keyPeriodAct));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read()) {
                    if (dataReader["Respuesta"].ToString() == "EXISTS" && dataReader["FechaBaja"].ToString() != "none") {
                        downVerify.sMensaje    = "EXISTS";
                        downVerify.sFecha_baja = dataReader["FechaBaja"].ToString(); 
                    } else {
                        downVerify.sMensaje    = "NOTEXISTS";
                        downVerify.sFecha_baja = dataReader["FechaBaja"].ToString();
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return downVerify;
        }

        public DescEmpleadoVacacionesBean sp_Select_Dias_A_Anteriores(int business, int employee)
        {
            DescEmpleadoVacacionesBean descEmpleado = new DescEmpleadoVacacionesBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Select_Dias_A_Anteriores", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", employee));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", business));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read()) {
                    descEmpleado.DiasAAnteriores = Convert.ToInt32(data["DiasAAnteriores"].ToString());
                } else {
                    descEmpleado.DiasAAnteriores = 0;
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return descEmpleado;
        }

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

        public BajasEmpleadosBean sp_CNomina_Finiquito(int keyBusiness, int keyEmployee, string dateAntiquityEmp, int idTypeDown, int idReasonsDown, string dateDownEmp, string dateReceipt, int typeDate, int typeCompensation, int daysPendings, int yearAct, int keyPeriodAct, string dateStartPayment, string dateEndPayment, int typeOper, int propSet)
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
                cmd.Parameters.Add(new SqlParameter("@motivo_baja_id", idReasonsDown));
                cmd.Parameters.Add(new SqlParameter("@tipo_operacion", typeOper));
                cmd.Parameters.Add(new SqlParameter("@status", propSet));
                bool proc = Convert.ToBoolean(cmd.ExecuteNonQuery());
                if (proc) {
                    downEmployee.sMensaje = "SUCCESS";
                } else {
                    downEmployee.sMensaje = "ERRINSERT";
                }
                cmd.Parameters.Clear(); cmd.Dispose();
            }
            catch (Exception exc)
            {
                downEmployee.sMensaje = exc.Message.ToString();
                Console.WriteLine(exc.Message.ToString());
            }
            finally
            {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return downEmployee;
        }

        public BajasEmpleadosBean sp_Crea_Baja_Sin_Baja_Calculos(int keyBusiness, int keyEmployee, string dateDownEmp, int idTypeDown, int idReasonsDown, int yearAct, int keyPeriodAct)
        {
            BajasEmpleadosBean bajasEmpleadosBean = new BajasEmpleadosBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Crea_Baja_Sin_Baja_Calculos", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@FechaEfect", dateDownEmp));
                cmd.Parameters.Add(new SqlParameter("@FechaBaja", dateDownEmp));
                cmd.Parameters.Add(new SqlParameter("@TipoFiniquito", idTypeDown));
                cmd.Parameters.Add(new SqlParameter("@MotivoBaja", idReasonsDown));
                cmd.Parameters.Add(new SqlParameter("@Anio", yearAct));
                cmd.Parameters.Add(new SqlParameter("@Periodo", keyPeriodAct));
                if (cmd.ExecuteNonQuery() > 0) {
                    bajasEmpleadosBean.sMensaje = "SUCCESS";
                } else {
                    bajasEmpleadosBean.sMensaje = "ERROR";
                }
                cmd.Parameters.Clear(); cmd.Dispose(); 
            } catch (Exception exc) {
                bajasEmpleadosBean.sMensaje = exc.Message.ToString();
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return bajasEmpleadosBean;
        }

        public BajasEmpleadosBean sp_BajaEmpleado_Update_EmpleadoNomina(int keyEmployee, int keyBusiness, int keyTypeDown, string dateDown)
        {
            BajasEmpleadosBean downEmployeeBean = new BajasEmpleadosBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_BajaEmpleado_Update_EmpleadoNomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@TipoEmpleado", keyTypeDown));
                cmd.Parameters.Add(new SqlParameter("@FechaBaja", dateDown));
                if (cmd.ExecuteNonQuery() > 0) {
                    downEmployeeBean.sMensaje = "SUCCESSUPD";
                } else {
                    downEmployeeBean.sMensaje = "ERRUPDTE";
                }
            } catch (Exception exc){
                downEmployeeBean.sMensaje = exc.Message.ToString();
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return downEmployeeBean;
        }

        public List<BajasEmpleadosBean> sp_Finiquitos_Empleado(int keyEmployee, int keyBusiness, int keySettlement)
        {
            List<BajasEmpleadosBean> listDataDownEmpBean = new List<BajasEmpleadosBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Finiquitos_Empleado", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@IdFiniquito", keySettlement));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        listDataDownEmpBean.Add(new BajasEmpleadosBean {
                            iIdFiniquito = Convert.ToInt32(data["IdFiniquito"].ToString()),
                            sEffdt       = data["Effdt"].ToString(),
                            sFecha_antiguedad = data["Fecha_antiguedad"].ToString(),
                            sFecha_ingreso = data["Fecha_ingreso"].ToString(),
                            sFecha_baja    = data["Fecha_baja"].ToString(),
                            iAnios = Convert.ToInt32(data["Anios"].ToString()),
                            sDias  = data["Dias"].ToString(),
                            iTipo_finiquito_id = Convert.ToInt32(data["Tipo_finiquito_id"].ToString()),
                            sFiniquito_valor   = data["Finiquito_valor"].ToString(),
                            iEmpleado_id  = Convert.ToInt32(data["Empleado_id"].ToString()),
                            sFecha_recibo = data["Fecha_recibo"].ToString(),
                            sEmpresa = data["NombreEmpresa"].ToString(),
                            iEstatus = Convert.ToInt32(data["Estatus"].ToString()),
                            sRFC     = data["RFC"].ToString(),
                            iCentro_costo_id = (data["Centro_costo_id"].ToString() != "") ? Convert.ToInt32(data["Centro_costo_id"].ToString()) : 0,
                            sSalario_diario  = string.Format(CultureInfo.InvariantCulture, "{0:#,###,##0.00}", Convert.ToDecimal((data["Salario_diario"]))),
                            sSalario_mensual = string.Format(CultureInfo.InvariantCulture, "{0:#,###,##0.00}", Convert.ToDecimal((data["Salario_mensual"]))),
                            sPuesto = data["NombrePuesto"].ToString(),
                            sPuesto_codigo = data["PuestoCodigo"].ToString(),
                            sCentro_costo  = data["CentroCosto"].ToString(),
                            sDepartamento  = data["DescripcionDepartamento"].ToString(),
                            sDepto_codigo  = data["Depto_Codigo"].ToString(),
                            iAnioPeriodo   = Convert.ToInt32(data["Anio"].ToString()),
                            iPeriodo       = Convert.ToInt32(data["Periodo"].ToString()),
                            iDias_Pendientes = Convert.ToInt32(data["Dias_pendientes"].ToString()),
                            sCancelado    = data["Cancelado"].ToString(),
                            sRegistroImss = data["RegistroImss"].ToString(),
                            sCta_Cheques  = data["Cta_Cheques"].ToString(),
                            sFecha_Pago_Inicio = data["Fecha_Pago_Inicio"].ToString(),
                            sFecha_Pago_Fin    = data["Fecha_Pago_Fin"].ToString(),
                            sMotivo_baja = data["Motivo_baja"].ToString(),
                            iMotivo_baja = Convert.ToInt32(data["Cg_motivo_baja_id"].ToString()),
                            sTipo_Operacion = data["Tipo_operacion"].ToString()
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

        public List<ComplementosFiniquitos> sp_Info_Complement_Settlement(int keySettlement, int keyBusiness, int keyEmployee, int seq)
        {
            List<ComplementosFiniquitos> listConcepts = new List<ComplementosFiniquitos>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Info_Complement_Settlement", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@FiniquitoId", keySettlement));
                cmd.Parameters.Add(new SqlParameter("@EmpresaId", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@EmpleadoId", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@Seq", seq));
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows) {
                    while (dataReader.Read()) {
                        ComplementosFiniquitos finiquitos = new ComplementosFiniquitos();
                        finiquitos.iFiniquitoId = Convert.ToInt32(dataReader["Finiquito_id"]);
                        finiquitos.iSeq = Convert.ToInt32(dataReader["Seq"]);
                        finiquitos.iEmpresaId = Convert.ToInt32(dataReader["Empresa_id"]);
                        finiquitos.iRenglonId = Convert.ToInt32(dataReader["Renglon_id"]);
                        finiquitos.dImporte = Convert.ToDecimal(dataReader["Importe"]);
                        finiquitos.sImporte = string.Format(CultureInfo.InvariantCulture, "{0:#,###,##0.00}", Convert.ToDecimal(Convert.ToDecimal(dataReader["Importe"])));
                        finiquitos.sFechaComplemento = dataReader["FechaComplemento"].ToString();
                        finiquitos.sNombreRenglon    = dataReader["Nombre_Renglon"].ToString();
                        finiquitos.iTipoRenglonId    = Convert.ToInt32(dataReader["Tipo_renglon_id"]);
                        finiquitos.sNombreEmpleado   = dataReader["NombreEmpleado"].ToString(); 
                        listConcepts.Add(finiquitos);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); dataReader.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return listConcepts;
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

        public BajasEmpleadosBean sp_Cancela_Finiquito(int keySetlement, int typeCancel, int keyEmployee, int keyBusiness)
        {
            BajasEmpleadosBean downEmployeeBean = new BajasEmpleadosBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Cancela_Finiquito", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdFiniquito", keySetlement));
                cmd.Parameters.Add(new SqlParameter("@Cancelado", typeCancel));
                cmd.Parameters.Add(new SqlParameter("@Empleado_id", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@Empresa_id", keyBusiness));
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


        public BajasEmpleadosBean sp_Finiquito_UpdateEstatus_Pagado(int keySettlement)
        {
            BajasEmpleadosBean downEmployeeBean = new BajasEmpleadosBean();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Finiquito_UpdateEstatus_Pagado", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@ctrlIdFiniquito", keySettlement));
                if (cmd.ExecuteNonQuery() > 0) {
                    downEmployeeBean.sMensaje = "SUCCESS";
                } else {
                    downEmployeeBean.sMensaje = "ERRUPDPAID";
                }
                cmd.Parameters.Clear(); cmd.Dispose();
            } catch (Exception exc) {
                downEmployeeBean.sMensaje = exc.Message.ToString();
                Console.WriteLine(exc.Message.ToString());
            } finally {
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