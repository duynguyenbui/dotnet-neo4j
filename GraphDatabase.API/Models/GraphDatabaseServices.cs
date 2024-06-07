namespace GraphDatabase.API.Services;

public class GraphDatabaseServices(
    IMediator mediator,
    ILogger<GraphDatabaseServices> logger,
    IGraphDatabaseQueries graphDatabaseQueries)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<GraphDatabaseServices> Logger { get; } = logger;
    public IGraphDatabaseQueries GraphDatabaseQueries { get; } = graphDatabaseQueries;
}