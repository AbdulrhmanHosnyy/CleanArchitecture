using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Users.Commands.Models
{
    public record AddUserCommand(string FullName, string UserName, string Password, string ConfirmPassword,
        string Email, string? Address, string? Country, string? PhoneNumber) : IRequest<Response<string>>;
}
