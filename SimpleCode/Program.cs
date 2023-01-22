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
        context.Response.ContentType = "text/html; charset=utf-8";

        var path = context.Request.Path;
        var fullpath = $"html/{path}";
        var response = context.Response;

        if (File.Exists(fullpath))
        {
            await response.SendFileAsync(fullpath);
        }
        else
        {
            response.StatusCode= 404;
            await response.WriteAsync("<h1>Not Found</h1>");
        }
    }
);

app.Run();
