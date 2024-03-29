﻿using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.AuthenticationRouting.SignIn)]
        public async Task<IActionResult> Create([FromForm] SignInCommand signInCommand)
        {
            var response = await Mediator.Send(signInCommand);
            return NewResult(response);
        }
        [HttpPost(Router.AuthenticationRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand refreshTokenCommand)
        {
            var response = await Mediator.Send(refreshTokenCommand);
            return NewResult(response);
        }
        [HttpGet(Router.AuthenticationRouting.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery authorizeUserQuery)
        {
            var response = await Mediator.Send(authorizeUserQuery);
            return NewResult(response);
        }
        [HttpGet(Router.AuthenticationRouting.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery confirmEmailQuery)
        {
            var response = await Mediator.Send(confirmEmailQuery);
            return NewResult(response);
        }
        [HttpPost(Router.AuthenticationRouting.SendResetPassword)]
        public async Task<IActionResult> SendResetPassword([FromQuery] SendResetPasswordCommand sendResetPasswordCommand)
        {
            var response = await Mediator.Send(sendResetPasswordCommand);
            return NewResult(response);
        }
        [HttpGet(Router.AuthenticationRouting.ConfirmResetPassword)]
        public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordQuery confirmResetPasswordQuery)
        {
            var response = await Mediator.Send(confirmResetPasswordQuery);
            return NewResult(response);
        }
        [HttpPost(Router.AuthenticationRouting.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand resetPasswordCommand)
        {
            var response = await Mediator.Send(resetPasswordCommand);
            return NewResult(response);
        }
    }
}
