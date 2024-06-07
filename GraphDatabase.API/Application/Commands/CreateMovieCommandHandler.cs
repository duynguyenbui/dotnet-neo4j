using GraphDatabase.Entities.Domain;
using GraphDatabase.Infrastructure.Repositories;
using MediatR;

namespace GraphDatabase.API.Application.Commands;

public class CreateMovieCommandHandler(IMoviesRepository repository) : IRequestHandler<CreateMovieCommand, bool>
{
    public async Task<bool> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.CreateMovie(new Movie() { Title = request.Title });

        return result;
    }
}