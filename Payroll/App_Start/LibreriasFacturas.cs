using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Web;
using System.Xml;
using System.Xml.Xsl;


namespace Payroll
{
    public class LibreriasFacturas
    {
        public static string StrReverse(string s)
        {
            int j = 0;
            char[] c = new char[s.Length];
            for (int i = s.Length - 1; i >= 0; i--) c[j++] = s[i];
            return new string(c);
        }

        public static String GetCadenaOriginal(String xmlDoc, String fileXSLT, String DirectorioReportes)
        {
            String strCadenaOriginal;
            String newFile = DirectorioReportes + "\\Temporal1.xml";

            XslCompiledTransform Xsl1 = new XslCompiledTransform();
            Xsl1.Load(fileXSLT);
            Xsl1.Transform(xmlDoc, newFile);
            Xsl1 = null;

            var sr = new System.IO.StreamReader(newFile);
            strCadenaOriginal = sr.ReadToEnd();
            sr.Close();

            //Eliminamos el archivo Temporal
            System.IO.File.Delete(newFile);

            fileXSLT = null;
            newFile = null;
            Xsl1 = null;
            sr.Dispose();
            return strCadenaOriginal;
        }

        public static String ObtenerSelloDigital(String cadenaOriginal, String rutaLlavePrivada, String password)
        {
            var passwordSeguro = new SecureString();
            passwordSeguro.Clear();
            foreach (char c in password.ToCharArray())
            {
                passwordSeguro.AppendChar(c);
            }
            byte[] llavePrivadaBytes = System.IO.File.ReadAllBytes(rutaLlavePrivada);
            string publicKeyBase64 = Convert.ToBase64String(llavePrivadaBytes);
            RSACryptoServiceProvider rsa = opensslkey.DecodeEncryptedPrivateKeyInfo(llavePrivadaBytes, passwordSeguro);
            string ssss = Convert.ToBase64String(rsa.ExportCspBlob(true));
            var hasher = new SHA256CryptoServiceProvider();
            byte[] bytesFirmados = rsa.SignData(System.Text.Encoding.UTF8.GetBytes(cadenaOriginal), hasher);
            String selloDigital = Convert.ToBase64String(bytesFirmados);
            return selloDigital;

        }
        public static void AplicarSelloDigital(String selloDigitalOriginal, String ArchivoXml)
        {
            // Open the XML file
            var docXML = new XmlDocument();
            docXML.Load(ArchivoXml);

            // Create an attribute and add it to the root element
            docXML.DocumentElement.SetAttribute("Sello", selloDigitalOriginal);
            docXML.Save(ArchivoXml);

        }
    }
}