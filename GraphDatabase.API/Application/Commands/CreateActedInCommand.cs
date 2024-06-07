namespace GraphDatabase.API.Application.Commands;

public class CreateActedInCommand : IRequest<bool>
{
    public string? PersonName { get; set; }
    public string? MovieName { get; set; }
}