namespace LTuri.Api.Framework.Response
{
    public class Response<TEntity>
    {
        public bool Success { get; set; } = true;
        public int Code { get; set; } = 200;
        public TEntity? Data { get; set; }
    }
}
