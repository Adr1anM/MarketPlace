using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Services;
using MarketPlace.Application.App.Authors.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Authors.Querries
{
    public record GeAuthorByUserIdQuerry(int Id) : IRequest<AuthorDto>;
    public class GeAuthorByUserIdQuerryHandler : IRequestHandler<GeAuthorByUserIdQuerry, AuthorDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        public GeAuthorByUserIdQuerryHandler(IMapper mapper, IUnitOfWork unitOfWork, IFileManager fileManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
        }
        public async Task<AuthorDto> Handle(GeAuthorByUserIdQuerry request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Authors.GetByUserIdWithInclude(request.Id);
            return _mapper.Map<AuthorDto>(result);

        }
    }
  
}
