using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payroll.Models.Utilerias
{
    public class Utilerias
    {

        public String ConvertDateText(string dateConvert)
        {
            String convertDate = "";
            try
            {
                string year = dateConvert.Substring(0, 4);
                string month = dateConvert.Substring(5, 2);
                string day = dateConvert.Substring(8, 2);
                string[] days = new string[] { "Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sábado" };
                string[] months = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                convertDate = day + " de " + months[Convert.ToInt32(month) - 1] + " del " + year;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message.ToString());
            }
            return convertDate;
        }

        public String ConvertDateFormat_year_month_day(string date)
        {
            String convertDate = "";
            try {
                DateTime dateObject = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                string   dateMonth  = (dateObject.Month < 9) ? "0" + dateObject.Month.ToString() : dateObject.Month.ToString();
                string   dateDay    = (dateObject.Day   < 9) ? "0" + dateObject.Day.ToString()   : dateObject.Day.ToString();
                convertDate         = dateObject.Year + "-" + dateMonth + "-" + dateDay;
            } catch (Exception exc) {
                Console.WriteLine(exc.Message.ToString());
            }
            return convertDate;
        }

    }
}