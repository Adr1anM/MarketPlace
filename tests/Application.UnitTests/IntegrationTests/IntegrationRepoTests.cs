using MarketPlace.Application;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure;
using Moq;
using Xunit;

namespace Application.UnitTests
{
    public class IntegrationRepoTests
    {
        private readonly IRepository<Paint> _repository;

        public IntegrationRepoTests()
        {
            _repository = new InMemoryRepository<Paint>();
        }

        [Fact]
        public void Add_Should_Add_Entity_To_Repository()
        {
            // Arrange
            var entityToAdd = new Paint { Id = 4, Artist = "Mark Zuckerberg", InchSize = 30, Title = "Modernistic", Price = 2000, PaintingMaterial = "Stofa" };

            // Act
            _repository.Add(entityToAdd);

            // Assert
            var actualList = _repository.GetAll().ToList();

            Assert.Contains(entityToAdd, actualList);
        }


        [Fact]
        public void Update_Should_Update_Entity_Price_In_Repository()
        {
            // Arrange
            var initialEntity = new Paint { Id = 1, Artist = "Picasso", InchSize = 20, Title = "Abstract", Price = 1500, PaintingMaterial = "Canvas" };
            _repository.Add(initialEntity);


            var updatedEntity = new Paint { Id = initialEntity.Id, Artist = initialEntity.Artist, InchSize = initialEntity.InchSize, Title = initialEntity.Title, Price = 2000, PaintingMaterial = initialEntity.PaintingMaterial };

            // Act
            _repository.Update(updatedEntity);

            // Assert
            var updatedEntityInRepository = _repository.GetById(updatedEntity.Id);
            Assert.NotNull(updatedEntityInRepository);
            Assert.Equal(updatedEntity.Price, updatedEntityInRepository.Price);
        }

        [Fact]
        public void Delete_Should_Remove_Entity_From_Repository()
        {
            // Arrange
            var entityToDelete = new Paint { Id = 3, Artist = "Alan Berg", InchSize = 22, Title = "Baroc", Price = 4100, PaintingMaterial = "Stofa" };
            _repository.Add(entityToDelete);
            var initialCount = _repository.GetAll().Count();

            // Act
            _repository.Delete(3);

            // Assert
            var finalCount = _repository.GetAll().Count();
            Assert.Equal(initialCount - 1, finalCount);
        }

        [Fact]
        public void GetAll_Should_Return_All_Entities_From_Repository()
        {
            // Arrange
            var entities = new List<Paint>
            {
                new Paint { Id = 3, Artist = "George Cluni", InchSize = 25, Title = "Baroc", Price = 100, PaintingMaterial = "Stofa" },
                new Paint { Id = 4, Artist = "Brad Pit", InchSize = 21, Title = "Medieval", Price = 12200, PaintingMaterial = "Skin" },
                new Paint { Id = 5, Artist = "Oscar Vails", InchSize = 12, Title = "Abstract", Price = 3290, PaintingMaterial = "Syntetic" }
            };

            // Act
            foreach (var entity in entities)
            {
                _repository.Add(entity);
            }

            // Assert
            var result = _repository.GetAll();
            Assert.Equal(entities.Count, result.Count());
            foreach (var entity in entities)
            {
                Assert.Contains(entity, result);
            }
        }

        [Fact]
        public void GetById_Should_Return_Entity_With_Specified_Id()
        {
            // Arrange
            var entityToReturn = new Paint { Id = 1, Artist = "Mark Zuckerberg", InchSize = 30, Title = "Modernistic", Price = 2000, PaintingMaterial = "Stofa" };
            _repository.Add(entityToReturn);

            // Act
            var result = _repository.GetById(1);

            // Assert
            Assert.Equal(entityToReturn, result);
        }

        [Fact]
        public void GetById_Doesnt_Return_Entity_With_Specified_Id()
        {
            // Assert
            Assert.Throws<KeyNotFoundException>(() => _repository.GetById(2));
        }

        [Fact]
        public void Add_Should_Not_Add_Entity_With_Duplicate_Id()
        {
            // Arrange
            var initialEntity = new Paint { Id = 1, Artist = "Picasso", InchSize = 20, Title = "Abstract", Price = 1500, PaintingMaterial = "Canvas" };
            _repository.Add(initialEntity);

            var entityWithDuplicateId = new Paint { Id = 1, Artist = "Van Gogh", InchSize = 25, Title = "Starry Night", Price = 2000, PaintingMaterial = "Oil" };

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _repository.Add(entityWithDuplicateId));
        }

        [Fact]
        public void Update_Should_Not_Update_Nonexistent_Entity()
        {
            // Arrange
            var nonExistentEntity = new Paint { Id = 100, Artist = "Da Vinci", InchSize = 30, Title = "Mona Lisa", Price = 5000, PaintingMaterial = "Canvas" };

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _repository.Update(nonExistentEntity));
        }
    }
}
