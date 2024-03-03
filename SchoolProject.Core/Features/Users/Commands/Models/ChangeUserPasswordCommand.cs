using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Users.Commands.Models
{
    public record ChangeUserPasswordCommand(int Id, string CurrentPassword, string NewPassword, string ConfirmPassword)
        : IRequest<Response<string>>;
}
