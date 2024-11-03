using AutoMapper;
using Core.Exceptions;
using Moq;
using ToDoList.DataAccess.Abstracts;
using ToDoList.Models.Dtos.ToDos.Requests;
using ToDoList.Models.Dtos.ToDos.Responses;
using ToDoList.Models.Entities;
using ToDoList.Service.Concretes;
using ToDoList.Service.Rules;

namespace Service.Tests;

public class ToDoServiceTest
{
    private ToDoService _toDoService;
    private Mock<IMapper> _mockMapper;
    private Mock<IToDoRepository> _repositoryMock;
    private Mock<ToDoBusinessRules> _rulesMock;


    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IToDoRepository>();
        _mockMapper = new Mock<IMapper>();
        _rulesMock = new Mock<ToDoBusinessRules>();
        _toDoService = new ToDoService(_repositoryMock.Object, _mockMapper.Object, _rulesMock.Object);
    }


    [Test]
    public void GetAll_ReturnsSuccess()
    {
        // Arange
        List<ToDo> toDos = new List<ToDo>();
        List<ToDoResponseDto> responses = new();
        _repositoryMock.Setup(x => x.GetAll(null, true)).Returns(toDos);
        _mockMapper.Setup(x => x.Map<List<ToDoResponseDto>>(toDos)).Returns(responses);

        // Act 

        var result = _toDoService.GetAll();

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(responses, result.Data);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual(string.Empty, result.Message);
    }

    [Test]
    public void Add_WhenToDoAdded_ReturnsSuccess()
    {
        ToDoCreateRequestDto dto = new ToDoCreateRequestDto("Deneme", "Content", DateTime.Now, DateTime.Now.AddDays(7),Priority.Hig,1);
        ToDo toDo = new ToDo()
        {
            Title = "Deneme",
            Description = "Content",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(7), 
            Priority = Priority.Hig, 
            CategoryId = 1 
        };

        ToDoResponseDto response = new ToDoResponseDto(
        new Guid("{6C95E9E2-3ECE-4465-8A1D-8E38CA2BFFDC}"),
        "Deneme",
        "Content",
        DateTime.Now,
        DateTime.Now.AddDays(7),
        Priority.Hig, 
        true,
        null,
        null  
            );

        _mockMapper.Setup(x => x.Map<ToDo>(dto)).Returns(toDo);
        _repositoryMock.Setup(x => x.Add(toDo)).Returns(toDo);
        _mockMapper.Setup(x => x.Map<ToDoResponseDto>(toDo)).Returns(response);


        // Act
        var result = _toDoService.Add(dto, "{5C95E9E2-3ECE-4465-8A1D-8E38CA2BFFDC}");

        //Assert
        Assert.AreEqual(response, result.Data);
        Assert.IsTrue(result.Success);
    }

    [Test]
    public void GetById_WhenPostIsNotPresent_ThrowsException()
    {
        // Arange 
        Guid id = new Guid("{BA663833-98D6-4BE6-93C3-65997006B13A}");
        ToDo toDo = null;
        _rulesMock.Setup(x => x.ToDoIsNullCheck(toDo)).Throws(new NotFoundException("İlgili todo bulunamadı."));


        // Assert
        Assert.Throws<NotFoundException>(() => _toDoService.GetById(id), "İlgili todo bulunamadı.");
    }


    [Test]
    public void GetById_WhenPostIsPresent_ReturnsSuccess()
    {
        ToDo toDo = new ToDo
        {
            Id = new Guid("{BA663833-98D6-4BE6-93C3-65997006B13A}")
        };

        Guid id = new Guid("{BA663833-98D6-4BE6-93C3-65997006B13A}");
        
        ToDoResponseDto response = new ToDoResponseDto(
            new Guid("{6C95E9E2-3ECE-4465-8A1D-8E38CA2BFFDC}"),
            "Deneme",
            "Content",
            DateTime.Now,
            DateTime.Now.AddDays(7),
            Priority.Hig, 
            true,
            null,
            null  
        );

        _repositoryMock.Setup(x => x.GetById(id)).Returns(toDo);
        _rulesMock.Setup(x => x.ToDoIsNullCheck(toDo));
        _mockMapper.Setup(x => x.Map<ToDoResponseDto>(toDo)).Returns(response);


        var result = _toDoService.GetById(id);


        Assert.AreEqual(response, result.Data);
        Assert.IsTrue(result.Success);
    }
}