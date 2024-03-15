using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Features.Authentication.Commands.Models
{
    public record SignInCommand(string UserName, string Password) : IRequest<Response<JwtAuthResult>>;

}
