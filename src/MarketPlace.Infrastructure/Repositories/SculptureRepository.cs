/*using MarketPlace.Application.Abstractions;
using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Repositories
{
    public class SculptureRepository : ISculptureRepository
    {
        public List<Sculpture> _sculptureList = new List<Sculpture>();

        public Sculpture Create(Sculpture sculpture)
        {
            _sculptureList.Add(sculpture);
            return sculpture;
        }

        public List<Sculpture> GetSculptureByIds(List<int> sculptureIds)
        {
            return _sculptureList.Where(p => sculptureIds.Contains(p.Id)).ToList();
        }

        public Sculpture GetSculptureById(int id)
        {
            return _sculptureList.FirstOrDefault(p => p.Id == id);
        }

        public void RemoveSculpture(Sculpture sculpture)
        {
            _sculptureList?.Remove(sculpture);
           
        }

        public List<Sculpture> GetAllSculptures() => _sculptureList;

        public int GetLastId()
        {
            if (_sculptureList.Count == 0) return 1;
            var lastId = _sculptureList.Max(a => a.Id);
            return lastId;
        }
    }
}
*/