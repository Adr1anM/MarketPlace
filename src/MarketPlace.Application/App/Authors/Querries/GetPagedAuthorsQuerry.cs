using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.Common.Models;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Application.Products.GetPagedResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Authors.Querries
{
    public record GetPagedAuthorsQuerry(PagedRequest PagedRequest) : IRequest<PaginatedResult<AuthorDto>>;

    public class GetPagedAuthorsQuerryHandler : IRequestHandler<GetPagedAuthorsQuerry, PaginatedResult<AuthorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetPagedAuthorsQuerryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<AuthorDto>> Handle(GetPagedAuthorsQuerry request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Authors.GetPagedData<AuthorDto>(request.PagedRequest, _mapper);

            return result;
        }
    }
}
