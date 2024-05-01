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
    public record GetAuthorByIdQuerry(int Id) : IRequest<AuthorDto>;
    public class GetAuthorByIdQuerryHandler : IRequestHandler<GetAuthorByIdQuerry, AuthorDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetAuthorByIdQuerryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<AuthorDto> Handle(GetAuthorByIdQuerry request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Authors.GetByIdAsync(request.Id);

            return _mapper.Map<AuthorDto>(result);
        }
    }
}
