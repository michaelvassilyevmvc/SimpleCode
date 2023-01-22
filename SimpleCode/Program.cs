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
        var path = context.Request.Path;
        var now = DateTime.Now;
        var response = context.Response;

        if (path == "/date") await response.WriteAsync($"Date: {now.ToShortDateString()}");
        else if (path == "/time") await response.WriteAsync($"Time: {now.ToShortTimeString()}");
        else await response.WriteAsync("Hello Metanit");

    }
);

app.Run();
