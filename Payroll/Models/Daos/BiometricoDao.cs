using Payroll.Models.Beans;
using Payroll.Models.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Payroll.Models.Daos
{
    public class FuncionBiometricoDao : Conexion
    {

        // inseta datos en la tabla CHorariosHD
        public EmpreHorarioBean sp_Insert_CHorarioHd(int CtrliEmpid, int CtrliTurno, string CtrlsDescrip, string CtrlsHrEntra, string CtrlsHrSalida, string CtrlsHrEntraPa, string CtrlsHrSalidaPA, int CtrliTipTurno, int CtrliTipPausa, int CtrlsDiasDesc, int ctrlsCancelado, int iIdusario, int CtrliTipoTruno, int CtrliTipoPausa)
        {
            EmpreHorarioBean bean = new EmpreHorarioBean();

            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_Insert_CHorarioHd", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliEmpid", CtrliEmpid));
                cmd.Parameters.Add(new SqlParameter("@CtrliTurno", CtrliTurno));
                cmd.Parameters.Add(new SqlParameter("@CtrlsDescrip", CtrlsDescrip));
                cmd.Parameters.Add(new SqlParameter("@CtrlsHrEntra", CtrlsHrEntra));
                cmd.Parameters.Add(new SqlParameter("@CtrlsHrSalida", CtrlsHrSalida));
                cmd.Parameters.Add(new SqlParameter("@CtrlsHrEntraPa", CtrlsHrEntraPa));
                cmd.Parameters.Add(new SqlParameter("@CtrlsHrSalidaPA", CtrlsHrSalidaPA));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipTurno", CtrliTipTurno));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipPausa", CtrliTipPausa));
                cmd.Parameters.Add(new SqlParameter("@CtrlsDiasDesc", CtrlsDiasDesc));
                cmd.Parameters.Add(new SqlParameter("@CtrliCancelado", ctrlsCancelado));
                cmd.Parameters.Add(new SqlParameter("@CtrliUsuario", iIdusario));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipoTruno", CtrliTipoTruno));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipoPausa", CtrliTipoPausa));

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

        // consulta los horarios de empresas
        public List<EmpreHorarioBean> sp_HorarioEmpresa_Retrieve_CHorarioHD(int EmpresaId)
        {
            List<EmpreHorarioBean> list = new List<EmpreHorarioBean>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_HorarioEmpresa_Retrieve_CHorarioHD", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliEmpresaID", EmpresaId));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EmpreHorarioBean ls = new EmpreHorarioBean();
                        {
                            ls.iIdHorario = int.Parse(data["IdHorario"].ToString());
                            ls.iEmpresaId = int.Parse(data["Empresa_id"].ToString());
                            ls.sNombreEmpresa = data["EmpresaNom"].ToString();
                            ls.sDescrip = data["Descripcion"].ToString();
                            ls.iTurno = int.Parse(data["Turno"].ToString());
                            ls.sHrEnt = data["HoraEntrada"].ToString();
                            ls.sHrSal = data["HoraSalida"].ToString();
                            ls.sHrEntCom = data["Hora_PausaEntrada"].ToString();
                            ls.sHrSalCom = data["Hora_PausaSalida"].ToString();
                            ls.iTipCheckNorm = int.Parse(data["TipCheck_HorarioNormal"].ToString());
                            ls.iTipCheckPausa = int.Parse(data["TipCheck_HorarioPausa"].ToString());
                            ls.iDiasDesc = int.Parse(data["DiasDescanso"].ToString());
                            ls.iCancelado = int.Parse(data["Cancel"].ToString());
                            ls.iUsuario = int.Parse(data["Usuario_id"].ToString());
                            ls.iTipoTurno = int.Parse(data["TipoTurno"].ToString());
                            ls.iTipoPausa = int.Parse(data["TipoPausa"].ToString());
                            ls.sMensaje = "success";
                        }
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

        // Actualiza CHorariosHD 
        public EmpreHorarioBean sp_updateChora_Update_CHorarioHd(int CtrliIdHora, int CtrliTurno, string CtrlsDescrip, string CtrlsHrEntra, string CtrlsHrSalida, string CtrlsHrEntraPa, string CtrlsHrSalidaPA, int CtrliTipTurno, int CtrliTipPausa, int CtrlsDiasDesc, int CtrliTipoTruno, int CtrliTipoPausa, int Cancel,int UserID)
        {
            EmpreHorarioBean bean = new EmpreHorarioBean();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_updateChora_Update_CHorarioHd", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliHrId", CtrliIdHora));
                cmd.Parameters.Add(new SqlParameter("@CtrliTurno", CtrliTurno));
                cmd.Parameters.Add(new SqlParameter("@CtrlsDescripcion", CtrlsDescrip));
                cmd.Parameters.Add(new SqlParameter("@CtrlsHrEntra", CtrlsHrEntra));
                cmd.Parameters.Add(new SqlParameter("@CtrlsHrSal", CtrlsHrSalida));
                cmd.Parameters.Add(new SqlParameter("@CtrlsHrEntPau", CtrlsHrEntraPa));
                cmd.Parameters.Add(new SqlParameter("@CtrlsHrSalPau", CtrlsHrSalidaPA));
                cmd.Parameters.Add(new SqlParameter("@CtrliCheckNor", CtrliTipTurno));
                cmd.Parameters.Add(new SqlParameter("@CtrliCheckHrPAu", CtrliTipPausa));
                cmd.Parameters.Add(new SqlParameter("@CtrliDiasDesc", CtrlsDiasDesc));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipTurno", CtrliTipoTruno));
                cmd.Parameters.Add(new SqlParameter("@CtrliTipPausa", CtrliTipoPausa));
                cmd.Parameters.Add(new SqlParameter("@CtrliCacnel", Cancel));
                cmd.Parameters.Add(new SqlParameter("@CtrliUsuario", UserID));
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

        // consulta los horarios semanal de empresas
        public List<EmprHrSemanalBea> sp_HrSemanal_Retrieve_ChorarioLn(int EmpresaId)
        {
            List<EmprHrSemanalBea> list = new List<EmprHrSemanalBea>();
            try
            {
                this.Conectar();
                SqlCommand cmd = new SqlCommand("sp_HrSemanal_Retrieve_ChorarioLn", this.conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@CtrliEmpId", EmpresaId));
                SqlDataReader data = cmd.ExecuteReader();
                cmd.Dispose();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        EmprHrSemanalBea ls = new EmprHrSemanalBea();
                        {
                            ls.iIdHrSM = int.Parse(data["IdHr_Semanal"].ToString());
                            ls.iEmpId = int.Parse(data["Empresa_id"].ToString());
                            //ls.sEmp = data[""].ToString();
                            ls.iNoHr = int.Parse(data["NoHorario"].ToString());
                            ls.sdescrip = data["Descrip"].ToString();
                            ls.iLu =int.Parse(data["Lunes"].ToString());
                            ls.iMa =int.Parse(data["Martes"].ToString());
                            ls.iMe =int.Parse(data["Miercoles"].ToString());
                            ls.iJu =int.Parse(data["Jueves"].ToString());
                            ls.iVi = int.Parse(data["Viernes"].ToString());
                            ls.iSa = int.Parse(data["Sabado"].ToString());
                            ls.iDo = int.Parse(data["Domingo"].ToString());
                            ls.iUsuId = int.Parse(data["Usuario_Id"].ToString());
                            ls.iCancel = int.Parse(data["Cancelado"].ToString());
                            ls.sMensaje = "success";
                        }
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