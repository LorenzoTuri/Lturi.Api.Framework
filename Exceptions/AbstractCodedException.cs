namespace LTuri.Api.Framework.Exceptions
{
    public abstract class AbstractCodedException: Exception
    {
        public AbstractCodedException(string message) : base(message) { }

        public abstract int Code { get; }
    }
}
