using vs2026_Service_lifetimes.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TransientService>();
builder.Services.AddSingleton<SingletonService>();
builder.Services.AddScoped<ScopedService>();

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

app.MapGet("/all",
(
    TransientService transient1,
    TransientService transient2,

    ScopedService scoped1,
    ScopedService scoped2,

    SingletonService singleton1,
    SingletonService singleton2
) =>
{
    return
$"""
==============================
        TRANSIENT
==============================
Service 1: {transient1.GetId()}
Service 2: {transient2.GetId()}

==============================
          SCOPED
==============================
Service 1: {scoped1.GetId()}
Service 2: {scoped2.GetId()}

==============================
        SINGLETON
==============================
Service 1: {singleton1.GetId()}
Service 2: {singleton2.GetId()}
""";
});

app.Run();

