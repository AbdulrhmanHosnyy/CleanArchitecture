﻿using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Emails.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Emails.Commands.Handlers
{
    public class EmaileCommandHandler : ResponseHandler,
        IRequestHandler<SendEmailCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IEmailsService _emailsService;
        #endregion

        #region Constructors
        public EmaileCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
           IEmailsService emailsService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _emailsService = emailsService;
        }

        #endregion
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailsService.SendEmail(request.Email, request.Message);
            if (response == "Success") return Success<string>("");
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SendEmailFailed]);
        }
        #region Handle Functions

        #endregion

    }
}
