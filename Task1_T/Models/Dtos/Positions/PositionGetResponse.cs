using System.Text.Json.Serialization;

namespace Task1_T.Models.Dtos.Positions
{
    public class PositionGetResponse
    {
        [JsonPropertyName("position")]
        public PositionDto PositionDto { get; set; }
    }
}
