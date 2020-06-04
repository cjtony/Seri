using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Payroll.Models.Daos
{
    public class ConfigDataBankDao { }

    public class LoadDataTableDaoD : Conexion
    {
        public List<LoadDataTableBean> sp_Carga_Bancos_Empresa(int keyBusiness)
        {
            List<LoadDataTableBean> lDataTableBean = new List<LoadDataTableBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Carga_Bancos_Empresa", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        lDataTableBean.Add(new LoadDataTableBean
                        {
                            iIdBanco = Convert.ToInt32(data["banco"].ToString()),
                            sNombreBanco = data["descripcion"].ToString(),
                            sNumeroCliente = data["num_cliente"].ToString(),
                            sNumeroCuenta = data["num_cta_empresa"].ToString(),
                            sNumeroPlaza = data["plaza"].ToString(),
                            sClabe = data["clabe"].ToString(),
                            iGeneraInterface = Convert.ToInt32(data["genera_interface"].ToString()),
                            iCodigoBanco = Convert.ToInt32(data["codigo_banco"].ToString())
                        });
                    }
                }
                cmd.Parameters.Clear(); cmd.Dispose(); data.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }
            return lDataTableBean;
        }

        public LoadDataTableBean sp_Actualiza_Banco_Empresa(int keyBusiness, int keyBank, string numClientBank, string numBillBank, string numSquareBank, string numClabeBank, int numCodeBank, int interfaceGen)
        {
            LoadDataTableBean dataBankBean = new LoadDataTableBean();
            try
            {
                this.Conectar();
                int interbancarios;
                if (interfaceGen == 1)
                {
                    interbancarios = 0;
                }
                else
                {
                    interbancarios = 1;
                }
                SqlCommand cmd = new SqlCommand("sp_Actualiza_Banco_Empresa", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@IdBanco", keyBank));
                cmd.Parameters.Add(new SqlParameter("@Cliente", numClientBank));
                cmd.Parameters.Add(new SqlParameter("@Cuenta", numBillBank));
                cmd.Parameters.Add(new SqlParameter("@Plaza", numSquareBank));
                cmd.Parameters.Add(new SqlParameter("@Interface", interfaceGen));
                cmd.Parameters.Add(new SqlParameter("@Clabe", numClabeBank));
                cmd.Parameters.Add(new SqlParameter("@Interbancario", interbancarios));
                cmd.Parameters.Add(new SqlParameter("@Codigo", numCodeBank));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    dataBankBean.sMensaje = "update";
                }
                else
                {
                    dataBankBean.sMensaje = "error";
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
            return dataBankBean;
        }

    }
}