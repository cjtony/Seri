﻿using Payroll.Models.Beans;
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

        public List<CatalogoGeneralBean> sp_TiposDispersion_Retrieve_TiposDispersion()
        {
            List<CatalogoGeneralBean> lTypeDispersion = new List<CatalogoGeneralBean>();
            try {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_TiposDispersion_Retrieve_TiposDispersion", this.conexion) { CommandType = CommandType.StoredProcedure };
                SqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows) {
                    while (data.Read()) {
                        CatalogoGeneralBean typeDispersion = new CatalogoGeneralBean();
                        typeDispersion.iId = Convert.ToInt32(data["id"].ToString());
                        typeDispersion.iCampoCatalogoId = Convert.ToInt32(data["Campos_Catalogo_id"].ToString());
                        typeDispersion.iIdValor = Convert.ToInt32(data["IdValor"].ToString());
                        typeDispersion.sValor = data["Valor"].ToString();
                        typeDispersion.sDescripcion = data["Descripcion"].ToString();
                        lTypeDispersion.Add(typeDispersion);
                    }
                }
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            } finally {
                this.conexion.Close();
                this.Conectar().Close();
            }
            return lTypeDispersion;
        }

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
                            iIdBancoEmpresa = Convert.ToInt32(data["idBanco_emp"].ToString()),
                            iEmpresa_id = Convert.ToInt32(data["Empresa_id"].ToString()),
                            iIdBanco = Convert.ToInt32(data["Banco_id"].ToString()),
                            sNombreBanco = data["Descripcion"].ToString(),
                            sNumeroCliente = data["Num_cliente"].ToString(),
                            sNumeroCuenta = data["Num_Cta_Empresa"].ToString(),
                            sNumeroPlaza = data["Plaza"].ToString(),
                            sClabe = data["Clabe"].ToString(),
                            sCancelado = data["Cancelado"].ToString(),
                            iCodigoBanco = Convert.ToInt32(data["codigo"].ToString()),
                            iCg_tipo_dispersion = Convert.ToInt32(data["Cg_tipo_dispercion"].ToString()),
                            sValor = data["Valor"].ToString()
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

        public LoadDataTableBean sp_Actualiza_Banco_Empresa(int keyBusiness, int keyBank, string numClientBank, string numBillBank, string numSquareBank, string numClabeBank, int interfaceGen)
        {
            LoadDataTableBean dataBankBean = new LoadDataTableBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Actualiza_Banco_Empresa", this.conexion) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", keyBusiness));
                cmd.Parameters.Add(new SqlParameter("@IdBanco", keyBank));
                cmd.Parameters.Add(new SqlParameter("@Cliente", numClientBank));
                cmd.Parameters.Add(new SqlParameter("@Cuenta", numBillBank));
                cmd.Parameters.Add(new SqlParameter("@Plaza", numSquareBank));
                cmd.Parameters.Add(new SqlParameter("@Clabe", numClabeBank));
                cmd.Parameters.Add(new SqlParameter("@Interbancario", interfaceGen));
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