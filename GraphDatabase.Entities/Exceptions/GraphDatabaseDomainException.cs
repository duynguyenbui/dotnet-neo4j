namespace GraphDatabase.Entities.Exceptions;

/// <summary>
/// Exception type for domain exceptions
/// </summary>
public class GraphDatabaseDomainException : Exception
{
    public GraphDatabaseDomainException()
    {
    }

    public GraphDatabaseDomainException(string message)
        : base(message)
    {
    }

    public GraphDatabaseDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}