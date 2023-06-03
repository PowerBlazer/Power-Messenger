
namespace PowerMessenger.Domain.RepositoryTransferObjects.MessageRepository;

public class LastMessage
{
    public string? Content { get; set; }
    public required string Type { get; set; }
    public DateTime DateCreate { get; set; }
}