using Microsoft.Extensions.FileProviders;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run(async (context) => 
    {
        context.Response.ContentType= "text/html;charset=utf-8";

        if(context.Request.Path == "/postuser")
        {
            var form = context.Request.Form;
            var name = form["name"];
            var age = form["age"];
            await context.Response.WriteAsync($"<div>Name: {name}</div><div>Age: {age}</div>");
        }
        else
        {
            await context.Response.SendFileAsync("html/index.html");
        }


    }
);

app.Run();
