namespace GraphDatabase.API.Application.Commands;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, bool>
{
    private readonly IPersonRepository _personRepository;

    public CreatePersonCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<bool> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = new Person { Born = request.Born, Name = request.Name };

        var result = await _personRepository.AddPerson(person);

        return result;
    }
}