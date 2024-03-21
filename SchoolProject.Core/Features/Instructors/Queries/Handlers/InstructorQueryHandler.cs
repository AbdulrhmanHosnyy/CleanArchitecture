using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructors.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Instructors.Queries.Handlers
{
    public class InstructorQueryHandler : ResponseHandler,
        IRequestHandler<GetInstructorSalarySummationQuery, Response<decimal>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IInstructorService _instructorService;
        #endregion

        #region Constructors
        public InstructorQueryHandler(IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer,
            IInstructorService instructorService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _instructorService = instructorService;
        }

        #endregion

        #region Functions Handling
        public async Task<Response<decimal>> Handle(GetInstructorSalarySummationQuery request, CancellationToken cancellationToken)
        {
            var result = await _instructorService.GetInstructorSalarySummation();
            return Success(result);
        }
        #endregion

    }
}
