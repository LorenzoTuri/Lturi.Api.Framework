namespace LTuri.Api.Framework.Response
{
    /// <summary>
    /// Response model for the api.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Response<TEntity>
    {
        /// <summary>
        /// Always true, unless the response is an ExeptionResponse
        /// </summary>
        public bool Success { get; set; } = true;
        /// <summary>
        /// Http code, if available of the resèpmse
        /// </summary>
        public int Code { get; set; } = 200;
        /// <summary>
        /// True data contained in the response
        /// </summary>
        public TEntity? Data { get; set; }
    }
}
