using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler : ResponseHandler,
        IRequestHandler<GetStudentsListQuery, Response<List<GetStudentsListResponse>>>,
        IRequestHandler<GetStudentdByIdQuery, Response<GetSingleStudentResponse>>,
        IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public StudentQueryHandler(IStudentService studentService, IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Functions Handling
        public async Task<Response<List<GetStudentsListResponse>>> Handle(GetStudentsListQuery request, CancellationToken cancellationToken)
        {
            var studentsList = await _studentService.GetStudentsListAsync();
            var studentsListMapper = _mapper.Map<List<GetStudentsListResponse>>(studentsList);
            var result = Success(studentsListMapper);
            result.Meta = new { Count = studentsList.Count() };
            return result;
        }
        public async Task<Response<GetSingleStudentResponse>> Handle(GetStudentdByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdWithIncludeAsync(request.Id);
            if (student == null) return NotFound<GetSingleStudentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var result = _mapper.Map<GetSingleStudentResponse>(student);
            return Success(result);
        }
        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedListResponse>> expression = e =>
                new GetStudentPaginatedListResponse(e.StudID, e.Localize(e.NameAr, e.NameEn), e.Address,
                e.Localize(e.Department.DNameAr, e.Department.DNameEn));

            var filterQuery = _studentService.FilterStudentPaginatedQuerable(request.OrderBy, request.Search);
            var paginatedList = await filterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            paginatedList.Meta = new { Count = paginatedList.Data.Count() };
            return paginatedList;
        }
        #endregion

    }
}
