using AccessControl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AccessControlService>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
