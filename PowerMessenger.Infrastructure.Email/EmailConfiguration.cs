﻿using JetBrains.Annotations;

namespace PowerMessenger.Infrastructure.Email;

[UsedImplicitly]
public class EmailConfiguration
{
    public string? Host { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? SenderName { get; set; }
    public string? SenderEmail { get; set; }
    public int Port { get; set; }
}