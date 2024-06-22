using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Categories.Commands;
using MarketPlace.Application.App.Categories.Responses;
using MarketPlace.Application.Paints.Responses;
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
    public record GetAllCategoriesQuerry() : IRequest<IEnumerable<CategoryDto>>;
    public class GetAllCategoriesQuerryHandler : IRequestHandler<GetAllCategoriesQuerry, IEnumerable<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCategoriesQuerryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoriesQuerryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllCategoriesQuerryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuerry request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.GetGenericRepository<Category>().GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
    }
    
}
