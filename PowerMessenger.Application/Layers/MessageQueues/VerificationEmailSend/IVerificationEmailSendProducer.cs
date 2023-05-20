﻿namespace PowerMessenger.Application.Layers.MessageQueues.VerificationEmailSend;

public interface IVerificationEmailSendProducer
{
    Task PublishEmailSend(VerificationEmailSendEvent verificationEmailSendEvent);
}