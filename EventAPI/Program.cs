using EventAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.MapHealthChecks("/healthz");

app.UseHttpsRedirection();

app.RegisterEndpoints(new EventService());

app.Run();