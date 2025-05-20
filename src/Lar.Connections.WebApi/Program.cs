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
builder.Services.AddOutputCache(x =>
{
	x.AddBasePolicy(c => c.Cache());
	x.AddPolicy("PersonByDocumentCache", c =>
		c.Cache()
			.Expire(TimeSpan.FromHours(1))
			.Tag("people"));
	x.AddPolicy("PersonByIdCache", c =>
		c.Cache()
			.Expire(TimeSpan.FromHours(1))
			.Tag("people"));

	x.AddPolicy("GetAllCache", c =>
	{
		c.Cache()
			.Expire(TimeSpan.FromHours(1))
			.SetVaryByQuery([
				"page",
				"pageSize",
				"searchTerm",
				"includeInactive",
				"includePhones",
				"sortBy",
				"sortDescending"
			])
			.Tag("people");
	});

	x.AddPolicy("SearchCache", c =>
	{
		c.Cache()
			.Expire(TimeSpan.FromHours(1))
			.SetVaryByQuery([
				"page",
				"pageSize",
				"includeInactive",
				"includePhones"
			])
			.Tag("people");
	});
});

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
app.UseOutputCache();

app.MapControllers();

var verifyTables = app.Services.GetRequiredService<IDatabaseInitializer>();
await verifyTables.InitializeAsync();

app.Run();