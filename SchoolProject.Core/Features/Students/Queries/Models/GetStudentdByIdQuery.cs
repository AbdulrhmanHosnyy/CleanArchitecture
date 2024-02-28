using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Queries.Models
{
    public class GetStudentdByIdQuery : IRequest<Response<GetSingleStudentResponse>>
    {
        public int Id { get; set; }
        public GetStudentdByIdQuery(int id)
        {
            Id = id;
        }
    }
}
