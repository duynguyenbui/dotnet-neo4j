namespace GraphDatabase.API.Extensions;

public static class Extension
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = builder.Configuration.GetValue<string>("ApplicationName"),
                Description = "Dotnet Core Neo4j Database Project API"
            });
        });

        // DataAccess Neo4j

        services.RegisterDataAccessDependencies();
        builder.Services.AddOptions<ApplicationSettings>()
            .BindConfiguration("ApplicationSettings");

        // Mediator
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
            cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
        });

        // IValidator Registration
        services.AddSingleton<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();
        services.AddSingleton<IValidator<CreateMovieCommand>, CreateMovieCommandValidator>();
        services.AddSingleton<IValidator<CreateFriendCommand>, CreateFriendCommandValidator>();

        // Scoped Services
        services.AddScoped<IGraphDatabaseQueries, GraphDatabaseQueries>();
    }
}