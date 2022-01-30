namespace LTuri.Api.Framework.Request
{
    /// <summary>
    /// Composition of the request parameters
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Used for list filtering
        /// </summary>
        public IEnumerable<RequestFilter>? Filters { get; set; } = null;
    }
}
