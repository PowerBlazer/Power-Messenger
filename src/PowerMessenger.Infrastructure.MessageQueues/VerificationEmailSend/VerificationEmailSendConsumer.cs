using JetBrains.Annotations;
using MassTransit;
using PowerMessenger.Application.Layers.Email.Services;
using PowerMessenger.Application.Layers.MessageQueues.VerificationEmailSend;
using RazorLight;

namespace PowerMessenger.Infrastructure.MessageQueues.VerificationEmailSend;

[UsedImplicitly]
public class VerificationEmailSendConsumer: IConsumer<VerificationEmailSendEvent>
{
    private readonly ISmtpEmailService _smtpEmailService;

    public VerificationEmailSendConsumer(ISmtpEmailService smtpEmailService)
    {
        _smtpEmailService = smtpEmailService;
    }

    public async Task Consume(ConsumeContext<VerificationEmailSendEvent> context)
    {
        var emailPagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Common","EmailPage.cshtml");
        
        if (!File.Exists(emailPagePath))
        {
            throw new Exception("Шаблон для отправки сообщения отсутствует");
        }
        
        var razor = new RazorLightEngineBuilder()
            .UseMemoryCachingProvider()
            .Build();

        var template = await File.ReadAllTextAsync(emailPagePath);

        var htmlContent = await razor.CompileRenderStringAsync("template", template,context.Message);

        await _smtpEmailService.SendEmailAsync(context.Message.Email, "Подтверждение почты в PowerMessenger",
            htmlContent);
    }
}