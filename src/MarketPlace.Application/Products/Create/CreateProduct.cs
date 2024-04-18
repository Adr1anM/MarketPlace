using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Paints.Create
{
    public record CreateProduct(string Title, string description,int categoryId,int authorId,int quentity, decimal Price, DateTime createdDate) : IRequest<Product>;

    public class CreateProductHandler : IRequestHandler<CreateProduct, Product>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _authorRepository;
        public CreateProductHandler(IUnitOfWork unitOfWork, IProductRepository authorRepository)
        {
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
        }
        public async Task<Product> Handle(CreateProduct request, CancellationToken cancellationToken)
        {
            try 
            { 
             
             
            }
            catch (Exception ex) 
            { 
            
            }
        }
    }
}
