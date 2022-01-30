namespace LTuri.Api.Framework.Request
{
    /// <summary>
    /// Parameters used in range filter
    /// </summary>
    public class RequestFilterParameter
    {
        public object? GreaterEquals { get; set; } = null;
        public object? Greater { get; set; } = null;
        public object? LowerEquals { get; set; } = null;
        public object? Lower { get; set; } = null;
    }
}
