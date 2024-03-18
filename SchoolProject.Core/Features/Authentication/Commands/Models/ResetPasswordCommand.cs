using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Commands.Models
{
    public record ResetPasswordCommand(string Email, string NewPassword, string ConfirmPassword) :
        IRequest<Response<string>>;
}
