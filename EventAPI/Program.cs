using EventAPI.Endpoints;
using EventAPI.Services;
using EventAPI.Extensions;
using EventAPI.ExceptionHandler;

var builder = WebApplication.CreateBuilder(args);

//Health Checks
builder.Services.AddHealthChecks();

//Logging
builder.AddLogSettings("EventAPI", builder.Configuration);

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Tratamento de erros global
builder.Services.AddExceptionHandler<GlobalException>();
builder.Services.AddProblemDetails();

builder.Services.AddSingleton<IEventService, EventService>();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//Health Checks
app.MapHealthChecks("/healthz");

app.UseHttpsRedirection();

//Rotas
app.RegisterEndpoints();

app.UseExceptionHandler();

app.Run();