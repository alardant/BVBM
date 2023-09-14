using BVBM.API.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;

namespace BVBM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ContactController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] ContactDto contactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string message = $"Prénom: {contactDto.Name}\n" +
                            $"Email: {contactDto.Email}\n" +
                            $"Téléphone: {contactDto.Phone}\n" +
                            $"Sujet: {contactDto.Subject}\n" +
                            $"Message: {contactDto.Message}";

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_config.GetSection("SmtpSettings:Username").Value);
                mail.To.Add(_config.GetSection("SmtpSettings:Username").Value);
                mail.Subject = "Nouveau message de contact - " + contactDto.Subject;
                mail.Body = message;

                // Create SmtpClient object and send email
                SmtpClient smtpClient = new SmtpClient(_config.GetSection("SmtpSettings:Server").Value, int.Parse(_config.GetSection("SmtpSettings:Port").Value));
                smtpClient.EnableSsl = true;
                //A modifier
                smtpClient.Credentials = new System.Net.NetworkCredential(_config.GetSection("SmtpSettings:Username").Value, _config.GetSection("SmtpSettings:Password").Value); // SMTP ID
                smtpClient.SendMailAsync(mail);

                return Ok("E-mail envoyé avec succès.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de l'envoi de l'e-mail : {ex.Message}");
            }
        }
    }
}
