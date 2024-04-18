using MarketPlace.Application;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Repositories
{
    public abstract class GenericRepository<TEntity> where TEntity : Entity
    {
        protected readonly ArtMarketPlaceDbContext _context;

        protected GenericRepository(ArtMarketPlaceDbContext context)
        {
            _context = context;
        }

        public async Task Add(TEntity entity)
        {
           await _context.Set<TEntity>().AddAsync(entity);
           await _context.SaveChangesAsync();

        }

        public async Task<TEntity> Delete(int entityId)
        {
            var entity = await _context.Set<TEntity>().FindAsync(entityId);

            if(entity == null)
            {
                throw new ValidationException($"Object of type {typeof(TEntity)} with id {entityId} not found");
            }

            await _context.SaveChangesAsync();
            return entity; 
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }


        public async Task<TEntity> Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity); 
            await _context.SaveChangesAsync();
            return entity;  
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);

        }


    }
}
