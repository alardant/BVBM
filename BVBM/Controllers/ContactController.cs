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

            string message = $"Nom: {contactDto.Name}\n" +
                            $"Email: {contactDto.Email}\n" +
                            $"Téléphone: {contactDto.Phone}\n" +
                            $"Sujet: {contactDto.Subject}\n" +
                            $"Message: {contactDto.Message}";

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_config["smtpUsername"]);
                mail.To.Add(_config["smtpUsername"]);
                mail.Subject = "Nouveau message de contact - " + contactDto.Subject;
                mail.Body = message;

                // Create SmtpClient object and send email
                SmtpClient smtpClient = new SmtpClient(_config.GetSection("SmtpSettings:Server").Value, int.Parse(_config.GetSection("SmtpSettings:Port").Value));
                smtpClient.EnableSsl = true;
                //A modifier
                smtpClient.Credentials = new System.Net.NetworkCredential(_config["smtpUsername"], _config["smtpPassword"]); // SMTP ID
                smtpClient.SendMailAsync(mail);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to send the email. Please try again later.");
            }
        }
    }
}
