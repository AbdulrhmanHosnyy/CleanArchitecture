using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.DTOs;

namespace SchoolProject.Core.Features.Authorization.Commands.Models
{
    public record EditRoleCommand : EditRoleDto, IRequest<Response<string>>
    {
        public EditRoleCommand(int Id, string Name) : base(Id, Name)
        {
        }
    }
}
