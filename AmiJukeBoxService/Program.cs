var exeDir = AppContext.BaseDirectory;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = exeDir,
    WebRootPath = Path.Combine(exeDir, "wwwroot")
});

builder.Host.UseWindowsService();
builder.WebHost.UseUrls("http://0.0.0.0:8083");

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddSingleton<AmiJukeBoxService.Database.DatabaseFunctions>();
builder.Services.AddSingleton<AmiJukeBoxService.Mqtt.MqttService>();
builder.Services.AddSingleton<AmiJukeBoxService.Images.ImageStripService>();

builder.Logging.AddEventLog(settings => settings.SourceName = "AmiJukeBoxService");
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseCors();

// Prevent browsers from caching API responses
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api") ||
        context.Request.Path.StartsWithSegments("/AmiJukeBoxRemote/api"))
    {
        context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
        context.Response.Headers["Pragma"] = "no-cache";
    }
    await next();
});

app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Prevent caching of JS files so updates are always picked up
        var ext = Path.GetExtension(ctx.File.Name);
        if (ext == ".js" || ext == ".html")
        {
            ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            ctx.Context.Response.Headers["Pragma"] = "no-cache";
            ctx.Context.Response.Headers["Expires"] = "0";
        }
    }
});

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    app.UseDeveloperExceptionPage();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
