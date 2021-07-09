using System;
using System.Net.Mail;
using System.Text;

/// <summary>
/// Descripción breve de Correo
/// </summary>
public class Correo
{
	Boolean estado = true;
    string merror; 
	public Correo(string destinatario,string asunto, string mensaje,string path,string EmailEmp,string PasswordEmpre)
	{
        //
        // TODO: Add constructor logic here
        //
        //MailMessage correo = new MailMessage();
        //SmtpClient Protocolo = new SmtpClient();
        //correo.To.Add(destinatario);
        //correo.From = new MailAddress("capitalhumano@gruposeri.com", "Grupo Seri",System.Text.Encoding.UTF8);
        //correo.Subject = asunto;
        //correo.SubjectEncoding = System.Text.Encoding.UTF8;
        //correo.Body = mensaje;
        //correo.Attachments.Add(new Attachment(path));
        ////correo.BodyEncoding = System.Text.Encoding.UTF8;
        //correo.IsBodyHtml = true;
        //correo.Priority = MailPriority.Normal;
        // smtp.Credentials = new NetworkCredential("capitalhumano@gruposeri.com", "cH*150519");
        // Protocolo.Credentials = new System.Net.NetworkCredential("pp709672@gmail.com","S3r12020c#");

        //Protocolo.Port = 587;
        //Protocolo.Host = "mail.dmlink.com";
        //Protocolo.EnableSsl = true;
        //Protocolo.UseDefaultCredentials = false;
        //Protocolo.Credentials = new System.Net.NetworkCredential("capitalhumano@gruposeri.com", "cH*150519");


        MailMessage correo = new MailMessage();
        SmtpClient Protocolo = new SmtpClient();
        correo.To.Add(destinatario);
        correo.From = new MailAddress(EmailEmp, "", System.Text.Encoding.UTF8);
        correo.Subject = asunto;
        correo.SubjectEncoding = System.Text.Encoding.UTF8;
        correo.Body = mensaje;
        correo.Attachments.Add(new Attachment(path));
        //correo.BodyEncoding = System.Text.Encoding.UTF8;
        correo.IsBodyHtml = true;
        correo.Priority = MailPriority.Normal;

 
        //smtp.Credentials = new NetworkCredential("capitalhumano@gruposeri.com", "cH*150519");
        // Protocolo.Credentials = new System.Net.NetworkCredential("pp709672@gmail.com","S3r12020c#");
 

        Protocolo.Port = 587;
        Protocolo.Host = "smtp.office365.com";
        Protocolo.Credentials = new System.Net.NetworkCredential(EmailEmp, PasswordEmpre);
        Protocolo.EnableSsl = true;
        //Protocolo.UseDefaultCredentials = false;
        
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

public class MandarCorreos
{
    public bool CustomMail(string destinatario, string asunto, string mensaje)
    {
        bool status;
        MailMessage correo = new MailMessage();
        SmtpClient Protocolo = new SmtpClient();
        correo.To.Add(destinatario);
        correo.From = new MailAddress("capitalhumano@gruposeri.com", "Capital Humano", Encoding.UTF8);
        correo.Subject = asunto;
        correo.SubjectEncoding = Encoding.UTF8;

        AlternateView htmlView = AlternateView.CreateAlternateViewFromString("<html><body>" + mensaje + "</body></html>", null, System.Net.Mime.MediaTypeNames.Text.Html);
        correo.BodyEncoding = UTF8Encoding.UTF8;
        //correo.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        var aa = AppDomain.CurrentDomain.BaseDirectory;

        LinkedResource imageResource = new LinkedResource(aa + @"Content\img\calendar.png");
        LinkedResource imageResource2 = new LinkedResource(aa + @"Content\img\calendar.png");
        LinkedResource imageResource3 = new LinkedResource(aa + @"Content\img\hash.png");

        imageResource.ContentId = "calendar1";
        imageResource2.ContentId = "calendar2";
        imageResource3.ContentId = "hashtag";
        htmlView.LinkedResources.Add(imageResource);
        htmlView.LinkedResources.Add(imageResource2);
        htmlView.LinkedResources.Add(imageResource3);
        correo.AlternateViews.Add(htmlView);

        //correo.Body = mensaje;
        correo.IsBodyHtml = true;
        correo.Priority = MailPriority.Normal;
        correo.BodyEncoding = UTF8Encoding.UTF8;

        Protocolo.Port = 587;
        Protocolo.Host = "mail.dmlink.com";
        Protocolo.EnableSsl = true;
        Protocolo.UseDefaultCredentials = false;
        //Protocolo.Credentials = new System.Net.NetworkCredential("capitalhumano@gruposeri.com", "cH*150519");
        Protocolo.Credentials = new System.Net.NetworkCredential("capitalhumano@gruposeri.com", "cH*150519");

        try
        {
            Protocolo.Send(correo);
            status = true;
        }
        catch (SmtpException error)
        {
            status = false;
            Console.WriteLine("----------------------------------------------------- ERROR ------------------------------------------------------");
            Console.WriteLine(error.Message);
        }

        return status;
    }

} 