using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using @switch.api.Hubs;
using @switch.infrastructure.Extensions;
using @switch.infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwitch(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SwitchDatabase"))); 

builder.Services.AddSignalR(); 
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<SwitchToggleNotifier>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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
app.UseCors();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "public")),
    RequestPath = "/switch-dashboard"
});

app.UseRouting();
app.MapHub<SwitchHub>("/switch-hub");
app.MapControllers();
app.Run();