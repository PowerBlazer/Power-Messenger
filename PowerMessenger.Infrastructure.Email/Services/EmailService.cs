﻿using MimeKit;
using MailKit.Net.Smtp;
using PowerMessenger.Application.Layers.Email;
using PowerMessenger.Application.Layers.Email.Services;

namespace PowerMessenger.Infrastructure.Email.Services;

public class EmailService: IEmailService
{
    private readonly EmailConfiguration _emailConfiguration;
    
    public EmailService(EmailConfiguration emailConfiguration)
    {
        _emailConfiguration = emailConfiguration;
    }
    
    public async Task SendEmailAsync(string recipient, string subject, string body)
    {
        var message = new MimeMessage();
        
        message.From.Add(new MailboxAddress(_emailConfiguration.SenderName, _emailConfiguration.SenderEmail));
        message.To.Add(new MailboxAddress("", recipient));
        message.Subject = subject;

        var builder = new BodyBuilder
        {
            HtmlBody = body
        };
        
        message.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        
        await client.ConnectAsync(_emailConfiguration.Host, _emailConfiguration.Port);
        await client.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}