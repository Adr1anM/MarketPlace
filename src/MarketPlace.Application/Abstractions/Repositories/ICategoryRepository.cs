using MarketPlace.Application.App.Categories.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Abstractions.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategWithSubCategDto>> GetCategoriesWithSubcategoriesAsync();
    }
}
