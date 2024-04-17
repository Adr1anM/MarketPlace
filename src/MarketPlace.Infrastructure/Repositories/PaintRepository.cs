/*using MarketPlace.Application.Abstractions;
using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Repositories
{
    public class PaintRepository : IPaintRepository
    {
        public List<Paint> _paintList = new List<Paint>();

        public Paint Create(Paint paint)
        {
            _paintList.Add(paint);
            return paint;
        }

        public List<Paint> GetPaintByIds(List<int> paintIds)
        {
            return _paintList.Where(p => paintIds.Contains(p.Id)).ToList();
        }

        public Paint GetPaintById(int id)
        {
            return _paintList.FirstOrDefault(p => p.Id == id);
        }

        public void RemovePaint(Paint paint)   
        {
            _paintList?.Remove(paint);

        }

        public List<Paint> GetAllPaints() => _paintList;

        public int GetLastId()
        {
            if (_paintList.Count == 0) return 1;
            var lastId = _paintList.Max(p => p.Id);
            return lastId;  
        }


    }
}
*/