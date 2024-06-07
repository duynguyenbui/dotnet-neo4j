using GraphDatabase.Infrastructure.Repositories;

namespace GraphDatabase.API.Application.Queries;

public class GraphDatabaseQueries(
    IPersonRepository personRepository,
    IMoviesRepository moviesRepository,
    ILogger<GraphDatabaseQueries> logger) : IGraphDatabaseQueries
{
    public async Task<List<string>> GetPeople()
    {
        logger.LogInformation("Getting all people");
        return await personRepository.GetPeople();
    }

    public async Task<List<Dictionary<string, object>>> SearchPersonsByName(string searchString)
    {
        return await personRepository.SearchPersonsByName(searchString) ?? [];
    }
}