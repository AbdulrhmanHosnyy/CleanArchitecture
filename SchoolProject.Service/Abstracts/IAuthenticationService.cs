﻿using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(User user);
        public JwtSecurityToken ReadJWTToken(string accessToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken);
        public Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken token, DateTime? expiryDate, string refreshToken);
        public Task<string> ValidateToken(string accessToken);

    }
}
