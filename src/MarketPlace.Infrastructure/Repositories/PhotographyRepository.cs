/*using MarketPlace.Application.Abstractions;
using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Repositories
{
    public class PhotographyRepository : IPhotographyRepository
    {
        public List<Photography> _photographyList = new List<Photography>();

        public Photography Create(Photography photo)
        {
            _photographyList.Add(photo);    
            return photo;   
        }

        public List<Photography> GetPhotoByIds(List<int> photoIds)
        {
            return _photographyList.Where(p => photoIds.Contains(p.Id)).ToList();   
        }

        public Photography GetPhotoById(int id)
        {
            return _photographyList.FirstOrDefault(p => p.Id == id);
        }
        
        public void RemovePhoto(Photography photo)
        {
            _photographyList?.Remove(photo);
          
        }

        public List<Photography> GetAllPhotos() => _photographyList;

        public int GetLastId()
        {
            if (_photographyList.Count == 0) return 1;
            var lastId = _photographyList.Max(p => p.Id);
            return lastId;
        }

    }
}
*/