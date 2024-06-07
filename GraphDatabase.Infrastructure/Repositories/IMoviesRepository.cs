using GraphDatabase.Entities.Common;
using GraphDatabase.Entities.Domain;
using GraphDatabase.Entities.ViewModels;

namespace GraphDatabase.Infrastructure.Repositories;

public interface IMoviesRepository
{
    Task<bool> CreateMovie(Movie movie);

    Task<bool> ActedIn(string? personName, string? movieName);

    Task<bool> MakeFriend(string? personName1, string? personName2);
}