using AutoMapper;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Categories.Responses;
using MarketPlace.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Application.Exceptions;

namespace MarketPlace.Application.App.Categories.Commands
{
    public record UpdateCategory(int Id, string Name) : ICommand<CategoryDto>;

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategory, CategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCategoryCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(IMapper mapper, ILogger<UpdateCategoryCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<CategoryDto> Handle(UpdateCategory request, CancellationToken cancellationToken)
        {

            var result = await _unitOfWork.GetGenericRepository<Category>().GetByIdAsync(request.Id);

            if (result == null)
            {
                _logger.LogError($"Entity of type '{typeof(Category).Name}' with ID '{request.Id}' not found.");
                throw new EntityNotFoundException(typeof(Category), request.Id);
            }

            try
            {
                _mapper.Map(request, result);
                return _mapper.Map<CategoryDto>(result);
            }
            catch(AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "An error occurred while mappinig: {Message}", ex.Message);
                throw;
            }
  
        }
    }
}
