using AutoMapper;
using Core.Responses;
using ToDoList.DataAccess.Abstracts;
using ToDoList.Models.Dtos.ToDos.Requests;
using ToDoList.Models.Dtos.ToDos.Responses;
using ToDoList.Models.Entities;
using ToDoList.Service.Abstracts;
using ToDoList.Service.Rules;

namespace ToDoList.Service.Concretes;

public class ToDoService : IToDoService
{
    private readonly IToDoRepository _toDoRepository;
    private readonly IMapper _mapper;
    private readonly ToDoBusinessRules _businessRules;

    public ToDoService(IToDoRepository todoRepository, IMapper mapper,ToDoBusinessRules toDoBusinessRules)
    {
        _toDoRepository = todoRepository;
        _mapper = mapper;
        _businessRules = toDoBusinessRules;
    }


    public ReturnModel<List<ToDoResponseDto>> GetAll()
    {
        List<ToDo> toDos = _toDoRepository.GetAll();
        List<ToDoResponseDto> responses = _mapper.Map<List<ToDoResponseDto>>(toDos);

        return new ReturnModel<List<ToDoResponseDto>>
        {
            Data = responses,
            Message = string.Empty,
            StatusCode = 200,
            Success = true
        };
    }


    public ReturnModel<ToDoResponseDto?> GetById(Guid id)
    {
        var toDo = _toDoRepository.GetById(id);
        _businessRules.ToDoIsNullCheck(toDo);

        var response = _mapper.Map<ToDoResponseDto>(toDo);

        return new ReturnModel<ToDoResponseDto?>
        {
            Data = response,
            Message = string.Empty,
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<ToDoResponseDto> Add(ToDoCreateRequestDto create,string userId)
    {
        ToDo toDo = _mapper.Map<ToDo>(create);
        _toDoRepository.Add(toDo);

        ToDoResponseDto response = _mapper.Map<ToDoResponseDto>(toDo);

        return new ReturnModel<ToDoResponseDto>
        {
            Data = response,
            Message = "Post Eklendi",
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<ToDoResponseDto> Update(ToDoUpdateRequestDto update)
    {
        ToDo toDo = _toDoRepository.GetById(update.Id);

        ToDo updated = new ToDo
        {
            CategoryId = update.CategoryId,
            Title = update.Description,
            Description = update.Description,
            EndDate = update.EndDate,
            Priority = update.Priority,
            UserId = update.UserId
        };

        ToDo updatedToDo = _toDoRepository.Update(updated);

        ToDoResponseDto dto = _mapper.Map<ToDoResponseDto>(updatedToDo);
        return new ReturnModel<ToDoResponseDto>
        {
            Data = dto,
            Message = "Post Güncellendi",
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<ToDoResponseDto> Remove(Guid id)
    {
        ToDo toDo = _toDoRepository.GetById(id);
        ToDo deletedToDo =_toDoRepository.Remove(toDo);
        ToDoResponseDto response = _mapper.Map<ToDoResponseDto>(deletedToDo);
      
        return new ReturnModel<ToDoResponseDto>
        {
            Data = response,
            Message = "ToDo Silindi",
            StatusCode = 200,
            Success = true
        };
    }   
    
    public ReturnModel<List<ToDoResponseDto>> GetUserTodos(string userId)
    {
        var todos = _toDoRepository.GetAll()
            .Where(t => t.UserId == userId)
            .ToList();

        var response = _mapper.Map<List<ToDoResponseDto>>(todos);

        return new ReturnModel<List<ToDoResponseDto>>
        {
            Data = response,
            Message = "Kullanıcının yapılacak işleri",
            StatusCode = 200,
            Success = true
        };
    }
    
    public ReturnModel<List<ToDoResponseDto>> GetFilteredUserTodos(string userId, ToDoFilterDto filter)
    {
        var query = _toDoRepository.GetAll().Where(t => t.UserId == userId);

        if (!string.IsNullOrEmpty(filter.Title))
        {
            query = query.Where(t => t.Title.Contains(filter.Title));
        }
        if (filter.IsCompleted.HasValue)
        {
            query = query.Where(t => t.Completed == filter.IsCompleted);
        }
        if (filter.Priority.HasValue)
        {
            query = query.Where(t => t.Priority == (Priority)filter.Priority.Value);
        }
        if (filter.StartDate.HasValue)
        {
            query = query.Where(t => t.StartDate >= filter.StartDate);
        }
        if (filter.EndDate.HasValue)
        {
            query = query.Where(t => t.EndDate <= filter.EndDate);
        }

        var todos = query.ToList();
        var response = _mapper.Map<List<ToDoResponseDto>>(todos);

        return new ReturnModel<List<ToDoResponseDto>>
        {
            Data = response,
            Message = "Filtreler getirildi",
            StatusCode = 200,
            Success = true
        };
    }
}