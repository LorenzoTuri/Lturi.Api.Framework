namespace LTuri.Api.Framework.Response
{
    /// <summary>
    /// Possible api response, each exception may happen it's returned as one
    /// of this objects. While they are typed along TEntity, "Response.Data" is always null.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ExceptionResponse<TEntity> : Response<TEntity>
    {
        public string Exception { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
