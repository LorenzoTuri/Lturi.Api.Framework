namespace LTuri.Api.Framework.Exceptions
{
    public class PropertyNotFoundException: AbstractCodedException
    {
        public PropertyNotFoundException(string field, string entity): base(
            String.Format("property '{0}' not found in '{1}'",
            field,
            entity
        )) {
        }

        public override int Code => 400;
    }
}
