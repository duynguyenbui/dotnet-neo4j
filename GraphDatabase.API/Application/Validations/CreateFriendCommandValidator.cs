namespace GraphDatabase.API.Application.Validations;

public class CreateFriendCommandValidator : AbstractValidator<CreateFriendCommand>
{
    public CreateFriendCommandValidator(ILogger<CreateFriendCommandValidator> logger)
    {
        RuleFor(command => command.PersonName).NotEmpty();
        RuleFor(command => command.FriendName).NotEmpty();

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}