using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler,
        IRequestHandler<AddStudentCommand, Response<string>>,
        IRequestHandler<EditStudentCommand, Response<string>>,
        IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        #endregion

        #region Constructors
        public StudentCommandHandler(IStudentService studentService, IMapper mapper,
           IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapper = _mapper.Map<Student>(request);
            var result = await _studentService.AddAsync(studentMapper);
            if (result == "Success") return Created("");
            return BadRequest<string>();
        }
        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdWithIncludeAsync(request.Id);
            if (student is null) return NotFound<string>("Studnet is not found!");
            var studentMapper = _mapper.Map(request, student);
            var result = await _studentService.EditAsync(studentMapper);
            if (result == "Success") return Success($"Student With Id : {studentMapper.StudID} Edited Successfully ");
            return BadRequest<string>();
        }
        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetByIdAsync(request.Id);
            if (student is null) return NotFound<string>("Studnet is not found!");
            var result = await _studentService.DeleteAsync(student);
            if (result == "Success") return Deleted<string>($"Student With Id : {student.StudID} Deleted Successfully ");
            return BadRequest<string>();
        }
        #endregion

    }
}
