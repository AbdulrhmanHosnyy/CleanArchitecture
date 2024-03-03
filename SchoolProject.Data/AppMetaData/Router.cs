namespace SchoolProject.Data.AppMetaData
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
    }
}
