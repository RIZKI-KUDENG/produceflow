using ProduceFlow.Domain.Entities;
using MediatR;
using ProduceFlow.Application.Interfaces;
using ProduceFlow.Application.DTOs.Auth;

namespace ProduceFlow.Application.UseCases.Users.Queries.GetUser;
public record GetUsersQuery(string? Search) : IRequest<IEnumerable<UserResponse>>;
public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUsersAsync(request.Search);
    }
}