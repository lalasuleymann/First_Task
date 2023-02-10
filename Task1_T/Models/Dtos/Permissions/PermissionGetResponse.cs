using System.Text.Json.Serialization;

namespace Task1_T.Models.Permissions
{
    public class PermissionGetResponse
    {
        [JsonPropertyName("permission")]
        public PermissionDto PermissionDto { get; set; }
    }
}
