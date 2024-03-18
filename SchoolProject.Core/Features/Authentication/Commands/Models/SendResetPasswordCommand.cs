using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Commands.Models
{
    public record SendResetPasswordCommand(string Email) : IRequest<Response<string>>;

}
