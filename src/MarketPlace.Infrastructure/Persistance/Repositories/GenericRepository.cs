using AutoMapper;
using MarketPlace.Application;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Application.Common.Models;
using MarketPlace.Application.Extensions;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;


namespace MarketPlace.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        protected readonly ArtMarketPlaceDbContext _context;

        public GenericRepository(ArtMarketPlaceDbContext context)
        {
            _context = context;
        }   

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var  result = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> GetAllByIdWithIncludeAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = IncludeProperties(includeProperties);
            return await query.Where(entity => entity.Id == id).ToListAsync();
        }

        public async Task<TEntity> GetByIdWithIncludeAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = IncludeProperties(includeProperties);
            return await query.FirstOrDefaultAsync(entity => entity.Id == id);
        }
        public async Task<List<TResult>> GetWithConditionAndSelect<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector)where TResult : class
        {
            return await _context.Set<TEntity>()
                .Where(predicate)
                .Select(selector)
                .ToListAsync();
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                throw new ValidationException($"Object of type {typeof(TEntity)} with id {id} not found");
            }

            _context.Set<TEntity>().Remove(entity);

            return entity;
        }

        public async Task<List<TResult>> GetDistinctValues<TResult>(
        Expression<Func<TEntity, TResult>> selector)
        {
            return await _context.Set<TEntity>()
                .Select(selector)
                .Distinct()
                .ToListAsync();
        }

        private IQueryable<TEntity> IncludeProperties(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> entities = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }

        public async Task<List<TEntity>> FindByWhere(Expression<Func<TEntity, bool>> expresionPredicate)
        {
            return await _context.Set<TEntity>().Where(expresionPredicate).ToListAsync();
        }

        public virtual async Task<PagedResult<TDto>> GetPagedData<TDto>(PagedRequest pagedRequest, IMapper mapper )  where TDto : class
        {
            return await _context.Set<TEntity>().CreatePaginatedResultAsync<TEntity, TDto>(pagedRequest, mapper);
        }
    }
}
