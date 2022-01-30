namespace LTuri.Api.Framework.Response
{
    public class ExceptionResponse<TEntity> : Response<TEntity>
    {
        public string Exception { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
