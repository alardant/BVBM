using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using BVBM.API.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;
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
                var keyVaultUrl = _config["KeyVault:KeyVaultUrl"];
                var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(client.GetSecret("smtpUsername").Value.Value.ToString());
                mail.To.Add(client.GetSecret("smtpUsername").Value.Value.ToString());
                mail.Subject = "Nouveau message de contact - " + contactDto.Subject;
                mail.Body = message;

                // Create SmtpClient object and send email
                SmtpClient smtpClient = new SmtpClient(_config.GetSection("SmtpSettings:Server").Value, int.Parse(client.GetSecret("smtpPort").Value.Value.ToString()));
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(client.GetSecret("smtpUsername").Value.Value.ToString(), client.GetSecret("smtpPassword").Value.Value.ToString()); // SMTP ID
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
