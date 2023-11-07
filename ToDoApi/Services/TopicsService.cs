using ToDoApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ToDoApi.Services;

public class TopicsService
{
    private readonly IMongoCollection<TopicItem> _TopicsCollection;

    public TopicsService(
        IOptions<TaskDatabaseSettings> taskDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            taskDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            taskDatabaseSettings.Value.DatabaseName);

        _TopicsCollection = mongoDatabase.GetCollection<TopicItem>(
            taskDatabaseSettings.Value.TopicsCollectionName);
    }

    public async Task<List<TopicItem>> GetAsync() =>
        await _TopicsCollection.Find(_ => true).ToListAsync();

    public async Task<TopicItem?> GetAsync(string id) =>
        await _TopicsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TopicItem newTask) =>
        await _TopicsCollection.InsertOneAsync(newTask);

    public async Task UpdateAsync(string id, TopicItem updatedTask) =>
        await _TopicsCollection.ReplaceOneAsync(x => x.Id == id, updatedTask);

    public async Task RemoveAsync(string id) =>
        await _TopicsCollection.DeleteOneAsync(x => x.Id == id);
}