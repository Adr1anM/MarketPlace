/*using MarketPlace.Application;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure;
using MarketPlace.Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests
{
    public class MockRepositoryTests
    {
        private readonly Mock<IGenericRepository<Product>> _mockRepository;
        private readonly IProductRepository _repository;

        public MockRepositoryTests()
        {
            _mockRepository = new Mock<IGenericRepository<Product>>();
            _repository = _mockRepository.Object;
        }

        [Fact]
        public void Add_Should_Add_Entity_To_Repository()
        {
            // Arrange
            var entityToAdd = new Product { Id = 4, Title = "Mozart's Picture", Description = "This is Mozart's Picture made in 1666", CategoryID = 2, AuthorId = 3, Quantity = 2 ,Price = 5000, CreatedDate = DateTime.Now };

            _mockRepository.Setup(r => r.Add(It.IsAny<Product>()));
            _mockRepository.Setup(r => r.GetAll()).Returns(new List<Product> { entityToAdd });

            // Act
            _repository.Add(entityToAdd);

            // Verify
            _mockRepository.Verify(r => r.Add(
                It.Is<Product>(p =>
                     p.Id == entityToAdd.Id &&
                    p.Title == entityToAdd.Title &&
                    p.Description == entityToAdd.Description &&
                    p.CategoryID == entityToAdd.CategoryID &&
                    p.AuthorId == entityToAdd.AuthorId &&
                    p.Quantity == entityToAdd.Quantity &&
                    p.Price == entityToAdd.Price &&
                    p.CreatedDate == entityToAdd.CreatedDate
                )
            ), Times.Once);

            var result = _repository.GetAll().ToList();

            Assert.Contains(entityToAdd, result);
        }

     *//*   [Fact]
        public void Update_Should_Update_Entity_Price_In_Repository()
        {
            // Arrange
            var initialEntity = new Paint { Id = 1, Artist = "Picasso", InchSize = 20, Title = "Abstract", Price = 1500, PaintingMaterial = "Canvas" };

            _repository.Add(initialEntity);

            var updatedEntity = new Paint { Id = initialEntity.Id, Artist = initialEntity.Artist, InchSize = initialEntity.InchSize, Title = initialEntity.Title, Price = 2000, PaintingMaterial = initialEntity.PaintingMaterial };

            _mockRepository.Setup(r => r.GetById(initialEntity.Id)).Returns(updatedEntity);

            // Act
            _repository.Update(updatedEntity);

            var updatedEntityInRepository = _repository.GetById(updatedEntity.Id);
            // Assert

            Assert.Multiple(() =>
            {
                Assert.NotNull(updatedEntityInRepository);
                Assert.Equal(updatedEntity.Price, updatedEntityInRepository.Price);
                Assert.Equal(initialEntity.Artist, updatedEntityInRepository.Artist);
                Assert.Equal(initialEntity.InchSize, updatedEntityInRepository.InchSize);
                Assert.Equal(initialEntity.Title, updatedEntityInRepository.Title);
                Assert.Equal(initialEntity.PaintingMaterial, updatedEntityInRepository.PaintingMaterial);
            });

        }

        [Fact]
        public void Delete_Should_Remove_Entity_From_Repository()
        {
            // Arrange
            var entityToDelete = new Paint { Id = 3, Artist = "Alan Berg", InchSize = 22, Title = "Baroc", Price = 4100, PaintingMaterial = "Stofa" };
            _repository.Add(entityToDelete);

            // Act
            _repository.Delete(entityToDelete.Id);


            _mockRepository.Verify(r => r.Delete(entityToDelete.Id), Times.Once);
            // Assert


            var entityAfterDeletion = _repository.GetById(entityToDelete.Id);
            Assert.Null(entityAfterDeletion);
        }



        [Fact]
        public void GetAll_Should_Return_All_Entities_From_Repository()
        {

            var entities = new List<Paint>
            {
                new Paint { Id = 3, Artist = "George Cluni", InchSize = 25, Title = "Baroc", Price = 100, PaintingMaterial = "Stofa" },
                new Paint { Id = 4, Artist = "Brad Pit", InchSize = 21, Title = "Medieval", Price = 12200, PaintingMaterial = "Skin" },
                new Paint { Id = 5, Artist = "Oscar Vails", InchSize = 12, Title = "Abstract", Price = 3290, PaintingMaterial = "Syntetic" }
            };
            _mockRepository.Setup(r => r.GetAll()).Returns(entities);

            var result = _repository.GetAll();


            Assert.Equal(entities, result);
        }

        [Fact]
        public void GetById_Should_Return_Entity_With_Specified_Id()
        {

            var entityToReturn = new Paint { Id = 1, Artist = "Mark Zuckerberg", InchSize = 30, Title = "Modernistic", Price = 2000, PaintingMaterial = "Stofa" };
            _mockRepository.Setup(r => r.GetById(1)).Returns(entityToReturn);

            var result = _repository.GetById(1);

            Assert.Equal(entityToReturn, result);
        }


        [Fact]
        public void Add_Should_Not_Add_Entity_With_Duplicate_Id()
        {
            // Arrange
            var initialEntity = new Paint { Id = 1, Artist = "Picasso", InchSize = 20, Title = "Abstract", Price = 1500, PaintingMaterial = "Canvas" };
            _mockRepository.Setup(r => r.Add(It.IsAny<Paint>())).Throws<InvalidOperationException>();

            // Act and Assert
            var entityWithDuplicateId = new Paint { Id = 1, Artist = "Van Gogh", InchSize = 25, Title = "Starry Night", Price = 2000, PaintingMaterial = "Oil" };

            Assert.Throws<InvalidOperationException>(() => _repository.Add(entityWithDuplicateId));

        }*//*

    }
}


*/