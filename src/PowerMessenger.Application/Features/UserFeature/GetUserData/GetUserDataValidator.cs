using FluentValidation;
using PowerMessenger.Application.Layers.Shared.Services;

namespace PowerMessenger.Application.Features.UserFeature.GetUserData;

public class GetUserDataValidator: AbstractValidator<GetUserDataQuery>
{
    public GetUserDataValidator()
    {
        RuleFor(p => p.UserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Поле не может быть пустым");
        // .MustAsync(async (userId, _) => await userService.ContainUserById(userId))
        // .WithMessage("Такого пользователя не сущесвует");
    }
}