using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Instructors.Commands.Models
{
    public record AddInstructorCommand(string? NameAr, string? NameEn, string? Address, string? Position, decimal? Salary,
        IFormFile? Image, int? SupervisorId, int DID) : IRequest<Response<string>>;

}
