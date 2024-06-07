using MediatR;

namespace GraphDatabase.API.Application.Commands;

public class CreateFriendCommand : IRequest<bool>
{
    public string PersonName { get; set; }
    public string FriendName { get; set; }
}