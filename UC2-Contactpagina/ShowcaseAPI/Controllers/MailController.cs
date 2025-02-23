using Microsoft.AspNetCore.Mvc;
using ShowcaseAPI.Models;
using System.Net.Mail;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShowcaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        // POST api/<MailController>
        [HttpPost]
        public ActionResult Post([Bind("FirstName, LastName, Email, Phone")] Contactform form)
        {
            //Op brightspace staan instructies over hoe je de mailfunctionaliteit werkend kunt maken:
            //Project Web Development > De showcase > Week 2: contactpagina (UC2) > Hoe verstuur je een mail vanuit je webapplicatie met Mailtrap?

            try
            {
                var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("dd297052ddf633", "c2db2857899843"),
                    EnableSsl = true
                };

                string fromEmail = form.Email;
                string toEmail = "to@example.com";
                string subject = form.Subject;
                string body =   $"Name: {form.FirstName} {form.LastName}" +
                                $"\nEmail: {form.Email}" +
                                $"\nPhone: {form.Phone}" +
                                $"\n Bericht: {form.Message}";

                client.Send(fromEmail, toEmail, subject, body);

                return Ok(new { success = true, message = "E-mail succesvol verzonden!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Fout bij verzenden: {ex.Message}" });
            }
        }
    }
}
