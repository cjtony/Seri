namespace Payroll.Models.Beans
{
    public class PruebaEmpleadosBean
    {
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidosEmpleado { get; set; }
        public string NombreDepartamento { get; set; }
        public string Puesto { get; set; }
        public string FechaIngreso { get; set; }
        public decimal Sueldo { get; set; }
        public int IdEmpresa { get; set; }
    }
}