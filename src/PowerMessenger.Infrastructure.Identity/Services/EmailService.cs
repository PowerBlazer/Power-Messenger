using System.Text.RegularExpressions;
using PowerMessenger.Application.Layers.Identity.Services;
using PowerMessenger.Infrastructure.Identity.Interfaces;

namespace PowerMessenger.Infrastructure.Identity.Services;

public class EmailService: IEmailService
{
    private readonly IIdentityUserRepository _identityUserRepository;

    public EmailService(IIdentityUserRepository identityUserRepository)
    {
        _identityUserRepository = identityUserRepository;
    }

    public async Task<bool> ContainEmailAsync(string email)
    {
        var user = await _identityUserRepository.GetUserByEmailAsync(email);

        return user is not null;
    }

    public bool ValidationEmail(string email)
    {
        var regex = 
            new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        
        return regex.IsMatch(email);
    }
}