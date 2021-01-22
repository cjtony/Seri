
using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Payroll.Models.Daos
{

    public class NominaDao : Conexion 
    {
        public String ConvertDateText(string dateConvert)
        {
            String convertDate = "";
            try
            {
                string year = dateConvert.Substring(0, 4);
                string month = dateConvert.Substring(5, 2);
                string day = dateConvert.Substring(8, 2);
                string[] days = new string[] { "Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sábado" };
                string[] months = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                convertDate = day + " de " + months[Convert.ToInt32(month) - 1] + " del " + year;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }
            return convertDate;
        }
        public List<DatosNominaBean> sp_Carga_Historial_Nomina(int keyBusiness, int keyEmployee)
        {
            List<DatosNominaBean> listNomina = new List<DatosNominaBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Carga_Historial_Nomina", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpleado", keyEmployee));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        DatosNominaBean nomina = new DatosNominaBean();
                        nomina.iIdNomina = Convert.ToInt32(data["IdNomina"]);
                        nomina.sFechaEfectiva = Convert.ToDateTime(data["Effdt"]).ToString("yyyy-MM-dd");
                        nomina.sPeriodo = data["Periodo"].ToString();
                        nomina.sSalarioMensual = string.Format(CultureInfo.InvariantCulture, "{0:#,###,##0.00}", Convert.ToDecimal(data["SalarioMensual"]));
                        nomina.sTipoEmpleado = data["Tipo_Empleado"].ToString();
                        nomina.sNivelEmpleado = data["Nivel_Empleado"].ToString();
                        nomina.sTipoJornada = data["Tipo_Jornada"].ToString();
                        nomina.sTipoContrato = data["Tipo_Contrato"].ToString();
                        nomina.sTipoContratacion = data["Tipo_Contratacion"].ToString();
                        nomina.sFechaIngreso = ConvertDateText(Convert.ToDateTime(data["FechaIngreso"]).ToString("yyyy-MM-dd"));
                        nomina.sFechaAntiguedad = ConvertDateText(Convert.ToDateTime(data["FechaAntiguedad"]).ToString("yyyy-MM-dd"));
                        nomina.sVencimientoContrato = (data["Vencimiento_contrato"].ToString() != "") ? ConvertDateText(Convert.ToDateTime(data["Vencimiento_contrato"]).ToString("yyyy-MM-dd")) : "Sin fecha";
                        nomina.iPosicion_id = Convert.ToInt32(data["Posicion_id"]);
                        nomina.sTipoPago = data["Tipo_Pago"].ToString();
                        nomina.sBanco = data["Banco"].ToString();
                        nomina.sCuentaCheques = data["Cta_Cheques"].ToString();
                        nomina.sFechaAlta = ConvertDateText(Convert.ToDateTime(data["Fecha_Alta"]).ToString("yyyy-MM-dd"));
                        nomina.sTipoSueldo = data["TipoSueldo"].ToString();
                        nomina.iPolitica = Convert.ToInt32(data["Politica"].ToString());
                        nomina.dDiferencia = Convert.ToDouble(data["DiferenciaP"].ToString());
                        nomina.dTransporte = Convert.ToDouble(data["Transporte"].ToString());
                        listNomina.Add(nomina);
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return listNomina;
        }

    }

    public class FuncionesNomina : Conexion
    {
        public NominahdBean sp_DefineNom_insert_DefineNom(string CtrsNombre, string CtrsDEscripcion, int CtriAno, int ctrlsCancelado, int iIdusario)
        {
            NominahdBean bean = new NominahdBean();

            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_DefineNom_insert_DefineNom", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrsNombre", CtrsNombre));
                cmd.Parameters.Add(new SqlParameter("@CtrsDEscripcion", CtrsDEscripcion));
                cmd.Parameters.Add(new SqlParameter("@sCtriAno", CtriAno));
                cmd.Parameters.Add(new SqlParameter("@ctrlsCancelado", ctrlsCancelado));
                cmd.Parameters.Add(new SqlParameter("@ctrlsUsuarioAlta", iIdusario));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }
        public List<EmpresasBean> sp_CEmpresas_Retrieve_Empresas(int idPerfil)
        {
            List<EmpresasBean> list = new List<EmpresasBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CEmpresas_Retrieve_Empresas", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrlPerfil_id", idPerfil));
               
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EmpresasBean ls = new EmpresasBean
                        {
                            iIdEmpresa = int.Parse(data["IdEmpresa"].ToString()),
                            sNombreEmpresa = data["NombreEmpresa"].ToString()

                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }
        public List<CTipoPeriodoBean> sp_CTipoPeriod_Retrieve_TiposPeriodos(int  Idempresa)
        {
            List<CTipoPeriodoBean> list = new List<CTipoPeriodoBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CTipoPeriod_Retrieve_TiposPeriodos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrliIdempresa", Idempresa));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CTipoPeriodoBean ls = new CTipoPeriodoBean();
                        {
                            ls.iId = int.Parse(data["id"].ToString());
                            ls.sValor = data["Valor"].ToString();

                        };


                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }

                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        public List<CRenglonesBean> sp_CRenglones_Retrieve_CRenglones(int IdEmpresa ,int ctrliElemntoNOm)
        {
            List<CRenglonesBean> list = new List<CRenglonesBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CRenglones_Retrieve_CRenglones", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrliIdEmpresa", IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@ctrliElemntoNOm", ctrliElemntoNOm));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CRenglonesBean ls = new CRenglonesBean();
                        {
                            ls.iIdRenglon = int.Parse(data["IdRenglon"].ToString());
                            ls.sNombreRenglon = data["NombreRenglon"].ToString();
                            ls.iEspejo = int.Parse(data["rng_espejo"].ToString());
                         
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
        public List<CAcumuladosRenglon> sp_CAcumuladoREnglones_Retrieve_CAcumuladoREnglones(int ctrliIdEmpresa, int ctrliIdRenglon)
        {
            List<CAcumuladosRenglon> list = new List<CAcumuladosRenglon>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CAcumuladoREnglones_Retrieve_CAcumuladoREnglones", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrliIdEmpresa", ctrliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@ctrlsiIdRenglon ", ctrliIdRenglon));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CAcumuladosRenglon LAc = new CAcumuladosRenglon();
                        {
                            LAc.iIdAcumulado = int.Parse(data["IdAcumulado"].ToString());
                            LAc.sDesAcumulado = data["Descripcion_Acumulado"].ToString();

                        };


                        list.Add(LAc);
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
        public List<NominahdBean> sp_IdDefinicionNomina_Retrieve_IdDefinicionNomina()
        {
            List<NominahdBean> list = new List<NominahdBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_IdDefinicionNomina_Retrieve_IdDefinicionNomina", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.Add(new SqlParameter("@ctrlNombreEmpresa", txt));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        NominahdBean LDNH = new NominahdBean
                        {
                            iIdDefinicionhd = int.Parse(data["IdDefinicionHd"].ToString())
                        };
                        list.Add(LDNH);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }
        public List<NominahdBean> sp_DefCancelados_Retrieve_DefCancelados(int ctrliIdDefinicion)
        {
            List<NominahdBean> list = new List<NominahdBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_DefCancelados_Retrieve_DefCancelados", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrliIdDefinicion", ctrliIdDefinicion));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        NominahdBean ls = new NominahdBean();
                        {
                            ls.iIdDefinicionhd = int.Parse(data["IdDefinicion_Hd"].ToString());
                            ls.iAno = int.Parse(data["Anio"].ToString());
                            ls.iCancelado = data["Cancelado"].ToString();

                        };


                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }

                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }
        public NominaLnBean sp_CDefinicionLN_insert_CDefinicionLN(int CtriIdDefinicion, int CtriIdEmpresaid, int CtriIdTipoPeriodo, /*int CtriIdPeriodo,*/ int CtriIdRenglon, int CtriCancelado, int CtriIdUsuarioAlta, int sCtriIdElementoNomina, int ctrliEspejo, int ctrliIDAcumulado)
        {
            NominaLnBean bean = new NominaLnBean();

            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CDefinicionLN_insert_CDefinicionLN", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtriIdDefinicion", CtriIdDefinicion));
                cmd.Parameters.Add(new SqlParameter("@CtriIdEmpresaid", CtriIdEmpresaid));
                cmd.Parameters.Add(new SqlParameter("@CtriIdTipoPeriodo", CtriIdTipoPeriodo));
                //cmd.Parameters.Add(new SqlParameter("@CtriIdPeriodo", CtriIdPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtriIdRenglon", CtriIdRenglon));
                cmd.Parameters.Add(new SqlParameter("@CtriCancelado", CtriCancelado));
                cmd.Parameters.Add(new SqlParameter("@CtriIdUsuarioAlta", CtriIdUsuarioAlta));
                cmd.Parameters.Add(new SqlParameter("@sCtriIdElementoNomina", sCtriIdElementoNomina));
                cmd.Parameters.Add(new SqlParameter("@ctrliEspejo", ctrliEspejo));
                cmd.Parameters.Add(new SqlParameter("@ctrliIDAcumulado", ctrliIDAcumulado));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }
        public List<CInicioFechasPeriodoBean> sp_Cperiodo_Retrieve_Cperiodo(int CtrliIdEmpresa, int CtrliAnio, int CtrliIdTipoPeriodo)
        {
            List<CInicioFechasPeriodoBean> list = new List<CInicioFechasPeriodoBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Cperiodo_Retrieve_Cperiodo", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliAnio ", CtrliAnio));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdTipoPeriodo", CtrliIdTipoPeriodo));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CInicioFechasPeriodoBean LP = new CInicioFechasPeriodoBean();
                        {
                            LP.iId = int.Parse(data["Id"].ToString());
                            if (CtrliIdTipoPeriodo != 0)
                            {
                                LP.iPeriodo = int.Parse(data["Periodo"].ToString());
                                LP.iPeriodo = int.Parse(data["Periodo"].ToString());
                                LP.sFechaInicio = data["Fecha_Inicio"].ToString();
                                LP.sFechaFinal = data["Fecha_Final"].ToString();
                                LP.sFechaPago = data["Fecha_Pago"].ToString();
                                LP.sNominaCerrada = data["Nomina_Cerrada"].ToString();
                            }
                            if (CtrliIdTipoPeriodo == 0)
                            {
                                LP.iPeriodo = int.Parse(data["Periodo"].ToString());
                                LP.sFechaInicio = data["Fecha_Inicio"].ToString();
                                LP.sFechaFinal = data["Fecha_Final"].ToString();
                                LP.sFechaPago = data["Fecha_Pago"].ToString();

                            }


                        };


                        list.Add(LP);
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
        public List<NominaLnDatBean> sp_DefinicionesNomLn_Retrieve_DefinicionesNomLn(int CtrliIdDefinicionHd)
        {
            List<NominaLnDatBean> list = new List<NominaLnDatBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_DefinicionesNomLn_Retrieve_DefinicionesNomLn", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdDefinicionHd", CtrliIdDefinicionHd));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        NominaLnDatBean LDN = new NominaLnDatBean();
                        {
                            LDN.iIdDefinicionln = data["IdDefinicion_Ln"].ToString();
                            LDN.IdEmpresa = data["NombreEmpresa"].ToString();
                            LDN.iRenglon = data["NombreRenglon"].ToString();
                            LDN.iTipodeperiodo = data["Valor"].ToString();
                            LDN.iIdAcumulado = data["Acumulado_id"].ToString();
                            LDN.iEsespejo = data["Es_Espejo"].ToString();
                            LDN.sMensaje = "success";
                        };


                        list.Add(LDN);
                    }
                }
                else
                {
                    NominaLnDatBean LDN = new NominaLnDatBean();
                    LDN.sMensaje = "NotDat";
                    list.Add(LDN);
                }

                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;

        }
        public List<NominaLnDatBean> sp_DescripAcu_Retrieve_DescripAcu(int CtrliIdAcumulado)
        {
            List<NominaLnDatBean> list = new List<NominaLnDatBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_DescripAcu_Retrieve_DescripAcu", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdAcumulado", CtrliIdAcumulado));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        NominaLnDatBean LDN = new NominaLnDatBean();
                        {
                            LDN.iIdAcumulado = data["Descripcion_Acumulado"].ToString();
                        };


                        list.Add(LDN);
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
        public List<NominaLnDatBean> sp_DefinicionesDeNomLn_Retrieve_DefinicionesDeNomLn(int CtrliIdDefinicionHd)
        {

            List<NominaLnDatBean> list = new List<NominaLnDatBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_DefinicionesDeNomLn_Retrieve_DefinicionesDeNomLn", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdDefinicionHd", CtrliIdDefinicionHd));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        NominaLnDatBean LDN = new NominaLnDatBean();
                        {
                            LDN.iIdDefinicionln = data["IdDefinicion_Ln"].ToString();
                            LDN.IdEmpresa = data["NombreEmpresa"].ToString();
                            LDN.iRenglon = data["NombreRenglon"].ToString();
                            LDN.iTipodeperiodo = data["Valor"].ToString();
                            //LDN.iIdperiodo = data["Periodo_id"].ToString();
                            LDN.iIdAcumulado = data["Acumulado_id"].ToString();
                            LDN.iEsespejo = data["Es_Espejo"].ToString();
                            LDN.sMensaje = "success";

                        };


                        list.Add(LDN);
                    }
                }
                else
                {
                    NominaLnDatBean LDN = new NominaLnDatBean();
                    LDN.sMensaje = "NotDat";
                    list.Add(LDN);
                }

                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;

        }
        public List<NominahdBean> sp_DefinicionNombresHd_Retrieve_DefinicionNombresHd()
        {
            List<NominahdBean> list = new List<NominahdBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_DefinicionNombresHd_Retrieve_DefinicionNombresHd", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.Add(new SqlParameter("@ctrlNombreEmpresa", txt));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        NominahdBean ls = new NominahdBean();
                        {

                            ls.sNombreDefinicion = data["Nombre_Definicion"].ToString();

                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;

        }
        public List<NominahdBean> sp_TpDefinicionesNom_Retrieve_TpDefinicionNom()
        {
            List<NominahdBean> list = new List<NominahdBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TpDefinicionesNom_Retrieve_TpDefinicionNom", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.Add(new SqlParameter("@ctrlNombreEmpresa", txt));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        NominahdBean ls = new NominahdBean();
                        {
                            ls.iIdDefinicionhd = int.Parse(data["IdDefinicion_Hd"].ToString());
                            ls.sNombreDefinicion = data["Nombre_Definicion"].ToString();
                            ls.sDescripcion = data["Descripcion"].ToString();
                            ls.iAno = int.Parse(data["Anio"].ToString());
                            ls.iCancelado = data["Cancelado"].ToString();
                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }
        public List<NominahdBean> sp_DeficionNominaCancelados_Retrieve_DeficionNominaCancelados(string CrtlsNombreDefinicio, int CrtliCanceldo)
        {

            List<NominahdBean> list = new List<NominahdBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_DeficionNominaCancelados_Retrieve_DeficionNominaCancelados", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CrtlsNombreDefinicio", CrtlsNombreDefinicio));
                cmd.Parameters.Add(new SqlParameter("@CrtliCanceldo ", CrtliCanceldo));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        NominahdBean TDN = new NominahdBean();
                        {
                            TDN.iIdDefinicionhd = int.Parse(data["IdDefinicion_Hd"].ToString());
                            TDN.sNombreDefinicion = data["Nombre_Definicion"].ToString();
                            TDN.sDescripcion = data["Descripcion"].ToString();
                            TDN.iAno = int.Parse(data["Anio"].ToString());
                            TDN.iCancelado = data["Cancelado"].ToString();
                        };


                        list.Add(TDN);
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
        public NominahdBean sp_TpDefinicion_Update_TpDefinicion(string CtrsNombre, string CtrsDEscripcion, int CtriAno, int ctrlsCancelado, int CtrliIdDefinicionhd)
        {
            NominahdBean bean = new NominahdBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TpDefinicion_Update_TpDefinicion", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrlsNombre", CtrsNombre));
                cmd.Parameters.Add(new SqlParameter("@CtrlsDEscripcion", CtrsDEscripcion));
                cmd.Parameters.Add(new SqlParameter("@sCtrliAno", CtriAno));
                cmd.Parameters.Add(new SqlParameter("@ctrlsCancelado", ctrlsCancelado));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdDefinicionhd", CtrliIdDefinicionhd));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }
        public NominahdBean sp_EliminarDefinicion_Delete_EliminarDefinicion(int CtrliIdDefinicionHd)
        {
            NominahdBean bean = new NominahdBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_EliminarDefinicion_Delete_EliminarDefinicion", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdDefinicionHd", CtrliIdDefinicionHd));

                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;


        }
        public NominaLnBean sp_TpDefinicionNomLn_Update_TpDefinicionNomLn(int CtrlIdDefinicionLn, int CtriIdEmpresaid, int CtriIdTipoPeriodo, /*int CtriIdPeriod,*/ int CtriIdRenglon, int ctrliEspejo, int ctrliIDAcumulado)
        {
            NominaLnBean bean = new NominaLnBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TpDefinicionNomLn_Update_TpDefinicionNomLn", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrlIdDefinicionLn", CtrlIdDefinicionLn));
                cmd.Parameters.Add(new SqlParameter("@CtriIdEmpresaid", CtriIdEmpresaid));
                cmd.Parameters.Add(new SqlParameter("@CtriIdTipoPeriodo", CtriIdTipoPeriodo));
                //cmd.Parameters.Add(new SqlParameter("@CtriIdPeriodo", CtriIdPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtriIdRenglon", CtriIdRenglon));
                cmd.Parameters.Add(new SqlParameter("@ctrliEspejo", ctrliEspejo));
                cmd.Parameters.Add(new SqlParameter("@ctrliIDAcumulado", ctrliIDAcumulado));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;

        }
        public NominaLnBean sp_RenglonesDefinicionNL_Update_TplantillaDefinicionNL(int CtrlIdDefinicionLn)
        {
            NominaLnBean bean = new NominaLnBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_RenglonesDefinicionNL_Update_TplantillaDefinicionNL", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrliIdDefinicionnl", CtrlIdDefinicionLn));

                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;

        }
        public NominaLnBean sp_EliminarDefinicionNl_Delete_EliminarDefinicionNl(int CtrliIdDefinicionNl)
        {
            NominaLnBean bean = new NominaLnBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_EliminarDefinicionNl_Delete_EliminarDefinicionNl", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdDefinicionNl", CtrliIdDefinicionNl));

                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;

        }
        public List<TpCalculosHd> sp_ExiteDefinicionTpCalculo_Retrieve_ExiteDefinicionTpCalculo(int CtrliIdDefinicion)
        {
            List<TpCalculosHd> list = new List<TpCalculosHd>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_ExiteDefinicionTpCalculo_Retrieve_ExiteDefinicionTpCalculo", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdDefinicion", CtrliIdDefinicion));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TpCalculosHd ls = new TpCalculosHd();
                        {
                            ls.iIdCalculosHd = int.Parse(data["Existe"].ToString());

                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    TpCalculosHd ls = new TpCalculosHd();
                    {
                        ls.iIdCalculosHd = 0;

                    };
                    list.Add(ls);
                }
                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;

        }

        public TpCalculosHd sp_TpCalculos_Insert_TpCalculos(int CtrliIdDefinicionHd, int CtrliFolio, int CtrliNominaCerrada, int CtrliIdUsuarios)
        {
            TpCalculosHd bean = new TpCalculosHd();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TpCalculos_Insert_TpCalculos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdDefinicionHd", CtrliIdDefinicionHd));
                cmd.Parameters.Add(new SqlParameter("@CtrliFolio", CtrliFolio));
                cmd.Parameters.Add(new SqlParameter("@CtrliNominaCerrada", CtrliNominaCerrada));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdUsuario", CtrliIdUsuarios));

                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }
        public TpCalculosHd sp_TpCalculos_update_TpCalculos(int CtrliIdDedinicionHD, int CtrliNominacerrada)
        {
            TpCalculosHd bean = new TpCalculosHd();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TpCalculos_update_TpCalculos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdDedinicionHD", CtrliIdDedinicionHD));
                cmd.Parameters.Add(new SqlParameter("@CtrliNominacerrada", CtrliNominacerrada));

                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }
        public List<NominaLnDatBean> sp_TpDefinicionNomins_Retrieve_TpDefinicionNomins()
        {
            List<NominaLnDatBean> list = new List<NominaLnDatBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TpDefinicionNomins_Retrieve_TpDefinicionNomins", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.Add(new SqlParameter("@ctrlNombreEmpresa", txt));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        NominaLnDatBean ls = new NominaLnDatBean();
                        {
                            ls.iIdDefinicionln = data["IdDefinicion_Ln"].ToString();
                            ls.IdEmpresa = data["NombreEmpresa"].ToString();
                            ls.iRenglon = data["NombreRenglon"].ToString();
                            ls.iElementonomina = data["Cg_Elemento_Nomina_id"].ToString();
                            ls.iTipodeperiodo = data["Valor"].ToString();
                            ls.iIdperiodo = data["Periodo_id"].ToString();
                            ls.iIdAcumulado = data["Acumulado_id"].ToString();
                            ls.iEsespejo = data["Es_Espejo"].ToString();

                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;

        }
        public TPProcesos Sp_TPProcesosJobs_insert_TPProcesosJobs(int CtrliIdJobs, string CtrlsEstatusJobs, string CtrilsNombreJobs, string CtrlsParametrosJobs)
        {
            TPProcesos bean = new TPProcesos();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("Sp_TPProcesosJobs_insert_TPProcesosJobs", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdJobs", CtrliIdJobs));
                cmd.Parameters.Add(new SqlParameter("@CtrlsEstatusJobs", CtrlsEstatusJobs));
                cmd.Parameters.Add(new SqlParameter("@CtrilsNombreJobs", CtrilsNombreJobs));
                cmd.Parameters.Add(new SqlParameter("@CtrlsParametrosJobs", CtrlsParametrosJobs));

                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }
        public List<TPProcesos> sp_TPProcesosJobs_Retrieve_TPProcesosJobs(int Crtliop1, int Crtliop2, int Crtliop3, int CrtliIdJobs, int CtrliIdTarea)
        {
            List<TPProcesos> list = new List<TPProcesos>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TPProcesosJobs_Retrieve_TPProcesosJobs", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@Crtliop1", Crtliop1));
                cmd.Parameters.Add(new SqlParameter("@Crtliop2", Crtliop2));
                cmd.Parameters.Add(new SqlParameter("@Crtliop3", Crtliop3));
                cmd.Parameters.Add(new SqlParameter("@CrtliIdJobs", CrtliIdJobs));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdTarea", CtrliIdTarea));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TPProcesos ls = new TPProcesos();
                        {
                            ls.iIdTarea = int.Parse(data["IdTarea"].ToString());
                            ls.iIdJobs = int.Parse(data["IdJobs"].ToString());
                            ls.sEstatusJobs = data["EstatusJobs"].ToString();
                            ls.sNombre = data["NombreJobs"].ToString();
                            ls.sParametros = data["ParametrosJobs"].ToString();
                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }
        public List<HangfireJobs> sp_IdJobsHangfireJobs_Retrieve_IdJobsHangfireJobs(string CtrlsFecha)
        {
            List<HangfireJobs> list = new List<HangfireJobs>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_IdJobsHangfireJobs_Retrieve_IdJobsHangfireJobs", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrlsFecha", CtrlsFecha));

                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        HangfireJobs ls = new HangfireJobs();
                        {
                            ls.iId = int.Parse(data["Id"].ToString());

                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;

        }

        public List<TpCalculosLn> sp_IdEmpresasTPCalculoshd_Retrieve_IdEmpresasTPCalculoshd(int CtrliIdCalculoshd, int CrtliIdTipoPeriodo,int CrtliPeriodo)
        {
            List<TpCalculosLn> list = new List<TpCalculosLn>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_IdEmpresasTPCalculoshd_Retrieve_IdEmpresasTPCalculoshd", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdCalculoshd", CtrliIdCalculoshd));
                cmd.Parameters.Add(new SqlParameter("@CrtliIdTipoPeriodo", CrtliIdTipoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CrtliPeriodo", CrtliPeriodo));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TpCalculosLn ls = new TpCalculosLn();
                        {
                            ls.iIdCalculosLn = int.Parse(data["IdCalculos_Ln"].ToString());
                            ls.iIdCalculosHd = int.Parse(data["Calculos_Hd_id"].ToString());
                            ls.iIdEmpresa = int.Parse(data["Empresa_id"].ToString());
                            ls.iIdEmpleado = int.Parse(data["Empleado_id"].ToString());
                            ls.iAnio = int.Parse(data["Anio"].ToString());
                            ls.iIdTipoPeriodo = int.Parse(data["Tipo_Periodo_id"].ToString());
                            ls.iPeriodo = int.Parse(data["Periodo"].ToString());
                            ls.iConsecutivo = int.Parse(data["Consecutivo"].ToString());
                            ls.iIdRenglon = int.Parse(data["Renglon_id"].ToString());
                            ls.iImporte = data["Importe"].ToString();
                            ls.iSaldo = data["Saldo"].ToString();
                            ls.iGravado = data["Gravado"].ToString();
                            ls.iExcento = data["Excento"].ToString();
                            ls.sFecha = data["Fecha"].ToString();
                            ls.iInactivo = data["Inactivo"].ToString();
                            ls.iTipoEmpleado = int.Parse(data["Cg_TipoEmpleado_id"].ToString());
                            ls.iIdDepartamento = int.Parse(data["Departamento_id"].ToString());
                            ls.EsEspejo = data["es_espejo"].ToString();
                            ls.sMensaje = "success";
                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    TpCalculosLn ls = new TpCalculosLn();
                    ls.sMensaje = "No hay datos";
                    list.Add(ls);

                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        public List<TpCalculosCarBean> sp_Caratula_Retrieve_TPlantilla_Calculos(int CtrliIdCalculoshd, int CrtliIdTipoPeriodo, int CrtliPeriodo, int Idempresa,int CtrliAnio)
        {
            List<TpCalculosCarBean> list = new List<TpCalculosCarBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Caratula_Retrieve_TPlantilla_Calculos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdDefinicion", CtrliIdCalculoshd));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipodePerido", CrtliIdTipoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodo", CrtliPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", Idempresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliAnio", CtrliAnio));
                
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TpCalculosCarBean ls = new TpCalculosCarBean();
                        {
                            ls.sValor = data["Valor"].ToString();
                            ls.iIdRenglon = int.Parse(data["Renglon_id"].ToString());
                            ls.sNombreRenglon = data["Nombre_Renglon"].ToString();
                            if (data["total"].ToString() == "") { ls.dTotal = 0; }
                            if (data["total"].ToString() != "") { ls.dTotal = decimal.Parse(data["total"].ToString()); }
                            ls.sMensaje = "success";
                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    TpCalculosCarBean ls = new TpCalculosCarBean();
                    ls.sMensaje = "No hay datos";
                    list.Add(ls);

                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        public List<EmpresasBean> sp_Empresa_Retrieve_TpCalculosLN(int CtrliIdCalculoshd, int CrtliIdTipoPeriodo, int CrtliPeriodo)
        {
            List<EmpresasBean> list = new List<EmpresasBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Empresa_Retrieve_TpCalculosLN", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdDefinicion", CtrliIdCalculoshd));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipodePerido", CrtliIdTipoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodo", CrtliPeriodo));

                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EmpresasBean ls = new EmpresasBean();
                        {
                            ls.iIdEmpresa = int.Parse(data["Empresa_id"].ToString());
                            ls.sNombreEmpresa = data["NombreEmpresa"].ToString();

                            ls.sMensaje = "success";
                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    EmpresasBean ls = new EmpresasBean();
                    ls.sMensaje = "No hay datos";
                    list.Add(ls);

                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear(); 
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;

        }
        public List<TPProcesos> sp_EstatusJobsTbProcesos_retrieve_EstatusJobsTbProcesos()
        {
            List<TPProcesos> list = new List<TPProcesos>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_EstatusJobsTbProcesos_retrieve_EstatusJobsTbProcesos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TPProcesos ls = new TPProcesos();
                        {
                            ls.iIdTarea = int.Parse(data["TotalJbos"].ToString());
                            ls.sEstatusJobs = data["EstatusJobs"].ToString();
                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        public TPProcesos sp_EstatusTpProcesosJobs_Update_EstatusTpProcesosJobs()
        {
            TPProcesos bean = new TPProcesos();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_EstatusTpProcesosJobs_Update_EstatusTpProcesosJobs", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.Add(new SqlParameter("@CtrliIdDefinicionHd", CtrliIdDefinicionHd));


                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }

        public List<TipoEmpleadoBean> sp_Cgeneral_Retrieve_TipoEmpleadosBajas()
        {
            List<TipoEmpleadoBean> list = new List<TipoEmpleadoBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Cgeneral_Retrieve_TipoEmpleadosBajas", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TipoEmpleadoBean ls = new TipoEmpleadoBean();
                        {
                            ls.IdTipo_Empleado = int.Parse(data["id"].ToString());
                            ls.Descripcion = data["Valor"].ToString();
                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }
        public List<MotivoBajaBean> sp_Cgeneral_Retrieve_MotivoBajas()
        {
            List<MotivoBajaBean> list = new List<MotivoBajaBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Cgeneral_Retrieve_MotivoBajas", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        MotivoBajaBean ls = new MotivoBajaBean();

                        ls.IdMotivo_Baja = int.Parse(data["IdValor"].ToString());
                        ls.Descripcion = data["Valor"].ToString();

                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }
        public List<MotivoBajaBean> sp_Cgeneral_Retrieve_MotivoBajasxTe()
        {
            List<MotivoBajaBean> list = new List<MotivoBajaBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Cgeneral_Retrieve_MotivoBajas", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        MotivoBajaBean ls = new MotivoBajaBean();


                        switch (int.Parse(data["IdValor"].ToString()))
                        {
                            case 0:
                            case 3:
                            case 10:
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                            case 20:
                            case 21:
                            case 22:
                                ls.IdMotivo_Baja = int.Parse(data["id"].ToString());
                                ls.TipoEmpleado_id = 164;
                                ls.Descripcion = data["Valor"].ToString();
                                break;
                            case 4:
                            case 5:
                                ls.IdMotivo_Baja = int.Parse(data["id"].ToString());
                                ls.TipoEmpleado_id = 165;
                                ls.Descripcion = data["Valor"].ToString();
                                break;
                            case 18:
                                ls.IdMotivo_Baja = int.Parse(data["id"].ToString());
                                ls.TipoEmpleado_id = 168;
                                ls.Descripcion = data["Valor"].ToString();
                                break;
                            case 19:
                                ls.IdMotivo_Baja = int.Parse(data["id"].ToString());
                                ls.TipoEmpleado_id = 27;
                                ls.Descripcion = data["Valor"].ToString();
                                break;
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                                ls.IdMotivo_Baja = int.Parse(data["id"].ToString());
                                ls.TipoEmpleado_id = 172;
                                ls.Descripcion = data["Valor"].ToString();
                                break;
                            case 17:
                            case 23:
                                ls.IdMotivo_Baja = int.Parse(data["id"].ToString());
                                ls.TipoEmpleado_id = 30;
                                ls.Descripcion = data["Valor"].ToString();
                                break;
                            case 1:
                            case 2:
                                ls.IdMotivo_Baja = int.Parse(data["id"].ToString());
                                ls.TipoEmpleado_id = 31;
                                ls.Descripcion = data["Valor"].ToString();
                                break;
                        }


                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }
        public List<string> sp_TEmpleado_Nomina_Retrieve_DatosBaja(int Empresa_id, int Empleado_id)
        {
            List<string> list = new List<string>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TEmpleado_Nomina_Retrieve_DatosBaja", this.conexion)
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
                        list.Add(data["Empleado_id"].ToString());
                        list.Add(data["Nombre"].ToString());
                        list.Add(string.Format(CultureInfo.InvariantCulture, "{0:#,###,##0.00}", Convert.ToDecimal((data["Salario_Mensual"]))));
                        list.Add(data["Fecha_Aumento"].ToString());
                        list.Add(data["Fecha_Antiguedad"].ToString());
                        list.Add(data["Fecha_Ingreso"].ToString());
                        list.Add(data["Nivel_Empleado"].ToString());
                        list.Add(data["Posicion"].ToString());
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }
        public TPProcesos sp_CNomina_1(int p_Ano, int p_Tipo_periodo, int p_Periodo, int p_IdDefinicion_Hd,int p_Empresa_id,int Por_lista_empleado)
        {
            TPProcesos bean = new TPProcesos();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CNomina_1", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                   
            };

                cmd.Parameters.Add(new SqlParameter("@p_Ano", p_Ano));
                cmd.Parameters.Add(new SqlParameter("@p_Tipo_periodo", p_Tipo_periodo));
                cmd.Parameters.Add(new SqlParameter("@p_Periodo", p_Periodo));
                cmd.Parameters.Add(new SqlParameter("@p_IdDefinicion_Hd", p_IdDefinicion_Hd));
                cmd.Parameters.Add(new SqlParameter("@p_Empresa_id", p_Empresa_id));
                cmd.Parameters.Add(new SqlParameter("@Por_lista_empleado", Por_lista_empleado));

                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }

        public List<ReciboNominaBean> sp_TpCalculoEmpleado_Retrieve_TpCalculoEmpleado(int CtrliIdEmpresa, int CtrliIdemplado, int CtrliPeriodo, int CtrliTipodeperiodo, int Ctrlianio,int ctriliEspejo)
        {
            List<ReciboNominaBean> list = new List<ReciboNominaBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TpCalculoEmpleado_Retrieve_TpCalculoEmpleado", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdemplado", CtrliIdemplado));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipodeperiodo", CtrliTipodeperiodo));
                cmd.Parameters.Add(new SqlParameter("@Ctrlianio", Ctrlianio));
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodo", CtrliPeriodo));
                cmd.Parameters.Add(new SqlParameter("@ctriliEspejo", ctriliEspejo));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {


                    while (data.Read())
                    {
                        ReciboNominaBean ls = new ReciboNominaBean();
                        {

                            ls.iIdCalculoshd = int.Parse(data["Calculos_Hd_id"].ToString());
                            ls.sNombre_Renglon = data["Nombre_Renglon"].ToString();
                            if (data["Saldo"].ToString() == "") { ls.dSaldo = 0; }
                            if (data["Saldo"].ToString() != "") { ls.dSaldo = decimal.Parse(data["Saldo"].ToString()); }
                            if (data["Excento"].ToString() == "") { ls.dExcento = 0; }
                            if (data["Excento"].ToString() != "") { ls.dExcento = decimal.Parse(data["Excento"].ToString()); }
                            if (data["Gravado"].ToString() == "") { ls.dGravado = 0; }
                            if (data["Gravado"].ToString() != "") { ls.dGravado = decimal.Parse(data["Gravado"].ToString()); }

                            ls.iConsecutivo = int.Parse(data["Consecutivo"].ToString());
                            //ls.iElementoNomina = int.Parse(data["Cg_Elemento_Nomina_id"].ToString());
                            ls.iIdRenglon = int.Parse(data["Renglon_id"].ToString());
                            ls.sValor = data["Valor"].ToString();
                            ls.sIdSat = int.Parse(data["Sat_id"].ToString());
                        }

                        list.Add(ls);
                    }

                }
                else
                {
                    list = null;
                }
                data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        public List<CTipoPeriodoBean> sp_TipoPeridoTpDefinicionNomina_Retrieve_TpDefinicionNomina(int CrtliIdDefinicionHd)
        {
            List<CTipoPeriodoBean> list = new List<CTipoPeriodoBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TipoPeridoTpDefinicionNomina_Retrieve_TpDefinicionNomina", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CrtliIdDefinicion", CrtliIdDefinicionHd));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CTipoPeriodoBean ls = new CTipoPeriodoBean();
                        {
                            ls.iId = int.Parse(data["Tipo_Periodo_id"].ToString());
                            ls.sValor = data["Valor"].ToString();
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


        public List<CInicioFechasPeriodoBean> sp_PeridosEmpresa_Retrieve_CinicioFechasPeriodo(int CrtliIdDeficionHd,int CrtliPeriodo, int CtrliNomCerr, int CrtliAnio)
        {
            List<CInicioFechasPeriodoBean> list = new List<CInicioFechasPeriodoBean>();
            try
            {
                int CrtliIdEmpresa = 0;
                int CrtliTipoPeriodo = 0;
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_PeridosEmpresa_Retrieve_CinicioFechasPeriodo", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CrtliIdDeficionHd", CrtliIdDeficionHd));
                cmd.Parameters.Add(new SqlParameter("@CrtliIdEmpresa", CrtliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CrtliTipoPeriodo", CrtliTipoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CrtliPeriodo", CrtliPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliNomCerr", CtrliNomCerr));
                cmd.Parameters.Add(new SqlParameter("@CrtliAnio", CrtliAnio));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CInicioFechasPeriodoBean LP = new CInicioFechasPeriodoBean();
                        {
                            LP.iId = int.Parse(data["Id"].ToString());
                            LP.iPeriodo = int.Parse(data["Periodo"].ToString());
                            LP.sFechaInicio = data["Fecha_Inicio"].ToString();
                            LP.sFechaFinal = data["Fecha_Final"].ToString();
                            LP.sNominaCerrada = data["Nomina_Cerrada"].ToString();
                            LP.sMensaje = "success";
                        };

                        list.Add(LP);
                    }
                }
                else
                {
                    CInicioFechasPeriodoBean LP = new CInicioFechasPeriodoBean();
                    {
                        LP.sMensaje = "error";
                    }
                    list.Add(LP);

                }

                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;


        }

        public CInicioFechasPeriodoBean sp_NomCerradaCInicioFechaPeriodo_Update_CInicioFechasPeriodo(int CrtliIdDeficionHd, int CtrliPeriodo, int CtrliNominaCerrada,int CtrliAnio, int CtrlIdTipoPeriodo, int CtrliIdempresa)
        {
            CInicioFechasPeriodoBean bean = new CInicioFechasPeriodoBean();
            try
            {
                
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_NomCerradaCInicioFechaPeriodo_Update_CInicioFechasPeriodo", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CrtliIdDeficionHd", CrtliIdDeficionHd));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdempresa", CtrliIdempresa));
                cmd.Parameters.Add(new SqlParameter("@CtrlIdTipoPeriodo", CtrlIdTipoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodo", CtrliPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliNominaCerrada", CtrliNominaCerrada));
                cmd.Parameters.Add(new SqlParameter("@CtrliAnio", CtrliAnio));
                
                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }

        public List<NominaLnBean> sp_ExitReglon_Retrieve_TpDefinicionNominaLn(int CtrliIdEmpresa, int CtrliIdrenglon, int CtrliIdDefinicion, int CtrliElemnom)
        {
            List<NominaLnBean> list = new List<NominaLnBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_ExitReglon_Retrieve_TpDefinicionNominaLn", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdrenglon", CtrliIdrenglon));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdDefinicion", CtrliIdDefinicion));
                cmd.Parameters.Add(new SqlParameter("@CtrliElemnom", CtrliElemnom));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        NominaLnBean ls = new NominaLnBean();

						{
							ls.iIdDefinicionHd = int.Parse(data["IdDefinicion_Ln"].ToString());

						};
						list.Add(ls);
					}
				}
				else
				{
                    NominaLnBean ls = new NominaLnBean();
                    {
                        ls.iIdDefinicionHd = 0;

                    };
                    list.Add(ls);
                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;

        }

        public List<int> sp_ExitPercepODeduc_Retrieve_TPlantilla_Definicion_Nomina_Ln(int ctrliIdDefinicionHd)
        {
            List<int> list = new List<int>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_ExitPercepODeduc_Retrieve_TPlantilla_Definicion_Nomina_Ln", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrliIdDefinicionHd", ctrliIdDefinicionHd));
                
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {

                        list.Add(int.Parse(data["Existe"].ToString()));

                        
                    }
                }
                else
                {
                   
                    list.Add(0);

                   
                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;

        }

        public List<NominahdBean> sp_DefinicionConNomCe_Retrieve_TpDefinicionNominaHd()
        {

            List<NominahdBean> list = new List<NominahdBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_DefinicionConNomCe_Retrieve_TpDefinicionNominaHd", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        NominahdBean TDN = new NominahdBean();
                        {
                            TDN.iIdDefinicionhd = int.Parse(data["IdDefinicion_Hd"].ToString());
                            TDN.sNombreDefinicion = data["Nombre_Definicion"].ToString();
                            TDN.sDescripcion = data["Descripcion"].ToString();
                            TDN.iAno = int.Parse(data["Anio"].ToString());
                            TDN.iCancelado = data["Cancelado"].ToString();
                        };


                        list.Add(TDN);
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

        public SelloSatBean sp_Tsellos_InsertUPdate_TSellosSat(int Ctrliop, int CrtiIdCalculos, int CtrliIdEmpresa, int CtrliIdEmpleado, int CtrliAnio, int CtrliTipoPerdio, int CtrliPeriodo, string CtrlsRecibo, string CtrlsSello, string CtrlsUUID, string CtrlsSelloCFD, string CtrlsRfcProvCertif, string CtrlsNoCertificadoSAT, string CtrlsFechatim)
        {
            SelloSatBean bean = new SelloSatBean();

            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Tsellos_InsertUPdate_TSellosSat", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@Ctrliop", Ctrliop));
                cmd.Parameters.Add(new SqlParameter("@CrtiIdCalculos", CrtiIdCalculos));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpleado", CtrliIdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@CtrliAnio", CtrliAnio));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipoPerdio", CtrliTipoPerdio));
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodo", CtrliPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrlsRecibo", CtrlsRecibo));
                cmd.Parameters.Add(new SqlParameter("@CtrlsSello", CtrlsSello));
                cmd.Parameters.Add(new SqlParameter("@CtrlsUUID", CtrlsUUID));
                cmd.Parameters.Add(new SqlParameter("@CtrlsSelloCFD", CtrlsSelloCFD));
                cmd.Parameters.Add(new SqlParameter("@CtrlsRfcProvCertif", CtrlsRfcProvCertif));
                cmd.Parameters.Add(new SqlParameter("@CtrlsNoCertificadoSAT", CtrlsNoCertificadoSAT));
                cmd.Parameters.Add(new SqlParameter("@CtrlsFechatim", CtrlsFechatim));

                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }

        public List<TPProcesos> sp_StatusProceso_Retrieve_TPProceso(string CtrlsParametro)
        {
            List<TPProcesos> list = new List<TPProcesos>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_StatusProceso_Retrieve_TPProceso", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrlsParametro", CtrlsParametro));
            
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TPProcesos ls = new TPProcesos();
                        {
                            ls.sEstatusJobs = data["EstatusJobs"].ToString();
                            ls.sMensaje = "success";
                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    TPProcesos ls = new TPProcesos();
                    ls.sMensaje = "No hay datos";
                    list.Add(ls);

                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        public List<int> sp_EmpresaDef_Retrieve_TPDefinicionNomina(int CltrliIdDefinicionHd)
        {
            List<int> list = new List<int>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_EmpresaDef_Retrieve_TPDefinicionNomina", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CltrliIdDefinicionHd", CltrliIdDefinicionHd));

                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        list.Add(int.Parse(data["EstatusJobs"].ToString()));
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

        /// List Empleados
        public List<EmpleadosEmpresaBean> sp_EmpleadosDeEmpresa_Retreive_Templeados(int CtrliIdEmpresa)
        {
            List<EmpleadosEmpresaBean> list = new List<EmpleadosEmpresaBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_EmpleadosDeEmpresa_Retreive_Templeados", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EmpleadosEmpresaBean ls = new EmpleadosEmpresaBean();
                        ls.iIdEmpleado = int.Parse(data["IdEmpleado"].ToString());
                        ls.sNombreCompleto = data["NombreCompleto"].ToString();
                        ls.sMensaje = "success";
                        list.Add(ls);
                    }
                }
                else
                {
                    EmpleadosEmpresaBean ls = new EmpleadosEmpresaBean();
                    ls.sMensaje = "Error";
                    list.Add(ls);
                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }


        /// Insert/update LisEmpleado
        public ListEmpleadoNomBean sp_LisEmpleados_InsertUpdate_TlistaEmpladosNomina(int CtrloiIdEmpresa, int CtrliIdEmpleado,
        int CtrliAnio, int CtrlTipoPeriodo, int CtrliPeriodo, int CltrliExite)
        {
            ListEmpleadoNomBean bean = new ListEmpleadoNomBean();

            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_LisEmpleados_InsertUpdate_TlistaEmpladosNomina", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrloiIdEmpresa", CtrloiIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpleado", CtrliIdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@CtrliAnio", CtrliAnio));
                cmd.Parameters.Add(new SqlParameter("@CtrlTipoPeriodo", CtrlTipoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodo", CtrliPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CltrliExite", CltrliExite));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }

        ///Numero Empleado de Empresas
        public List<EmpresasBean> sp_NoEmpleadosEmpresa_Retrieve_TempleadoNomina(int CtrliIdEmpresa, int Ctrliop)
        {
            List<EmpresasBean> list = new List<EmpresasBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_NoEmpleadosEmpresa_Retrieve_TempleadoNomina", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@Ctrliop", Ctrliop));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                       
                            EmpresasBean ls = new EmpresasBean
                            {
                                iNoEmpleados = int.Parse(data["NoEmple"].ToString()),

                            };
                            list.Add(ls);
                        
                       
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        // Tipo Peridod y Peridodo de empresa 
        public List<CInicioFechasPeriodoBean> sp_TipoPPEmision_Retrieve_CInicioPeriodo(int CtrliEmpresa, int CtrliOpcion)
        { 
            List<CInicioFechasPeriodoBean> list = new List<CInicioFechasPeriodoBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TipoPPEmision_Retrieve_CInicioPeriodo", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliEmpresa", CtrliEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliOpcion", CtrliOpcion));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CInicioFechasPeriodoBean ls = new CInicioFechasPeriodoBean();
                        {
                            if (CtrliOpcion == 0)
                            {
                                ls.iIdEmpresesas = int.Parse(data["IdEmpresa"].ToString());
                                ls.iTipoPeriodo = int.Parse(data["Tipo_Periodo_id"].ToString());
                            }
                            if (CtrliOpcion == 1)
                            {
                                ls.iIdEmpresesas = int.Parse(data["Empresa_id"].ToString());
                                ls.iTipoPeriodo = int.Parse(data["Tipo_Periodo_id"].ToString());
                                ls.iPeriodo = int.Parse(data["Periodo"].ToString());
                                ls.sFechaInicio = data["Fecha_Inicio"].ToString();
                                ls.sFechaFinal = data["Fecha_final"].ToString();

                            }
                        
           
                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        ///empleados de Empresas
        public List<EmpleadosBean> sp_EmpleadosEmpresa_Retrieve_TempleadoNomina(int CtrliIdEmpresa, int Ctrliop)
        {
            List<EmpleadosBean> list = new List<EmpleadosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_NoEmpleadosEmpresa_Retrieve_TempleadoNomina", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@Ctrliop", Ctrliop));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EmpleadosBean ls = new EmpleadosBean();
                        ls.iIdEmpleado = int.Parse(data["Empleado_id"].ToString());
                        ls.iNumeroNomina = int.Parse(data["IdNomina"].ToString());
                        ls.sNombreEmpleado = data["Nombre_Empleado"].ToString();
                        list.Add(ls);
                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        ///Consulta calculos de empresa 

        public List<TpCalculosHd> sp_ExitCalculo_Retreve_TPlantillaCalculos(int CtrliIdempresa, int CtrliAnio,int CtrliTipoperiodo,int CtrliPeriodo)
        {
            List<TpCalculosHd> list = new List<TpCalculosHd>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_ExitCalculo_Retreve_TPlantillaCalculos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdempresa", CtrliIdempresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliAnio", CtrliAnio));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipoperiodo", CtrliTipoperiodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodo", CtrliPeriodo));
             
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TpCalculosHd ls = new TpCalculosHd();
                        {
                            ls.iIdDefinicionHd = int.Parse(data["Definicion_Hd_id"].ToString());
                            ls.sMensaje = "success";
                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    TpCalculosHd ls = new TpCalculosHd();
                    {
                        ls.sMensaje = "error";

                    };
                    list.Add(ls);
                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;

        }


        // Consulta los renglones del finiquito del empleado 
        public List<ReciboNominaBean> sp_TpCalculoFiniEmpleado_Retrieve_TFiniquito(int CtrliIdempresa, int CtrliIdempleado, int CtrliPeriodo, int CtrliAnio,int CtrliTipoFiniquito)
        {
            List<ReciboNominaBean> list = new List<ReciboNominaBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TpCalculoFiniEmpleado_Retrieve_TFiniquito", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdempresa", CtrliIdempresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdempleado", CtrliIdempleado));
                cmd.Parameters.Add(new SqlParameter("@CtrliAnio", CtrliAnio));
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodo", CtrliPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipoFiniquito", CtrliTipoFiniquito));


                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {


                    while (data.Read())
                    {
                        ReciboNominaBean ls = new ReciboNominaBean();
                        {

                            //ls.iIdCalculoshd = int.Parse(data["Calculos_Hd_id"].ToString());
                            ls.iIdFiniquito = int.Parse(data["Idfiniquito"].ToString());
                            ls.iIdEmpleado = int.Parse(data["Empleado_id"].ToString());
                            ls.sNombre_Renglon = data["Nombre_Renglon"].ToString();
                            ls.dSaldo = decimal.Parse(data["Saldo"].ToString());
                            ls.dGravado = decimal.Parse(data["Gravado"].ToString());
                            ls.dExcento = decimal.Parse(data["Excento"].ToString());
                            //ls.iConsecutivo = int.Parse(data["Consecutivo"].ToString());
                            //ls.iElementoNomina = int.Parse(data["Cg_Elemento_Nomina_id"].ToString());
                            ls.iIdRenglon = int.Parse(data["Renglon_id"].ToString());
                            ls.sValor = data["Valor"].ToString();
                        }

                        list.Add(ls);
                    }

                }
                else
                {
                    list = null;
                }
                data.Close(); conexion.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        ///Numero de empleados de periodos de Empresas
        public List<EmpresasBean> sp_NumeroEmple_Retrieve_TpCalculosLn(int CtrliIdempresa, int CtrliIdTipoPeriodo,int ctrliIdPerido,int CtrliAnio, int CtrliOp)
        {
            List<EmpresasBean> list = new List<EmpresasBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_NumeroEmple_Retrieve_TpCalculosLn", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdempresa", CtrliIdempresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdTipoPeriodo", CtrliIdTipoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@ctrliIdPerido", ctrliIdPerido));
                cmd.Parameters.Add(new SqlParameter("@CtrliAnio", CtrliAnio));
                cmd.Parameters.Add(new SqlParameter("@CtrliOp", CtrliOp));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {

                        EmpresasBean ls = new EmpresasBean
                        {
                            iNoEmpleados = int.Parse(data["Noemple"].ToString()),

                        };
                        list.Add(ls);


                    }
                }
                else
                {
                    list = null;
                }
                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }


        ///empleados de Empresas
        public List<EmpleadosBean> sp_EmpleadosEmpresa_periodo(int CtrliIdempresa, int CtrliIdTipoPeriodo, int ctrliIdPerido,int CtrliAnio, int CtrliOp)
        {
            List<EmpleadosBean> list = new List<EmpleadosBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_NumeroEmple_Retrieve_TpCalculosLn", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdempresa", CtrliIdempresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdTipoPeriodo", CtrliIdTipoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@ctrliIdPerido", ctrliIdPerido));
                cmd.Parameters.Add(new SqlParameter("@CtrliAnio", CtrliAnio));
                cmd.Parameters.Add(new SqlParameter("@CtrliOp",CtrliOp));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EmpleadosBean ls = new EmpleadosBean();
                        ls.iIdEmpleado = int.Parse(data["Empleado_id"].ToString());
                        ls.iNumeroNomina = int.Parse(data["IdNomina"].ToString());
                        ls.sNombreEmpleado = data["Nombrecompleto"].ToString();
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

        /// listPeriodo

        public List<CInicioFechasPeriodoBean> sp_CIncioPeriodo_Retrieve_Periodo(int CtrliIdEmpresa, int CtrliAnio, int CtrliTipoPeriodo)
        {
            List<CInicioFechasPeriodoBean> list = new List<CInicioFechasPeriodoBean>();
            try
            {
                
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CIncioPeriodo_Retrieve_Periodo", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliAnio", CtrliAnio));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipoPeriodo", CtrliTipoPeriodo));
              
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CInicioFechasPeriodoBean LP = new CInicioFechasPeriodoBean();
                        {
                          
                            LP.iPeriodo = int.Parse(data["Periodo"].ToString());
                            LP.sMensaje = "success";
                        };

                        list.Add(LP);
                    }
                }
                else
                {
                    CInicioFechasPeriodoBean LP = new CInicioFechasPeriodoBean();
                    {
                        LP.sMensaje = "error";
                    }
                    list.Add(LP);

                }

                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;


        }
       
        // Consulta el proceso de nomina
        public List<TPProcesos> sp_CalculosHdFinProces_Retrieve_TPlantillaCalculosHd(int ctrliFolio, int CtrliDefinicionHd)
        {
            List<TPProcesos> list = new List<TPProcesos>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CalculosHdFinProces_Retrieve_TPlantillaCalculosHd", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@ctrliFolio", ctrliFolio));
                cmd.Parameters.Add(new SqlParameter("@CtrliDefinicionHd", CtrliDefinicionHd));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TPProcesos ls = new TPProcesos();
                        {
                            ls.sEstatusJobs = data["EstatusJobs"].ToString();
                            ls.sMensaje = "success";
                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    TPProcesos ls = new TPProcesos();
                    ls.sMensaje = "success";
                    ls.sEstatusJobs = "Procesando";
                    list.Add(ls);

                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }
        // consulta la diferencia de dos nominas

        public List<CompativoNomBean>sp_CompativoNomina_Retrieve_TPCalculosln(int CrtliIdEmpresa, int CrtliAnio, int CrtliTipoPeriodo, int CtrliPeriodo, int CtrliPeriodoAnte , int CtrliIdEmpleado,int CtrliEspejo)
        {

            List<CompativoNomBean> list = new List<CompativoNomBean>();
            try
            {

                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CompativoNomina_Retrieve_TPCalculosln", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@CrtliIdEmpresa", CrtliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CrtliAnio", CrtliAnio));
                cmd.Parameters.Add(new SqlParameter("@CrtliTipoPeriodo", CrtliTipoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodo", CtrliPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodoAnte", CtrliPeriodoAnte));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpleado", CtrliIdEmpleado));
                cmd.Parameters.Add(new SqlParameter("@CtrliEspejo", CtrliEspejo));

                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CompativoNomBean LP = new CompativoNomBean();
                        {

                            LP.iIdEmpresa = CrtliIdEmpresa;
                            LP.TipodNom = data["Tipo"].ToString();
                            LP.iIdRenglon = int.Parse(data["Renglon_id"].ToString());
                            LP.sNombreRenglon = data["Nombre_Renglon"].ToString();
                            LP.sTotal = data["Total"].ToString();
                            LP.sTotal2 = data["Total2"].ToString();
                            LP.sTotalDif = data["TotalDif"].ToString();
                            LP.sMensaje = "success";
                        };

                        list.Add(LP);
                    }
                }
                else
                {
                    CompativoNomBean LP = new CompativoNomBean();
                    {
                        LP.sMensaje = "error";
                    }
                    list.Add(LP);

                }

                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;


        }

        /// consulta los datos de definicion para generar folio en cualculos ln
        public List<CInicioFechasPeriodoBean> sp_DatFolioDefNomina_Retreieve(int CrtliIdDefinicion)
        {
            List<CInicioFechasPeriodoBean> list = new List<CInicioFechasPeriodoBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_DatFolioDefNomina_Retreieve", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CrtliIdDefinicion", CrtliIdDefinicion));
        
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CInicioFechasPeriodoBean ls = new CInicioFechasPeriodoBean();
                        {
                            
                                ls.iIdEmpresesas = int.Parse(data["Empresa_id"].ToString());
                                ls.ianio = int.Parse(data["Anio"].ToString());
                                ls.iTipoPeriodo = int.Parse(data["Tipo_Periodo_id"].ToString());
                                ls.iPeriodo = int.Parse(data["Periodo"].ToString());
                            
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

        // consulta la diferencias de saldos de las empresasd

        public List<CompativoNomBean> sp_ComparativoNominaXEmpresa_Retrieve_TpCalculosLN(int CrtliIdEmpresa, int CrtliAnio, int CrtliTipoPeriodo, int CtrliPerido, int CtrliPeriodoAnte)
        {

            List<CompativoNomBean> list = new List<CompativoNomBean>();
            try
            {

                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_ComparativoNominaXEmpresa_Retrieve_TpCalculosLN", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@CrtliIdEmpresa", CrtliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CrtliAnio", CrtliAnio));
                cmd.Parameters.Add(new SqlParameter("@CrtliTipoPeriodo", CrtliTipoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliPerido", CtrliPerido));
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodoAnte", CtrliPeriodoAnte));

                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CompativoNomBean LP = new CompativoNomBean();
                        {

                            LP.iIdEmpresa = CrtliIdEmpresa;
                            LP.sNombreRenglon = int.Parse(data["id"].ToString()) + " " + data["Descripcion"].ToString();
                            LP.sTotal = data["TotalPeriodoActual"].ToString();
                            LP.sTotal2 = data["TotalPeridoAnter"].ToString();
                            LP.sTotalDif = data["TotalDif"].ToString();
                            LP.iNoEmpleado = int.Parse(data["NoempleActual"].ToString());
                            LP.iNoEmpleadosNuevos = int.Parse(data["NoEmpleados"].ToString());
                            LP.sMensaje = "success";
                        };

                        list.Add(LP);
                    }
                }
                else
                {
                    CompativoNomBean LP = new CompativoNomBean();
                    {
                        LP.sMensaje = "error";
                    }
                    list.Add(LP);

                }

                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;


        }


        // consulta la diferencias de saldos de por empleado

        public List<CompativoNomBean> sp_ComparativoNomXEmpleado_Retrieve_TpCalculosLN(int CrtliIdEmpresa, int CrtliAnio, int CrtliTipoPeriodo, int CtrliPerido, int CtrliPeriodoAnte, int CtrliTipoPAgo, int CtrliEsEspejo)
        {

            List<CompativoNomBean> list = new List<CompativoNomBean>();
            try
            {

                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_ComparativoNomXEmpleado_Retrieve_TpCalculosLN", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                
                cmd.Parameters.Add(new SqlParameter("@CrtliIdEmpresa", CrtliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CrtliAnio", CrtliAnio));
                cmd.Parameters.Add(new SqlParameter("@CrtliTipoPeriodo", CrtliTipoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodo", CtrliPerido));
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodoAnt", CtrliPeriodoAnte));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipoPAgo", CtrliTipoPAgo));
                cmd.Parameters.Add(new SqlParameter("@CtrliEsEspejo", CtrliEsEspejo));
               

                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CompativoNomBean LP = new CompativoNomBean();
                        {

                            LP.iIdEmpresa = CrtliIdEmpresa;
                            LP.iIdEmpleado = int.Parse(data["Empleado_id"].ToString());
                            LP.sNombreEmpleado = data["NombreCompleto"].ToString();
                            LP.sTotal = data["Saldo"].ToString();
                            LP.sTotal2 = data["SaldoAnterior"].ToString();
                            LP.sTotalDif = data["DiferenciaTotal"].ToString();
                            LP.sMensaje = "success";
                        };

                        list.Add(LP);
                    }
                }
                else
                {
                    CompativoNomBean LP = new CompativoNomBean();
                    {
                        LP.sMensaje = "error";
                    }
                    list.Add(LP);

                }

                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;


        }

        /// lista de periodo que tiene el empleado 
        public List<CInicioFechasPeriodoBean> sp_PeriodoEmpleado_Retrieve_TPCalculosLN (int CtrliIdEmpresa , int CtrliAnio, int CtrliTipoPeriodo ,int CtrliIdEmpleado)
        {

            List<CInicioFechasPeriodoBean> list = new List<CInicioFechasPeriodoBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_PeriodoEmpleado_Retrieve_TPCalculosLN", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliAnio", CtrliAnio));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipoPeriodo", CtrliTipoPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpleado", CtrliIdEmpleado));

                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CInicioFechasPeriodoBean ls = new CInicioFechasPeriodoBean();
                        {
                            ls.iPeriodo = int.Parse(data["Periodo"].ToString());

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


        /// compensaciones fijas
        public List<CompensacionFijaBean> sp_Compensacionfija_Retrieve_CCompensacionfija()
        {

            List<CompensacionFijaBean> list = new List<CompensacionFijaBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Compensacionfija_Retrieve_CCompensacionfija", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                //cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                //cmd.Parameters.Add(new SqlParameter("@CtrliAnio", CtrliAnio));
                //cmd.Parameters.Add(new SqlParameter("@CtrliTipoPeriodo", CtrliTipoPeriodo));
                //cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpleado", CtrliIdEmpleado));

                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CompensacionFijaBean ls = new CompensacionFijaBean();
                        {
                            ls.iId = int.Parse(data["Id"].ToString());
                            ls.iIdEmpresa = int.Parse(data["Empresa_id"].ToString());
                            ls.sNombreEmpresa = int.Parse(data["Empresa_id"].ToString()) + " " + data["NombreEmpresa"].ToString();
                            if (data["Premio_PyA"].ToString() == "True") { ls.iPremioPyA = 1; }; if (data["Premio_PyA"].ToString() == "False") { ls.iPremioPyA = 0; };
                            if (data["Puesto_id"].ToString() == null) { ls.iIdPuesto = 0; }; if (data["Puesto_id"].ToString() != null) { ls.iIdPuesto = int.Parse(data["Puesto_id"].ToString()); }
                            if (data["sPuesto"].ToString() == null) { ls.sPuesto = " "; }; if (data["sPuesto"].ToString() != null) { ls.sPuesto = data["sPuesto"].ToString(); };
                            ls.iIdRenglon = int.Parse(data["Renglon_id"].ToString());
                            if (data["Renglon_id"].ToString() == null) { ls.sNombreRenglon = " "; };if (data["Renglon_id"].ToString() != null) { ls.sNombreRenglon = int.Parse(data["Renglon_id"].ToString()) + " " + data["Nombre_Renglon"].ToString(); };
                            if (data["Renglon_id"].ToString() == null) { ls.iImporte = 0; }; if (data["Renglon_id"].ToString() != null) { ls.iImporte = double.Parse(data["Importe"].ToString()); };
                            ls.sDescripcion = data["Descripcion"].ToString();
                            ls.iIdUsuario = int.Parse(data["Usuario_id"].ToString());
                            ls.sFecha = data["Fecha"].ToString();
                            ls.sMensaje= "success";

                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    CompensacionFijaBean ls = new CompensacionFijaBean();
                    {
                        ls.sMensaje = "error";
                    }
                    list.Add(ls);
                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }


        /// LisPuestoXEmpresas
        public List<PuestosNomBean> sp_PuestosXEmpresa_Retrieve_Tpuestos( int CtrliIdEmpresa)
        {

            List<PuestosNomBean> list = new List<PuestosNomBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_PuestosXEmpresa_Retrieve_Tpuestos", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdEmpresa", CtrliIdEmpresa));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        PuestosNomBean ls = new PuestosNomBean();
                        {
                            ls.iIdPuesto = int.Parse(data["IdPuesto"].ToString());
                            ls.sPuestoCodigo = data["PuestoCodigo"].ToString();
                            ls.sNombrePuesto = data["PuestoCodigo"].ToString()+" "+ data["NombrePuesto"].ToString();
                            ls.sMensaje = "success";
                        };
                        list.Add(ls);
                    }
                }
                else
                {
                    PuestosNomBean ls = new PuestosNomBean();
                    {
                        ls.sMensaje = "error";
                    }
                    list.Add(ls);
                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }

        // Inserta una nueva Compensacion fija

        public List<CompensacionFijaBean> sp_Compensacion_Insert_CCompensacionFija(int CtrliIdempresa, int CtrliPyA, int CtrliIdPuesto,int CtrliIdRenglon, double CtrliImporte, string CtrlsDescrip, int CtrliIdUsuario)
        {
           List<CompensacionFijaBean> bean = new List<CompensacionFijaBean>();

            try
            {
                this.Conectar(); //sp_DefineNom_insert_DefineNom
                SqlCommand cmd = new SqlCommand("sp_Compensacion_Insert_CCompensacionFija", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliIdempresa", CtrliIdempresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliPyA", CtrliPyA));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdPuesto", CtrliIdPuesto));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdRenglon", CtrliIdRenglon));
                cmd.Parameters.Add(new SqlParameter("@CtrliImporte", CtrliImporte));
                cmd.Parameters.Add(new SqlParameter("@CtrlsDescrip", CtrlsDescrip));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdUsuario", CtrliIdUsuario));
                cmd.Parameters.Add(new SqlParameter("@CtrliExitNoAct", "0"));
                cmd.Parameters.Add(new SqlParameter("@CtrliExitAct", "0"));
                cmd.Parameters.Add(new SqlParameter("@CtrliNotExit", "0"));

                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        CompensacionFijaBean ls = new CompensacionFijaBean();
                        {
                            ls.iId = int.Parse(data["Id"].ToString());
                            ls.sMensaje = "success";
                        };
                        bean.Add(ls);
                    }
                }
                else
                {
                    CompensacionFijaBean ls = new CompensacionFijaBean();
                    {
                        ls.sMensaje = "error";
                    }
                    bean.Add(ls);
                }
                data.Close(); cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }

        public CompensacionFijaBean Sp_CCompensacion_update_CCompensacion(int CtrliId, int CtrliIdempresa, int CtrliPyA, int CtrliIdPuesto, int CtrliIdRenglon, double CtrliImporte, string CtrlsDescrip, int CtrliIdUsuario, int CtrIiCanceldo)
        {
            CompensacionFijaBean bean = new CompensacionFijaBean();

            try
            {
                this.Conectar(); //sp_DefineNom_insert_DefineNom
                SqlCommand cmd = new SqlCommand("Sp_CCompensacion_update_CCompensacion", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliId", CtrliId));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdempresa", CtrliIdempresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliPyA", CtrliPyA));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdPuesto", CtrliIdPuesto));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdRenglon", CtrliIdRenglon));
                cmd.Parameters.Add(new SqlParameter("@CtrliImporte", CtrliImporte));
                cmd.Parameters.Add(new SqlParameter("@CtrlsDescrip", CtrlsDescrip));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdUsuario", CtrliIdUsuario));
                cmd.Parameters.Add(new SqlParameter("@CtrIiCanceldo", CtrIiCanceldo));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        

            return bean;
        }

       // verifica si hay un proceso en ejecucion 
        public List<TPProcesos> sp_ProcesEje_Retrieve_TpProcesosJobs(int IdUsuario)
        {
            List<TPProcesos> list = new List<TPProcesos>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_ProcesEje_Retrieve_TpProcesosJobs", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CrltriOpc", "0"));
                cmd.Parameters.Add(new SqlParameter("@Crltr1Opc2", "0"));
                cmd.Parameters.Add(new SqlParameter("@CrltriIdUsuario", IdUsuario));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TPProcesos ls = new TPProcesos
                        {

                            sEstatusJobs = data["Estatus"].ToString()

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

        // Actulaiza el Usuario de la Plantillas de tabla CalculoHd

        public TpCalculosHd sp_Usuario_Update_TplantillaCalculosHd(int CtrliDefinicionId, int CtrliFolio, int  @CtrliUduarioId)
        {
            TpCalculosHd bean = new TpCalculosHd();

            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Usuario_Update_TplantillaCalculosHd", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliDefinicionId", CtrliDefinicionId));
                cmd.Parameters.Add(new SqlParameter("@CtrliFolio", CtrliFolio));
                cmd.Parameters.Add(new SqlParameter("@CtrliUduarioId", CtrliUduarioId));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }

        public List<TPProcesos> sp_ExistUsuProcesJobs_Retrieve_Tp_Usuario_ProcesJobs(int IdUsuario)
        {
            List<TPProcesos> list = new List<TPProcesos>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_ExistUsuProcesJobs_Retrieve_Tp_Usuario_ProcesJobs", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CntrliUsuarioId", IdUsuario));
                cmd.Parameters.Add(new SqlParameter("@CntrliOp", "0"));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        TPProcesos ls = new TPProcesos
                        {

                            iExistUsuario = int.Parse(data["Estatus"].ToString())

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

        /// inserte definicion en la tabla de calculos Hd cuando el cambia el año
        public TpCalculosHd sp_updateAnio_Insert_TPlantilla_Calculos_Hd(int CtrliIdDefinicionHd, int CtrliIdUsuarios)
        {
            TpCalculosHd bean = new TpCalculosHd();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_updateAnio_Insert_TPlantilla_Calculos_Hd", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliDefinicionIdHd", CtrliIdDefinicionHd));
                cmd.Parameters.Add(new SqlParameter("@CtrliUserId", CtrliIdUsuarios));

                if (cmd.ExecuteNonQuery() > 0)
                {
                    bean.sMensaje = "success";
                }
                else
                {
                    bean.sMensaje = "error";
                }
                cmd.Dispose(); conexion.Close(); cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return bean;
        }
    }
}

