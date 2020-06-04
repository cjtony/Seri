using System;

namespace Payroll.Models.Beans
{
    public class PeriodoVacacionesBean
    {
        public int iIdEmpleado { get; set; }
        public int iAnio { get; set; }
        public DateTime sFechaInicio { get; set; }
        public DateTime sFechaTermino { get; set; }
        public int iDiasDisfrutados { get; set; }
        public decimal iDiasPrima { get; set; }
    }
}