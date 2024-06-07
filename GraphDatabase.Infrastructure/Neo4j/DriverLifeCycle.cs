using GraphDatabase.Entities.Common;
using Microsoft.Extensions.Options;
using Neo4j.Driver;

namespace GraphDatabase.Infrastructure.Neo4j;

public class DriverLifeCycle : IDisposable
{
    private readonly IOptions<ApplicationSettings> _appSettings;

    public DriverLifeCycle(IOptions<ApplicationSettings> appSettings)
    {
        var options = appSettings.Value;
        Driver = global::Neo4j.Driver.GraphDatabase.Driver(options.Neo4jConnection,
            AuthTokens.Basic(options.Neo4jUser, options.Neo4jPassword));
    }

    public IDriver Driver { get; }

    public void Dispose()
    {
        Driver?.Dispose();
    }
}