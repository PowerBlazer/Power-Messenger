using MassTransit;
using PowerMessenger.Application.Layers.MessageQueues.VerificationEmailSend;

namespace PowerMessenger.Infrastructure.MessageQueues.VerificationEmailSend;

public class VerificationEmailSendProducer: IVerificationEmailSendProducer
{
    private readonly IBusControl _busControl;

    public VerificationEmailSendProducer(IBusControl busControl)
    {
        _busControl = busControl;
    }

    public async Task PublishEmailSend(VerificationEmailSendEvent verificationEmailSendEvent)
    {
       await _busControl.Publish(verificationEmailSendEvent);
    }
}