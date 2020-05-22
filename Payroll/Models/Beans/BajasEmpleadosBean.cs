using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Models.Beans
{
    public class BajasEmpleadosBean
    {

        public int iIdFiniquito               { get; set; }
        public int iEmpresa_id                { get; set; }
        public int iEmpleado_id               { get; set; }
        public string sEffdt                  { get; set; }
        public string sFecha_antiguedad       { get; set; }
        public string sFecha_ingreso          { get; set; }
        public string sFecha_baja             { get; set; }
        public int iAnios                     { get; set; }
        public string sDias                   { get; set; }
        public string sRFC                    { get; set; }
        public int iCentro_costo_id           { get; set; }
        public int iPuesto_id                 { get; set; }
        public decimal dSalario_mensual       { get; set; }
        public decimal dSalario_diario        { get; set; }
        public int iTipo_finiquito_id         { get; set; }
        public string sFiniquito_valor        { get; set; }
        public string sFecha_recibo           { get; set; }
        public string sFecha                  { get; set; }
        public int iInactivo                  { get; set; }
        public int iban_fecha_ingreso         { get; set; }
        public int iban_compensacion_especial { get; set; }
        public string sMensaje                { get; set; }

    }
}