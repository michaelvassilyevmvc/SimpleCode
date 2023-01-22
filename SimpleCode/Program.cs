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
        context.Response.ContentType = "text/html;charset = utf-8";

        StringBuilder sb = new StringBuilder();

        sb.Append("<table>");
        foreach (var header in context.Request.Headers)
        {
            sb.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
        }
        sb.Append("</table>");
        await context.Response.WriteAsync(sb.ToString());
    }
);

app.Run();
