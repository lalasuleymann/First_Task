namespace Task1_T.Models.Shared
{
    public interface ISoftDeletedEntity
    {
        public bool IsDeleted { get; set; }
    }
}
