using GraphDatabase.Infrastructure.Repositories;
using MediatR;

namespace GraphDatabase.API.Application.Commands;

public class CreateFriendCommandHandler(IMoviesRepository repository) : IRequestHandler<CreateFriendCommand, bool>
{
    public async Task<bool> Handle(CreateFriendCommand request, CancellationToken cancellationToken)
    {
        return await repository.MakeFriend(request.PersonName, request.FriendName);
    }
}