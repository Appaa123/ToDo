using ToDoApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ToDoApi.Services;

public class TasksService
{
    private readonly IMongoCollection<TaskItem> _TasksCollection;

    public TasksService(
        IOptions<TaskDatabaseSettings> taskDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            taskDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            taskDatabaseSettings.Value.DatabaseName);

        _TasksCollection = mongoDatabase.GetCollection<TaskItem>(
            taskDatabaseSettings.Value.TasksCollectionName);
    }

    public async Task<List<TaskItem>> GetAsync() =>
        await _TasksCollection.Find(_ => true).ToListAsync();

    public async Task<TaskItem?> GetAsync(string id) =>
        await _TasksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TaskItem newTask) =>
        await _TasksCollection.InsertOneAsync(newTask);

    public async Task UpdateAsync(string id, TaskItem updatedTask) =>
        await _TasksCollection.ReplaceOneAsync(x => x.Id == id, updatedTask);

    public async Task RemoveAsync(string id) =>
        await _TasksCollection.DeleteOneAsync(x => x.Id == id);
}