

namespace GraphDatabase.Infrastructure.Services;

public static class DependencyRegistration
{
    /// <summary>
    /// Registers the data access dependencies.
    /// </summary>
    public static void RegisterDataAccessDependencies(this IServiceCollection services)
    {
        services.AddScoped<DriverLifeCycle>();

        services.AddTransient<IPersonRepository, PersonRepository>();
        services.AddTransient<IMoviesRepository, MoviesRepository>();
    }
}