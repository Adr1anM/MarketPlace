using AutoMapper;
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

namespace MarketPlace.Application.App.Categories.Querries
{
    public record GetAllCategWithSubcateg() : IRequest<IEnumerable<CategWithSubCategDto>>;
    public class GetAllCategWithSubcategHandler : IRequestHandler<GetAllCategWithSubcateg, IEnumerable<CategWithSubCategDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCategWithSubcategHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategWithSubcategHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllCategWithSubcategHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IEnumerable<CategWithSubCategDto>> Handle(GetAllCategWithSubcateg request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Categories.GetCategoriesWithSubcategoriesAsync();

            return result;
        }
    }
    
    
}
