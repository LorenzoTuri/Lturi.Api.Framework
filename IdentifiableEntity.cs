namespace LTuri.Api.Framework
{
    /// <summary>
    /// Mark the entity as "Identifiable". The identifier must be implemented in each entity,
    /// but normally it should just return the primary id of the entity
    /// </summary>
    public interface IdentifiableEntity
    {
        public object Identifier { get; }
    }
}
