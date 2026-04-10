using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace GestionDesStagesTFYA.Server.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Tes informations de connexion Mailtrap (Sandbox)
            var mail = "0c05ede95ad0d9"; // Le Username de ta capture d'écran
            var pw = "0d1403ad3e1f2a"; // Remplace ceci par le vrai mot de passe sans les étoiles

            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mail, pw)
            };

            var message = new MailMessage(
                from: "admin@gestiondesstages.com", // Tu peux inventer l'adresse que tu veux !
                to: email,
                subject,
                htmlMessage
            )
            {
                IsBodyHtml = true
            };

            return client.SendMailAsync(message);
        }
    }
}