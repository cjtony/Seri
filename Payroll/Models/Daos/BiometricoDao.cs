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
        public EmpreHorarioBean sp_Insert_CHorarioHd(int CtrliEmpid, int CtrliTurno, string CtrlsDescrip ,string  CtrlsHrEntra,string CtrlsHrSalida , string CtrlsHrEntraPa,string  CtrlsHrSalidaPA, int CtrliTipTurno, int CtrliTipPausa, int CtrlsDiasDesc, int ctrlsCancelado, int iIdusario, int CtrliTipoTruno, int CtrliTipoPausa)
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
                cmd.Parameters.Add(new SqlParameter("@iIdusario", iIdusario));
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


    }
}