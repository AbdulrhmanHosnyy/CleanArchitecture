﻿using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Core.Features.Authentication.Commands.Models
{
    public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<Response<JwtAuthResult>>;

}
