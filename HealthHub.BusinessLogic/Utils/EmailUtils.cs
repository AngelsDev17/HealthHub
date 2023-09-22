using HealthHub.Application.Constants;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace HealthHub.BusinessLogic.Utils;

public static class EmailUtils
{
    public static async Task SendEmailToUsers(
        List<string> userEmails,
        string subject,
        BodyBuilder bodyBuilder)
    {
        var mimeMessage = new MimeMessage();
        var smtpClient = new SmtpClient { CheckCertificateRevocation = true };

        mimeMessage.From.Add(
            new MailboxAddress(
                name: EnvironmentVariables.EMAIL_USERNAME,
                address: EnvironmentVariables.EMAIL_USERNAME_ADDRESS));

        foreach (var email in userEmails)
            mimeMessage.To.Add(
                new MailboxAddress(name: email, address: email));

        mimeMessage.Subject = subject;
        mimeMessage.Body = bodyBuilder.ToMessageBody();

        await smtpClient.ConnectAsync(
            host: EnvironmentVariables.EMAIL_HOST,
            port: EnvironmentVariables.EMAIL_PORT,
            options: SecureSocketOptions.StartTls);

        await smtpClient.AuthenticateAsync(
            userName: EnvironmentVariables.EMAIL_USERNAME_ADDRESS,
            password: EnvironmentVariables.EMAIL_PASSWORD);

        await smtpClient.SendAsync(message: mimeMessage);
        await smtpClient.DisconnectAsync(quit: true);

        smtpClient.Dispose();
    }
}
