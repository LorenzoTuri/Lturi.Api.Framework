using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LTuri.Api.Framework.Exceptions;
using LTuri.Api.Framework.Request;
using LTuri.Api.Framework.Response;

namespace LTuri.Api.Framework
{
    public class ApiController<TEntity> : ControllerBase 
        where TEntity : class, IdentifiableEntity
    {
        protected readonly DbContext _context;
        protected readonly ApiControllerFilter<TEntity> _filter;

        public ApiController(DbContext context)
        {
            _context = context;
            _filter = new ApiControllerFilter<TEntity>();
        }

        public Response<IEnumerable<TEntity>> ListRequest(
            DbSet<TEntity> entities,
            IEnumerable<RequestFilter>? filters = null
        ) {
            return WrapException(() =>
            {
                return new Response<IEnumerable<TEntity>>()
                {
                    Data = _filter.FilterEntity(entities.ToList(), filters)
                };
            });
        }

        public Response<TEntity> GetRequest(
            DbSet<TEntity> entities,
            int id
        )
        {
            return WrapException(() =>
            {
                var entity = entities.Find(id);
                if (entity == null)
                    throw new EntityIdNotFoundException(
                        typeof(TEntity).Name,
                        id.ToString()
                    );

                return new Response<TEntity>()
                {
                    Data = entity
                };
            });
        }

        public Response<TEntity> PutRequest(
            int id,
            TEntity entity
        )
        {
            return WrapException(() =>
            {
                if (!entity.Identifier.Equals(id)) throw new BadRequestException("Request id mismatch body id");

                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();

                return new Response<TEntity>()
                {
                    Data = entity
                };
            });
        }

        public Response<TEntity> PostRequest(
            DbSet<TEntity> entities,
            TEntity entity
        )
        {
            return WrapException(() =>
            {
                entities.Add(entity);
                _context.SaveChanges();

                return new Response<TEntity>()
                {
                    Data = entity
                };
            });
        }

        public Response<bool> DeleteRequest(
            DbSet<TEntity> entities,
            int id
        )
        {
            return WrapException(() =>
            {
                var entity = entities.Find(id);
                if (entity == null)
                {
                    throw new EntityIdNotFoundException(
                        typeof(TEntity).Name,
                        id.ToString()
                    );
                }

                entities.Remove(entity);
                _context.SaveChanges();

                return new Response<bool>()
                {
                    Data = true
                };
            });
        }


        private Response<TResponse> WrapException<TResponse>(Func<Response<TResponse>> wrappedMethod)
        {
            try
            {
                return wrappedMethod();
            }
            catch (AbstractCodedException ex)
            {
                return new ExceptionResponse<TResponse>()
                {
                    Success = false,
                    Code = ex.Code,
                    Exception = ex.GetType().Name,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<TResponse>()
                {
                    Success = false,
                    Code = 500,
                    Exception = ex.GetType().Name,
                    Message = ex.Message
                };
            }
        }
    }
}
