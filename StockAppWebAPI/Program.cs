using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockAppWebApi.Filters;
using StockAppWebApi.Repositories;
using StockAppWebAPI.Models;
using StockAppWebAPI.Repositories;
using StockAppWebAPI.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var settings = builder.Configuration
                .GetRequiredSection("ConnectionStrings"); //read data from appsettings.json
// Định cấu hình DbContext và chuyển chuỗi kết nối vào
builder.Services.AddDbContext<StockAppContext>(options =>
    options.UseSqlServer(settings["DefaultConnection"]));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IWatchListRepository, WatchListRepository>();
builder.Services.AddScoped<IWatchListService, WatchListService>();

builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockService, StockService>();

builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
builder.Services.AddScoped<IQuoteService, QuoteService>();

builder.Services.AddScoped<JwtAuthorizeFilter>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
}
);
builder.Services.AddSwaggerGen();
// Thêm xác thực JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
// 2 phút là khoảng thời gian mà client và server duy trì kết nối, tức là 2 phút thì tôi gọi anh 1 tieengs 
var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2),
};

app.UseWebSockets(webSocketOptions);

app.Run();
