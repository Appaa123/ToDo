using ToDoApi.Models;
using ToDoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TasksService _tasksService;

    public TasksController(TasksService tasksService) =>
        _tasksService = tasksService;

    [HttpGet]
    public async Task<List<TaskItem>> Get() =>
        await _tasksService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<TaskItem>> Get(string id)
    {
        var task = await _tasksService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        return task;
    }

    [HttpPost]
    public async Task<IActionResult> Post(TaskItem newtask)
    {
        await _tasksService.CreateAsync(newtask);

        return CreatedAtAction(nameof(Get), new { id = newtask.Id }, newtask);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, TaskItem updatedtask)
    {
        var task = await _tasksService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        updatedtask.Id = task.Id;

        await _tasksService.UpdateAsync(id, updatedtask);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var task = await _tasksService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        await _tasksService.RemoveAsync(id);

        return NoContent();
    }
}