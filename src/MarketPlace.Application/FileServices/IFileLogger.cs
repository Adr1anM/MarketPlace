using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.FileServices
{
    public interface IFileLogger
    {
        Task LogSuccess(string message);
        Task LogFailure(string message);

    }
}
