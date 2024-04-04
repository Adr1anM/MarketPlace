using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Abstractions
{
    public interface IPhotographyRepository
    {
        List<Photography> GetPhotoByIds(List<int> photoIds);
        Photography Create(Photography photo);
        Photography GetPhotoById(int id);
        void RemovePhoto(Photography photo);
        List<Photography> GetAllPhotos();
        int GetLastId();
    }
}
