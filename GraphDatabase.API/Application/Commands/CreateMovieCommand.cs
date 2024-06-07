

namespace GraphDatabase.API.Application.Commands;

public class CreateMovieCommand : IRequest<bool>
{
    [DataMember] public string? Title { get; set; }
}