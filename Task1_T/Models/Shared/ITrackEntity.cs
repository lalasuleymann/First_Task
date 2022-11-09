namespace Task1_T.Models.Shared
{
    public interface ITrackEntity : ISoftDeletedEntity, ICreatedDateEntity,
        IModifiedDateEntity, ISoftDeletedDateEntity
    {
    }
}
