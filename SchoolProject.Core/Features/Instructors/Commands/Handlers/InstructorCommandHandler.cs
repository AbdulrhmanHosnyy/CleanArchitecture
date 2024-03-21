using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Instructors.Commands.Handlers
{
    public class InstructorCommandHandler : ResponseHandler,
        IRequestHandler<AddInstructorCommand, Response<string>>
    {
        #region Fields
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public InstructorCommandHandler(IInstructorService instructorService, IMapper mapper,
           IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _instructorService = instructorService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = _mapper.Map<Instructor>(request);
            var result = await _instructorService.AddInstructorAsync(instructor, request.Image);
            switch (result)
            {
                case "NoImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
                case "FailedToUploadImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUploadImage]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);
            }
            return Success("");
        }
        #endregion
    }
}