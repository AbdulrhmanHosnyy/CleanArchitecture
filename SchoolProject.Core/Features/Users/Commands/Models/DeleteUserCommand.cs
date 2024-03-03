using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Users.Commands.Models
{
    public record DeleteUserCommand(int Id) : IRequest<Response<string>>;

}
