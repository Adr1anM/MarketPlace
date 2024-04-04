using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Application.Sculptures.Responses;
using MarketPlace.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Paints.Create
{
    public record CreatePiant(string Title, string Artist, decimal Price, string PaintingMaterial, decimal InchSize) : IRequest<PaintDTO>;

    public class CreatePaintHandler : IRequestHandler<CreatePiant, PaintDTO>
    {
        private readonly IPaintRepository _paintRepository;

        public CreatePaintHandler(IPaintRepository paintRepository)
        {
            _paintRepository = paintRepository;
        }
    
        public Task<PaintDTO> Handle(CreatePiant request, CancellationToken cancellationToken)
        {
            var paint = new Paint() { Id = GetNextId(), Title = request.Title, Artist = request.Artist, Price = request.Price, PaintingMaterial = request.PaintingMaterial, InchSize = request.InchSize };
            var createdPaint = _paintRepository.Create(paint);
            return Task.FromResult(PaintDTO.FromPaint(createdPaint));
        }
        public int GetNextId()
        {
            return _paintRepository.GetLastId() + 1;
        }

    }
}
