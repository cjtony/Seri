
using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Payroll.Models.Daos
{

    public class NominaDao
    {
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
        public List<EmpresasBean> sp_CEmpresas_Retrieve_Empresas()
        {
            List<EmpresasBean> list = new List<EmpresasBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_CEmpresas_Retrieve_Empresas", this.conexion)
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

        public TpCalculosHd sp_TpCalculos_Insert_TpCalculos(int CtrliIdDefinicionHd, int CtrliNominaCerrada)
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
                cmd.Parameters.Add(new SqlParameter("@CtrliNominaCerrada", CtrliNominaCerrada));

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

        public List<TpCalculosCarBean> sp_Caratula_Retrieve_TPlantilla_Calculos(int CtrliIdCalculoshd, int CrtliIdTipoPeriodo, int CrtliPeriodo, int Idempresa)
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
                            ls.dTotal = decimal.Parse(data["total"].ToString());
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
                                ls.IdMotivo_Baja = int.Parse(data["IdValor"].ToString());
                                ls.TipoEmpleado_id = 164;
                                ls.Descripcion = data["Valor"].ToString();
                                break;
                            case 4:
                            case 5:
                                ls.IdMotivo_Baja = int.Parse(data["IdValor"].ToString());
                                ls.TipoEmpleado_id = 165;
                                ls.Descripcion = data["Valor"].ToString();
                                break;
                            case 18:
                                ls.IdMotivo_Baja = int.Parse(data["IdValor"].ToString());
                                ls.TipoEmpleado_id = 168;
                                ls.Descripcion = data["Valor"].ToString();
                                break;
                            case 19:
                                ls.IdMotivo_Baja = int.Parse(data["IdValor"].ToString());
                                ls.TipoEmpleado_id = 27;
                                ls.Descripcion = data["Valor"].ToString();
                                break;
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                                ls.IdMotivo_Baja = int.Parse(data["IdValor"].ToString());
                                ls.TipoEmpleado_id = 172;
                                ls.Descripcion = data["Valor"].ToString();
                                break;
                            case 17:
                            case 23:
                                ls.IdMotivo_Baja = int.Parse(data["IdValor"].ToString());
                                ls.TipoEmpleado_id = 30;
                                ls.Descripcion = data["Valor"].ToString();
                                break;
                            case 1:
                            case 2:
                                ls.IdMotivo_Baja = int.Parse(data["IdValor"].ToString());
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
                        list.Add(data["Salario_Mensual"].ToString());
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

        public TPProcesos sp_CNomina_1(int p_Ano, int p_Tipo_periodo, int p_Periodo, int p_IdDefinicion_Hd)
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

        public List<ReciboNominaBean> sp_TpCalculoEmpleado_Retrieve_TpCalculoEmpleado(int CtrliIdEmpresa, int CtrliIdemplado, int CtrliPeriodo)
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
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodo", CtrliPeriodo));
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
                            ls.dSaldo = decimal.Parse(data["Saldo"].ToString());
                            ls.iConsecutivo = int.Parse(data["Consecutivo"].ToString());
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

                data.Close(); cmd.Dispose(); conexion.Close(); //cmd.Parameters.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return list;
        }


        public List<CInicioFechasPeriodoBean> sp_PeridosEmpresa_Retrieve_CinicioFechasPeriodo(int CrtliIdDeficionHd,int CrtliPeriodo, int CtrliNomCerr)
        {
            List<CInicioFechasPeriodoBean> list = new List<CInicioFechasPeriodoBean>();
            try
            {
                int CrtliIdEmpresa = 0;
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_PeridosEmpresa_Retrieve_CinicioFechasPeriodo", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CrtliIdDeficionHd", CrtliIdDeficionHd));
                cmd.Parameters.Add(new SqlParameter("@CrtliIdEmpresa", CrtliIdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@CrtliPeriodo", CrtliPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliNomCerr", CtrliNomCerr));
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

        public CInicioFechasPeriodoBean sp_NomCerradaCInicioFechaPeriodo_Update_CInicioFechasPeriodo(int CrtliIdDeficionHd, int CtrliPeriodo, int CtrliNominaCerrada)
        {
            CInicioFechasPeriodoBean bean = new CInicioFechasPeriodoBean();
            try
            {
                int CtrliIdempresa = 0;
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_NomCerradaCInicioFechaPeriodo_Update_CInicioFechasPeriodo", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CrtliIdDeficionHd", CrtliIdDeficionHd));
                cmd.Parameters.Add(new SqlParameter("@CtrliIdempresa", CtrliIdempresa));
                cmd.Parameters.Add(new SqlParameter("@CtrliPeriodo", CtrliPeriodo));
                cmd.Parameters.Add(new SqlParameter("@CtrliNominaCerrada", CtrliNominaCerrada));
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

    }
}

