namespace Task1_T.PermissionSet
{
    public struct PermissionNames
    {
        public const string Director = "DeleteDirector";
        public const string Default = "Permission";

        public struct Department
        {
            public const string GetAll = Default;
            public const string Get = Default + "/department/{departmentId}";
            public const string Update = Default + "/department/{departmentId}";
            public const string Delete = Default + "";
            public const string Create = Default + "/department";
        }

        public struct Position
        {
            public const string GetAll = Default;
            public const string Get = Default + "/position/{positionId}";
            public const string Update = Default + "/position/{positionId}";
            public const string Delete = Default + "/position/{positionId}";
            public const string Create = Default + "/position";
        }

        public struct Employee
        {
            public const string GetAll = Default;
            public const string Get = Default + "/employee/{employeeId}";
            public const string Update = Default + "/employee/{employeeId}";
            public const string Delete = Default + Director;
            public const string Create = Default + "/employee";
        }

        public struct UserPermission
        {
            public const string GetAll = Default;
            public const string Create = Default + "/userpermission";
        }

        public struct Manage
        {
            public const string GetDependentEmployees = Default + "/manageDependent";

            public const string GetManagerEmployees = Default + "/manageManager";
        }
    }
}
