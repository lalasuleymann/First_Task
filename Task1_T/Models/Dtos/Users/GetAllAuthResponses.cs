using System.Text.Json.Serialization;
using Task1_T.Models.Departments;

namespace Task1_T.Models.Dtos.Users
{
    public class GetAllAuthResponses
    {
        [JsonPropertyName("users")]
        public ICollection<AuthResponse> AuthResponses { get; set; }

        public GetAllAuthResponses()
        {
            AuthResponses = new List<AuthResponse>();
        }
    }
}
