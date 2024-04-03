using MarketPlace.Application;
using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseArtProduct
    {
        private List<T> _products;

        public InMemoryRepository()
        {
            _products = new List<T>();
        }

        public void Add(T entity)
        {
            if (IsEmptyObject(entity))
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            }

            if (_products.Any(e => e.Id == entity.Id))
            {
                throw new InvalidOperationException($"An entity with ID {entity.Id} already exists in the repository.");
            }

            _products.Add(entity);
 
        }

        public void Delete(int id)
        {
            var entityToDelete = _products.FirstOrDefault(e => e.Id == id);
            if (entityToDelete != null)
            {
                _products.Remove(entityToDelete);
            }
            else
            {
                throw new InvalidOperationException($"No entity with ID {id} exists in the repository.");
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _products;
        }

        public T GetById(int id)
        {
            var entity = _products.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found in the repository.");
            }
            return entity;
        }

        public void Update(T entity)
        {
            var existingProduct = _products.FirstOrDefault(e => e.Id == entity.Id);
            if (existingProduct != null)
            {
                existingProduct.Price = entity.Price;
            }
            else
            {
                throw new InvalidOperationException($"No entity with ID {entity.Id} exists in the repository.");
            }
        }

        private bool IsEmptyObject(T entity)
        {
            return entity.GetType().GetProperties()
                .Where(pi => pi.PropertyType == typeof(string))
                .Select(pi => pi.GetValue(entity)?.ToString())
                .Any(value => string.IsNullOrEmpty(value));
        }
    }
}
