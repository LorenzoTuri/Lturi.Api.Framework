namespace LTuri.Api.Framework.Exceptions
{
    public class EntityIdNotFoundException : AbstractCodedException
    {
        public EntityIdNotFoundException(string entity, string id): base(
            String.Format("Entity '{0}' with id '{1}' not found",
            entity,
            id
        )) {
        }

        public override int Code => 404;
    }
}
