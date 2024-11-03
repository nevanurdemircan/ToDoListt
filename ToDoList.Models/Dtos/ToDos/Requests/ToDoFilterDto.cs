namespace ToDoList.Models.Dtos.ToDos.Requests;

public class ToDoFilterDto
{
    public string? Title { get; set; }
    public bool? IsCompleted { get; set; }
    public int? Priority { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}