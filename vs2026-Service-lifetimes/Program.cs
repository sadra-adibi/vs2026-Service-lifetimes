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

app.MapGet("/",
(
    TransientService transient1,
    TransientService transient2,

    ScopedService scoped1,
    ScopedService scoped2,

    SingletonService singleton1,
    SingletonService singleton2
) =>
{
    string html = $@"
<!DOCTYPE html>
<html>
<head>
    <title>Service Lifetimes</title>

    <style>
        body
        {{
            background-color: #1e1e1e;
            color: white;
            font-family: Arial;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }}

        .box
        {{
            background-color: #2d2d2d;
            padding: 40px;
            border-radius: 15px;
            width: 700px;
        }}

        h1
        {{
            text-align: center;
        }}

        .section
        {{
            margin-top: 30px;
        }}
    </style>
</head>

<body>

    <div class='box'>

        <h1>Service Lifetimes</h1>

        <div class='section'>
            <h2>Transient</h2>

            <p>Service 1: {transient1.GetId()}</p>
            <p>Service 2: {transient2.GetId()}</p>
        </div>

        <div class='section'>
            <h2>Scoped</h2>

            <p>Service 1: {scoped1.GetId()}</p>
            <p>Service 2: {scoped2.GetId()}</p>
        </div>

        <div class='section'>
            <h2>Singleton</h2>

            <p>Service 1: {singleton1.GetId()}</p>
            <p>Service 2: {singleton2.GetId()}</p>
        </div>

    </div>

</body>
</html>";

    return Results.Content(html, "text/html");
});

app.Run();

