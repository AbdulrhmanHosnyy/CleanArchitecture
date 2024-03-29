﻿namespace SchoolProject.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root + "/" + version + "/";
        public const string SingleRoute = "{id}";
        public static class StudentRouting
        {
            public const string prefix = Rule + "Student/";
            public const string List = prefix + "List";
            public const string GetById = prefix + "GetById/" + SingleRoute;
            public const string Create = prefix + "Create";
            public const string Edit = prefix + "Edit";
            public const string Delete = prefix + "Delete/" + SingleRoute;
            public const string Paginated = prefix + "Paginated";
        }
        public static class DepartmentRouting
        {
            public const string prefix = Rule + "Department/";
            public const string List = prefix + "List";
            public const string GetById = prefix + "GetById";
            public const string Create = prefix + "Create";
            public const string Edit = prefix + "Edit";
            public const string Delete = prefix + "Delete/" + SingleRoute;
            public const string Paginated = prefix + "Paginated";
            public const string GetDepartmentStudentsCount = prefix + "GetDepartmentStudentsCount";
            public const string GetDepartmentStudentsCountById = prefix + "GetDepartmentStudentsCountById/";
        }
        public static class UserRouting
        {
            public const string prefix = Rule + "User/";
            public const string List = prefix + "List";
            public const string GetById = prefix + "GetById";
            public const string Create = prefix + "Create";
            public const string Edit = prefix + "Edit";
            public const string Delete = prefix + "Delete/" + SingleRoute;
            public const string Paginated = prefix + "Paginated";
            public const string ChangePassword = prefix + "ChangePassword";
        }
        public static class AuthenticationRouting
        {
            public const string prefix = Rule + "Authentication/";
            public const string SignIn = prefix + "SignIn";
            public const string RefreshToken = prefix + "RefreshToken";
            public const string ValidateToken = prefix + "ValidateToken";
            public const string ConfirmEmail = "/Api/Authentication/ConfirmEmail";
            public const string SendResetPassword = prefix + "SendResetEmail";
            public const string ConfirmResetPassword = prefix + "ConfirmResetPassword";
            public const string ResetPassword = prefix + "ResetPassword";
        }
        public static class AuthorizationRouting
        {
            public const string prefix = Rule + "Authorization/";
            public const string roles = Rule + "Roles/";
            public const string claims = Rule + "Claims/";
            public const string Create = roles + "Role/Create";
            public const string Edit = roles + "Role/Edit";
            public const string Delete = roles + "Role/Delete/" + SingleRoute;
            public const string RoleList = roles + "RoleList";
            public const string GetRoleById = roles + "GetRoleById/" + SingleRoute;
            public const string ManageUserRoles = roles + "ManageUserRoles/" + SingleRoute;
            public const string UpdateUserRoles = roles + "UpdateUserRoles";
            public const string ManageUserClaims = claims + "ManageUserClaims/" + SingleRoute;
            public const string UpdateUserClaims = claims + "UpdateUserClaims/";
        }

        public static class EmailsRouting
        {
            public const string prefix = Rule + "Email/";
            public const string SendEmail = prefix + "SendEmail";

        }
        public static class InstructortRouting
        {
            public const string prefix = Rule + "Instructort/";
            public const string GetInstructorSalarySummation = prefix + "GetInstructorSalarySummation";
            public const string Create = prefix + "Create";

        }

    }
}
