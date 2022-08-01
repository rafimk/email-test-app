using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.AspNetCore.Mvc;

namespace email_test_app.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        await SendEmail();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    private async Task  SendEmail()
    {
        // Note : From address change based on the configuration outlook or membership-app.me
        var from = "uaekmcc@outlook.com";

        var html = HtmlBinder.Create("1", "2", "3", "4");
        var subject = "Test email service";
        var to = "rafi.genius.cs@gmail.com";

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from ?? "uaekmcc@outlook.com"));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = html };

        // send email
        using var smtp = new SmtpClient();
        smtp.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
        smtp.Authenticate("uaekmcc@outlook.com", "Kmcc@7425403");

        // Note : From address change based on the configuration
        // smtp.Connect("mail.privateemail.com", 465, SecureSocketOptions.SslOnConnect);
        // smtp.Authenticate("muhammed.rafi@membership-app.me", "Mem@4296326");

        await smtp.SendAsync(email);
        smtp.Disconnect(true);

        // client.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
        // client.Authenticate("me@outlook.com", "mypassword");
    }
}
