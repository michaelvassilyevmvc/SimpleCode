using Microsoft.Extensions.FileProviders;
using SimpleCode.Models;
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
        Person person = new Person
        {
            Name = "Tom",
            Age = 22
        };



        await context.Response.WriteAsJsonAsync(person);


    }
);

app.Run();
