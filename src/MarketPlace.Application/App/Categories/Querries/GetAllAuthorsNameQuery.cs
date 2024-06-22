using AutoMapper;
using Castle.Core.Logging;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarketPlace.Application.App.Categories.Querries
{
    public record GetAllAuthorsNameQuery() : IRequest<IEnumerable<AuthorNameDto>>;
    public class GetAllAuthorsByCountryQuerryHandler : IRequestHandler<GetAllAuthorsNameQuery, IEnumerable<AuthorNameDto>>
    {
        private readonly IUnitOfWork _uniOfWork;
        private readonly ILogger<GetAllAuthorsByCountryQuerryHandler> _logger;


        public GetAllAuthorsByCountryQuerryHandler(IUnitOfWork uniOfWork, ILogger<GetAllAuthorsByCountryQuerryHandler> logger)
        {
   
            _uniOfWork = uniOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<AuthorNameDto>> Handle(GetAllAuthorsNameQuery request, CancellationToken cancellationToken)
        {
            var names = await _uniOfWork.Authors.GetAllAuthorsName();
            if(names == null)
            {
                _logger.LogError("Error fetching author names");
                throw new Exception("Error fetching author names");
            }
            return names;   
        }
    }
  
}
