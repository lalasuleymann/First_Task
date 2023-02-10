namespace Task1_T.Models.Shared
{
    public interface ITrackEntity : ICreatedDateEntity, ISoftDeletedEntity,
        IModifiedDateEntity, ISoftDeletedDateEntity
    {
    }
}
