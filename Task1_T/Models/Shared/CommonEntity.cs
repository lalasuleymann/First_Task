namespace Task1_T.Models.Shared
{
    public class CommonEntity : BaseEntity, ITrackEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
