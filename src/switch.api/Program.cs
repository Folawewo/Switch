using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using @switch.application.Implementation;
using @switch.application.Interface;
using @switch.domain.Entities;
using @switch.domain.Repository;
using @switch.infrastructure.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SwitchDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SwitchDatabase")));

builder.Services.AddScoped<IRepository<SwitchToggle>, SqlFeatureToggleRepository>();
builder.Services.AddScoped<ISwitchToggleService, SwitchToggleService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Switch API v1");
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "public")),
    RequestPath = "/switch" 
});

app.UseRouting();

app.MapControllers(); 

app.Run();