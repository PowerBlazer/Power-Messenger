using JetBrains.Annotations;

namespace PowerMessenger.Domain.Common;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
// ReSharper disable once InconsistentNaming
public class TActionResult<T>
{
    public T? Result { get; set; }
    public Dictionary<string,List<string>>? Errors { get; set; }
}