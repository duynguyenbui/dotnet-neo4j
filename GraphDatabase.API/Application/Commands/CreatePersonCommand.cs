using System.Runtime.Serialization;
using MediatR;

namespace GraphDatabase.API.Application.Commands;

public class CreatePersonCommand : IRequest<bool>
{
    [DataMember] public int? Born { get; set; }
    [DataMember] public string? Name { get; set; }
}