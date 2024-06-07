namespace GraphDatabase.Infrastructure.Repositories;

public class MoviesRepository(DriverLifeCycle driver) : IMoviesRepository
{
    private IDriver _driver = driver.Driver;

    public async Task<bool> CreateMovie(Movie movie)
    {
        var session = _driver.AsyncSession();

        try
        {
            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync("CREATE (a:Movie {title: $title})",
                    new { title = movie.Title });
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

    public async Task<bool> ActedIn(string? personName, string? movieName)
    {
        var session = _driver.AsyncSession();
        try
        {
            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(
                    @"MATCH (person:Person {name: $personName})
                 MATCH (movie:Movie {title: $movieName})
                 CREATE (person)-[:ACTED_IN]->(movie)",
                    new { personName, movieName });
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

    public async Task<bool> MakeFriend(string? name1, string? name2)
    {
        var session = _driver.AsyncSession();

        try
        {
            await session.ExecuteWriteAsync(async tx =>
            {
                await tx.RunAsync(@"MATCH (a:Person {name: $name1})
                 MATCH (b:Person {name: $name2})
                 MERGE (a)-[:KNOWS]->(b)",
                    new { name1, name2 });
            });

            await session.CloseAsync();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}