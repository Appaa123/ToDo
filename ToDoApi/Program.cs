using ToDoApi.Models;
using ToDoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials()  // If you need to include credentials like cookies
                          .WithExposedHeaders("mode"));  // Allow 'mode' in exposed headers
});


builder.Services.Configure<TaskDatabaseSettings>(
    builder.Configuration.GetSection("TaskDatabase")
);

builder.Services.AddSingleton<TasksService>();

builder.Services.AddSingleton<TopicsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
}


app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Access-Control-Expose-Headers", "mode");
    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
