using ToDoApi.Models;
using ToDoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController : ControllerBase
{
    private readonly TopicsService _TopicsService;

    public TopicsController(TopicsService TopicsService) =>
        _TopicsService = TopicsService;

    [HttpGet]
    public async Task<List<TopicItem>> Get() =>
        await _TopicsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<TopicItem>> Get(string id)
    {
        var task = await _TopicsService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        return task;
    }

    [HttpPost]
    public async Task<IActionResult> Post(TopicItem newtask)
    {
        await _TopicsService.CreateAsync(newtask);

        return CreatedAtAction(nameof(Get), new { id = newtask.Id }, newtask);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, TopicItem updatedtask)
    {
        var task = await _TopicsService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        updatedtask.Id = task.Id;

        await _TopicsService.UpdateAsync(id, updatedtask);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var task = await _TopicsService.GetAsync(id);

        if (task is null)
        {
            return NotFound();
        }

        await _TopicsService.RemoveAsync(id);

        return NoContent();
    }
}