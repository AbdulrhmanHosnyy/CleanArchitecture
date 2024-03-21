using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Departments.Queries.Responses;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Service.Abstracts;
using Serilog;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Departments.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler,
        IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>,
        IRequestHandler<GetDepartmentStudentCountQuery, Response<List<GetDepartmentStudentCountResponse>>>,
        IRequestHandler<GetDepartmentStudentCountByIdQuery, Response<GetDepartmentStudentCountByIdResponse>>

    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public DepartmentQueryHandler(IDepartmentService departmentService, IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer, IStudentService studentService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _departmentService = departmentService;
            _studentService = studentService;
        }
        #endregion

        #region Functions
        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            //  Getting the service
            var department = await _departmentService.GetDepartmentByIdAsync(request.Id);

            if (department is null) return NotFound<GetDepartmentByIdResponse>
                    (_stringLocalizer[SharedResourcesKeys.NotFound]);

            //  Mapping
            var response = _mapper.Map<GetDepartmentByIdResponse>(department);

            //  Paginated Student List
            Expression<Func<Student, GetDepartmentByIdStudentsResponse>> expression = e =>
                new GetDepartmentByIdStudentsResponse(e.StudID, e.Localize(e.NameAr, e.NameEn));
            var studentList = _studentService.GetStudentsByDepartmentIdQueryable(request.Id);
            var paginatedList = await studentList.Select(expression)
                .ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            response.StudentsL = paginatedList;

            Log.Information($"Get Department by Id {request.Id}");
            return Success(response);
        }

        public async Task<Response<List<GetDepartmentStudentCountResponse>>> Handle(GetDepartmentStudentCountQuery request, CancellationToken cancellationToken)
        {
            var viewDepartmentResult = await _departmentService.GetViewDepartmentData();
            var result = _mapper.Map<List<GetDepartmentStudentCountResponse>>(viewDepartmentResult);
            return Success(result);
        }

        public async Task<Response<GetDepartmentStudentCountByIdResponse>> Handle(GetDepartmentStudentCountByIdQuery request, CancellationToken cancellationToken)
        {
            var parameters = _mapper.Map<DepartmentStudentCountProcedureParameters>(request);
            var procResult = await _departmentService.GetDepartmentStudentCountProcedure(parameters);
            var result = _mapper.Map<GetDepartmentStudentCountByIdResponse>(procResult.FirstOrDefault());
            return Success(result);

        }
        #endregion

    }
}
