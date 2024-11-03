using AutoMapper;
using Moq;
using ToDoList.DataAccess.Abstracts;
using ToDoList.Models.Dtos.Categories.Requests;
using ToDoList.Models.Dtos.Categories.Responses;
using ToDoList.Models.Entities;
using ToDoList.Service.Concretes;

namespace Service.Tests;

public class CategoryServiceTest
{
      public class CategoryServiceUnitTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly IMapper _mapper;
        private readonly CategoryService _categoryService;

        public CategoryServiceUnitTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryCreateRequestDto, Category>();
                cfg.CreateMap<Category, CategoryResponseDto>();
            });
            _mapper = config.CreateMapper();

            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapper);
        }

        [Test]
        public void GetAll_ShouldReturnAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" }
            };
            _categoryRepositoryMock.Setup(repo => repo.GetAll(null, true)).Returns(categories);

            // Act
            var result = _categoryService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equals(2, result.Data.Count);
        }

        [Test]
        public void Add_ShouldAddCategory()
        {
            // Arrange
            var createDto = new CategoryCreateRequestDto("New Category");
            var category = new Category { Id = 1, Name = "New Category" };
            _categoryRepositoryMock.Setup(repo => repo.Add(It.IsAny<Category>())).Returns(category);

            // Act
            var result = _categoryService.Add(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equals("Category Eklendi", result.Message);
            Assert.Equals("New Category", result.Data.Name);
        }

        [Test]
        public void Remove_ShouldRemoveCategory()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Category to Remove" };
            _categoryRepositoryMock.Setup(repo => repo.GetById(1)).Returns(category);
            _categoryRepositoryMock.Setup(repo => repo.Remove(category)).Returns(category);

            // Act
            var result = _categoryService.Remove(1);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equals("Category Silindi", result.Message);
            Assert.Equals("Category to Remove", result.Data.Name);
        }
    }
}