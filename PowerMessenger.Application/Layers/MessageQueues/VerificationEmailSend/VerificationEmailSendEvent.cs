using JetBrains.Annotations;

namespace PowerMessenger.Application.Layers.MessageQueues.VerificationEmailSend;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class VerificationEmailSendEvent
{
    public string? Email { get; set; }
    public string? ConfirmLink { get; set; }
    public string? VerificationCode { get; set; }
}