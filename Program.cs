using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();  // ← Cette ligne est essentielle pour l'API

// Ajouter CORS
// Enable CORS (optional, but useful)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Ajouter SignalR et les services Observer
builder.Services.AddSignalR();
builder.Services.AddScoped<WebSocketService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddSingleton<EmailService>();

var app = builder.Build();
app.UseCors("AllowAll");

app.UseRouting();
app.UseAuthorization();

// Serve static files
app.UseDefaultFiles(); // Enables serving index.html by default
app.UseStaticFiles();


app.MapControllers();  // Nouvelle syntaxe qui remplace UseEndpoints()
app.MapHub<NotificationHub>("/notificationsHub");

// Set default fallback to index.html
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Request.Path = "/index.html";
    }
    await next();
});
app.Run();
