using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace PowerMessenger.Domain.Entities.Abstractions;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class BaseEntity<T>
{
    [Key]
    public T? Id { get; set; }
}