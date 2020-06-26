namespace Payroll.Models.Beans
{
    public class ConfigDataBankBean { }

    public class LoadDataTableBean
    {
        public int iIdBancoEmpresa { get; set; }
        public int iEmpresa_id { get; set; }
        public int iIdBanco { get; set; }
        public string sNumeroCliente { get; set; }
        public string sNumeroCuenta { get; set; }
        public string sNumeroPlaza { get; set; }
        public string sClabe { get; set; }
        public string sNombreBanco { get; set; }
        public int iCodigoBanco { get; set; }
        public string sCancelado { get; set; }
        public int iCg_tipo_dispersion { get; set; }
        public string sValor { get; set; }
        public string sMensaje { get; set; }
    }
}