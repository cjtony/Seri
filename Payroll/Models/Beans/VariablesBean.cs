namespace Payroll.Models.Beans
{
    public class VariablesBean { }
    public class NumeroNominaBean
    {
        public int iNominaSiguiente { get; set; }
        public int iNominaTope { get; set; }
        public string sMensaje { get; set; }
    }

    public class NumeroPosicionBean
    {
        public int iPosicionUltima { get; set; }
        public int iPosicionSiguiente { get; set; }
        public string sMensaje { get; set; }
    }
}