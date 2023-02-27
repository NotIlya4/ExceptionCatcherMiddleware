using ExceptionCatcherMiddleware.Demo.ExceptionMappers;
using ExceptionCatcherMiddleware.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding your own mappers
builder.Services.AddExceptionCatcherMiddlewareServices(optionsBuilder =>
{
    optionsBuilder.RegisterExceptionMapper<Exception, ExceptionMapper>();
    optionsBuilder.RegisterExceptionMapper<ArgumentException, ArgumentExceptionMapper>();
});

var app = builder.Build();

// Add middleware to pipeline
app.UseExceptionCatcherMiddleware();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();