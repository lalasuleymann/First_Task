using System.Text.Json.Serialization;

namespace Task1_T.Models.Dtos.Positions
{
    public class PositionGetAllResponse
    {
        [JsonPropertyName("positions")]
        public ICollection<PositionDto> PositionDtos { get; set; }

        public PositionGetAllResponse()
        {
            PositionDtos = new List<PositionDto>();
        }
    }
}
