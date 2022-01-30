namespace LTuri.Api.Framework.Exceptions
{
    public class NotImplementedFilter: AbstractCodedException
    {
        public NotImplementedFilter(string type) : base(
            String.Format("Filter type '{0}' not implemented",
            type
        )) {
        }

        public override int Code => 501;
    }
}
