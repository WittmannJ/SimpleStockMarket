using SimpleStockMarket.API.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        policy => policy.WithOrigins("http://localhost:5001") // Your Blazor app URL
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<StocksClient>(httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["Stocks:ApiUrl"]!);
});
builder.Services.AddScoped<StockService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazorApp"); // Apply CORS policy

// if (!app.Environment.IsDevelopment())
// {
//     app.UseHttpsRedirection();
// }

app.UseAuthorization();

app.MapControllers();

app.Run();
