using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;


namespace MarketPlace.Application.App.Authors.Commands
{
    public record UpdateAuthor(int Id, int UserId, string Biography, string Country, DateTime BirthDate, string SocialMediaLinks, int NumberOfPosts) : ICommand<AuthorDto>;
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthor, AuthorDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;   


        public UpdateAuthorHandler(IMapper mapper, IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<UpdateAuthorHandler>();
        }
        public async Task<AuthorDto> Handle(UpdateAuthor request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Authors.GetByIdAsync(request.Id);


            try
            {
                await _unitOfWork.BeginTransactionAsync();
                _mapper.Map(request, result);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
                return _mapper.Map<AuthorDto>(result);

            }
            catch (ArgumentNullException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "ArgumentNullException occurred: {Message}", ex.Message);
                Console.WriteLine($"The Author does not exists", ex.Message);
                throw;
            }
            catch (AutoMapperMappingException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "AutoMapperMappingException occurred: {Message}", ex.Message);
                Console.WriteLine($"Mapping failed: {ex.Message}");
                throw;

            }

            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }


        }
    }
}
