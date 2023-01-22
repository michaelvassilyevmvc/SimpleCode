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
        if(context.Request.Path == "/old")
        {
            context.Response.Redirect("/new");
        }
        else if(context.Request.Path == "/new")
        {
            await context.Response.WriteAsync("New page");
        }
        else
        {
            await context.Response.WriteAsync("Main page");
        }


    }
);

app.Run();
