namespace Task1_T.Models.Shared
{
    public interface ISoftDeletedDateEntity
    {
        public DateTime? DeletedDate { get; set; }
    }
}
