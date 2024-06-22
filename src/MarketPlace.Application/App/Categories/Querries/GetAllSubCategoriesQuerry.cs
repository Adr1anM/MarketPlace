using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Categories.Commands;
using MarketPlace.Application.App.Categories.Responses;
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
    public record GetAllSubCategoriesQuerry() : IRequest<IEnumerable<SubCategoryDto>>;
    public class GetAllSubCategoriesQuerryHandler : IRequestHandler<GetAllSubCategoriesQuerry, IEnumerable<SubCategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllSubCategoriesQuerryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSubCategoriesQuerryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllSubCategoriesQuerryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IEnumerable<SubCategoryDto>> Handle(GetAllSubCategoriesQuerry request, CancellationToken cancellationToken)
        {
            var subCategories = await _unitOfWork.GetGenericRepository<SubCategory>().GetAllAsync();
            return _mapper.Map<IEnumerable<SubCategoryDto>>(subCategories);
        }
    }
    
}
