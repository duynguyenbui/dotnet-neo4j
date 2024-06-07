namespace GraphDatabase.API.Application.Queries;

public interface IGraphDatabaseQueries
{
    Task<List<string>> GetPeople();
    Task<List<Dictionary<string, object>>> SearchPersonsByName(string searchString);
}