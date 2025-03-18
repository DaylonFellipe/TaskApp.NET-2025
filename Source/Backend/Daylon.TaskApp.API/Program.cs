using Daylon.TaskApp.Application;
using Daylon.TaskApp.Infrastructure;

//C:\WS\TaskApp\Source\Frontend\AngularTaskApp> ng serve

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Dependency Injection
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("TaskApp", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("TaskApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//     ╱|、
//    (-ˎ- >  
//    |、˜〵          
//    じしˍ,)ノ D A Y L O N