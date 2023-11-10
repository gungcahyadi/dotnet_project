using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project3.Models.Tasks;
using project3.Services;

namespace project3.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private ITaskProjectService _taskService;

    public TasksController(ITaskProjectService taskService)
    {
        _taskService = taskService;
    }
        
    [HttpGet]
    [Authorize]
    public IActionResult All()
    {
        var tasks = _taskService.All();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult Find(int id)
    {
        var task = _taskService.Find(id);
        return Ok(task);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create(CreateTaskRequest model)
    {
        var task = _taskService.Create(model);
        return Ok(new { message = "Task created successfully" });

    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(int id, UpdateTaskRequest model)
    {
        _taskService.Update(id, model);
        return Ok(new { message = "Task updated successfully" });
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Delete(int id)
    {
        _taskService.Delete(id);
        return Ok(new { message = "Task deleted successfully" });
    }

    

}
