using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.FileServices
{
    public interface IFileService
    {
        Task SaveToFile(string message);
        Task<string> GetFile(string path);
    }
}
