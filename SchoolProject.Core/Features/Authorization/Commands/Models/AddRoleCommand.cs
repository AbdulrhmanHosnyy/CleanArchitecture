using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authorization.Commands.Models
{
    public record AddRoleCommand(string RoleName) : IRequest<Response<string>>;

}
