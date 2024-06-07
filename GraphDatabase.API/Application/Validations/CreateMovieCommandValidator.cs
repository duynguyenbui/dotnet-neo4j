namespace GraphDatabase.API.Application.Validations;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator(ILogger<CreateMovieCommandValidator> logger)
    {
        RuleFor(command => command.Title).NotEmpty();
        
        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}