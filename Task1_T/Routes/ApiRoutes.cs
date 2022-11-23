namespace Task1_T.Routes
{
    public struct ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public struct Department
        {
            public const string GetAll = Base + "/department";
            public const string Get = Base + "/department/{departmentId}";
            public const string Update = Base + "/department/{departmentId}";
            public const string Delete = Base + "/department/{departmentId}";
            public const string Create = Base + "/department";
        }

        public struct Position
        {
            public const string GetAll = Base + "/position";
            public const string Get = Base + "/position/{positionId}";
            public const string Update = Base + "/position/{positionId}";
            public const string Delete = Base + "/position/{positionId}";
            public const string Create = Base + "/position";
        }

        public struct Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
        }

        public struct Employee
        {
            public const string GetAll = Base + "/employee";
            public const string Get = Base + "/employee/{employeeId}";
            public const string Update = Base + "/employee/{employeeId}";
            public const string Delete = Base + "/employee/{employeeId}";
            public const string Create = Base + "/employee";
        }

        public struct UserPermission
        {
            public const string GetAll = Base + "/userpermission";
            public const string Create = Base + "/userpermission";
        }

        public struct Manage
        {
            public const string GetDependentEmployees = Base + "/manageDependent";

            public const string GetManagerEmployees = Base + "/manageManager";
        }
    }
}
