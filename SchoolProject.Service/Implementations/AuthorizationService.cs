using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Service.Abstracts;
using System.Security.Claims;

namespace SchoolProject.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _appDbContext;
        #endregion

        #region Constructors
        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager,
            AppDbContext appDbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _appDbContext = appDbContext;
        }
        #endregion

        #region Functions
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded) return "Success";
            return "Failed";
        }

        public async Task<bool> IsRoleExistByName(string roleName) => await _roleManager.RoleExistsAsync(roleName);
        public async Task<string> EditRoleAsync(EditRoleDto editRoleDto)
        {
            var role = await _roleManager.FindByIdAsync(editRoleDto.Id.ToString());
            if (role is null) return "NotFound";
            role.Name = editRoleDto.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return "Success";
            var errors = string.Join("-", result.Errors);
            return errors;
        }
        public async Task<bool> IsRoleExistById(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            return role != null;
        }
        public async Task<string> DeleteRoleAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return "NotFound";

            var users = await _userManager.GetUsersInRoleAsync(role.Name);

            if (users != null && users.Count() > 0) return "Used";

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded) return "Success";

            var errors = string.Join("-", result.Errors);
            return errors;
        }
        public async Task<List<Role>> GetRolesList() => await _roleManager.Roles.ToListAsync();
        public async Task<Role> GetRoleById(int id) => await _roleManager.FindByIdAsync(id.ToString());
        public async Task<ManageUserRolesResponse> ManageUserRolesData(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var roles = await _roleManager.Roles.ToListAsync();

            var response = new ManageUserRolesResponse();
            response.UserId = user.Id;

            var rolesList = new List<UserRoles>();
            foreach (var role in roles)
            {
                var userRole = new UserRoles();
                userRole.Id = role.Id;
                userRole.Name = role.Name;
                if (userRoles.Contains(role.Name))
                    userRole.HasRole = true;
                rolesList.Add(userRole);
            }

            response.Roles = rolesList;

            return response;
        }
        public async Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
        {
            var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user is null) return "UserIsNull";

                var userRoles = await _userManager.GetRolesAsync(user);

                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded) return "FailedToRemoveRoles";

                var selectedRoles = request.Roles.Where(x => x.HasRole == true).Select(x => x.Name);
                var addedResult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!addedResult.Succeeded) return "FailedToAddRoles";

                await transaction.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return "Failed";
                throw;
            }
        }
        public async Task<ManageUserClaimsResponse> ManageUserClaimsData(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var response = new ManageUserClaimsResponse();
            response.UserId = user.Id;

            var claimsList = new List<UserClaim>();
            foreach (var claim in ClaimsStore.claims)
            {
                var userClaim = new UserClaim();
                userClaim.Type = claim.Type;
                if (userClaims.Any(x => x.Type == claim.Type))
                    userClaim.Value = true;
                claimsList.Add(userClaim);
            }

            response.Claims = claimsList;

            return response;
        }
        public async Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
        {
            var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user is null) return "UserIsNull";

                var userClaims = await _userManager.GetClaimsAsync(user);

                var removeResult = await _userManager.RemoveClaimsAsync(user, userClaims);
                if (!removeResult.Succeeded) return "FailedToRemoveClaims";

                var selectedClaims = request.Claims.Where(x => x.Value == true)
                    .Select(x => new Claim(x.Type, x.Value.ToString()));
                var addedResult = await _userManager.AddClaimsAsync(user, selectedClaims);
                if (!addedResult.Succeeded) return "FailedToAddClaims";

                await transaction.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return "Failed";
                throw;
            }
        }

        #endregion
    }
}
