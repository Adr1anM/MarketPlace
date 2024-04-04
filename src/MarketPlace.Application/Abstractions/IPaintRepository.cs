using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Abstractions
{
    public interface IPaintRepository
    {
        List<Paint> GetPaintByIds(List<int> paintIds);
        Paint Create(Paint paint);
        Paint GetPaintById(int id);
        void RemovePaint(Paint paint);
        List<Paint> GetAllPaints();
        int GetLastId();

    }
}
