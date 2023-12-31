﻿using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace CapaAdmin.Models.Services
{
    public class Recursos :IRecursos
    {
        #region USUARIO
        // Encriptacion de password
        public string GetSHA2S6(string str)
        {

            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();

            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }


        // Generador de password
        public string GetPassword() => Guid.NewGuid().ToString("N").Substring(0,6);

        // Enviar Correo
        public bool SendEmail( string email, string subject, string mensaje)
        {
            bool result = false;

            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(email);
                mailMessage.From = new MailAddress("dotnetprogramacion@gmail.com");
                mailMessage.Subject = subject;
                mailMessage.Body = mensaje;
                mailMessage.IsBodyHtml = true;

                var smtp = new SmtpClient() {
                    Credentials = new NetworkCredential("dotnetprogramacion@gmail.com", "bskvacfguigpaxbw"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };

                smtp.Send(mailMessage);
                result = true;
            }
            catch(Exception ex) 
            {
                result = false;
            }

            return result;
        }
        #endregion

        #region IMAGEN
        public static string ConvertirBase64(string ruta, out bool conversion)
        {
            string textBase64 = string.Empty;
            conversion = true;
            try
            {
                if (File.Exists(ruta))
                {
                    // El archivo existe, procede a leerlo y convertirlo a Base64.
                    byte[] bytes = File.ReadAllBytes(ruta);
                    textBase64 = Convert.ToBase64String(bytes);
                }
                else
                {
                    conversion = false;
                }
            }
            catch
            {
                conversion = false;
            }
            return textBase64;
        }
        #endregion
    }
}
