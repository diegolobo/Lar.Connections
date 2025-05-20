using Lar.Connections.Application;
using Lar.Connections.Infrastructure;
using Lar.Connections.Infrastructure.Seeds;
using Lar.Connections.WebApi.Constants;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddDatabase();

if (builder.Environment.IsDevelopment())
{
	builder.Configuration.AddJsonFile(ApiConstants.AppSettingsDevelopmentJson, true);
	builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var verifyTables = app.Services.GetRequiredService<IDatabaseInitializer>();
await verifyTables.InitializeAsync();

app.Run();