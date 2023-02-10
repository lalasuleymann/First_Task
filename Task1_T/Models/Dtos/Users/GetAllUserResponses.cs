using System.Text.Json.Serialization;
using Task1_T.Models.Departments;

namespace Task1_T.Models.Dtos.Users
{
    public class GetAllUserResponses
    {
        [JsonPropertyName("users")]
        public ICollection<UserDto> UserDtos { get; set; }

        public GetAllUserResponses()
        {
            UserDtos = new List<UserDto>();
        }
    }
}
