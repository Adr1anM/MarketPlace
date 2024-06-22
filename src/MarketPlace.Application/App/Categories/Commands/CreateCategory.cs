using AutoMapper;
using Castle.Core.Logging;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.App.Categories.Responses;
using MarketPlace.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MarketPlace.Application.App.Categories.Commands
{   
    public record CreateCategory(string Name) : ICommand<CategoryDto>;

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategory, CategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(IMapper mapper, ILogger<CreateCategoryCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<CategoryDto> Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Category>(request);

            var result = await _unitOfWork.GetGenericRepository<Category>().AddAsync(entity);

            return  _mapper.Map<CategoryDto>(result);
        }
    }
}
