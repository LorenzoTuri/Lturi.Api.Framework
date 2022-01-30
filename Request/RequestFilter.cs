namespace LTuri.Api.Framework.Request
{
    public class RequestFilter
    {
        public enum Types { 
            equals, 
            equalsAny,
            contains,
            range,
            not,
            and,
            or
        }

        /// <summary>
        /// Field name (property). Required in equals, equalsAny, contains, range, not
        /// </summary>
        public string Field { get; set; } = "{field name}";

        /// <summary>
        /// Field value. Required in equals, equalsAny, contains, range, not
        /// </summary>
        public object Value { get; set; } = "{field value}";

        /// <summary>
        /// How to filter. Default to equals.
        /// </summary>
        public Types Type { get; set; } = Types.equals;

        /// <summary>
        /// Parameters for the filter, only used in range.
        /// </summary>
        public RequestFilterParameter? Parameters { get; set; } = null;

        /// <summary>
        /// Filters for cumulative queries, like and, or
        /// </summary>
        public IList<RequestFilter>? Filters { get; set; } = null;
    }
}
