using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Results;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
        }
        #endregion

        #region Functions
        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var (token, accessToken) = await GenerateJWTToken(user);


            var refreshToken = GetRefreshToken(user.UserName!);

            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = token.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id,
            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);

            var response = new JwtAuthResult
            {
                AccessToken = accessToken,
                refreshToken = refreshToken
            };
            return response;
        }
        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            var claims = await GetClaims(user);

            var token = new JwtSecurityToken(_jwtSettings.Issuer,
                                             _jwtSettings.Audience,
                                             claims,
                                             expires:
                                             DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                                             signingCredentials: new SigningCredentials(new SymmetricSecurityKey
                                             (Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return (token, accessToken);
        }
        private RefreshToken GetRefreshToken(string userName)
        {
            var refreshToken = new RefreshToken
            {
                UserName = userName,
                TokenString = GenerateRefreshToken(),
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate)
            };

            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public async Task<List<Claim>> GetClaims(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(nameof(UserClaims.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaims.Id), user.Id.ToString()),
            };
            foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role, role));

            var userCalims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userCalims);

            return claims;
        }
        public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken token, DateTime? expiryDate, string refreshToken)
        {
            var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);
            var respone = new JwtAuthResult();
            respone.AccessToken = newToken;
            var refreshTokenDto = new RefreshToken();
            refreshTokenDto.UserName = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaims.UserName)).Value;
            refreshTokenDto.TokenString = refreshToken;
            refreshTokenDto.ExpireAt = (DateTime)expiryDate;
            respone.refreshToken = refreshTokenDto;

            return respone;
        }
        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var respone = handler.ReadJwtToken(accessToken);
            return respone;
        }
        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validToken);

                if (validator is null)
                {
                    return "Invalid Token";
                }
                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("WrongAlgorithm", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            }

            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaims.Id)).Value;
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking().
                FirstOrDefaultAsync(x => x.Token == accessToken && x.RefreshToken == refreshToken
                && x.UserId == int.Parse(userId));

            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("RefreshTokenIsExpired", null);
            }
            return (userId, userRefreshToken.ExpiryDate);
        }

        #endregion
    }
}

