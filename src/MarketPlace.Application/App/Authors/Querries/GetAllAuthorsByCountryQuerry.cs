using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Authors.Querries
{
    public record GetAllAuthorsByCountryQuerry(string Country) : IRequest<IEnumerable<AuthorDto>>;
    public class GetAllAuthorsByCountryQuerryHandler : IRequestHandler<GetAllAuthorsByCountryQuerry, IEnumerable<AuthorDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uniOfWork;

        public GetAllAuthorsByCountryQuerryHandler(IMapper mapper, IUnitOfWork uniOfWork)
        {
            _mapper = mapper;
            _uniOfWork = uniOfWork;   
        }

        public async Task<IEnumerable<AuthorDto>> Handle(GetAllAuthorsByCountryQuerry request, CancellationToken cancellationToken)
        {
            var result = await _uniOfWork.Authors.GetAuthorWhere(a => a.Country == request.Country);
            return _mapper.Map<IEnumerable<AuthorDto>>(result);
        }
    }
}
