using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Categories.Responses;
using MarketPlace.Domain;
using MarketPlace.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Categories.Querries
{
    public record GetSubCategoriesByCategIdQuerry(int Id) : IRequest<IEnumerable<SubCategoryDto>>;
    public class GetSubCategoriesByCategIdQuerryHandler : IRequestHandler<GetSubCategoriesByCategIdQuerry, IEnumerable<SubCategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllSubCategoriesQuerryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GetSubCategoriesByCategIdQuerryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllSubCategoriesQuerryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IEnumerable<SubCategoryDto>> Handle(GetSubCategoriesByCategIdQuerry request, CancellationToken cancellationToken)
        {
            var subCategories = await _unitOfWork.GetGenericRepository<CategorySubcategory>()
                     .GetWithConditionAndSelect<SubCategory>(cs => cs.CategoryId == request.Id, cs => cs.SubCategory);


            return _mapper.Map<IEnumerable<SubCategoryDto>>(subCategories);
        }
    }
  
}
