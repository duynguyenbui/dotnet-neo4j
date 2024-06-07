

namespace GraphDatabase.Infrastructure.Repositories;

public class PersonRepository(DriverLifeCycle driver, ILogger<PersonRepository> logger) : IPersonRepository
{
    private IDriver _driver = driver.Driver;

    public async Task<List<Dictionary<string, object>>> SearchPersonsByName(string searchString)
    {
        var query = @"MATCH (p:Person) WHERE toUpper(p.name) CONTAINS toUpper($searchString) 
                                RETURN p{ name: p.name, born: p.born } ORDER BY p.Name LIMIT 5";

        var parameters = new Dictionary<string, object> { { "searchString", searchString } };

        var persons =
            await ExecuteReadTransactionAsync<Dictionary<string, object>>(_driver.AsyncSession(), query, "p",
                parameters);

        return persons;
    }

    public async Task<bool> AddPerson(Person person)
    {
        var session = _driver.AsyncSession();

        try
        {
            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync("CREATE (a:Person {name: $name, born: $born})",
                    new { name = person.Name, born = person.Born });
            });
            await session.CloseAsync();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<List<string>> GetPeople()
    {
        await using var session = _driver.AsyncSession();
        var query = @"MATCH (a:Person) RETURN a.name AS name ORDER BY a.name";

        var results = await ExecuteReadTransactionAsync<string>(session, query, "name", null);
        return results ?? [];
    }

    private async Task<List<T>> ExecuteReadTransactionAsync<T>(IAsyncSession session, string query,
        string returnObjectKey,
        IDictionary<string, object>? parameters)
    {
        try
        {
            parameters ??= new Dictionary<string, object>();

            var result = await session.ReadTransactionAsync(async tx =>
            {
                var data = new List<T>();
                var res = await tx.RunAsync(query, parameters);
                var records = await res.ToListAsync();

                data = records.Select(x => (T)x[returnObjectKey]).ToList();
                return data;
            });

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "There was an exception while making database query");
            throw;
        }
    }
}