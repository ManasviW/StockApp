using StockApp.Context;
using StockApp.Interfaces;
using StockApp.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<StockDbContext>();
builder.Services.AddScoped<IStockRepo, StockRepo>();
builder.Services.AddScoped<IStockPriceRepo, StockPriceRepo>();
//builder.Services.AddExceptionHandler<>
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
