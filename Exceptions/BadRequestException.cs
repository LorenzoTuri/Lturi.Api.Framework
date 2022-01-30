namespace LTuri.Api.Framework.Exceptions
{
    public class BadRequestException : AbstractCodedException
    {
        public BadRequestException(string message): base(message) {
        }


        public override int Code => 400;
    }
}
