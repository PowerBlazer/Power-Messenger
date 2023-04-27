using JetBrains.Annotations;
using PowerMessenger.Domain.Entities.Abstractions;

namespace PowerMessenger.Domain.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class MessageType : BaseEntity<long>
{
    public string Type { get; set; } = string.Empty;
    public ICollection<Message>? Messages { get; set; }
}