namespace GraphDatabase.API.Application.Validations;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator(ILogger<CreatePersonCommandValidator> logger)
    {
        RuleFor(command => command.Born).NotEmpty().Must(BeValidYear)
            .WithMessage("Please specify a valid year");
        RuleFor(command => command.Name).NotEmpty();
        
        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }

    private bool BeValidYear(int? born)
    {
        if (born == null) return false;
        
        return born <= DateTime.Now.Year;
    }
}