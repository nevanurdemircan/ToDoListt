using AutoMapper;
using Core.Responses;
using ToDoList.DataAccess.Abstracts;
using ToDoList.Models.Dtos.Categories.Requests;
using ToDoList.Models.Dtos.Categories.Responses;
using ToDoList.Models.Entities;
using ToDoList.Service.Abstracts;

namespace ToDoList.Service.Concretes;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;


    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public ReturnModel<List<CategoryResponseDto>> GetAll()
    {
        List<Category> categories = _categoryRepository.GetAll();
        List<CategoryResponseDto> responses = _mapper.Map<List<CategoryResponseDto>>(categories);

        return new ReturnModel<List<CategoryResponseDto>>
        {
            Data = responses,
            Message = string.Empty,
            StatusCode = 200,
            Success = true
        };
    }


    public ReturnModel<CategoryResponseDto> Add(CategoryCreateRequestDto create)
    {
        Category category = _mapper.Map<Category>(create);
        _categoryRepository.Add(category);
        CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(category);

        return new ReturnModel<CategoryResponseDto>
        {
            Data = response,
            Message = "Post Eklendi",
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<CategoryResponseDto> Remove(int id)
    {
        Category category = _categoryRepository.GetById(id);
        Category deletedCategory = _categoryRepository.Remove(category);
        CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(deletedCategory);
      
        return new ReturnModel<CategoryResponseDto>
        {
            Data = response,
            Message = "Post Silindi",
            StatusCode = 200,
            Success = true
        };
    }
}