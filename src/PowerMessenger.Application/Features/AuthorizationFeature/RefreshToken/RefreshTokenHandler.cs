using AutoMapper;
using MediatR;
using PowerMessenger.Application.Layers.Identity.Services;
using PowerMessenger.Domain.DTOs.Authorization;

namespace PowerMessenger.Application.Features.AuthorizationFeature.RefreshToken;

public class RefreshTokenHandler: IRequestHandler<RefreshTokenCommand,RefreshTokenResponse>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;

    public RefreshTokenHandler(IAuthorizationService authorizationService,
        IMapper mapper)
    {
        _authorizationService = authorizationService;
        _mapper = mapper;
    }

    public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenInput = _mapper.Map<RefreshTokenCommand, RefreshTokenRequest>(request);

        var result = await _authorizationService.RefreshToken(refreshTokenInput);

        return result;
    }
}