using System.Reflection;
using LTuri.Api.Framework.Exceptions;
using LTuri.Api.Framework.Request;

namespace LTuri.Api.Framework
{
    public class ApiControllerFilter<TEntity>
    {
        public IEnumerable<TEntity> FilterEntity(
            IEnumerable<TEntity> entities,
            IEnumerable<RequestFilter>? filters,
            bool andFilter = true
        )
        {
            IEnumerable<TEntity>? result = entities;
            if (filters != null)
            {
                if (andFilter)
                {
                    foreach (RequestFilter filter in filters)
                    {
                        result = FilterEntity(entities, filter);
                    }
                }
                else
                {
                    result = new HashSet<TEntity>();
                    foreach (RequestFilter filter in filters)
                    {
                        ((HashSet<TEntity>)result).UnionWith(FilterEntity(entities, filter));
                    }
                }
            }
            return result;
        }

        protected IEnumerable<TEntity> FilterEntity(IEnumerable<TEntity> entities, RequestFilter filter)
        {
            switch (filter.Type)
            {
                case RequestFilter.Types.equals:
                case RequestFilter.Types.equalsAny:
                case RequestFilter.Types.contains:
                case RequestFilter.Types.range:
                case RequestFilter.Types.not:
                    PropertyInfo? propertyInfo = typeof(TEntity).GetProperty(
                        filter.Field,
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
                    );
                    if (propertyInfo == null) throw new PropertyNotFoundException(filter.Field, typeof(TEntity).ToString());

                    switch (filter.Type)
                    {
                        case RequestFilter.Types.equals: return FilterEquals(entities, propertyInfo, filter);
                        case RequestFilter.Types.equalsAny: return FilterEqualsAny(entities, propertyInfo, filter);
                        case RequestFilter.Types.contains: return FilterContains(entities, propertyInfo, filter);
                        case RequestFilter.Types.range: return FilterRange(entities, propertyInfo, filter);
                        case RequestFilter.Types.not: return FilterNot(entities, propertyInfo, filter);
                
                        default: throw new NotImplementedFilter(filter.Type.ToString());
                    }

                case RequestFilter.Types.and:
                    if (filter.Filters == null) throw new MissingFilterProperty("filters", "and");
                    else return FilterEntity(entities, filter.Filters, true);
                
                case RequestFilter.Types.or:
                    if (filter.Filters == null) throw new MissingFilterProperty("filters", "or");
                    else return FilterEntity(entities, filter.Filters, false);
                
                default: throw new NotImplementedFilter(filter.Type.ToString());
            }
        }

        private IEnumerable<TEntity> FilterEquals(
            IEnumerable<TEntity> entities,
            PropertyInfo propertyInfo,
            RequestFilter filter
        )
        {
            return entities.Where(entity =>
                propertyInfo.GetValue(entity, null) == filter.Value
            );
        }
        private IEnumerable<TEntity> FilterEqualsAny(
            IEnumerable<TEntity> entities,
            PropertyInfo propertyInfo,
            RequestFilter filter
        )
        {
            return entities.Where(entity =>
            {
                object? propertyValue = propertyInfo.GetValue(entity, null);
                foreach (object value in (Array)filter.Value)
                {
                    if (propertyValue == value) return true;
                }
                return false;
            });
        }
        private IEnumerable<TEntity> FilterContains(
            IEnumerable<TEntity> entities,
            PropertyInfo propertyInfo,
            RequestFilter filter
        )
        {
            return entities.Where(entity =>
            {
                object? value = propertyInfo.GetValue(entity, null);
                if (value == null || filter.Value == null) return false;
                else return value.ToString().Contains(filter.Value.ToString());
            });
        }
        private IEnumerable<TEntity> FilterRange(
            IEnumerable<TEntity> entities,
            PropertyInfo propertyInfo,
            RequestFilter filter
        )
        {
            return entities.Where(entity =>
            {
                if (filter.Parameters == null) throw new MissingFilterProperty("parameters", "range");

                float? propertyValue = (float)propertyInfo.GetValue(entity, null);
                bool partialResult = false;
                if (filter.Parameters.GreaterEquals != null)
                    partialResult &= propertyValue >= (float)filter.Parameters.GreaterEquals;
                if (filter.Parameters.Greater != null)
                    partialResult &= propertyValue > (float)filter.Parameters.Greater;
                if (filter.Parameters.LowerEquals != null)
                    partialResult &= propertyValue <= (float)filter.Parameters.LowerEquals;
                if (filter.Parameters.Lower != null)
                    partialResult &= propertyValue < (float)filter.Parameters.Lower;
                return partialResult;
            });
        }
        private IEnumerable<TEntity> FilterNot(
            IEnumerable<TEntity> entities,
            PropertyInfo propertyInfo,
            RequestFilter filter
        )
        {
            return entities.Where(entity =>
                !(propertyInfo.GetValue(entity, null) == filter.Value)
            );
        }
    }
}
