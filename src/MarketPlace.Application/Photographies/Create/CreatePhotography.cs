using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Photographies.Responses;
using MarketPlace.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Photographies.Create
{
    public record CreatePhotography(string Title, string Artist, decimal Price) : IRequest<PhotographyDTO>;

    public class CreatePhotographyHandler : IRequestHandler<CreatePhotography, PhotographyDTO>
    {
        private readonly IPhotographyRepository _photographyRepository;

        public CreatePhotographyHandler(IPhotographyRepository photographyRepository)
        {
            _photographyRepository = photographyRepository;
        }
        public Task<PhotographyDTO> Handle(CreatePhotography request, CancellationToken cancellationToken)
        {
            var photography = new Photography() { Id = GetNextId(), Artist = request.Artist, Price = request.Price, Title = request.Title };
            var createdPhotography = _photographyRepository.Create(photography);

            return Task.FromResult(PhotographyDTO.FromPhotography(createdPhotography));
        }


        public int GetNextId()
        {
            return _photographyRepository.GetLastId() + 1;  
        }
    }

}
