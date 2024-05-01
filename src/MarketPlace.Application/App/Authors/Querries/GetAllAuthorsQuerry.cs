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
    public record GetAllAuthorsQuerry() : IRequest<IEnumerable<AuthorDto>>;
    public class GetAllAuthorsQuerryHandler : IRequestHandler<GetAllAuthorsQuerry, IEnumerable<AuthorDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAuthorsQuerryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<AuthorDto>> Handle(GetAllAuthorsQuerry request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Authors.GetAllAsync();

            return _mapper.Map<IEnumerable<AuthorDto>>(result);
        }
    }
}
