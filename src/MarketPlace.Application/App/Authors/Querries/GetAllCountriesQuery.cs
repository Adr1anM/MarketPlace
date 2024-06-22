using AutoMapper;
using Castle.Core.Logging;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Authors.Querries
{
    public record GetAllCountriesQuery() : IRequest<IEnumerable<string>>;
    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, IEnumerable<string>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllCountriesQueryHandler> _logger;


        public GetAllCountriesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllCountriesQueryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IEnumerable<string>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Authors.GetAllCountries();
            if(result == null)
            {
                _logger.LogError("Countries not found");
                throw new Exception("Countries not found");
            }

            return result;

        }
    }
   
}
