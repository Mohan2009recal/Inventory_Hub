var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Enable CORS for Blazor WASM client
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowBlazorClient");
app.UseAuthorization();
app.MapControllers();

app.Run();