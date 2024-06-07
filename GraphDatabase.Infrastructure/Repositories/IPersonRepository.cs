namespace GraphDatabase.Infrastructure.Repositories;

public interface IPersonRepository
{
    Task<bool> AddPerson(Person person);

    Task<List<string>> GetPeople();

    Task<List<Dictionary<string, object>>> SearchPersonsByName(string searchString);
}