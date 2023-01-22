using SimpleCode.Converters;
using SimpleCode.Models;
using System.Text.Json;

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
        var response = context.Response;
        var request = context.Request;

        if (request.Path == "/api/user")
        {
            var responseText = "Некорректные данные";
            
            if (request.HasJsonContentType())
            {
                var jsonoptions = new JsonSerializerOptions();
                jsonoptions.Converters.Add(new PersonConverter());

                var person = await request.ReadFromJsonAsync<Person>(jsonoptions);
                if (person != null)
                {
                    responseText = $"Name: {person.Name} Age: {person.Age}";
                }
            }

            await response.WriteAsJsonAsync(new { text = responseText });
        }
        else
        {
            response.ContentType = "text/html; charset=utf-8";
            await response.SendFileAsync("html/index.html");
        }
    }
);

app.Run();
