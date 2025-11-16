using ClinicaMedica2025.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ClinicaMedica2025.Servicios
{
    public static class CorreoServicio
    {
        private static string _Host = "smtp.gmail.com";
        private static int _Puerto = 465;

        private static string _NombreEnvia = "Germán Hijonosa";
        private static string _Correo = "higieneyseguridad036@gmail.com";
        private static string _Clave = "nbaujqkwwvtmjotc";
        public static bool Enviar(CorreoDTO correodto)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress(_NombreEnvia, _Correo));
                email.To.Add(MailboxAddress.Parse(correodto.Para));
                email.Subject = correodto.Asunto;

                email.Date = DateTimeOffset.Now;
                email.MessageId = MimeKit.Utils.MimeUtils.GenerateMessageId();

                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = correodto.Contenido
                };

                var smtp = new SmtpClient();
                smtp.Connect(_Host, _Puerto, SecureSocketOptions.SslOnConnect);

                smtp.Authenticate(_Correo, _Clave);
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch(Exception ex)
            {

                Console.WriteLine("Error al enviar correo: " + ex.Message);
                return false;

            }
        }


    }
}