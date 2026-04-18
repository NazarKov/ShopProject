using Microsoft.Extensions.Hosting;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Extensions;
using ShopProjectWebServer.Services.Infrastructure.Exception;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AppAppServices();
builder.Services.AddAppApiServices();


builder.Services.AddDataBaseServices();
builder.Services.AddSession(option =>
{
    option.IOTimeout = TimeSpan.FromDays(1);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});
builder.Host.UseWindowsService();

var app = builder.Build(); 


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}  
app.UseHttpsRedirection();
app.UseStaticFiles(); 
app.UseRouting(); 
app.UseAuthorization();
app.UseSession();


app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Start}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Start}/{action=Index}/{id?}");

app.MapGet("/", () => Results.Redirect("/admin/start"));

app.MapPost("/setup/complete", (IHostApplicationLifetime appLifetime) =>
{ 
    appLifetime.StopApplication();
    string html = "<!DOCTYPE html>\r\n<html lang=\"uk\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <title>Конфігурація завершена</title>\r\n    <style>\r\n        body {\r\n            margin: 0;\r\n            height: 100vh;\r\n            display: flex;\r\n            justify-content: center;\r\n            align-items: center;\r\n            background: #f4f6f8;\r\n            font-family: Arial, sans-serif;\r\n        }\r\n\r\n        .container {\r\n            text-align: center;\r\n            padding: 40px;\r\n            background: white;\r\n            border-radius: 12px;\r\n            box-shadow: 0 10px 25px rgba(0,0,0,0.1);\r\n            max-width: 500px;\r\n        }\r\n\r\n        h1 {\r\n            color: #2c3e50;\r\n            margin-bottom: 15px;\r\n        }\r\n\r\n        p {\r\n            font-size: 16px;\r\n            color: #555;\r\n            margin-bottom: 25px;\r\n        }\r\n\r\n        .note {\r\n            font-size: 14px;\r\n            color: #888;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n\r\n<div class=\"container\">\r\n    <h1>Конфігурація завершена</h1>\r\n    <p>Сервіс буде створено через Windows Service.</p>\r\n    <p><strong>Закрийте поточну сторінку.</strong></p>\r\n    <div class=\"note\">Ви можете безпечно закрити це вікно.</div>\r\n</div>\r\n\r\n</body>\r\n</html>";
    return Results.Content(html, "text/html");
});

app.Run();
