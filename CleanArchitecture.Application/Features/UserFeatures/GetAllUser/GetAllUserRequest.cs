using MediatR;

namespace CleanArchitecture.Application.Features.UserFeatures.GetAllUser;

public sealed record GetAllUserRequest : IRequest<List<GetAllUserResponse>>;