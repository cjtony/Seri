using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

/// <summary>
/// Descripción breve de Correo
/// </summary>
public class Correo
{
	 Boolean estado = true;
    string merror; 
	public Correo(string destinatario,string asunto, string mensaje)
	{
        string path = @"C:\Users\DELL\Desktop\gitproyecto\Proyecto Actualizado AAA\Seri\Payroll\Archivos\certificados\PDF2\Fiscal\Empresa18\Periodo2\IPSNetReciboFiscal_E18_N31_F202030020.pdf";
		//
		// TODO: Add constructor logic here
		//
        MailMessage correo = new MailMessage();
        SmtpClient Protocolo = new SmtpClient();
        correo.To.Add(destinatario);
        correo.From = new MailAddress("pp709672@gmail.com", "Grupo Seri",System.Text.Encoding.UTF8);
        correo.Subject = asunto;
        correo.SubjectEncoding = System.Text.Encoding.UTF8;
        correo.Body = mensaje;
        correo.Attachments.Add(new Attachment(path));
        correo.BodyEncoding = System.Text.Encoding.UTF8;
        correo.IsBodyHtml = false;

        Protocolo.Credentials = new System.Net.NetworkCredential("pp709672@gmail.com","S3r12020c#");
        Protocolo.Port = 587;
        Protocolo.Host = "smtp.gmail.com";
        Protocolo.EnableSsl = true;

        try
        {
            Protocolo.Send(correo);

        }
        catch(SmtpException error)
        {
            estado = false;
            merror = error.Message.ToString();
        
        } 
	}
    public bool Estado
    {
        get { return estado; }

    }

    public string Error
    {
        get { return merror; }
 
  
    }
}