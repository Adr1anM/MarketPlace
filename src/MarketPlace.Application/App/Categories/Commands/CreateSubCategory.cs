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

namespace MarketPlace.Application.App.Categories.Commands
{
    public record CreateSubCategory(string Name) : ICommand<SubCategoryDto>;

    public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategory, SubCategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSubCategoryCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSubCategoryCommandHandler(IMapper mapper, ILogger<CreateSubCategoryCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<SubCategoryDto> Handle(CreateSubCategory request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<SubCategory>(request);

            var result = await _unitOfWork.GetGenericRepository<SubCategory>().AddAsync(entity);

            return _mapper.Map<SubCategoryDto>(result);
        }
    }
 
}
