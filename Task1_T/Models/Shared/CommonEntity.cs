namespace Task1_T.Models.Shared
{
    public class CommonEntity : BaseEntity, ITrackEntity
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
