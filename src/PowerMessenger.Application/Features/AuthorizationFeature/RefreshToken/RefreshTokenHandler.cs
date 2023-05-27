using AutoMapper;
using MediatR;
using PowerMessenger.Application.DTOs.Authorization;
using PowerMessenger.Application.Layers.Identity.Services;

namespace PowerMessenger.Application.Features.AuthorizationFeature.RefreshToken;

public class RefreshTokenHandler: IRequestHandler<RefreshTokenCommand,RefreshTokenResult>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;

    public RefreshTokenHandler(IAuthorizationService authorizationService,
        IMapper mapper)
    {
        _authorizationService = authorizationService;
        _mapper = mapper;
    }

    public async Task<RefreshTokenResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenInput = _mapper.Map<RefreshTokenCommand, RefreshTokenInput>(request);

        var result = await _authorizationService.RefreshToken(refreshTokenInput);

        return result;
    }
}