using System.Text.Json.Serialization;
using Task1_T.Models.Departments;

namespace Task1_T.Models.Dtos.UserPermissions
{
    public class UserPermissiongetAllResponse 
    {
        [JsonPropertyName("userpermissions")]
        public ICollection<UserPermissionDto> UserPermissionDtos { get; set; }

        public UserPermissiongetAllResponse()
        {
            UserPermissionDtos = new List<UserPermissionDto>();
        }
    }
}
