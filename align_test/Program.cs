
using infrastructure.interfaces;
using infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddMemoryCache();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddHttpClient("Picsum", c =>
{
    c.BaseAddress = new Uri(configuration.GetSection("Api:Picsum:Url").Value);
    c.DefaultRequestHeaders.Add("Accept", "text/html, application/json, */*");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<PhotosService>();
builder.Services.AddTransient<IStartupService, StartupService>();

var app = builder.Build();
var startupTasks = app.Services.GetServices<IStartupService>();
foreach (var task in startupTasks)
{
     await task.Initialize(); 
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
