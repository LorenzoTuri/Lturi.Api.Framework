namespace LTuri.Api.Framework.Exceptions
{
    public class MissingFilterProperty: AbstractCodedException
    {
        public MissingFilterProperty(string propertyName, string filterType) : base(
            String.Format("Filter.{0} cannot be null for '{1}' filter",
            propertyName,
            filterType
        )) {
        }

        public override int Code => 400;
    }
}
