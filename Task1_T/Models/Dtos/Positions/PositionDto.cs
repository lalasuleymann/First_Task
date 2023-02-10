namespace Task1_T.Models.Dtos.Positions
{
    public class PositionDto : BaseDto
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
