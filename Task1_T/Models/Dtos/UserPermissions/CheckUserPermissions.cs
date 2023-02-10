namespace Task1_T.Models.Dtos.UserPermissions
{
    public class CheckUserPermissions
    {
        public string Email { get; set; }
        public List<CheckPermissionDto> Permissions { get; set; }
    }
}
