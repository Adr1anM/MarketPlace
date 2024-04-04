using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Photographies.Responses;
using MarketPlace.Application.Sculptures.Responses;
using MarketPlace.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Sculptures.Create
{
    public record CreateSculture(string Title,string Artist, decimal Price , string Material , double Weight ) : IRequest<SculptureDTO>;

    public class CreateScultureHandler : IRequestHandler<CreateSculture, SculptureDTO>
    {
        private readonly ISculptureRepository _sculptureRepository;

        public CreateScultureHandler(ISculptureRepository sculptureRepository)
        {
            _sculptureRepository = sculptureRepository;
        }
        
        public Task<SculptureDTO> Handle(CreateSculture request, CancellationToken cancellationToken)
        {
            var sculpture = new Sculpture() { Id = GetNextId(), Artist = request.Artist, Price = request.Price, Material = request.Material, Title = request.Title, Weight = request.Weight };
            var createdSculpture = _sculptureRepository.Create(sculpture);

            return Task.FromResult(SculptureDTO.FromSculpture(createdSculpture));
        }

        public int GetNextId()
        {
            return _sculptureRepository.GetLastId() + 1;
        }

    }
}
