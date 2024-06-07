using GraphDatabase.API.Application.Commands;
using GraphDatabase.API.Extensions;
using GraphDatabase.API.Services;
using GraphDatabase.Entities.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GraphDatabase.API.Apis;

public static class GraphDatabaseApi
{
    public static RouteGroupBuilder MapGraphDatabaseApi(this IEndpointRouteBuilder app)
    {
        const string apiVersion = "v1";

        var api = app.MapGroup($"api/{apiVersion}/graph-database");

        // Person API Routes
        api.MapGet("/persons/get-all", GetPeople);
        api.MapGet("/persons/searchBy/{searchString:minlength(1)}", SearchPeopleByName);
        api.MapPost("/persons/add", AddPerson);
        api.MapPost("/persons/make-friend", MakeFriend);

        // Movie API Routes
        api.MapPost("/movies/add", AddMovie);
        api.MapPost("/movies/acted-in", ActedIn);

        // Error API Routes
        api.MapGet("/error", () => { throw new NotImplementedException(); });
        return api;
    }

    public static async Task<Ok<List<Dictionary<string, object>>>> SearchPeopleByName(
        [AsParameters] GraphDatabaseServices services, string searchString)
    {
        return TypedResults.Ok(await services.GraphDatabaseQueries.SearchPersonsByName(searchString));
    }

    public static async Task<Ok<List<string>>> GetPeople([AsParameters] GraphDatabaseServices services)
    {
        var result = await services.GraphDatabaseQueries.GetPeople();

        return TypedResults.Ok(result);
    }

    public static async Task<Results<Created, BadRequest<string>>> MakeFriend(
        [AsParameters] GraphDatabaseServices services,
        [FromBody] CreateFriendCommand command
    )
    {
        services.Logger.LogInformation(
            "Sending command: {CommandName} - {PersonName} - {FriendName}", command.GetGenericTypeName(),
            command.PersonName, command.FriendName);

        var send = await services.Mediator.Send(command);

        if (send) return TypedResults.Created();

        return TypedResults.BadRequest("Something went wrong when making friend");
    }

    public static async Task<Results<Created, BadRequest<string>>> ActedIn(
        [AsParameters] GraphDatabaseServices services,
        [FromBody] CreateActedInCommand command)
    {
        services.Logger.LogInformation(
            "Sending command: {CommandName} - {PersonName} - {MovieName}", command.GetGenericTypeName(),
            command.PersonName, command.MovieName);

        var result = await services.Mediator.Send(command);

        if (result) return TypedResults.Created();

        return TypedResults.BadRequest("Something went wrong when acting in");
    }

    public static async Task<Results<Created, BadRequest<string>>> AddMovie(
        [AsParameters] GraphDatabaseServices services,
        [FromBody] CreateMovieCommand command)
    {
        services.Logger.LogInformation(
            "Sending command: {CommandName} - {Title}", command.GetGenericTypeName(), command.Title);

        var result = await services.Mediator.Send(command);

        if (result) return TypedResults.Created();

        return TypedResults.BadRequest("Something went wrong when creating movie");
    }

    public static async Task<Results<Created, BadRequest<string>>> AddPerson(
        [AsParameters] GraphDatabaseServices services,
        [FromBody] CreatePersonCommand command)
    {
        services.Logger.LogInformation(
            "Sending command: {CommandName} - {Name}: {Born} ({@Command})", command.GetGenericTypeName(),
            nameof(command.Name),
            command.Born,
            command);

        var result = await services.Mediator.Send(command);

        if (result) return TypedResults.Created();

        return TypedResults.BadRequest("Something went wrong when creating person");
    }
}