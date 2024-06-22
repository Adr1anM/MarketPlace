using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public Type EntityType { get; }
        public int EntityId { get; }

        public EntityNotFoundException(Type entityType, int entityId)
            : base($"Entity of type '{entityType.Name}' with ID '{entityId}' not found.")
        {
            EntityType = entityType;
            EntityId = entityId;
        }   
    }
}
