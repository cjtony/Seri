namespace Payroll.Models.Beans
{
    public class PruebaEmpresaBean
    {
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public int IdEstado { get; set; }
        public int IdCiudad { get; set; }
        public string Delegacion { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string Giro { get; set; }
        public string RFC { get; set; }
        public int IdRegistroPatronal { get; set; }
        public int NominaAutomaticaUltimo { get; set; }
        public int NomimaAutomaticaInicial { get; set; }
        public int NominaAutomaticaFinal { get; set; }
        public string SueldoPeriodo { get; set; }
        public bool PagoSueldo { get; set; }

    }
}