using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Categories.Commands;
using MarketPlace.Application.App.Categories.Responses;
using MarketPlace.Application.Exceptions;
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
    public record GetCategoryByIdQuerry(int Id) : IRequest<CategoryDto>;
    public class GetCategoryByIdQuerryHandler : IRequestHandler<GetCategoryByIdQuerry, CategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetCategoryByIdQuerryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryByIdQuerryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetCategoryByIdQuerryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<CategoryDto> Handle(GetCategoryByIdQuerry request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.GetGenericRepository<Category>().GetByIdAsync(request.Id);

            if (category == null)
            {
                _logger.LogError($"Entity of type '{typeof(Category).Name}' with ID '{request.Id}' not found.");
                throw new EntityNotFoundException(typeof(Category), request.Id);
            }

            return _mapper.Map<CategoryDto>(category);
        }
    }
   
}
