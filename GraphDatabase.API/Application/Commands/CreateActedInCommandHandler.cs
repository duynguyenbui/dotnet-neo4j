

namespace GraphDatabase.API.Application.Commands;

public class CreateActedInCommandHandler(IMoviesRepository repository) : IRequestHandler<CreateActedInCommand, bool>
{
    public async Task<bool> Handle(CreateActedInCommand request, CancellationToken cancellationToken)
    {
        return await repository.ActedIn(request.PersonName, request.MovieName);
    }
}