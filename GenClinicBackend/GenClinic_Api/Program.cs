using GenClinic_Api.Extensions;
using GenClinic_Api.Middlewares;
using GenClinic_Repository.Data;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.RegisterDatabaseConnection(config);
builder.Services.AddControllers()
     .ConfigureApiBehaviorOptions(options =>
     {
         options.SuppressModelStateInvalidFilter = true;
     });
builder.Services.ConfigureSwagger();
builder.Services.RegisterServices();
builder.Services.RegisterRepositories();
builder.Services.RegisterAutoMapper();
builder.Services.SetRequestBodySize();
builder.Services.ConfigureCors();
builder.Services.AddTransient<ErrorHandlerMiddleware>();
builder.Services.RegisterMail(config);
builder.Services.ConfigAuthentication(config);

WebApplication? app = builder.Build();

//auto migartion
using (IServiceScope? scope = app.Services.CreateScope())
{
    AppDbContext? dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();