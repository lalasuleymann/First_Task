using System.Text.Json.Serialization;

namespace Task1_T.Models.Permissions
{
    public class PermissionGetAllResponse
    {
        [JsonPropertyName("permissions")]
        public List<PermissionDto> PermissionDtos { get; set; }

        public PermissionGetAllResponse()
        {
            PermissionDtos = new List<PermissionDto>();
        }

    }
}
