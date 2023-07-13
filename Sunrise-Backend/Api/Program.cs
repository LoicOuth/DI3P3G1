using Api.Common.Auth;
using Application;
using Domain.Model;
using Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceService(builder.Configuration);

builder.Services.AddCors(options =>
{
	options.AddPolicy("MyAllowedOrigins", policy =>
	{
		policy
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowAnyOrigin();
	});
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors("MyAllowedOrigins");
app.MapControllers();
app.UseAuthorization();
app.UseAuthentication();
app.UseMiddleware<JwtMiddleware>();

app.Run();

public class program
{
	
}