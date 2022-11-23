using System.Text.Json.Serialization;

namespace Task1_T.Models.Dtos.UserPermissions
{
    public class UserPermissiongetResponse :BaseDto
    {
        [JsonPropertyName("userPermission")]
        public UserPermissionDto UserPermissionDto { get; set; }
        
    }
}
