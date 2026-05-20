using vs2026_Service_lifetimes.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TransientService>();
//builder.Services.AddSingleton<SingletonService>();

//// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/transient", (TransientService service) =>
{
    return $"Transient ID: {service.GetId()}";
});




app.Run();

